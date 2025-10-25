using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Controllers
{
    public static class StammEndpoints
    {
        public static void MapStammEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/stamm")
                .WithTags("Stamm")
                .WithOpenApi();

            // GET /stamm - Get top 10 Stamm entries
            group.MapGet("/", async (ApplicationDbContext db) =>
            {
                var stamms = await db.Stamms
                    .OrderByDescending(s => s.CreatedAt)
                    .Take(10)
                    .ToListAsync();
                
                return Results.Ok(stamms);
            })
            .WithName("GetStamms")
            .WithSummary("Get top 10 Stamm entries")
            .Produces<List<Stamm>>();

            // GET /stamm/{id} - Get Stamm by ID
            group.MapGet("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var stamm = await db.Stamms.FindAsync(id);
                
                return stamm is not null 
                    ? Results.Ok(stamm) 
                    : Results.NotFound($"Stamm with ID {id} not found.");
            })
            .WithName("GetStammById")
            .WithSummary("Get Stamm by ID")
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
            .WithSummary("Create a new Stamm")
            .Accepts<Stamm>("application/json")
            .Produces<Stamm>(201);

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
            .WithSummary("Update Stamm by ID")
            .Accepts<Stamm>("application/json")
            .Produces<Stamm>()
            .Produces(404);

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
            .WithSummary("Delete Stamm by ID")
            .Produces(204)
            .Produces(404);
        }
    }
}