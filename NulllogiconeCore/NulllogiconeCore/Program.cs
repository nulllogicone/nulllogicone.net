using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;
using NulllogiconeCore.Data;
using NulllogiconeCore.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddRazorPages();


// Use our own API in UI pages with HttpClient
var backendApiBaseUrl = builder.Configuration["BackendApiBaseUrl"]!;
builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(backendApiBaseUrl, UriKind.RelativeOrAbsolute);
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.MaxDepth = 256;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});


// BUILD
var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
//app.UseOpenApi();
//app.UseSwaggerUi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});


app.UseCors();
app.UseHttpsRedirection();



// Map minimal API endpoints for SAPCT
app.MapStammEndpoints();
app.MapAnglerEndpoints();
app.MapPostItEndpoints();
app.MapTopLabEndpoints();

// Map SPARQL endpoint (proxies to Ontop at localhost:8080)
app.MapSparqlEndpoints();

// Nulllogicone API Info
app.MapGet("about", () => new
{
    Name = "Nulllogicone Core",
    Version = "1.0.0",
    Description = "UI and API for NullLogicOne",
    MaschineName = Environment.MachineName,
    Status = "Connected to real database - null - Minimal APIs"
})
.WithName("GetApiInfo")
.WithSummary("Get API information")
.WithTags("Info");

// Test with EF 
app.MapGet("/test/{id:guid}", async (Guid id, ApplicationDbContext context) =>
{
    var result = await context.Stamms
        .Where(s => s.StammGuid == id)
        .Select(stamm => new
        {
            Stamm = new
            {
                //stamm.StammGuid,
                Name = stamm.Stamm1,
                Description = stamm.Beschreibung
            },
            Angler = stamm.Anglers.Select(a => new
            {
                //a.AnglerGuid,
                Name = a.Angler1,
                Description = a.Beschreibung
            }),
            Postit = stamm.Wurzelns.Select(p => new
            {
                //p.PostItGuid,
                p.PostIt.Titel,
                Description = p.PostIt.PostIt1,
            })
        })
        .FirstOrDefaultAsync();

    if (result == null)
        return Results.NotFound();

    return Results.Ok(result);
})
.WithName("TestDbContextInjection")
.WithSummary("Tests DbContext injection")
.WithTags("Test");

// Configure static files with custom MIME types
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".sparql"] = "application/sparql-query";
provider.Mappings[".ttl"] = "text/turtle";
provider.Mappings[".rdf"] = "application/rdf+xml";
provider.Mappings[".rdfs"] = "application/rdf+xml";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// Razor pages
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();























