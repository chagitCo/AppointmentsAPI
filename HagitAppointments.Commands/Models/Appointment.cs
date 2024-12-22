namespace HagitAppointments.Commands.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid GovernmentOfficeId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
