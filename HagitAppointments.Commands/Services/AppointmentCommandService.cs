using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HagitAppointments.Commands.Services
{
    public class AppointmentCommandService : IAppointmentCommandService
    {
        private readonly ILogger<AppointmentCommandService> _logger;
        private readonly IAppointmentCommandRepository _appointmentRepository;

        public AppointmentCommandService(ILogger<AppointmentCommandService> logger,
            IAppointmentCommandRepository appointmentRepository)
        {
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task CreateAppointment(CreateAppointmentCommand command)
        {
            _logger.LogInformation($"AppointmentService => CreateAppointment started with CreateAppointmentCommand: {JsonConvert.SerializeObject(command)}");

            try
            {
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Date = command.Date,
                    Description = command.Description,
                    GovernmentOfficeId = command.GovernmentOfficeId,
                    BranchId = command.BranchId,
                    CreatedDate = DateTime.Now,
                    LastModified = DateTime.Now
                };

                await _appointmentRepository.Create(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentService => CreateAppointment failed for CreateAppointmentCommand: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"AppointmentService => CreateAppointment finished for CreateAppointmentCommand: {JsonConvert.SerializeObject(command)}");
        }

        public async Task UpdateAppointment(UpdateAppointmentCommand command)
        {
            _logger.LogInformation($"AppointmentService => UpdateAppointment started with UpdateAppointmentCommand: {JsonConvert.SerializeObject(command)}");

            try
            {
                var appointment = await _appointmentRepository.Get(command.Id);

                if (appointment == null)
                {
                    throw new Exception("Appointment not found");
                }

                appointment.Date = command.Date;
                appointment.Description = command.Description;
                appointment.LastModified = DateTime.Now;

                await _appointmentRepository.Update(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentService => UpdateAppointment failed for UpdateAppointmentCommand: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"AppointmentService => UpdateAppointment finished for UpdateAppointmentCommand: {JsonConvert.SerializeObject(command)}");
        }

        public async Task DeleteAppointment(DeleteAppointmentCommand command)
        {
            _logger.LogInformation($"AppointmentService => DeleteAppointment started with DeleteAppointmentCommand: {JsonConvert.SerializeObject(command)}");

            try
            {
                var appointment = await _appointmentRepository.Get(command.Id);

                if (appointment == null)
                {
                    throw new Exception("Appointment not found");
                }

                await _appointmentRepository.Delete(appointment.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentService => DeleteAppointment failed for DeleteAppointmentCommand: {JsonConvert.SerializeObject(command)}. Error: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"AppointmentService => DeleteAppointment finished for DeleteAppointmentCommand: {JsonConvert.SerializeObject(command)}");
        }
    }
}
