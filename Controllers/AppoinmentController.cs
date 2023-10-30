using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly dbContext dbContext;

        public AppointmentController(dbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        public IActionResult CreatePatient(BookedAppointment patient)
        {
            var operationParam = new SqlParameter("@Operation", "Create");
            var patientIdParam = new SqlParameter("@Id", SqlDbType.Int);
            patientIdParam.Value = patient.Id;
            var patientNameParam = new SqlParameter("@PatientName", SqlDbType.NVarChar);
            patientNameParam.Value = patient.PatientName;

            // Format the appointment date as 'yyyy-MM-dd HH:mm:ss' and use SqlDbType.DateTime
            var formattedAppointmentDate = patient.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");
            var appointmentDateParam = new SqlParameter("@AppointmentDate", SqlDbType.DateTime);
            appointmentDateParam.Value = formattedAppointmentDate;

            var selectedDoctorParam = new SqlParameter("@SelectedDoctor", SqlDbType.NVarChar);
            selectedDoctorParam.Value = patient.SelectedDoctor;

            var searchDiseaseNameParam = new SqlParameter("@SearchDiseaseName", SqlDbType.NVarChar);
            searchDiseaseNameParam.Value = patient.SearchDiseaseName;   

            dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[ManagePatient] @Operation, @Id, @PatientName, @AppointmentDate, @SelectedDoctor, @SearchDiseaseName",
                operationParam, patientIdParam, patientNameParam, appointmentDateParam, selectedDoctorParam, searchDiseaseNameParam);

            return Ok(patient);
        }


        [HttpGet]
        public IActionResult GetPatients()
        {
            var operationParam = new SqlParameter("@Operation", "Read");
            var patients = dbContext.BookedAppointments.FromSqlRaw("EXEC ManagePatient @Operation", operationParam).ToList();
            return Ok(patients);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, BookedAppointment patient)
        {
            var operationParam = new SqlParameter("@Operation", "Update");
            var idParam = new SqlParameter("@Id", id);
            var patientNameParam = new SqlParameter("@PatientName", patient.PatientName);
            var appointmentDateParam = new SqlParameter("@AppointmentDate", patient.AppointmentDate);
            var selectedDoctorParam = new SqlParameter("@SelectedDoctor", patient.SelectedDoctor);
            var searchDiseaseNameParam = new SqlParameter("@SearchDiseaseName", patient.SearchDiseaseName);

            dbContext.Database.ExecuteSqlRaw("EXEC ManagePatient @Operation, @Id, @PatientName, @AppointmentDate, @SelectedDoctor, @SearchDiseaseName",
                operationParam, idParam, patientNameParam, appointmentDateParam, selectedDoctorParam, searchDiseaseNameParam);

            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var operationParam = new SqlParameter("@Operation", "Delete");
            var idParam = new SqlParameter("@Id", id);

            dbContext.Database.ExecuteSqlRaw("EXEC ManagePatient @Operation, @Id", operationParam, idParam);

            return NoContent();
        }
    }
}
