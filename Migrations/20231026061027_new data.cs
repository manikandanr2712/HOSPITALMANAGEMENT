using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSPITALMANAGEMENT.Migrations
{
    /// <inheritdoc />
    public partial class newdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    DiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.DiseaseID);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorID);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_Patient_Disease",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DiseaseID = table.Column<int>(type: "int", nullable: false),
                    DoctorPatientDiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Patient_Disease", x => new { x.DoctorID, x.PatientID, x.DiseaseID });
                    table.ForeignKey(
                        name: "FK_Doctor_Patient_Disease_Diseases_DiseaseID",
                        column: x => x.DiseaseID,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Patient_Disease_Doctor_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctor",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Patient_Disease_DiseaseID",
                table: "Doctor_Patient_Disease",
                column: "DiseaseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_Patient_Disease");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Doctor");
        }
    }
}
