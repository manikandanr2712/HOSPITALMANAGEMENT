using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSPITALMANAGEMENT.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookingappoinment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "BookedAppointment");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "BookedAppointment");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "BookedAppointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchDiseaseName",
                table: "BookedAppointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedDoctor",
                table: "BookedAppointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "BookedAppointment");

            migrationBuilder.DropColumn(
                name: "SearchDiseaseName",
                table: "BookedAppointment");

            migrationBuilder.DropColumn(
                name: "SelectedDoctor",
                table: "BookedAppointment");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "BookedAppointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "BookedAppointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
