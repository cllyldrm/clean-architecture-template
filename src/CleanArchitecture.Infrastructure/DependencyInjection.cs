using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Domain.AggregateRoots.Order;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Repositories.Order;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry();
            services.Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)));
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            var serviceBus = new ServiceBusService(configuration.GetSection("ServiceBusConnectionString").Value);
            services.AddSingleton<IServiceBusService>(serviceBus);
            return services;
        }
    }
}