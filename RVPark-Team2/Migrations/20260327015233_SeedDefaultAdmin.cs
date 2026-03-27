using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccessLevel", "Email", "FirstName", "IsLocked", "LastName", "PasswordHash" },
                values: new object[] { 1, "Admin", "admin@admin.com", "Admin", false, "User", "$2a$11$iLPVgQQZV5f3NI1gSGTKwuHrvT4iHnPVavbFn6f5AvGLj9eZBwrQi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
