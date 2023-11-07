namespace HOSPITALMANAGEMENT.Model
{
    public class BookedAppointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string SelectedDoctor { get; set; }
        public string SearchDiseaseName { get; set; }
    }
}
