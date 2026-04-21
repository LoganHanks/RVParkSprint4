using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVPark_Team2.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerReservationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Pets",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequests",
                table: "Reservations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleLength",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NumberOfAdults", "Pets", "SpecialRequests", "VehicleLength" },
                values: new object[] { 0, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Pets",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "SpecialRequests",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "VehicleLength",
                table: "Reservations");
        }
    }
}
