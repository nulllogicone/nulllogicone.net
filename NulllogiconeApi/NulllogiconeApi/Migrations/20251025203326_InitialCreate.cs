using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NulllogiconeApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostIts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostIts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stamms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stamms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopLabs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLabs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PostIts",
                columns: new[] { "Id", "Content", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Complete the API documentation", new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(58), "Remember to...", null },
                    { 2, "Discuss new features with the team", new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(467), "Meeting notes", null }
                });

            migrationBuilder.InsertData(
                table: "Stamms",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(4790), "First stamm entry", "Stamm Alpha", null },
                    { 2, new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(5298), "Second stamm entry", "Stamm Beta", null },
                    { 3, new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(5303), "Third stamm entry", "Stamm Gamma", null }
                });

            migrationBuilder.InsertData(
                table: "TopLabs",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "Priority", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Research", new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(3162), "Initial research phase", "Lab Experiment 1", 1, null },
                    { 2, "Development", new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(3619), "Development phase", "Lab Experiment 2", 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostIts");

            migrationBuilder.DropTable(
                name: "Stamms");

            migrationBuilder.DropTable(
                name: "TopLabs");
        }
    }
}
