using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class renamedSitePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SitesPhoto",
                table: "SitesPhoto");

            migrationBuilder.RenameTable(
                name: "SitesPhoto",
                newName: "SitePhotos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SitePhotos",
                table: "SitePhotos",
                column: "Id");

            migrationBuilder.InsertData(
                table: "SitePhotos",
                columns: new[] { "Id", "PhotoUrl", "SiteID" },
                values: new object[,]
                {
                    { 1, "/images/site1MapPhoto.png", 1 },
                    { 2, "/images/site1Photo1.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "SiteNumber", "SiteTypeId" },
                values: new object[] { 1, "A1", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SitePhotos",
                table: "SitePhotos");

            migrationBuilder.DeleteData(
                table: "SitePhotos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SitePhotos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "SitePhotos",
                newName: "SitesPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SitesPhoto",
                table: "SitesPhoto",
                column: "Id");
        }
    }
}
