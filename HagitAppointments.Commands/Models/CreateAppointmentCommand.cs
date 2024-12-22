namespace HagitAppointments.Commands.Models
{
    public class CreateAppointmentCommand
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid GovernmentOfficeId { get; set; }
        public Guid BranchId { get; set; }
    }
}
