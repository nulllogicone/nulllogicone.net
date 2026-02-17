// Example ASP.NET Core Integration with Ontop SPARQL Endpoint
// Add this to your Program.cs or create a new SparqlEndpoints.cs file

using System.Net.Http;
using System.Text;

namespace NulllogiconeCore.Endpoints;

public static class SparqlEndpoints
{
    public static void MapSparqlEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("sparql")
            .WithTags("SPARQL");

        // POST /sparql - Execute SPARQL query
        group.MapPost("/", async (HttpRequest request, IHttpClientFactory clientFactory) =>
        {
            var client = clientFactory.CreateClient();
            var ontopEndpoint = "http://localhost:8080/sparql";

            // Read query from body (supports both form-encoded and raw SPARQL)
            string? query = null;
            if (request.ContentType?.Contains("application/x-www-form-urlencoded") == true)
            {
                var form = await request.ReadFormAsync();
                query = form["query"].ToString();
            }
            else
            {
                using var reader = new StreamReader(request.Body);
                query = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                return Results.BadRequest("Query parameter is required");
            }

            // Determine accept header (default to JSON)
            var acceptHeader = request.Headers.Accept.ToString();
            if (string.IsNullOrEmpty(acceptHeader))
            {
                acceptHeader = "application/sparql-results+json";
            }

            // Forward to Ontop
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("query", query)
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, ontopEndpoint)
            {
                Content = content
            };
            httpRequest.Headers.Add("Accept", acceptHeader);

            var response = await client.SendAsync(httpRequest);
            var result = await response.Content.ReadAsStringAsync();
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/sparql-results+json";

            return Results.Content(result, contentType);
        })
        .WithName("ExecuteSparqlQuery")
        .WithSummary("Execute a SPARQL query against the knowledge graph")
        .Produces(200)
        .Produces(400);

        // GET /sparql - Execute SPARQL query via GET (useful for simple queries)
        group.MapGet("/", async (string query, IHttpClientFactory clientFactory) =>
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Results.BadRequest("Query parameter is required");
            }

            var client = clientFactory.CreateClient();
            var ontopEndpoint = $"http://localhost:8080/sparql?query={Uri.EscapeDataString(query)}";

            var response = await client.GetAsync(ontopEndpoint);
            var result = await response.Content.ReadAsStringAsync();
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/sparql-results+json";

            return Results.Content(result, contentType);
        })
        .WithName("ExecuteSparqlQueryGet")
        .WithSummary("Execute a SPARQL query via GET");

        // GET /sparql/examples - Return example queries
        group.MapGet("/examples", () =>
        {
            return Results.Ok(new
            {
                examples = new[]
                {
                    new
                    {
                        name = "Get all Stamm",
                        query = @"PREFIX : <http://nulllogicone.net/ontology#>
SELECT ?stamm ?name ?email
WHERE {
  ?stamm a :Stamm ;
         :stammName ?name ;
         :email ?email .
}
LIMIT 10"
                    },
                    new
                    {
                        name = "Get Stamm with Anglers",
                        query = @"PREFIX : <http://nulllogicone.net/ontology#>
SELECT ?stammName ?anglerName
WHERE {
  ?stamm a :Stamm ;
         :stammName ?stammName ;
         :hasAngler ?angler .
  ?angler :anglerName ?anglerName .
}
LIMIT 50"
                    },
                    new
                    {
                        name = "Count entities",
                        query = @"PREFIX : <http://nulllogicone.net/ontology#>
SELECT ?type (COUNT(*) as ?count)
WHERE {
  ?entity a ?type .
  FILTER(?type IN (:Stamm, :Angler, :PostIt, :TopLab))
}
GROUP BY ?type"
                    }
                }
            });
        })
        .WithName("GetSparqlExamples");

        // GET /sparql/health - Check if Ontop is running
        group.MapGet("/health", async (IHttpClientFactory clientFactory) =>
        {
            try
            {
                var client = clientFactory.CreateClient();
                var response = await client.GetAsync("http://localhost:8080/");
                return response.IsSuccessStatusCode
                    ? Results.Ok(new { status = "healthy", message = "Ontop SPARQL endpoint is accessible" })
                    : Results.Problem("Ontop endpoint returned error status");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Cannot connect to Ontop: {ex.Message}");
            }
        })
        .WithName("CheckSparqlHealth");
    }
}

// Add this to Program.cs after your other endpoint mappings:
// app.MapSparqlEndpoints();
