using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllSitesAndTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SiteTypePricings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 35.00m);

            migrationBuilder.UpdateData(
                table: "SiteTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Standard back-in RV site", "Back-In" });

            migrationBuilder.InsertData(
                table: "SiteTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 2, "Standard pull-through RV site", "Pull-Through" },
                    { 3, "Dry camping site with no hook-ups", "Dry" },
                    { 4, "Tent-only camping site", "Tent" }
                });

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1,
                column: "SiteNumber",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2,
                column: "SiteNumber",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3,
                column: "SiteNumber",
                value: "3");

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "SiteNumber", "SiteTypeId" },
                values: new object[,]
                {
                    { 4, "4", 1 },
                    { 5, "5", 1 },
                    { 6, "6", 1 },
                    { 7, "7", 1 },
                    { 8, "8", 1 },
                    { 9, "9", 1 },
                    { 10, "10", 1 },
                    { 11, "11", 1 },
                    { 12, "11B", 1 },
                    { 13, "12", 1 },
                    { 14, "12B", 1 },
                    { 15, "13", 1 },
                    { 16, "14", 1 },
                    { 17, "15", 1 },
                    { 18, "17", 1 },
                    { 19, "18", 1 },
                    { 20, "19", 1 },
                    { 21, "20", 1 },
                    { 22, "21", 1 },
                    { 23, "22", 1 },
                    { 24, "23", 1 },
                    { 25, "24", 1 },
                    { 26, "25", 1 },
                    { 27, "26", 1 },
                    { 28, "27", 1 },
                    { 29, "28", 1 },
                    { 30, "29", 1 },
                    { 31, "30", 1 },
                    { 32, "31", 1 },
                    { 33, "C", 1 }
                });

            migrationBuilder.InsertData(
                table: "SiteTypePricings",
                columns: new[] { "Id", "EndDate", "Price", "SiteTypeId", "StartDate" },
                values: new object[,]
                {
                    { 2, null, 35.00m, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, 25.00m, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, 25.00m, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "SiteNumber", "SiteTypeId" },
                values: new object[,]
                {
                    { 34, "32", 2 },
                    { 35, "33", 2 },
                    { 36, "34", 2 },
                    { 37, "35", 2 },
                    { 38, "36", 2 },
                    { 39, "37", 2 },
                    { 40, "38", 2 },
                    { 41, "39", 2 },
                    { 42, "40", 2 },
                    { 43, "41", 2 },
                    { 44, "42", 2 },
                    { 45, "43", 2 },
                    { 46, "44", 2 },
                    { 47, "45", 2 },
                    { 48, "A", 2 },
                    { 49, "B", 2 },
                    { 50, "D-1", 3 },
                    { 51, "D-2", 3 },
                    { 52, "D-3", 3 },
                    { 53, "D-4", 3 },
                    { 54, "D-5", 3 },
                    { 55, "D-6", 3 },
                    { 56, "D-7", 3 },
                    { 57, "T-1", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteTypePricings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SiteTypePricings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SiteTypePricings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "SiteTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SiteTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SiteTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "SiteTypePricings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 50.00m);

            migrationBuilder.UpdateData(
                table: "SiteTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Site with all utility hookups", "Full Hookup" });

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1,
                column: "SiteNumber",
                value: "A1");

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2,
                column: "SiteNumber",
                value: "B1");

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3,
                column: "SiteNumber",
                value: "C1");
        }
    }
}
