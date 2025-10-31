using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Endpoints;

public static class StammEndpoints
{
    public static void MapStammEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/stamm")
            .WithTags("Stamm");

        // RDF endpoints (hardcoded for now)
        // GET /stamm.rdf
        routes.MapGet("/stamm.rdf", () =>
        {
            var rdf = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                      "<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" " +
                      "xmlns:dc=\"http://purl.org/dc/elements/1.1/\">" +
                      "<rdf:Description rdf:about=\"http://nulllogicone.net/stamm/sample\">" +
                      "<dc:title>Sample Stamm RDF</dc:title>" +
                      "</rdf:Description>" +
                      "</rdf:RDF>";

            return Results.Content(rdf, "application/rdf+xml");
        })
        .WithName("GetStammsRdf")
        .WithSummary("Get a hardcoded RDF/XML representation for stamms")
        .Produces(StatusCodes.Status200OK);

        // GET /stamm/{id}.rdf - return RDF/XML produced from the DB entity
        routes.MapGet("/stamm/{id:guid}.rdf", async (Guid id, ApplicationDbContext db) =>
        {
            var stamm = await db.Stamms
                .Include(s => s.Anglers)
                .FirstOrDefaultAsync(s => s.StammGuid == id);

            if (stamm is null)
                return Results.NotFound($"Stamm with ID {id} not found");

            // load related view collections for richer RDF
            var postIts = await db.StammPostIts.Where(p => p.StammGuid == id).ToListAsync();
            var topLabs = await db.StammTopLabs.Where(t => t.StammGuid == id).ToListAsync();

            var rdf = NulllogiconeCore.Services.Mappings.StammRdfMapper.ToRdfXml(stamm, postIts, topLabs);
            return Results.Content(rdf, "application/rdf+xml");
        })
        .WithName("GetStammByIdRdf")
        .WithSummary("Get an RDF/XML representation for a single Stamm by ID")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

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
        .Produces<IEnumerable<Stamm>>(StatusCodes.Status200OK);

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
