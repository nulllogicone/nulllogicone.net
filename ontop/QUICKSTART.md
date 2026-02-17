# Quick Start Guide - Ontop SPARQL Endpoint

## 🚀 Step-by-Step Setup

### 1. Download SQL Server JDBC Driver

```powershell
cd ontop
.\setup-jdbc.ps1
```

This will download and extract the Microsoft SQL Server JDBC driver to `ontop/jdbc/`

### 2. Update Database Configuration

Edit `ontop/ontop.properties` and update:
- Database name (line 3: currently `null`)
- Username (line 5: currently `xxxx`)
- Password (line 6: currently `xxxx`)

For SQL Server LocalDB from Docker, use:
```properties
jdbc.url=jdbc:sqlserver://host.docker.internal:1433;databaseName=YourDbName;encrypt=false
```

**Note**: LocalDB requires TCP/IP enabled in SQL Server Configuration Manager.

### 3. Start Ontop SPARQL Endpoint

```bash
# Option A: Using Docker Compose (recommended)
cd ontop
docker-compose up -d

# Option B: Using Docker CLI
docker run -d \
  --name nulllogicone-sparql \
  -p 8080:8080 \
  -v ${PWD}/ontop:/opt/ontop/input:ro \
  -v ${PWD}/ontop/jdbc:/opt/ontop/jdbc:ro \
  ontop/ontop:latest endpoint
```

### 4. Test the Endpoint

Open your browser: http://localhost:8080/

Or test with curl:
```bash
curl -X POST http://localhost:8080/sparql \
  -H "Accept: application/sparql-results+json" \
  --data-urlencode 'query=SELECT * WHERE { ?s ?p ?o } LIMIT 10'
```

### 5. Try Example Queries

See `example-queries.sparql` for ready-to-use SPARQL queries, including:
- Listing all Stamm entities
- Querying relationships (Stamm → Angler → TopLab)
- Aggregations and filters
- Full-text search

### 6. (Optional) Integrate with ASP.NET API

Copy code from `SparqlEndpoints.example.cs` to add SPARQL proxy endpoints to your API:
- POST /sparql - Execute queries
- GET /sparql/examples - Get example queries
- GET /sparql/health - Check endpoint health

## 🔍 What You Get

1. **Full SPARQL 1.1 Support**
   - SELECT, CONSTRUCT, ASK, DESCRIBE queries
   - Filters, aggregations, optional patterns
   - Property paths and subqueries

2. **No Data Duplication**
   - Queries translated to SQL in real-time
   - No triple store needed
   - Uses your existing relational data

3. **Standard RDF Output**
   - JSON, XML, Turtle, CSV formats
   - Compatible with any RDF tool
   - Linked Data ready

## 📋 Common Operations

### View logs
```bash
docker-compose logs -f
```

### Stop endpoint
```bash
docker-compose down
```

### Restart after config changes
```bash
docker-compose restart
```

## 🔧 Troubleshooting

**Cannot connect to database:**
- Enable TCP/IP in SQL Server Configuration Manager
- Use `host.docker.internal` for localhost connections from Docker
- Check firewall rules

**JDBC driver not found:**
```bash
ls ontop/jdbc/  # Should show mssql-jdbc-*.jar
```

**Mapping errors:**
```bash
docker logs nulllogicone-ontop-sparql
```

## 📚 Next Steps

1. Customize the ontology (`nulllogicone.ttl`) for your domain
2. Add more mappings in `nulllogicone.obda.ttl`
3. Explore SPARQL Federation to combine multiple data sources
4. Check Ontop docs: https://ontop-vkg.org/

Enjoy your new SPARQL endpoint! 🎉
