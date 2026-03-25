using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CustomerName", "EndDate", "IsCancelled", "SiteId", "StartDate", "TotalPrice" },
                values: new object[] { 1, "Test User", new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
