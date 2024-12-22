using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Commands.Handlers
{
    public class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand>
    {
        private readonly ILogger<CreateAppointmentCommandHandler> _logger;
        private readonly IAppointmentCommandService _appointmentCommandService;

        public CreateAppointmentCommandHandler(ILogger<CreateAppointmentCommandHandler> logger,
            IAppointmentCommandService appointmentCommandService)
        {
            _logger = logger;
            _appointmentCommandService = appointmentCommandService;
        }

        public async Task Handle(CreateAppointmentCommand command)
        {
            _logger.LogInformation($"Handling CreateAppointmentCommand started command: {JsonConvert.SerializeObject(command)}");

            try
            {
                await _appointmentCommandService.CreateAppointment(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handling CreateAppointmentCommand failed command: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"Handling CreateAppointmentCommand finished command: {JsonConvert.SerializeObject(command)}");
        }
    }
}
