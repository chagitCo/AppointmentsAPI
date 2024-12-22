using HagitAppointments.Commands.Models;

namespace HagitAppointments.Commands.Interfaces
{
    public interface IAppointmentCommandRepository
    {
        Task<Appointment> Get(Guid id);
        Task Create(Appointment appointment);
        Task Update(Appointment appointment);
        Task Delete(Guid id);
    }
}
