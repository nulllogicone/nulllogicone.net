using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Data;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Controllers
{
    public static class PostItEndpoints
    {
        public static void MapPostItEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/postit")
                .WithTags("PostIt")
                .WithOpenApi();

            // GET /postit - Get top 10 PostIt entries
            group.MapGet("/", async (ApplicationDbContext db, bool? completed = null) =>
            {
                var query = db.PostIts.AsQueryable();
                
                if (completed.HasValue)
                {
                    query = query.Where(p => p.IsCompleted == completed.Value);
                }
                
                var postIts = await query
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(10)
                    .ToListAsync();
                
                return Results.Ok(postIts);
            })
            .WithName("GetPostIts")
            .WithSummary("Get top 10 PostIt entries")
            .Produces<List<PostIt>>();

            // GET /postit/{id} - Get PostIt by ID
            group.MapGet("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var postIt = await db.PostIts.FindAsync(id);
                
                return postIt is not null 
                    ? Results.Ok(postIt) 
                    : Results.NotFound($"PostIt with ID {id} not found.");
            })
            .WithName("GetPostItById")
            .WithSummary("Get PostIt by ID")
            .Produces<PostIt>()
            .Produces(404);

            // POST /postit - Create new PostIt
            group.MapPost("/", async (PostIt postIt, ApplicationDbContext db) =>
            {
                postIt.CreatedAt = DateTime.UtcNow;
                db.PostIts.Add(postIt);
                await db.SaveChangesAsync();
                
                return Results.Created($"/postit/{postIt.Id}", postIt);
            })
            .WithName("CreatePostIt")
            .WithSummary("Create a new PostIt")
            .Accepts<PostIt>("application/json")
            .Produces<PostIt>(201);

            // PUT /postit/{id} - Update PostIt
            group.MapPut("/{id:int}", async (int id, PostIt updatedPostIt, ApplicationDbContext db) =>
            {
                var postIt = await db.PostIts.FindAsync(id);
                
                if (postIt is null)
                {
                    return Results.NotFound($"PostIt with ID {id} not found.");
                }
                
                postIt.Title = updatedPostIt.Title;
                postIt.Content = updatedPostIt.Content;
                postIt.IsCompleted = updatedPostIt.IsCompleted;
                postIt.UpdatedAt = DateTime.UtcNow;
                
                await db.SaveChangesAsync();
                
                return Results.Ok(postIt);
            })
            .WithName("UpdatePostIt")
            .WithSummary("Update PostIt by ID")
            .Accepts<PostIt>("application/json")
            .Produces<PostIt>()
            .Produces(404);

            // PATCH /postit/{id}/toggle - Toggle completed status
            group.MapPatch("/{id:int}/toggle", async (int id, ApplicationDbContext db) =>
            {
                var postIt = await db.PostIts.FindAsync(id);
                
                if (postIt is null)
                {
                    return Results.NotFound($"PostIt with ID {id} not found.");
                }
                
                postIt.IsCompleted = !postIt.IsCompleted;
                postIt.UpdatedAt = DateTime.UtcNow;
                
                await db.SaveChangesAsync();
                
                return Results.Ok(postIt);
            })
            .WithName("TogglePostItStatus")
            .WithSummary("Toggle PostIt completed status")
            .Produces<PostIt>()
            .Produces(404);

            // DELETE /postit/{id} - Delete PostIt
            group.MapDelete("/{id:int}", async (int id, ApplicationDbContext db) =>
            {
                var postIt = await db.PostIts.FindAsync(id);
                
                if (postIt is null)
                {
                    return Results.NotFound($"PostIt with ID {id} not found.");
                }
                
                db.PostIts.Remove(postIt);
                await db.SaveChangesAsync();
                
                return Results.NoContent();
            })
            .WithName("DeletePostIt")
            .WithSummary("Delete PostIt by ID")
            .Produces(204)
            .Produces(404);
        }
    }
}