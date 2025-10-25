var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

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

// Nulllogicone
// /about
app.MapGet("/about", () => new
{
    Name = "Nulllogicone API",
    Version = "1.0.0",
    Description = "An example API demonstrating Nulllogicone features."
});

// /stamm
app.MapGet("/stamm", () => new[]
{
    new { Id = 1, Name = "Stamm 1", Description = "This is the description for Stamm 1." },
    new { Id = 2, Name = "Stamm 2", Description = "This is the description for Stamm 2." },
    new { Id = 3, Name = "Stamm 3", Description = "This is the description for Stamm 3." }
});

// /stamm/id
app.MapGet("/stamm/{id}", (int id) => new
{
    Id = id,
    Name = $"Stamm {id}",
    Description = $"This is the description for Stamm {id}."
});


app.Run();




internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
