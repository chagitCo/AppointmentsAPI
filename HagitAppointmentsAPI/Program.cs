using HagitAppointments.Commands.Handlers;
using HagitAppointments.Commands.Interfaces;
using HagitAppointments.Commands.Models;
using HagitAppointments.Commands.Repositories;
using HagitAppointments.Commands.Services;
using HagitAppointments.Queries.Handlers;
using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using HagitAppointments.Queries.Repositories;
using HagitAppointments.Queries.Services;
using MongoDB.Driver;

namespace HagitAppointmentsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.RegisterQueries();
            builder.Services.AddKeyedSingleton<IMongoClient>(
                "Query",
                (prov, key) =>
                {
                    return new MongoClient(configuration.GetConnectionString("QueryMongoDb"));
                });

            builder.Services.AddTransient<IQueryHandler<GetAppointmentsQuery, List<QueryAppointment>>, GetAppointmentsQueryHandler>();
            builder.Services.AddTransient<IAppointmentQueryRepository, AppointmentQueryRepository>();
            builder.Services.AddTransient<IAppointmentQueryService, AppointmentQueryService>();

            //builder.Services.RegisterCommands();
            builder.Services.AddKeyedSingleton<IMongoClient>(
                "Command",
                (prov, key) =>
                {
                    return new MongoClient(configuration.GetConnectionString("CommandMongoDb"));
                });

            builder.Services.AddTransient<ICommandHandler<CreateAppointmentCommand>, CreateAppointmentCommandHandler>();
            builder.Services.AddTransient<ICommandHandler<DeleteAppointmentCommand>, DeleteAppointmentCommandHandler>();
            builder.Services.AddTransient<ICommandHandler<UpdateAppointmentCommand>, UpdateAppointmentCommandHandler>();
            builder.Services.AddTransient<IAppointmentCommandRepository, AppointmentCommandRepository>();
            builder.Services.AddTransient<IAppointmentCommandService, AppointmentCommandService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
