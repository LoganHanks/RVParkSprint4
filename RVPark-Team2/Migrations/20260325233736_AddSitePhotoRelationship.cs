using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class AddSitePhotoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteID",
                table: "SitePhotos",
                newName: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SitePhotos_SiteId",
                table: "SitePhotos",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_SitePhotos_Sites_SiteId",
                table: "SitePhotos",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitePhotos_Sites_SiteId",
                table: "SitePhotos");

            migrationBuilder.DropIndex(
                name: "IX_SitePhotos_SiteId",
                table: "SitePhotos");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "SitePhotos",
                newName: "SiteID");
        }
    }
}
