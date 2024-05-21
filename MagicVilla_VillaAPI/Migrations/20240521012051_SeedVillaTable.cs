using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "id", "UpdateDate", "amenity", "createDate", "details", "imageUrl", "name", "occupancy", "rate", "sqft" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Villa xin xo", "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Royal Villa", 1, 200.0, 200 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Villa nha giao", "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Luxury Villa", 2, 100.0, 100 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Villa standard", "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Normal Villa", 3, 300.0, 300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
