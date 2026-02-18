using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace NulllogiconeCore.Endpoints;

public static class SparqlEndpoints
{
    public static void MapSparqlEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("sparql")
            .WithTags("SPARQL");

        // POST /sparql - Execute SPARQL query
        group.MapPost("/", async (HttpRequest request, IHttpClientFactory clientFactory, IConfiguration config) =>
        {
            var client = clientFactory.CreateClient();
            var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
            var ontopEndpoint = $"{ontopBaseUrl}/sparql";

            // Read query from body (supports both form-encoded and raw SPARQL)
            string? query = null;
            if (request.ContentType?.Contains("application/x-www-form-urlencoded") == true)
            {
                var form = await request.ReadFormAsync();
                query = form["query"].ToString();
            }
            else if (request.ContentType?.Contains("application/sparql-query") == true)
            {
                using var reader = new StreamReader(request.Body);
                query = await reader.ReadToEndAsync();
            }
            else
            {
                return Results.BadRequest("Content-Type must be application/x-www-form-urlencoded or application/sparql-query");
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

            try
            {
                var response = await client.SendAsync(httpRequest);
                var result = await response.Content.ReadAsStringAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/sparql-results+json";

                return Results.Content(result, contentType, statusCode: (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return Results.Problem($"Cannot connect to Ontop SPARQL endpoint: {ex.Message}");
            }
        })
        .WithName("ExecuteSparqlQuery")
        .WithSummary("Execute a SPARQL query against the knowledge graph")
        .WithDescription("Supports both application/x-www-form-urlencoded (query parameter) and application/sparql-query (raw query in body)")
        .Produces(200)
        .Produces(400)
        .Produces(500);

        // GET /sparql - Execute SPARQL query via GET or show HTML interface
        group.MapGet("/", async (string? query, IHttpClientFactory clientFactory, HttpContext context, IConfiguration config) =>
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // Return simple instructions if no query provided
                return Results.Text(@"Nulllogicone SPARQL Endpoint

Namespace: http://nulllogicone.net/schema.rdfs# (prefix: nlo:)

Usage:
- POST /sparql with query parameter (form-encoded)
- GET /sparql?query=... (URL-encoded query)
- GET /sparql/examples (see example queries)
- GET /sparql/schema (see available entities)
- GET /sparql/health (check Ontop status)

Example query:
PREFIX nlo: <http://nulllogicone.net/schema.rdfs#>
SELECT ?stamm ?name ?email
WHERE {
  ?stamm a nlo:Stamm ;
         nlo:name ?name ;
         nlo:email ?email .
}
LIMIT 10
", "text/plain; charset=utf-8");
            }

            var client = clientFactory.CreateClient();
            var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
            var ontopEndpoint = $"{ontopBaseUrl}/sparql?query={Uri.EscapeDataString(query)}";

            // Determine accept header
            var acceptHeader = context.Request.Headers.Accept.ToString();
            if (string.IsNullOrEmpty(acceptHeader))
            {
                acceptHeader = "application/sparql-results+json";
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, ontopEndpoint);
            httpRequest.Headers.Add("Accept", acceptHeader);

            try
            {
                var response = await client.SendAsync(httpRequest);
                var result = await response.Content.ReadAsStringAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/sparql-results+json";

                return Results.Content(result, contentType, statusCode: (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return Results.Problem($"Cannot connect to Ontop SPARQL endpoint: {ex.Message}");
            }
        })
        .WithName("ExecuteSparqlQueryGet")
        .WithSummary("Execute a SPARQL query via GET or show usage instructions");

        // GET /sparql/examples - Return example queries
        group.MapGet("/examples", () =>
        {
            return Results.Ok(new
            {
                endpoint = "/sparql",
                @namespace = "http://nulllogicone.net/schema.rdfs#",
                prefix = "nlo:",
                examples = GetExamples()
            });
        })
        .WithName("GetSparqlExamples")
        .WithSummary("Get example SPARQL queries");

        // GET /sparql/health - Check if Ontop is running
        group.MapGet("/health", async (IHttpClientFactory clientFactory, IConfiguration config) =>
        {
            try
            {
                var client = clientFactory.CreateClient();
                var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
                var response = await client.GetAsync(ontopBaseUrl);
                return response.IsSuccessStatusCode
                    ? Results.Ok(new { status = "healthy", message = "Ontop SPARQL endpoint is accessible", endpoint = $"{ontopBaseUrl}/sparql" })
                    : Results.Problem("Ontop endpoint returned error status");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Cannot connect to Ontop: {ex.Message}");
            }
        })
        .WithName("CheckSparqlHealth")
        .WithSummary("Check if Ontop SPARQL endpoint is running");

        // GET /sparql/schema - Return schema/ontology info
        group.MapGet("/schema", () =>
        {
            return Results.Ok(new
            {
                @namespace = "http://nulllogicone.net/schema.rdfs#",
                prefix = "nlo:",
                ontologyFile = "schema.rdfs",
                entities = new[]
                {
                    new { name = "Stamm", description = "User", countApprox = 267 },
                    new { name = "Angler", description = "Filter Profile", countApprox = 518 },
                    new { name = "PostIt", description = "Message", countApprox = 1642 },
                    new { name = "Code", description = "Marking", countApprox = 1554 },
                    new { name = "TopLab", description = "Response", countApprox = 1690 },
                    new { name = "Ring", description = "Spot in Code (OLIs/Get)", countApprox = 9542 },
                    new { name = "Loch", description = "Spot in Angler (ILOs/Fit)", countApprox = 2905 },
                    new { name = "Netz", description = "Network", countApprox = 107 },
                    new { name = "Knoten", description = "Node", countApprox = 503 },
                    new { name = "Baum", description = "Tree", countApprox = 273 },
                    new { name = "Zweig", description = "Branch", countApprox = 1729 }
                },
                properties = new[]
                {
                    "name", "beschreibung", "datum", "datei", "link", "email", "tel",
                    "titel", "version", "olis", "get", "ilos", "fit"
                },
                relationships = new[]
                {
                    "stamm", "angler", "postit", "code", "toplab",
                    "ring", "loch", "netz", "knoten", "baum", "zweig",
                    "hasAngler", "hasPostIt", "hasCode", "hasTopLab"
                }
            });
        })
        .WithName("GetSparqlSchema")
        .WithSummary("Get schema information about available entities and properties");

        // GET /sparql/ontop-assets/{**path} - Proxy Ontop static resources (CSS, JS, etc)
        group.MapGet("/ontop-assets/{**path}", async (string path, IHttpClientFactory clientFactory, HttpContext context, IConfiguration config) =>
        {
            var client = clientFactory.CreateClient();
            var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
            var baseUrl = ontopBaseUrl.Replace("/sparql", "");
            var targetUrl = $"{baseUrl}/{path}";

            // Forward query string if present
            if (context.Request.QueryString.HasValue)
            {
                targetUrl += context.Request.QueryString.Value;
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, targetUrl);
                
                // Forward relevant headers
                if (context.Request.Headers.Accept.Count > 0)
                {
                    request.Headers.Add("Accept", context.Request.Headers.Accept.ToString());
                }

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";

                // Rewrite URLs in CSS/JS that might reference other resources
                if (contentType.Contains("text/css") || 
                    contentType.Contains("application/javascript") ||
                    contentType.Contains("text/javascript") ||
                    path.EndsWith(".js") || 
                    path.EndsWith(".css"))
                {
                    var textContent = Encoding.UTF8.GetString(content);
                    textContent = textContent.Replace("url(./", "url(/sparql/ontop-assets/")
                                             .Replace("url(/", "url(/sparql/ontop-assets/")
                                             .Replace("url(\"./", "url(\"/sparql/ontop-assets/")
                                             .Replace("url(\"/", "url(\"/sparql/ontop-assets/");
                    
                    // Fix JavaScript endpoint URL for YASGUI
                    if (path.EndsWith(".js"))
                    {
                        textContent = textContent.Replace("new Request('sparql')", "new Request('/sparql')");
                    }
                    
                    content = Encoding.UTF8.GetBytes(textContent);
                }

                context.Response.StatusCode = (int)response.StatusCode;
                context.Response.ContentType = contentType;
                await context.Response.Body.WriteAsync(content);
                
                return Results.Empty;
            }
            catch (HttpRequestException ex)
            {
                return Results.Problem($"Cannot proxy Ontop resource '{path}': {ex.Message}");
            }
        })
        .WithName("ProxyOntopAssets")
        .WithSummary("Proxy Ontop static resources (CSS, JS, images)");

        // GET /sparql/ontop/{**path} - Proxy Ontop API calls (portalConfig, etc)
        group.MapGet("/ontop/{**path}", async (string path, IHttpClientFactory clientFactory, HttpContext context, IConfiguration config) =>
        {
            var client = clientFactory.CreateClient();
            var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
            var baseUrl = ontopBaseUrl.Replace("/sparql", "");
            var targetUrl = $"{baseUrl}/ontop/{path}";

            // Forward query string if present
            if (context.Request.QueryString.HasValue)
            {
                targetUrl += context.Request.QueryString.Value;
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, targetUrl);
                
                // Forward relevant headers
                if (context.Request.Headers.Accept.Count > 0)
                {
                    request.Headers.Add("Accept", context.Request.Headers.Accept.ToString());
                }

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/json";

                return Results.Content(content, contentType, statusCode: (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return Results.Problem($"Cannot proxy Ontop API '{path}': {ex.Message}");
            }
        })
        .WithName("ProxyOntopAPI")
        .WithSummary("Proxy Ontop API endpoints (configuration, metadata)");

        // GET /sparql/ui - Proxy to Ontop Web UI main page
        group.MapGet("/ui", async (IHttpClientFactory clientFactory, HttpContext context, IConfiguration config) =>
        {
            var client = clientFactory.CreateClient();
            var ontopBaseUrl = config["Ontop:EndpointUrl"] ?? "http://localhost:8080";
            var targetUrl = ontopBaseUrl.Replace("/sparql", ""); // Get base URL

            try
            {
                var response = await client.GetAsync(targetUrl);
                var content = await response.Content.ReadAsStringAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "text/html";

                // Rewrite URLs in HTML to use our asset proxy path
                if (contentType.Contains("text/html"))
                {
                    // Handle both single and double quotes
                    // Replace relative paths (./) first
                    content = content.Replace("href=\"./", "href=\"/sparql/ontop-assets/")
                                     .Replace("src=\"./", "src=\"/sparql/ontop-assets/")
                                     .Replace("href='./", "href='/sparql/ontop-assets/")
                                     .Replace("src='./", "src='/sparql/ontop-assets/");
                    
                    // Replace absolute paths (/) but skip already-rewritten ones
                    content = System.Text.RegularExpressions.Regex.Replace(
                        content,
                        @"(href|src)=([""'])(/(?!sparql))",
                        "$1=$2/sparql/ontop-assets/");
                    
                    // Keep the actual SPARQL endpoint path intact
                    content = content.Replace("action=\"/sparql/ontop-assets/sparql\"", "action=\"/sparql\"")
                                     .Replace("action='/sparql/ontop-assets/sparql'", "action='/sparql'");
                    
                    // Fix JavaScript endpoint URL - change relative 'sparql' to absolute '/sparql'
                    content = content.Replace("new Request('sparql')", "new Request('/sparql')");
                }

                return Results.Content(content, contentType, statusCode: (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return Results.Problem($"Cannot connect to Ontop UI: {ex.Message}");
            }
        })
        .WithName("GetOntopUI")
        .WithSummary("Access Ontop web query interface");
    }

    private static object[] GetExamples()
    {
        return new object[]
        {
            new { name = "Get all Stamm", query = "PREFIX nlo: <http://nulllogicone.net/schema.rdfs#> SELECT ?stamm ?name WHERE { ?stamm a nlo:Stamm ; nlo:name ?name . } LIMIT 10" },
            new { name = "Count entities", query = "PREFIX nlo: <http://nulllogicone.net/schema.rdfs#> SELECT ?type (COUNT(*) as ?count) WHERE { ?entity a ?type . } GROUP BY ?type ORDER BY DESC(?count)" },
            new { name = "Get Stamm with Anglers", query = "PREFIX nlo: <http://nulllogicone.net/schema.rdfs#> SELECT ?stammName ?anglerName WHERE { ?stamm a nlo:Stamm ; nlo:name ?stammName ; nlo:hasAngler ?angler . ?angler nlo:name ?anglerName . } LIMIT 20" }
        };
    }
}
