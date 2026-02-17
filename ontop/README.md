# Ontop SPARQL Endpoint

Virtual Knowledge Graph exposing a SPARQL endpoint over SQL Server database using [Ontop](https://ontop-vkg.org/).

## What's Here

- **Configuration**: `ontop-localdb.properties`, `ontop-azure.properties`
- **Ontology**: `nulllogicone.ttl` (OWL domain model)
- **Mappings**: `nulllogicone.obda.ttl` (R2RML SQL-to-RDF mappings)
- **Docker**: `Dockerfile` (container image for deployment)
- **Deployment**: `deploy-aci.template.json` (secure Azure ARM template)
- **Examples**: `example-queries.sparql` (22 SPARQL query examples)

## Quick Links

📖 **Guides** (in `docs/` folder):
- [Local Development](docs/LOCAL-DEVELOPMENT.md) - Docker setup, JDBC driver, testing
- [Azure Deployment](docs/AZURE-DEPLOYMENT.md) - Secure deployment with Key Vault
- [Security Best Practices](docs/SECURITY.md) - Credential management, network security

## Quick Start

**Local (Docker)**:
```powershell
# Download JDBC driver
.\setup-jdbc.ps1

# Run container
docker run -p 8080:8080 -e SQL_SERVER=host.docker.internal\SQLEXPRESS -e SQL_USER=user -e SQL_PASSWORD=pass yourimage
```

**Azure (Production)**:
```powershell
# Deploy securely with Key Vault
az deployment group create --resource-group YourRG \
  --template-file deploy-aci.template.json \
  --parameters keyVaultName=your-vault
```

## Data Model

**11 Entities** from `oli` schema:
- **Stamm** (267) - Root entities
- **Angler** (518) - Connected to Stamm
- **PostIt** (1642), **Code** (1554), **TopLab** (1690)
- **Ring** (9542), **Loch** (2905)
- **Netz** (107), **Knoten** (503)
- **Baum** (273), **Zweig** (1729)

**Namespace**: `http://nulllogicone.net/schema.rdfs#` (prefix: `nlo:`)

**Example Query**:
```sparql
PREFIX nlo: <http://nulllogicone.net/schema.rdfs#>

SELECT ?stammName ?anglerName WHERE {
  ?stamm a nlo:Stamm ;
         nlo:name ?stammName .
  ?angler nlo:stammGuid ?stamm ;
          nlo:name ?anglerName .
} LIMIT 10
```

## Files Not in Git

For security, these files are git-ignored (contain credentials):
- `deploy-aci.json` - Filled deployment files
- `test-azure.properties`, `ontop-localdb.properties` - Config with passwords
- `jdbc/*.jar` - JDBC driver binaries

✅ **Safe to commit**: `deploy-aci.template.json` (uses parameters, no secrets)
