using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;
using NulllogiconeCore.Extensions;
using NulllogiconeCore.Services.Mappings;

namespace NulllogiconeCore.Endpoints;

public static class AnglerEndpoints
{
    public static void MapAnglerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("angler")
            .WithTags("Angler");


        // Unified GET /angler/{id}
        group.MapGet("/{id:guid}", (Guid id, ApplicationDbContext db, HttpContext context) => HandleAnglerRequest(id, db, context, null))
        .WithName("GetAnglerById")
        .WithSummary("Get a specific Angler by ID (supports Conneg)")
        .Produces<Angler>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /angler/{id}.rdf
        group.MapGet("/{id:guid}.rdf", (Guid id, ApplicationDbContext db, HttpContext context) => HandleAnglerRequest(id, db, context, ".rdf"))
        .WithName("GetAnglerByIdRdf")
        .Produces(StatusCodes.Status200OK);

        // GET /angler/{id}.json
        group.MapGet("/{id:guid}.json", (Guid id, ApplicationDbContext db, HttpContext context) => HandleAnglerRequest(id, db, context, ".json"))
        .WithName("GetAnglerByIdJson")
        .Produces<Angler>(StatusCodes.Status200OK);

    }

    private static async Task<IResult> HandleAnglerRequest(Guid id, ApplicationDbContext db, HttpContext context, string? extension)
    {
        var format = context.DetermineFormat(extension);

        if (format == RepresentationFormat.Html)
        {
            return Results.Redirect($"/ui/Angler/{id}");
        }

        if (format == RepresentationFormat.Rdf)
        {
            var angler = await db.Anglers
                .Include(a => a.Löchers)
                .Include(a => a.News)
                .Include(a => a.Spiegels)
                .FirstOrDefaultAsync(a => a.AnglerGuid == id);

            if (angler is null) return Results.NotFound($"Angler with ID {id} not found");

            var rdf = AnglerRdfMapper.ToRdfXml(angler);
            return Results.Content(rdf, "text/xml");
        }

        // Default to JSON
        var anglerJson = await db.Anglers.FindAsync(id);
        return anglerJson is not null
            ? Results.Ok(anglerJson)
            : Results.NotFound($"Angler with ID {id} not found");
    }
}
