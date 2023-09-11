using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyRoom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class In : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a62b2d8-f81a-4fa2-9849-e9ca354ae770"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b0eb91e1-32a7-492d-872c-9e351ed02892"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0ccfad34-19bf-4a89-8e55-ee4f288ec519"), null, "User", "USER" },
                    { new Guid("6672bb2e-aaf3-4bb6-b85b-08e3d95676b3"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0ccfad34-19bf-4a89-8e55-ee4f288ec519"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6672bb2e-aaf3-4bb6-b85b-08e3d95676b3"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a62b2d8-f81a-4fa2-9849-e9ca354ae770"), null, "Admin", "ADMIN" },
                    { new Guid("b0eb91e1-32a7-492d-872c-9e351ed02892"), null, "User", "USER" }
                });
        }
    }
}
