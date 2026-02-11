using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;
using NulllogiconeCore.Extensions;

namespace NulllogiconeCore.Endpoints;

public static class TopLabEndpoints
{
    public static void MapTopLabEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("toplab")
            .WithTags("TopLab");

        // Unified GET /toplab/{id}
        group.MapGet("/{id:guid}", (Guid id, ApplicationDbContext db, HttpContext context) => HandleTopLabRequest(id, db, context, null))
            .WithName("GetTopLabById")
            .WithSummary("Get a specific TopLab by ID (supports Conneg)")
            .Produces<TopLab>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        // GET /toplab/{id}.rdf
        group.MapGet("/{id:guid}.rdf", (Guid id, ApplicationDbContext db, HttpContext context) => HandleTopLabRequest(id, db, context, ".rdf"))
            .WithName("GetTopLabByIdRdf")
            .Produces(StatusCodes.Status200OK);

        // GET /toplab/{id}.json
        group.MapGet("/{id:guid}.json", (Guid id, ApplicationDbContext db, HttpContext context) => HandleTopLabRequest(id, db, context, ".json"))
            .WithName("GetTopLabByIdJson")
            .Produces<TopLab>(StatusCodes.Status200OK);

    }

    // Local handler implementing content negotiation similar to Stamm/PostIt
    private static async Task<IResult> HandleTopLabRequest(Guid id, ApplicationDbContext db, HttpContext context, string? extension)
    {
        var format = context.DetermineFormat(extension);

        if (format == RepresentationFormat.Html)
        {
            return Results.Redirect($"/ui/TopLab/{id}");
        }

        if (format == RepresentationFormat.Rdf)
        {
            var entity = await db.TopLabs.FindAsync(id);
            if (entity is null) return Results.NotFound($"TopLab with ID {id} not found");

            var rdf = NulllogiconeCore.Services.Mappings.TopLabRdfMapper.ToRdfXml(entity);
            return Results.Content(rdf, "text/xml");
        }

        var jsonEntity = await db.TopLabs.FindAsync(id);
        return jsonEntity is not null
            ? Results.Ok(jsonEntity)
            : Results.NotFound($"TopLab with ID {id} not found");
    }
}
