using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Queries.Services
{
    public class AppointmentQueryService : IAppointmentQueryService
    {
        private readonly ILogger<AppointmentQueryService> _logger;
        private readonly IAppointmentQueryRepository _appointmentRepository;

        public AppointmentQueryService(ILogger<AppointmentQueryService> logger,
            IAppointmentQueryRepository appointmentRepository)
        {
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<QueryAppointment>> GetAllAppointmentsByUserId(GetAppointmentsQuery query)
        {
            _logger.LogInformation($"AppointmentQueryService => GetAllAppointmentsByUserId started with GetAppointmentsQuery: {JsonConvert.SerializeObject(query)}");

            try
            {
                List<QueryAppointment> appointments = await _appointmentRepository.GetAll(query.UserId);
                _logger.LogInformation($"AppointmentQueryService => GetAllAppointmentsByUserId finished for GetAppointmentsQuery: {JsonConvert.SerializeObject(query)}.)");
                return appointments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentQueryService => GetAllAppointmentsByUserId failed for GetAppointmentsQuery: {JsonConvert.SerializeObject(query)}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
