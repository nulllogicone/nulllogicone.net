using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;
using NulllogiconeCore.Extensions;

namespace NulllogiconeCore.Endpoints;

public static class PostItEndpoints
{
    public static void MapPostItEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("postit")
            .WithTags("PostIt");

        // Unified GET /postit/{id}
        group.MapGet("/{id:guid}", (Guid id, ApplicationDbContext db, HttpContext context) => HandlePostItRequest(id, db, context, null))
        .WithName("GetPostItById")
        .WithSummary("Get a specific PostIt by ID (supports Conneg)")
        .Produces<PostIt>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /postit/{id}.rdf
        group.MapGet("/{id:guid}.rdf", (Guid id, ApplicationDbContext db, HttpContext context) => HandlePostItRequest(id, db, context, ".rdf"))
        .WithName("GetPostItByIdRdf")
        .Produces(StatusCodes.Status200OK);

        // GET /postit/{id}.json
        group.MapGet("/{id:guid}.json", (Guid id, ApplicationDbContext db, HttpContext context) => HandlePostItRequest(id, db, context, ".json"))
        .WithName("GetPostItByIdJson")
        .Produces<PostIt>(StatusCodes.Status200OK);

    }

    private static async Task<IResult> HandlePostItRequest(Guid id, ApplicationDbContext db, HttpContext context, string? extension)
    {
        var format = context.DetermineFormat(extension);

        if (format == RepresentationFormat.Html)
        {
            return Results.Redirect($"/ui/PostIt/{id}");
        }

        if (format == RepresentationFormat.Rdf)
        {
            var postit = await db.PostIts.FindAsync(id);

            if (postit is null) return Results.NotFound($"PostIt with ID {id} not found");

            var rdf = NulllogiconeCore.Services.Mappings.PostItRdfMapper.ToRdfXml(postit);
            return Results.Content(rdf, "text/xml");
        }

        // Default to JSON
        var postitJson = await db.PostIts.FindAsync(id);
        return postitJson is not null
            ? Results.Ok(postitJson)
            : Results.NotFound($"PostIt with ID {id} not found");
    }
}
