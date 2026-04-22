using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CustomerEmail", "CustomerName", "EndDate", "IsCancelled", "NumberOfAdults", "Pets", "SiteId", "SpecialRequests", "StartDate", "TotalPrice", "VehicleLength" },
                values: new object[,]
                {
                    { 6, "customer@customer.com", "Customer Customer", new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, false, 5, null, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 105m, null },
                    { 7, "customer@customer.com", "Customer Customer", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, false, 34, null, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 175m, null },
                    { 8, "customer@customer.com", "Customer Customer", new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0, false, 50, null, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 75m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
