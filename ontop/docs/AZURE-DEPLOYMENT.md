# Azure Deployment Guide

This guide shows how to deploy the Ontop SPARQL endpoint to Azure Container Instances with secure credential management using Azure Key Vault.

## Prerequisites

- Azure subscription
- Azure CLI installed (`az --version`)
- Logged in to Azure (`az login`)
- Resource group created
- Azure SQL Database deployed (with private endpoint recommended)
- Virtual Network configured

## Architecture Overview

```
Azure Container Instance (Ontop)
  ↓ (reads secrets via managed identity)
Azure Key Vault (SQL credentials)
  ↓ (stores connection details)
Azure SQL Database (private endpoint)
```

## Step 1: Prepare Key Vault Secrets

### Secret Creation

```powershell
$vaultName = "your-vault-name"

az keyvault secret set --vault-name $vaultName --name sql-server --value "yourserver.database.secure.windows.net"
az keyvault secret set --vault-name $vaultName --name sql-database --value "null-test"
az keyvault secret set --vault-name $vaultName --name sql-user --value "your-username"
az keyvault secret set --vault-name $vaultName --name sql-password --value "your-password"
```

## Step 2: Prepare Container Registry

### Create Azure Container Registry

```powershell
az acr create --resource-group YourResourceGroup `
  --name yourregistry --sku Basic --admin-enabled true
```

### Get Registry Credentials

```powershell
$acrPassword = az acr credential show --name yourregistry --query "passwords[0].value" -o tsv
```

### Build and Push Image

```powershell
# Login to ACR
az acr login --name yourregistry

# Build and push
docker build -t yourregistry.azurecr.io/nulllogicone-ontop:latest .
docker push yourregistry.azurecr.io/nulllogicone-ontop:latest
```

## Step 3: Setup Networking

### Create Container Subnet

```powershell
az network vnet subnet create `
  --resource-group YourResourceGroup `
  --vnet-name YourVNet `
  --name container-subnet `
  --address-prefixes 10.0.3.0/24 `
  --delegations Microsoft.ContainerInstance/containerGroups
```

**Note**: The subnet must be dedicated to Azure Container Instances and have the delegation configured.

## Step 4: Deploy Container

### Option A: Using ARM Template (Recommended)

Use the parameterized template for secure, repeatable deployments:

```powershell
az deployment group create `
  --resource-group YourResourceGroup `
  --template-file deploy-aci.template.json `
  --parameters keyVaultName=your-vault-name `
               containerRegistryName=yourregistry `
               containerRegistryPassword=$acrPassword
```

### Option B: Using az container create

```powershell
$vnetId = az network vnet show -g YourResourceGroup -n YourVNet --query id -o tsv

az container create `
  --resource-group YourResourceGroup `
  --name nullsparql `
  --image yourregistry.azurecr.io/nulllogicone-ontop:latest `
  --cpu 1 --memory 1.5 `
  --registry-login-server yourregistry.azurecr.io `
  --registry-username yourregistry `
  --registry-password $acrPassword `
  --vnet $vnetId `
  --subnet container-subnet `
  --environment-variables `
    SQL_SERVER="@Microsoft.KeyVault(SecretUri=https://your-vault-name.vault.azure.net/secrets/sql-server/)" `
    SQL_USER="@Microsoft.KeyVault(SecretUri=https://your-vault-name.vault.azure.net/secrets/sql-user/)" `
    SQL_PASSWORD="@Microsoft.KeyVault(SecretUri=https://your-vault-name.vault.azure.net/secrets/sql-password/)" `
  --assign-identity --scope $(az keyvault show -n your-vault-name --query id -o tsv)
```

## Step 5: Grant Key Vault Access

After deployment, get the managed identity and grant access:

```powershell
# Get the container's managed identity principal ID
$principalId = az container show -g YourResourceGroup -n nullsparql `
  --query identity.principalId -o tsv

# Grant Key Vault Secrets User role
az role assignment create `
  --role "Key Vault Secrets User" `
  --assignee-object-id $principalId `
  --assignee-principal-type ServicePrincipal `
  --scope $(az keyvault show -n your-vault-name --query id -o tsv)
```

