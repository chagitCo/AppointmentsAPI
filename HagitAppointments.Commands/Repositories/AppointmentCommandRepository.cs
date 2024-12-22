using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace HagitAppointments.Commands.Repositories
{
    public class AppointmentCommandRepository : IAppointmentCommandRepository
    {
        private readonly ILogger<AppointmentCommandRepository> _logger;
        private readonly IMongoCollection<Appointment> _appointments;

        public AppointmentCommandRepository(ILogger<AppointmentCommandRepository> logger,
            [FromKeyedServices("Command")] IMongoClient client)
        {
            _logger = logger;
            var database = client.GetDatabase("AppointmentsDb");
            _appointments = database.GetCollection<Appointment>("Appointments");
        }

        public async Task<Appointment> Get(Guid id)
        {
            _logger.LogInformation($"AppointmentCommandRepository => Get started with id: {id}");

            try
            {
                Appointment appointment = await _appointments.Find(a => a.Id == id).FirstOrDefaultAsync();

                _logger.LogInformation($"AppointmentCommandRepository => Get finished for id: {id}, appointment: {JsonConvert.SerializeObject(appointment)}");

                return appointment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentCommandRepository => Get failed for id: {id}. Error: {ex.Message}");
                throw;
            }
        }

        public async Task Create(Appointment appointment)
        {
            _logger.LogInformation($"AppointmentCommandRepository => Create started with appointment: {JsonConvert.SerializeObject(appointment)}");

            try
            {
                await _appointments.InsertOneAsync(appointment);

                _logger.LogInformation($"AppointmentCommandRepository => Create finished for appointment: {JsonConvert.SerializeObject(appointment)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentCommandRepository => Create failed for for appointment: {JsonConvert.SerializeObject(appointment)}. Error: {ex.Message}");
                throw;
            }
        }

        public async Task Update(Appointment appointment)
        {
            _logger.LogInformation($"AppointmentCommandRepository => Update started with appointment: {JsonConvert.SerializeObject(appointment)}");

            try
            {
                await _appointments.ReplaceOneAsync(a => a.Id == appointment.Id, appointment);

                _logger.LogInformation($"AppointmentCommandRepository => Update finished for appointment: {JsonConvert.SerializeObject(appointment)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentCommandRepository => Update failed for appointment: {JsonConvert.SerializeObject(appointment)}. Error: {ex.Message}");
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            _logger.LogInformation($"AppointmentCommandRepository => Delete started with id: {id}");

            try
            {
                await _appointments.DeleteOneAsync(a => a.Id == id);

                _logger.LogInformation($"AppointmentCommandRepository => Delete finished for id: {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentCommandRepository => Delete failed for id: {id}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
