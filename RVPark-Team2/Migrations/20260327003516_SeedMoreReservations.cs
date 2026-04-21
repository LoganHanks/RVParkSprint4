using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerEmail", "CustomerName" },
                values: new object[] { "", "John Doe" });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CustomerEmail", "CustomerName", "EndDate", "IsCancelled", "SiteId", "StartDate", "TotalPrice" },
                values: new object[,]
                {
                    { 2, "", "Jane Smith", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { 3, "", "Mike Johnson", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 4, "", "Emily Davis", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { 5, "", "Chris Brown", new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerEmail", "CustomerName" },
                values: new object[] { "testuser@email.com", "Test User" });
        }
    }
}
