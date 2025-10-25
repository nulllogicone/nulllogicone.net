using Microsoft.EntityFrameworkCore;
using NulllogiconeApi.Models;

namespace NulllogiconeApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stamm> Stamms { get; set; }
        public DbSet<PostIt> PostIts { get; set; }
        public DbSet<TopLab> TopLabs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Stamm entity
            modelBuilder.Entity<Stamm>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure PostIt entity
            modelBuilder.Entity<PostIt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            });

            // Configure TopLab entity
            modelBuilder.Entity<TopLab>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Priority).HasDefaultValue(1);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Seed some initial data with static dates
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            modelBuilder.Entity<Stamm>().HasData(
                new Stamm { Id = 1, Name = "Stamm Alpha", Description = "First stamm entry", CreatedAt = seedDate },
                new Stamm { Id = 2, Name = "Stamm Beta", Description = "Second stamm entry", CreatedAt = seedDate.AddDays(1) },
                new Stamm { Id = 3, Name = "Stamm Gamma", Description = "Third stamm entry", CreatedAt = seedDate.AddDays(2) }
            );

            modelBuilder.Entity<PostIt>().HasData(
                new PostIt { Id = 1, Title = "Remember to...", Content = "Complete the API documentation", CreatedAt = seedDate },
                new PostIt { Id = 2, Title = "Meeting notes", Content = "Discuss new features with the team", CreatedAt = seedDate.AddDays(1) }
            );

            modelBuilder.Entity<TopLab>().HasData(
                new TopLab { Id = 1, Name = "Lab Experiment 1", Description = "Initial research phase", Category = "Research", Priority = 1, CreatedAt = seedDate },
                new TopLab { Id = 2, Name = "Lab Experiment 2", Description = "Development phase", Category = "Development", Priority = 2, CreatedAt = seedDate.AddDays(1) }
            );
        }
    }
}