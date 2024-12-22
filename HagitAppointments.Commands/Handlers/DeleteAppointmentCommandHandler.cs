using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Commands.Handlers
{
    public class DeleteAppointmentCommandHandler : ICommandHandler<DeleteAppointmentCommand>
    {
        private readonly ILogger<DeleteAppointmentCommandHandler> _logger;
        private readonly IAppointmentCommandService _appointmentCommandService;

        public DeleteAppointmentCommandHandler(ILogger<DeleteAppointmentCommandHandler> logger,
            IAppointmentCommandService appointmentCommandService)
        {
            _logger = logger;
            _appointmentCommandService = appointmentCommandService;
        }

        public async Task Handle(DeleteAppointmentCommand command)
        {
            _logger.LogInformation($"Handling DeleteAppointmentCommand started command: {JsonConvert.SerializeObject(command)}");

            try
            {
                await _appointmentCommandService.DeleteAppointment(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handling DeleteAppointmentCommand failed command: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"Handling DeleteAppointmentCommand finished command: {JsonConvert.SerializeObject(command)}");
        }
    }
}
