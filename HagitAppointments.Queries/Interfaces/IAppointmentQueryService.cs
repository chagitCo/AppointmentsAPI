using HagitAppointments.Queries.Models;

namespace HagitAppointments.Queries.Interfaces
{
    public interface IAppointmentQueryService
    {
        Task<List<QueryAppointment>> GetAllAppointmentsByUserId(GetAppointmentsQuery query);
    }
}
