# Ontop SPARQL Endpoint Setup

This directory contains the configuration for exposing a SPARQL endpoint over your SQL Server database using Ontop.

## Files

- **ontop.properties** - Database connection and Ontop configuration
- **nulllogicone.ttl** - OWL ontology defining your domain model
- **nulllogicone.obda.ttl** - R2RML mappings from SQL tables to RDF triples
- **docker-compose.yml** - Docker Compose configuration for easy deployment

## Prerequisites

1. **SQL Server JDBC Driver**: Download the Microsoft SQL Server JDBC driver and place it in `ontop/jdbc/` directory:
   ```bash
   # Create jdbc directory
   mkdir -p ontop/jdbc
   
   # Download driver (or copy from your local installation)
   curl -L https://go.microsoft.com/fwlink/?linkid=2202909 -o mssql-jdbc.zip
   unzip mssql-jdbc.zip "*.jar" -d ontop/jdbc/
   ```

2. **Update credentials**: Edit `ontop.properties` and update:
   - Database name (currently: `null`)
   - Username and password
   - Database server if not using LocalDB with Docker

## Running with Docker Compose

```bash
# Start the SPARQL endpoint
docker-compose -f ontop/docker-compose.yml up -d

# View logs
docker-compose -f ontop/docker-compose.yml logs -f

# Stop the endpoint
docker-compose -f ontop/docker-compose.yml down
```

## Running with Docker CLI

```bash
docker run -d \
  --name nulllogicone-sparql \
  -p 8080:8080 \
  -v ${PWD}/ontop:/opt/ontop/input:ro \
  -v ${PWD}/ontop/jdbc:/opt/ontop/jdbc:ro \
  ontop/ontop:latest endpoint
```

## Accessing the SPARQL Endpoint

Once running, access:
- **SPARQL Endpoint**: http://localhost:8080/sparql
- **Web UI**: http://localhost:8080/ (Ontop provides a basic query interface)

## Example SPARQL Queries

See `example-queries.sparql` for sample queries.

## Integration with Your ASP.NET API

You can proxy SPARQL requests through your ASP.NET API:

```csharp
app.MapPost("/api/sparql", async (HttpContext context) =>
{
    using var client = new HttpClient();
    var query = await new StreamReader(context.Request.Body).ReadToEndAsync();
    
    var content = new FormUrlEncodedContent(new[]
    {
        new KeyValuePair<string, string>("query", query)
    });
    
    var response = await client.PostAsync("http://localhost:8080/sparql", content);
    var result = await response.Content.ReadAsStringAsync();
    
    return Results.Content(result, "application/sparql-results+json");
});
```

## Troubleshooting

### Cannot connect to SQL Server from Docker
- Use `host.docker.internal` instead of `localhost` in `ontop.properties`
- For LocalDB specifically, you might need to enable TCP/IP and configure SQL Server Configuration Manager

### JDBC Driver not found
- Ensure the .jar file is in the `ontop/jdbc/` directory
- Verify the volume mount in docker-compose.yml

### Mapping errors
- Check logs: `docker logs nulllogicone-ontop-sparql`
- Verify table names match your actual SQL Server schema
- Test database connection with: `docker exec -it nulllogicone-ontop-sparql sh`
