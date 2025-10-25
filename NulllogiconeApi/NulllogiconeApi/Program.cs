using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
    
    // Ensure database is created
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.UseCors();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Nulllogicone API Info
app.MapGet("/about", () => new
{
    Name = "Nulllogicone API",
    Version = "1.0.0",
    Description = "An API demonstrating Nulllogicone features with Entity Framework Core.",
    Endpoints = new[]
    {
        "/stamm - Manage Stamm entities",
        "/postit - Manage PostIt notes",
        "/toplab - Manage TopLab experiments"
    }
})
.WithName("GetApiInfo")
.WithSummary("Get API information")
.WithTags("Info");

// Map organized endpoints
app.MapStammEndpoints();
app.MapPostItEndpoints();
app.MapTopLabEndpoints();


app.Run();




internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
