using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentsManager.Migrations
{
    public partial class UpdateperAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppointmentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "AppointmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppointmentDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AppointmentDetails");
        }
    }
}
