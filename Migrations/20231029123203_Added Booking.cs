using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSPITALMANAGEMENT.Migrations
{
    /// <inheritdoc />
    public partial class AddedBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedAppointment",
                table: "BookedAppointment");

            migrationBuilder.RenameTable(
                name: "BookedAppointment",
                newName: "BookedAppointments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedAppointments",
                table: "BookedAppointments",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedAppointments",
                table: "BookedAppointments");

            migrationBuilder.RenameTable(
                name: "BookedAppointments",
                newName: "BookedAppointment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedAppointment",
                table: "BookedAppointment",
                column: "Id");
        }
    }
}
