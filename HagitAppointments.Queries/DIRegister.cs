using HagitAppointments.Queries.Handlers;
using HagitAppointments.Queries.Interfaces;
using HagitAppointments.Queries.Models;
using HagitAppointments.Queries.Repositories;
using HagitAppointments.Queries.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HagitAppointments.Queries
{
    public class DIRegister
    {
        public static void RegisterQueries(IServiceCollection services, IConfiguration configuration)
        {
            services.AddKeyedSingleton<IMongoClient>(
                "Query",
                (prov, key) =>
                {
                    return new MongoClient(configuration.GetConnectionString("QueryMongoDb"));
                });

            services.AddTransient<IQueryHandler<GetAppointmentsQuery, List<QueryAppointment>>, GetAppointmentsQueryHandler>();
            services.AddTransient<IAppointmentQueryRepository, AppointmentQueryRepository>();
            services.AddTransient<IAppointmentQueryService, AppointmentQueryService>();
        }
    }
}
