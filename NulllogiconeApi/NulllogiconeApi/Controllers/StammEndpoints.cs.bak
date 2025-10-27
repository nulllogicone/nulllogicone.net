using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;
using NulllogiconeApi.Services;

namespace NulllogiconeApi.Controllers
{
    public static class StammEndpoints
    {
        public static void MapStammEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/stamm")
                .WithTags("Stamm")
                .WithOpenApi();

            // GET /stamm - Get top 10 Stamm entries with content negotiation
            group.MapGet("/", async (ApplicationDbContext db, HttpContext context) =>
            {
                var stamms = await db.Stamms
                    .OrderByDescending(s => s.CreatedAt)
                    .Take(10)
                    .ToListAsync();
                
                // Check if client accepts RDF
                if (RdfContentNegotiation.AcceptsRdf(context))
                {
                    var mediaType = RdfContentNegotiation.GetPreferredRdfMediaType(context);
                    var rdfContent = RdfContentNegotiation.ConvertStammsToRdf(stamms, mediaType);
                    
                    return Results.Content(rdfContent, mediaType, System.Text.Encoding.UTF8);
                }
                
                return Results.Ok(stamms);
            })
            .WithName("GetStamms")
            .WithSummary("Get top 10 Stamm entries (supports RDF content negotiation)")
            .Produces<List<Stamm>>();

            // GET /stamm/{id} - Get Stamm by ID with content negotiation
            group.MapGet("/{id:int}", async (int id, ApplicationDbContext db, HttpContext context) =>
            {
                var stamm = await db.Stamms.FindAsync(id);
                
                if (stamm is null)
                {
                    return Results.NotFound($"Stamm with ID {id} not found.");
                }
                
                // Check if client accepts RDF
                if (RdfContentNegotiation.AcceptsRdf(context))
                {
                    var mediaType = RdfContentNegotiation.GetPreferredRdfMediaType(context);
                    var rdfContent = RdfContentNegotiation.ConvertStammToRdf(stamm, mediaType);
                    
                    return Results.Content(rdfContent, mediaType, System.Text.Encoding.UTF8);
                }
                
                return Results.Ok(stamm);
            })
            .WithName("GetStammById")
            .WithSummary("Get Stamm by ID (supports RDF content negotiation)")
            .Produces<Stamm>()
            .Produces(404);

            // POST /stamm - Create new Stamm
            group.MapPost("/", async (Stamm stamm, ApplicationDbContext db) =>
            {
                stamm.CreatedAt = DateTime.UtcNow;
                db.Stamms.Add(stamm);
                await db.SaveChangesAsync();
                
                return Results.Created($"/stamm/{stamm.Id}", stamm);
            })
            .WithName("CreateStamm")
            .WithSummary("Create a new Stamm");

            // PUT /stamm/{id} - Update Stamm
            group.MapPut("/{id:int}", async (int id, Stamm updatedStamm, ApplicationDbContext db) =>
            {
                var stamm = await db.Stamms.FindAsync(id);
                
                if (stamm is null)
                {
                    return Results.NotFound($"Stamm with ID {id} not found.");
                }
                
                stamm.Name = updatedStamm.Name;
                stamm.Description = updatedStamm.Description;
                stamm.UpdatedAt = DateTime.UtcNow;
                
                await db.SaveChangesAsync();
                
                return Results.Ok(stamm);
            })
            .WithName("UpdateStamm")
            .WithSummary("Update Stamm by ID");

            // DELETE /stamm/{id} - Delete Stamm
            group.MapDelete("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var stamm = await db.Stamms.FindAsync(id);
                
                if (stamm is null)
                {
                    return Results.NotFound($"Stamm with ID {id} not found.");
                }
                
                db.Stamms.Remove(stamm);
                await db.SaveChangesAsync();
                
                return Results.NoContent();
            })
            .WithName("DeleteStamm")
            .WithSummary("Delete Stamm by ID");
        }
    }
}