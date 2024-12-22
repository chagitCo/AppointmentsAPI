using HagitAppointments.Commands.Handlers;
using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using HagitAppointments.Commands.Repositories;
using HagitAppointments.Commands.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HagitAppointments.Commands
{
    public static class DIRegister
    {
        public static void RegisterCommands(IServiceCollection services, IConfiguration configuration)
        {
            services.AddKeyedSingleton<IMongoClient>(
                "Command",
                (prov, key) =>
                {
                    return new MongoClient(configuration.GetConnectionString("CommandMongoDb"));
                });

            services.AddTransient<ICommandHandler<CreateAppointmentCommand>, CreateAppointmentCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteAppointmentCommand>, DeleteAppointmentCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateAppointmentCommand>, UpdateAppointmentCommandHandler>();
            services.AddTransient<IAppointmentCommandRepository, AppointmentCommandRepository>();
            services.AddTransient<IAppointmentCommandService, AppointmentCommandService>();
        }
    }
}
