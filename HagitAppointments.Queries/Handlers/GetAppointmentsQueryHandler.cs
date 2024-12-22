using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Queries.Handlers
{
    public class GetAppointmentsQueryHandler : IQueryHandler<GetAppointmentsQuery, List<QueryAppointment>>
    {
        private readonly ILogger<GetAppointmentsQueryHandler> _logger;
        private readonly IAppointmentQueryService _appointmentService;

        public GetAppointmentsQueryHandler(ILogger<GetAppointmentsQueryHandler> logger,
            IAppointmentQueryService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        public async Task<List<QueryAppointment>> Handle(GetAppointmentsQuery query)
        {
            _logger.LogInformation($"Handling GetAppointmentsQuery started query: {JsonConvert.SerializeObject(query)}");

            try
            {
                List<QueryAppointment> appointments = await _appointmentService.GetAllAppointmentsByUserId(query);
                _logger.LogInformation($"Handling GetAppointmentsQuery finished query: {JsonConvert.SerializeObject(query)}");
                return appointments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handling GetAppointmentsQuery failed query: {JsonConvert.SerializeObject(query)}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
