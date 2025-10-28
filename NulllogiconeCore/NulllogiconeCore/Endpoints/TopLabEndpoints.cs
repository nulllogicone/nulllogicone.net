using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Endpoints;

public static class TopLabEndpoints
{
    public static void MapTopLabEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/toplab")
            .WithTags("TopLab");

        // GET /toplab
        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.TopLabs
                .Select(t => new
                {
                    t.TopLabGuid,
                    t.Titel,
                    Content = t.TopLab1.Length > 100 ? t.TopLab1.Substring(0, 100) + "..." : t.TopLab1,
                    t.Datum,
                    t.Lohn,
                    t.Typ,
                    t.Url
                })
                .OrderByDescending(t => t.Datum)
                .Take(20)
                .ToListAsync();
        })
        .WithName("GetTopLabs")
        .WithSummary("Get all TopLab entries (limited to 20)")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

        // GET /toplab/{id}
        group.MapGet("/{id:guid}", async (Guid id, ApplicationDbContext db) =>
        {
            var toplab = await db.TopLabs.FindAsync(id);
            
            return toplab is not null 
                ? Results.Ok(toplab) 
                : Results.NotFound($"TopLab with ID {id} not found");
        })
        .WithName("GetTopLabById")
        .WithSummary("Get a specific TopLab by ID")
        .Produces<TopLab>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /toplab/count
        group.MapGet("/count", async (ApplicationDbContext db) =>
        {
            var count = await db.TopLabs.CountAsync();
            return Results.Ok(new { Count = count });
        })
        .WithName("GetTopLabCount")
        .WithSummary("Get total count of TopLab entries")
        .Produces<object>(StatusCodes.Status200OK);

        // GET /toplab/search/{term}
        group.MapGet("/search/{term}", async (string term, ApplicationDbContext db) =>
        {
            var toplabs = await db.TopLabs
                .Where(t => (t.Titel != null && t.Titel.Contains(term)) || 
                           (t.TopLab1 != null && t.TopLab1.Contains(term)))
                .Select(t => new
                {
                    t.TopLabGuid,
                    t.Titel,
                    Content = t.TopLab1.Length > 100 ? t.TopLab1.Substring(0, 100) + "..." : t.TopLab1,
                    t.Datum,
                    t.Lohn,
                    t.Typ
                })
                .OrderByDescending(t => t.Datum)
                .Take(10)
                .ToListAsync();

            return Results.Ok(toplabs);
        })
        .WithName("SearchTopLabs")
        .WithSummary("Search TopLab entries by title or content")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

        // GET /toplab/with-postit
        group.MapGet("/with-postit", async (ApplicationDbContext db) =>
        {
            return await db.TopLabs
                .Include(t => t.PostIt)
                .Select(t => new
                {
                    t.TopLabGuid,
                    t.Titel,
                    t.Datum,
                    t.Lohn,
                    PostItTitle = t.PostIt != null ? t.PostIt.Titel : null
                })
                .OrderByDescending(t => t.Datum)
                .Take(20)
                .ToListAsync();
        })
        .WithName("GetTopLabsWithPostIt")
        .WithSummary("Get TopLab entries that are linked to PostIts")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);
    }
}
