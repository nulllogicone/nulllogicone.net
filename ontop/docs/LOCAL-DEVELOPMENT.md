# Local Development Guide

## Prerequisites

- Docker Desktop installed and running
- SQL Server database (LocalDB, Express, or Azure SQL)
- Basic understanding of SPARQL

## Setup Steps

### 1. Download JDBC Driver

The SQL Server JDBC driver is required but not included in git (it's a 1.5MB binary).

**Option A: Automatic (PowerShell)**
```powershell
cd ontop
.\setup-jdbc.ps1
```

**Option B: Manual**
```powershell
mkdir jdbc
Invoke-WebRequest -Uri "https://repo1.maven.org/maven2/com/microsoft/sqlserver/mssql-jdbc/12.8.1.jre11/mssql-jdbc-12.8.1.jre11.jar" `
  -OutFile "jdbc/mssql-jdbc-12.8.1.jre11.jar"
```

### 2. Configure Database Connection

Edit `ontop-localdb.properties`:

```properties
# For SQL Server Express/LocalDB
jdbc.url=jdbc:sqlserver://localhost\\SQLEXPRESS:1433;databaseName=your-db;encrypt=false;trustServerCertificate=true
jdbc.driver=com.microsoft.sqlserver.jdbc.SQLServerDriver
jdbc.user=your-username
jdbc.password=your-password

# Ontop settings
ontop.inferDefaultDatatype=true
```

**Note**: For LocalDB, ensure TCP/IP is enabled in SQL Server Configuration Manager.

### 3. Test Locally with Docker

Build and run the container:

```powershell
# Build image
docker build -t ontop-local .

# Run with environment variables
docker run -p 8080:8080 \
  -e SQL_SERVER=host.docker.internal\\SQLEXPRESS \
  -e SQL_USER=your-user \
  -e SQL_PASSWORD=your-pass \
  ontop-local
```

### 4. Test SPARQL Queries

Open your browser to `http://localhost:8080`

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

**Using curl**:
```powershell
curl -X POST http://localhost:8080/sparql `
  -H "Content-Type: application/sparql-query" `
  -H "Accept: application/sparql-results+json" `
  -d "PREFIX nlo: <http://nulllogicone.net/schema.rdfs#> SELECT * WHERE { ?s a nlo:Stamm } LIMIT 5"
```

## Troubleshooting

### Container Crashes Immediately

**Check logs**:
```powershell
docker ps -a
docker logs <container-id>
```

**Common issues**:
- Wrong database name in connection string
- SQL Server not reachable from Docker (use `host.docker.internal` instead of `localhost`)
- TCP/IP not enabled on SQL Server
- Firewall blocking port 1433

### Connection Timeout

1. Verify SQL Server is running
2. Check SQL Server allows TCP/IP connections
3. Verify firewall allows port 1433
4. For LocalDB, use named instance: `host.docker.internal\\SQLEXPRESS`

### No Results from Queries

1. Check database has data in the expected tables (oli schema)
2. Verify table names in `nulllogicone.obda.ttl` match your schema
3. Check GUID columns are lowercase (mappings use `LOWER(CAST(...))`)

## Development Workflow

1. **Edit mappings**: Modify `nulllogicone.obda.ttl`
2. **Edit ontology**: Modify `nulllogicone.ttl`
3. **Rebuild image**: `docker build -t ontop-local .`
4. **Restart container**: `docker stop <id> && docker run ...`
5. **Test queries**: Use the web interface or curl

## Files

- **ontop-localdb.properties** - Local development config (git-ignored)
- **ontop-azure.properties** - Azure config using environment variables (safe for git)
- **nulllogicone.obda.ttl** - R2RML mappings from SQL to RDF
- **nulllogicone.ttl** - OWL ontology definitions
- **Dockerfile** - Container image definition
- **jdbc/** - JDBC driver directory (git-ignored)

## Next Steps

- Deploy to Azure: See [AZURE-DEPLOYMENT.md](AZURE-DEPLOYMENT.md)
- Security best practices: See [SECURITY.md](SECURITY.md)
- Example queries: Check `example-queries.sparql`