**Note**: It may take 1-2 minutes for the role assignment to propagate.

## Step 6: Verify Deployment

```powershell
# Check container status
az container show -g YourResourceGroup -n nullsparql --query "instanceView.state" -o tsv

# View logs
az container logs -g YourResourceGroup -n nullsparql --tail 50

# Get IP address (if using public IP)
az container show -g YourResourceGroup -n nullsparql --query "ipAddress.ip" -o tsv
```

**Healthy logs should show**:
```
Started ServerConnector@xyz{HTTP/1.1, (http/1.1)}{0.0.0.0:8080}
Started @4379ms
```

## Step 7: Configure .NET Application

Update your ASP.NET Core application configuration:

**In Azure App Service Configuration**:
```
Ontop__EndpointUrl = http://10.0.3.4:8080
```

**Or in appsettings.Production.json**:
```json
{
  "Ontop": {
    "EndpointUrl": "http://10.0.3.4:8080"
  }
}
```

Replace `10.0.3.4` with your container's actual IP address.

## Troubleshooting

### Container in CrashLoopBackOff

**Check logs for specific errors**:
```powershell
az container logs -g YourResourceGroup -n nullsparql --tail 100
```

**Common causes**:
1. **Database connectivity**: Container subnet can't reach SQL private endpoint
   - Verify NSG rules allow container subnet → backend subnet on port 1433
   - Check private DNS zone is linked to VNet
   - Test DNS resolution: `nslookup yourserver.database.windows.net`

2. **Key Vault access**: Managed identity lacks permissions
   - Wait 2 minutes after role assignment
   - Verify role: `Key Vault Secrets User` (not `Reader`)
   - Check assignment scope includes the Key Vault resource

3. **Wrong secrets**: Secret URIs or values incorrect
   - Verify secret names match exactly (sql-server, sql-user, sql-password)
   - Check secret values don't have extra spaces or quotes
   - Use `az keyvault secret show` to verify

### Connection Timeout to SQL

1. **Private Endpoint DNS**: Ensure private DNS zone `privatelink.database.windows.net` is linked to VNet
2. **Firewall Rules**: Azure SQL firewall must allow Azure services OR use private endpoint exclusively
3. **NSG Rules**: Network Security Groups on both subnets must allow traffic:
   ```powershell
   az network nsg rule create -g YourResourceGroup --nsg-name YourNSG \
     --name AllowContainerToSQL --priority 100 \
     --source-address-prefixes 10.0.3.0/24 \
     --destination-port-ranges 1433 --protocol Tcp --access Allow
   ```

### Managed Identity Not Working

```powershell
# Verify identity exists
az container show -g YourResourceGroup -n nullsparql --query identity

# Verify role assignment
az role assignment list --assignee $principalId --scope $(az keyvault show -n your-vault-name --query id -o tsv)
```

## Network Architecture

Recommended setup for production:

```
VNet: 10.0.0.0/16
├─ default subnet: 10.0.0.0/24
├─ frontend subnet: 10.0.1.0/24 (App Service VNet Integration)
├─ backend subnet: 10.0.2.0/24 (SQL Private Endpoint)
└─ container subnet: 10.0.3.0/24 (ACI with delegation)
```

All subnets should be able to communicate within the VNet by default.

## Security Notes

- ✅ **Never commit** `deploy-aci.json`, `deploy-aci-simple.yaml`, or `test-azure.properties` to git
- ✅ **Always use** managed identities instead of connection strings
- ✅ **Always use** Key Vault references with `@Microsoft.KeyVault()` syntax
- ✅ **Safe to commit**: `deploy-aci.template.json` (uses parameters, no secrets)

## Cost Considerations

- **Azure Container Instance**: ~$30-50/month for 1 vCPU, 1.5GB RAM (always-on)
- **Azure Container Registry**: Basic tier ~$5/month
- **Key Vault**: ~$0.03 per 10,000 operations (minimal cost)
- **VNet**: Free for private IPs

## Next Steps

- Monitor with Azure Monitor and Application Insights
- Set up alerts for container restarts
- Configure auto-restart policies
- Scale to multiple container replicas if needed
- See [SECURITY.md](SECURITY.md) for additional hardening
