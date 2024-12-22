using HagitAppointments.Commands.Models;

namespace HagitAppointments.Commands.Interfaces
{
    public interface IAppointmentCommandService
    {
        Task CreateAppointment(CreateAppointmentCommand command);
        Task UpdateAppointment(UpdateAppointmentCommand command);
        Task DeleteAppointment(DeleteAppointmentCommand command);
    }
}
