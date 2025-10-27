using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Endpoints;

public static class PostItEndpoints
{
    public static void MapPostItEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/postit")
            .WithTags("PostIt");

        // GET /postit
        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.PostIts
                .Select(p => new
                {
                    p.PostItGuid,
                    p.Titel,
                    Content = p.PostIt1.Length > 100 ? p.PostIt1.Substring(0, 100) + "..." : p.PostIt1,
                    p.Datum,
                    p.KooK,
                    p.Typ,
                    p.Url
                })
                .OrderByDescending(p => p.Datum)
                .Take(20)
                .ToListAsync();
        })
        .WithName("GetPostIts")
        .WithSummary("Get all PostIt entries (limited to 20)")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

        // GET /postit/{id}
        group.MapGet("/{id:guid}", async (Guid id, ApplicationDbContext db) =>
        {
            var postit = await db.PostIts.FindAsync(id);
            
            return postit is not null 
                ? Results.Ok(postit) 
                : Results.NotFound($"PostIt with ID {id} not found");
        })
        .WithName("GetPostItById")
        .WithSummary("Get a specific PostIt by ID")
        .Produces<PostIt>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /postit/count
        group.MapGet("/count", async (ApplicationDbContext db) =>
        {
            var count = await db.PostIts.CountAsync();
            return Results.Ok(new { Count = count });
        })
        .WithName("GetPostItCount")
        .WithSummary("Get total count of PostIt entries")
        .Produces<object>(StatusCodes.Status200OK);

        // GET /postit/search/{term}
        group.MapGet("/search/{term}", async (string term, ApplicationDbContext db) =>
        {
            var postits = await db.PostIts
                .Where(p => (p.Titel != null && p.Titel.Contains(term)) || 
                           (p.PostIt1 != null && p.PostIt1.Contains(term)))
                .Select(p => new
                {
                    p.PostItGuid,
                    p.Titel,
                    Content = p.PostIt1.Length > 100 ? p.PostIt1.Substring(0, 100) + "..." : p.PostIt1,
                    p.Datum,
                    p.Typ
                })
                .OrderByDescending(p => p.Datum)
                .Take(10)
                .ToListAsync();

            return Results.Ok(postits);
        })
        .WithName("SearchPostIts")
        .WithSummary("Search PostIt entries by title or content")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

        // GET /postit/by-type/{type}
        group.MapGet("/by-type/{type}", async (string type, ApplicationDbContext db) =>
        {
            var postits = await db.PostIts
                .Where(p => p.Typ == type)
                .Select(p => new
                {
                    p.PostItGuid,
                    p.Titel,
                    Content = p.PostIt1.Length > 100 ? p.PostIt1.Substring(0, 100) + "..." : p.PostIt1,
                    p.Datum,
                    p.KooK
                })
                .OrderByDescending(p => p.Datum)
                .Take(20)
                .ToListAsync();

            return Results.Ok(postits);
        })
        .WithName("GetPostItsByType")
        .WithSummary("Get PostIt entries by type")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);
    }
}