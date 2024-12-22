using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HagitAppointmentsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IQueryHandler<GetAppointmentsQuery, List<QueryAppointment>> _GetQueryHandler;
        private readonly ICommandHandler<CreateAppointmentCommand> _createCommandHandler;
        private readonly ICommandHandler<UpdateAppointmentCommand> _updateCommandHandler;
        private readonly ICommandHandler<DeleteAppointmentCommand> _deleteCommandHandler;

        public AppointmentsController(ILogger<AppointmentsController> logger,
            IQueryHandler<GetAppointmentsQuery, List<QueryAppointment>> GetQueryHandler,
            ICommandHandler<CreateAppointmentCommand> createCommandHandler,
            ICommandHandler<UpdateAppointmentCommand> updateCommandHandler,
            ICommandHandler<DeleteAppointmentCommand> deleteCommandHandler)
        {
            _logger = logger;
            _GetQueryHandler = GetQueryHandler;
            _createCommandHandler = createCommandHandler;
            _updateCommandHandler = updateCommandHandler;
            _deleteCommandHandler = deleteCommandHandler;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid userId)
        {
            _logger.LogInformation($"AppointmentsController => GetAll started with userId: {userId}");

            try
            {
                GetAppointmentsQuery getAppointmentsQuery = new GetAppointmentsQuery() { UserId = userId};
                List<QueryAppointment> appointments = await _GetQueryHandler.Handle(getAppointmentsQuery);
                _logger.LogInformation($"AppointmentsController => GetAll finished with userId: {userId}");

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentsController => GetAll failed for userId: {userId}. Error: {ex.Message}");
                return Problem($"AppointmentsController => GetAll failed for userId: {userId}");
            }
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand createCommand)
        {
            _logger.LogInformation($"AppointmentsController => Create started with CreateAppointmentCommand: {JsonConvert.SerializeObject(createCommand)}");

            try
            {
                await _createCommandHandler.Handle(createCommand);
                _logger.LogInformation($"AppointmentsController => Create finished for CreateAppointmentCommand: {JsonConvert.SerializeObject(createCommand)}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentsController => Create failed for CreateAppointmentCommand: {JsonConvert.SerializeObject(createCommand)}. Error: {ex.Message}");
                return Problem($"AppointmentsController => Create failed for CreateAppointmentCommand: {JsonConvert.SerializeObject(createCommand)}");
            }
        }

        [HttpPost(Name = "Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentCommand updateCommand)
        {
            _logger.LogInformation($"AppointmentsController => Update started with UpdateAppointmentCommand: {JsonConvert.SerializeObject(updateCommand)}");

            try
            {
                await _updateCommandHandler.Handle(updateCommand);
                _logger.LogInformation($"AppointmentsController => Update finished for UpdateAppointmentCommand: {JsonConvert.SerializeObject(updateCommand)}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentsController => Update failed for UpdateAppointmentCommand: {JsonConvert.SerializeObject(updateCommand)}. Error: {ex.Message}");
                return Problem($"AppointmentsController => Update failed for UpdateAppointmentCommand: {JsonConvert.SerializeObject(updateCommand)}");
            }
        }

        [HttpPost(Name = "Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAppointmentCommand deleteCommand)
        {
            _logger.LogInformation($"AppointmentsController => Delete started with DeleteAppointmentCommand: {JsonConvert.SerializeObject(deleteCommand)}");

            try
            {
                await _deleteCommandHandler.Handle(deleteCommand);
                _logger.LogInformation($"AppointmentsController => Delete finished for DeleteAppointmentCommand: {JsonConvert.SerializeObject(deleteCommand)}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AppointmentsController => Delete failed for DeleteAppointmentCommand: {JsonConvert.SerializeObject(deleteCommand)}. Error: {ex.Message}");
                return Problem($"AppointmentsController => Delete failed for DeleteAppointmentCommand: {JsonConvert.SerializeObject(deleteCommand)}");
            }
        }
    }
}
