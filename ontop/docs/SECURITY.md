# Security Best Practices

## Overview

This guide covers security best practices for the Ontop SPARQL endpoint deployment, focusing on credential management, network isolation, and secure configuration.

## Credential Management

### ✅ DO: Use Azure Key Vault

**Store all sensitive values in Key Vault**:
- Database connection strings
- Usernames and passwords
- API keys and tokens
- Container registry passwords

**Access via Managed Identity**:
```powershell
# System-assigned identity (recommended for ACI)
az container create ... --assign-identity --scope $(az keyvault show -n your-vault --query id -o tsv)

# Grant access
az role assignment create --role "Key Vault Secrets User" --assignee-object-id $principalId ...
```

### ✅ DO: Use Key Vault References

In ARM templates and container definitions:
```json
"environmentVariables": [
  {
    "name": "SQL_PASSWORD",
    "secureValue": "@Microsoft.KeyVault(SecretUri=https://vault.vault.azure.net/secrets/sql-password/)"
  }
]
```

### ❌ DON'T: Hardcode Credentials

**Never put credentials in**:
- Source control (git repositories)
- Docker images
- Configuration files committed to git
- CI/CD pipeline definitions (unless encrypted)

### .gitignore Configuration

These files should **always** be git-ignored:
```
# .gitignore
ontop/deploy-aci-simple.yaml     # Contains plaintext credentials
ontop/deploy-aci.json            # Filled template with secrets
ontop/test-azure.properties      # Local test config with passwords
ontop/jdbc/*.jar                 # Binary JDBC driver (1.5MB)
```

**Safe to commit**:
```
ontop/deploy-aci.template.json   # Parameterized template (no secrets)
ontop/ontop-azure.properties     # Uses environment variables only
```

## Network Security

### Private Endpoints

**Use private endpoints for**:
- Azure SQL Database
- Container Registry (optional, for extra security)
- Key Vault (optional, for highly sensitive workloads)

**Benefits**:
- No public internet exposure
- Traffic stays within Azure backbone
- Supports NSG and firewall policies

### Network Segmentation

**Recommended VNet structure**:
```
VNet (10.0.0.0/16)
├─ frontend subnet (10.0.1.0/24)   - App Services
├─ backend subnet (10.0.2.0/24)    - Private endpoints
└─ container subnet (10.0.3.0/24)  - ACI (dedicated, delegated)
```

**subnet delegation required for ACI**:
```powershell
az network vnet subnet create ... --delegations Microsoft.ContainerInstance/containerGroups
```

### Network Security Groups

**Recommended NSG rules**:

**Container subnet outbound**:
- Allow HTTPS (443) to Azure services (Key Vault, ACR)
- Allow SQL (1433) to backend subnet
- Deny all other internet outbound (optional, depends on requirements)

**Backend subnet inbound**:
- Allow SQL (1433) from container subnet
- Allow SQL (1433) from frontend subnet (if App Service connects directly)

## Container Security

### Managed Identities

**Always use system-assigned managed identity**:
- No credentials to rotate
- Automatically managed by Azure
- Scoped access to specific resources

**Grant minimal permissions**:
```powershell
# Key Vault Secrets User (read secrets only, no manage/delete)
az role assignment create --role "Key Vault Secrets User" --assignee-object-id $principalId ...
```

### Image Security

**For production**:
- Use private container registry (Azure Container Registry)
- Enable vulnerability scanning
- Use specific image tags, not `:latest`
- Regularly update base images

**Build process**:
```powershell
# Tag with version + git commit
docker build -t registry.azurecr.io/ontop:v1.0-abc1234 .
docker push registry.azurecr.io/ontop:v1.0-abc1234
```

### Resource Limits

**Always set CPU and memory limits**:
```json
"resources": {
  "requests": {
    "cpu": 1.0,
    "memoryInGB": 1.5
  }
}
```

Prevents resource exhaustion attacks.

## Access Control

### SPARQL Endpoint Protection

**The Ontop endpoint has no built-in authentication**. Protect it with:

1. **Network isolation** - No public IP, only accessible within VNet
2. **Reverse proxy** - App Service or API Management in front
3. **Application-level auth** - ASP.NET Core endpoints with authentication

**Example ASP.NET Core endpoint with auth**:
```csharp
app.MapPost("/sparql", [Authorize] async (HttpContext context, IConfiguration config) => {
    var ontopUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
    // Proxy request to Ontop
});
```

### Logging and Monitoring

**Enable diagnostics**:
```powershell
az container logs -g YourResourceGroup -n nullsparql --tail 100
```

**Monitor for**:
- Failed authentication attempts (in .NET app)
- Unusual query patterns
- Container restarts (may indicate attacks or misconfigurations)
- Excessive resource usage

**Consider**:
- Azure Monitor for centralized logging
- Application Insights for .NET app tracing
- Log Analytics for advanced queries

## Configuration Security

