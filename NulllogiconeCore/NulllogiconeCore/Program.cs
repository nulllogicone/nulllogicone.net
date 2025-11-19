using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Endpoints;
using NulllogiconeCore.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddRazorPages();


// Use our own API in UI pages with HttpClient
var backendApiBaseUrl = builder.Configuration["BackendApiBaseUrl"] ?? "/";
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


// BUILD
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseOpenApi();
    //app.UseSwaggerUi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

    // Ensure database is created
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //context.Database.EnsureCreated();
}

app.UseCors();
app.UseHttpsRedirection();



// Map minimal API endpoints for SAPCT
app.MapStammEndpoints();
app.MapPostItEndpoints();
app.MapTopLabEndpoints();

// Nulllogicone API Info
app.MapGet("about", () => new
{
    Name = "Nulllogicone Core",
    Version = "1.0.0",
    Description = "Razor pages and API with rdf for NullLogicOne",
    MaschineName = Environment.MachineName,
    Status = "Connected to real database - null - Minimal APIs",
    Endpoints = new[]
    {
        "/stamm - Manage Stamm entities",
        "/postit - Manage PostIt entities",
        "/toplab - Manage TopLab entities",
        "/swagger - API documentation"
    }
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

// Razor pages
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();























