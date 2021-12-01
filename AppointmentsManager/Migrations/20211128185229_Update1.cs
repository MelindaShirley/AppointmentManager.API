using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentsManager.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppointmentDetails",
                newName: "CustomerName");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "AppointmentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "AppointmentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "AppointmentDetails");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AppointmentDetails");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "AppointmentDetails",
                newName: "Name");
        }
    }
}
