using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOSPITALMANAGEMENT.Model
{
    public class Doctor_Patient_Disease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorPatientDiseaseID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Doctor Doctor { get; set; } // Navigation property to Doctor
        public Disease Disease { get; set; } // Navigation property to Disease
    }
}
