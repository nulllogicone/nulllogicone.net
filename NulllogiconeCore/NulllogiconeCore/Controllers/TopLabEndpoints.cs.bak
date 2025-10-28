using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Controllers
{
    public static class TopLabEndpoints
    {
        public static void MapTopLabEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/toplab")
                .WithTags("TopLab")
                .WithOpenApi();

            // GET /toplab - Get top 10 TopLab entries
            group.MapGet("/", async (ApplicationDbContext db, string? category = null) =>
            {
                var query = db.TopLabs.AsQueryable();
                
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(t => t.Category == category);
                }
                
                var topLabs = await query
                    .OrderByDescending(t => t.Priority)
                    .ThenByDescending(t => t.CreatedAt)
                    .Take(10)
                    .ToListAsync();
                
                return Results.Ok(topLabs);
            })
            .WithName("GetTopLabs")
            .WithSummary("Get top 10 TopLab entries")
            .Produces<List<TopLab>>();

            // GET /toplab/categories - Get all unique categories
            group.MapGet("/categories", async (ApplicationDbContext db) =>
            {
                var categories = await db.TopLabs
                    .Where(t => t.Category != null)
                    .Select(t => t.Category)
                    .Distinct()
                    .ToListAsync();
                
                return Results.Ok(categories);
            })
            .WithName("GetTopLabCategories")
            .WithSummary("Get all TopLab categories")
            .Produces<List<string>>();

            // GET /toplab/{id} - Get TopLab by ID
            group.MapGet("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var topLab = await db.TopLabs.FindAsync(id);
                
                return topLab is not null 
                    ? Results.Ok(topLab) 
                    : Results.NotFound($"TopLab with ID {id} not found.");
            })
            .WithName("GetTopLabById")
            .WithSummary("Get TopLab by ID")
            .Produces<TopLab>()
            .Produces(404);

            // POST /toplab - Create new TopLab
            group.MapPost("/", async (TopLab topLab, ApplicationDbContext db) =>
            {
                topLab.CreatedAt = DateTime.UtcNow;
                db.TopLabs.Add(topLab);
                await db.SaveChangesAsync();
                
                return Results.Created($"/toplab/{topLab.Id}", topLab);
            })
            .WithName("CreateTopLab")
            .WithSummary("Create a new TopLab")
            .Accepts<TopLab>("application/json")
            .Produces<TopLab>(201);

            // PUT /toplab/{id} - Update TopLab
            group.MapPut("/{id:int}", async (int id, TopLab updatedTopLab, ApplicationDbContext db) =>
            {
                var topLab = await db.TopLabs.FindAsync(id);
                
                if (topLab is null)
                {
                    return Results.NotFound($"TopLab with ID {id} not found.");
                }
                
                topLab.Name = updatedTopLab.Name;
                topLab.Description = updatedTopLab.Description;
                topLab.Category = updatedTopLab.Category;
                topLab.Priority = updatedTopLab.Priority;
                topLab.UpdatedAt = DateTime.UtcNow;
                
                await db.SaveChangesAsync();
                
                return Results.Ok(topLab);
            })
            .WithName("UpdateTopLab")
            .WithSummary("Update TopLab by ID")
            .Accepts<TopLab>("application/json")
            .Produces<TopLab>()
            .Produces(404);

            // PATCH /toplab/{id}/priority - Update priority only
            group.MapPatch("/{id:int}/priority", async (int id, int priority, ApplicationDbContext db) =>
            {
                var topLab = await db.TopLabs.FindAsync(id);
                
                if (topLab is null)
                {
                    return Results.NotFound($"TopLab with ID {id} not found.");
                }
                
                topLab.Priority = priority;
                topLab.UpdatedAt = DateTime.UtcNow;
                
                await db.SaveChangesAsync();
                
                return Results.Ok(topLab);
            })
            .WithName("UpdateTopLabPriority")
            .WithSummary("Update TopLab priority")
            .Accepts<int>("application/json")
            .Produces<TopLab>()
            .Produces(404);

            // DELETE /toplab/{id} - Delete TopLab
            group.MapDelete("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var topLab = await db.TopLabs.FindAsync(id);
                
                if (topLab is null)
                {
                    return Results.NotFound($"TopLab with ID {id} not found.");
                }
                
                db.TopLabs.Remove(topLab);
                await db.SaveChangesAsync();
                
                return Results.NoContent();
            })
            .WithName("DeleteTopLab")
            .WithSummary("Delete TopLab by ID")
            .Produces(204)
            .Produces(404);
        }
    }
}