using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace HagitAppointments.Queries.Repositories
{
    public class AppointmentQueryRepository : IAppointmentQueryRepository
    {
        private readonly ILogger<AppointmentQueryRepository> _logger;
        private readonly IMongoCollection<QueryAppointment> _appointments;

        public AppointmentQueryRepository(ILogger<AppointmentQueryRepository> logger,
            [FromKeyedServices("Query")] IMongoClient client)
        {
            _logger = logger;
            var database = client.GetDatabase("AppointmentsDb");
            _appointments = database.GetCollection<QueryAppointment>("Appointments");
        }

        public async Task<List<QueryAppointment>> GetAll(Guid userId)
        {
            _logger.LogInformation($"AppointmentCommandRepository => GetAll started with userId: {userId}");

            try
            {
                List<QueryAppointment> appointments = await Task.FromResult((List<QueryAppointment>)_appointments.Find(a => a.UserId == userId).ToList());

                _logger.LogInformation($"AppointmentCommandRepository => GetAll finished for userId: {userId}, appointments: {JsonConvert.SerializeObject(appointments)}");

                return appointments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentCommandRepository => GetAll failed for userId: {userId}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
