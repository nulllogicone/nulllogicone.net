using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;
using NulllogiconeCore.Extensions;

namespace NulllogiconeCore.Endpoints;

public static class StammEndpoints
{
    public static void MapStammEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("stamm")
            .WithTags("Stamm");


        // Unified GET /stamm/{id}
        group.MapGet("/{id:guid}", (Guid id, ApplicationDbContext db, HttpContext context) => HandleStammRequest(id, db, context, null))
        .WithName("GetStammById")
        .WithSummary("Get a specific Stamm by ID (supports Conneg)")
        .Produces<Stamm>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /stamm/{id}.rdf
        group.MapGet("/{id:guid}.rdf", (Guid id, ApplicationDbContext db, HttpContext context) => HandleStammRequest(id, db, context, ".rdf"))
        .WithName("GetStammByIdRdf")
        .Produces(StatusCodes.Status200OK);

        // GET /stamm/{id}.json
        group.MapGet("/{id:guid}.json", (Guid id, ApplicationDbContext db, HttpContext context) => HandleStammRequest(id, db, context, ".json"))
        .WithName("GetStammByIdJson")
        .Produces<Stamm>(StatusCodes.Status200OK);

    }

    private static async Task<IResult> HandleStammRequest(Guid id, ApplicationDbContext db, HttpContext context, string? extension)
    {
        var format = context.DetermineFormat(extension);

        if (format == RepresentationFormat.Html)
        {
            return Results.Redirect($"/ui/Stamm/{id}");
        }

        if (format == RepresentationFormat.Rdf)
        {
            var stamm = await db.Stamms
                .Include(s => s.Anglers)
                .FirstOrDefaultAsync(s => s.StammGuid == id);

            if (stamm is null) return Results.NotFound($"Stamm with ID {id} not found");

            var postIts = await db.StammPostIts.Where(p => p.StammGuid == id).ToListAsync();
            var topLabs = await db.StammTopLabs.Where(t => t.StammGuid == id).ToListAsync();

            var rdf = NulllogiconeCore.Services.Mappings.StammRdfMapper.ToRdfXml(stamm, postIts, topLabs);
            return Results.Content(rdf, "text/xml");
        }

        // Default to JSON
        var stammJson = await db.Stamms
            .Where(s => s.StammGuid == id)
            .Select(s => new
            {
                s.StammGuid,
                s.Stamm1,
                s.Beschreibung,
                s.KooK,
                s.Datum,
                s.Datei,
                s.Link,

                Anglers = s.Anglers.Select(a => new
                {
                    a.AnglerGuid,
                    a.Angler1,
                    a.Beschreibung,
                    a.Datum
                }).ToList(),

                TopLabs = s.TopLabs.Select(t => new
                {
                    t.TopLabGuid,
                    t.Titel,
                    t.TopLab1,
                    t.Datum,
                    t.Datei,
                    PostIt = new 
                    {
                        t.PostIt.PostItGuid,
                        t.PostIt.Titel,
                        t.PostIt.PostIt1
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
        return stammJson is not null
            ? Results.Ok(stammJson)
            : Results.NotFound($"Stamm with ID {id} not found");
    }
}
