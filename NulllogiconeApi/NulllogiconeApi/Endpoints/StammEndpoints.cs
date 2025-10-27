using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Endpoints;

public static class StammEndpoints
{
    public static void MapStammEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/stamm")
            .WithTags("Stamm");

        // GET /stamm
        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.Stamms
                .Select(s => new
                {
                    s.StammGuid,
                    s.Stamm1,
                    s.EMail,
                    s.Beschreibung,
                    s.Datum,
                    s.KooK
                })
                .Take(20)
                .ToListAsync();
        })
        .WithName("GetStamms")
        .WithSummary("Get all Stamm entries (limited to 20)")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

        // GET /stamm/{id}
        group.MapGet("/{id:guid}", async (Guid id, ApplicationDbContext db) =>
        {
            var stamm = await db.Stamms.FindAsync(id);
            
            return stamm is not null 
                ? Results.Ok(stamm) 
                : Results.NotFound($"Stamm with ID {id} not found");
        })
        .WithName("GetStammById")
        .WithSummary("Get a specific Stamm by ID")
        .Produces<Stamm>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /stamm/count
        group.MapGet("/count", async (ApplicationDbContext db) =>
        {
            var count = await db.Stamms.CountAsync();
            return Results.Ok(new { Count = count });
        })
        .WithName("GetStammCount")
        .WithSummary("Get total count of Stamm entries")
        .Produces<object>(StatusCodes.Status200OK);

        // GET /stamm/search/{term}
        group.MapGet("/search/{term}", async (string term, ApplicationDbContext db) =>
        {
            var stamms = await db.Stamms
                .Where(s => s.Stamm1.Contains(term) || 
                           (s.Beschreibung != null && s.Beschreibung.Contains(term)) ||
                           (s.EMail != null && s.EMail.Contains(term)))
                .Select(s => new
                {
                    s.StammGuid,
                    s.Stamm1,
                    s.EMail,
                    s.Beschreibung,
                    s.Datum
                })
                .Take(10)
                .ToListAsync();

            return Results.Ok(stamms);
        })
        .WithName("SearchStamms")
        .WithSummary("Search Stamm entries by name, description, or email")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);
    }
}