### Environment-Specific Settings

**Local development** (`appsettings.json`):
```json
{
  "Ontop": {
    "EndpointUrl": "http://localhost:8080"
  }
}
```

**Azure production** (App Service Configuration):
```
Ontop__EndpointUrl = http://10.0.3.4:8080
```

**Never commit** `appsettings.Development.json` or `appsettings.Production.json` with real credentials.

### Connection String Parsing

**Use the parse script to split connection strings**:
```powershell
.\parse-connection-string.ps1 -VaultName your-vault -ConnectionSecretName full-connection
```

**Why?**
- Individual secrets have fine-grained permissions
- Easier to rotate (change password without rebuilding connection string)
- Clear audit trail in Key Vault logs

## Data Protection

### Transport Encryption

**Always use encrypted connections**:

**Azure SQL**:
```properties
jdbc.url=jdbc:sqlserver://server:1433;databaseName=db;encrypt=true;trustServerCertificate=false
```

**HTTPS for external access** (if exposing outside VNet):
- Use Azure Application Gateway or API Management
- Terminate TLS at the gateway
- Internal traffic can use HTTP within VNet

### Data at Rest

**Azure SQL Database**:
- Transparent Data Encryption (TDE) enabled by default
- Consider Always Encrypted for highly sensitive columns

**Azure Container Registry**:
- Images encrypted at rest automatically
- Use customer-managed keys for compliance requirements

## Secret Rotation

### Database Password Rotation

1. **Create new password in Azure SQL**:
   ```sql
   ALTER LOGIN [OliWebTest] WITH PASSWORD = 'NewPassword123!'
   ```

2. **Update Key Vault secret**:
   ```powershell
   az keyvault secret set --vault-name your-vault --name sql-password --value "NewPassword123!"
   ```

3. **Restart container** (picks up new secret):
   ```powershell
   az container restart -g YourResourceGroup -n nullsparql
   ```

**No code changes needed** - managed identity re-reads from Key Vault on restart.

### Container Registry Password Rotation

1. **Regenerate password**:
   ```powershell
   az acr credential renew --name yourregistry --password-name password
   ```

2. **Update Key Vault** (if stored there):
   ```powershell
   az keyvault secret set --vault-name your-vault --name acr-password --value "NewPassword"
   ```

3. **Redeploy container** (not just restart, needs new pull credentials)

## Compliance

### GDPR / Data Residency

- Deploy all Azure resources in same region (e.g., West Europe)
- Use private endpoints to keep data within regional boundaries
- Enable Azure Policy to enforce regional restrictions

### Audit Logging

**Enable Key Vault diagnostics**:
```powershell
az monitor diagnostic-settings create \
  --name key-vault-audit \
  --resource $(az keyvault show -n your-vault --query id -o tsv) \
  --logs '[{"category": "AuditEvent", "enabled": true}]' \
  --workspace $(az monitor log-analytics workspace show -g YourRG -n YourWorkspace --query id -o tsv)
```

Tracks all secret access attempts.

## Incident Response

### Container Compromise

1. **Immediately delete container**:
   ```powershell
   az container delete -g YourResourceGroup -n nullsparql --yes
   ```

2. **Rotate all secrets** in Key Vault

3. **Review logs** for unauthorized access:
   ```powershell
   az monitor activity-log list --resource-group YourResourceGroup --start-time 2024-01-01
   ```

4. **Rebuild image** from known-good source:
   ```powershell
   docker build -t registry.azurecr.io/ontop:v1.1-clean .
   docker push ...
   ```

5. **Redeploy** with new image and rotated secrets

### SQL Injection via SPARQL

**Ontop protects against SQL injection** by design (SPARQL queries are translated, not concatenated).

**However**:
- Sanitize user input in .NET app before passing to SPARQL
- Validate query syntax before proxying
- Consider query complexity limits (nested queries, large LIMIT values)

## Checklist

✅ All credentials in Key Vault (no hardcoded values)  
✅ Managed identity configured (no passwords in container definition)  
✅ Private endpoints for Azure SQL  
✅ Network segmentation with dedicated subnets  
✅ NSG rules limiting traffic between subnets  
✅ No public IP on Ontop container  
✅ .gitignore excludes files with credentials  
✅ deploy-aci.template.json uses parameters (safe to commit)  
✅ .NET endpoints use authentication/authorization  
✅ Diagnostic logging enabled  
✅ Resource limits set on container  
✅ HTTPS enabled for external traffic  
✅ Audit logging enabled on Key Vault  

## Additional Resources

- [Azure Key Vault Best Practices](https://learn.microsoft.com/azure/key-vault/general/best-practices)
- [Container Instance Security](https://learn.microsoft.com/azure/container-instances/container-instances-security)
- [Azure SQL Security](https://learn.microsoft.com/azure/azure-sql/database/security-overview)
- [OWASP API Security Top 10](https://owasp.org/www-project-api-security/)
