using HagitAppointments.Queries.Models;

namespace HagitAppointments.Queries.Interfaces
{
    public interface IAppointmentQueryRepository
    {
        Task<List<QueryAppointment>> GetAll(Guid userId);
    }
}
