using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSPITALMANAGEMENT.Migrations
{
    /// <inheritdoc />
    public partial class Appoinmentapiadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookedAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedAppointment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedAppointment");
        }
    }
}
