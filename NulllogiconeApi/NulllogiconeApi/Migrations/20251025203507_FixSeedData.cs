using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NulllogiconeApi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PostIts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "PostIts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "TopLabs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "TopLabs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PostIts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "PostIts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(4790));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(5298));

            migrationBuilder.UpdateData(
                table: "Stamms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 122, DateTimeKind.Utc).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "TopLabs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "TopLabs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 33, 25, 124, DateTimeKind.Utc).AddTicks(3619));
        }
    }
}
