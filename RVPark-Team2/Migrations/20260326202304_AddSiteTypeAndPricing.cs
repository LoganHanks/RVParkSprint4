using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteTypeAndPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteTypePricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTypePricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteTypePricings_SiteTypes_SiteTypeId",
                        column: x => x.SiteTypeId,
                        principalTable: "SiteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SiteTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Site with all utility hookups", "Full Hookup" });

            migrationBuilder.InsertData(
                table: "SiteTypePricings",
                columns: new[] { "Id", "EndDate", "Price", "SiteTypeId", "StartDate" },
                values: new object[] { 1, null, 50.00m, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Sites_SiteTypeId",
                table: "Sites",
                column: "SiteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteTypePricings_SiteTypeId",
                table: "SiteTypePricings",
                column: "SiteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_SiteTypes_SiteTypeId",
                table: "Sites",
                column: "SiteTypeId",
                principalTable: "SiteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sites_SiteTypes_SiteTypeId",
                table: "Sites");

            migrationBuilder.DropTable(
                name: "SiteTypePricings");

            migrationBuilder.DropTable(
                name: "SiteTypes");

            migrationBuilder.DropIndex(
                name: "IX_Sites_SiteTypeId",
                table: "Sites");
        }
    }
}
