using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi();
    
    // Ensure database is created
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //context.Database.EnsureCreated();
}

app.UseCors();
app.UseHttpsRedirection();



// Map minimal API endpoints
app.MapStammEndpoints();
app.MapPostItEndpoints();
app.MapTopLabEndpoints();

// Nulllogicone API Info
app.MapGet("/about", () => new
{
    Name = "Nulllogicone API",
    Version = "1.0.0",
    Description = "An API for Nulllogicone with scaffolded database models from your real database.",
    Status = "Connected to real database - Minimal APIs",
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


app.Run();




