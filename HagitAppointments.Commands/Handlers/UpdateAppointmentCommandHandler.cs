using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Commands.Handlers
{
    public class UpdateAppointmentCommandHandler : ICommandHandler<UpdateAppointmentCommand>
    {
        private readonly ILogger<UpdateAppointmentCommandHandler> _logger;
        private readonly IAppointmentCommandService _appointmentCommandService;

        public UpdateAppointmentCommandHandler(ILogger<UpdateAppointmentCommandHandler> logger,
            IAppointmentCommandService appointmentCommandService)
        {
            _logger = logger;
            _appointmentCommandService = appointmentCommandService;
        }

        public async Task Handle(UpdateAppointmentCommand command)
        {
            _logger.LogInformation($"Handling UpdateAppointmentCommand started command: {JsonConvert.SerializeObject(command)}");

            try
            {
                await _appointmentCommandService.UpdateAppointment(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handling UpdateAppointmentCommand failed command: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"Handling UpdateAppointmentCommand finished command: {JsonConvert.SerializeObject(command)}");
        }
    }
}
