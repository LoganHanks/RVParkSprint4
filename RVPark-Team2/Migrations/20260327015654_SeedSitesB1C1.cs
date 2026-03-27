using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedSitesB1C1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Sites] ON;
                IF NOT EXISTS (SELECT 1 FROM [Sites] WHERE [Id] = 2)
                    INSERT INTO [Sites] ([Id], [SiteNumber], [SiteTypeId]) VALUES (2, N'B1', 1);
                IF NOT EXISTS (SELECT 1 FROM [Sites] WHERE [Id] = 3)
                    INSERT INTO [Sites] ([Id], [SiteNumber], [SiteTypeId]) VALUES (3, N'C1', 1);
                SET IDENTITY_INSERT [Sites] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
