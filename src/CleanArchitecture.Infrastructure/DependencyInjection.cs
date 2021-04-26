using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Domain.AggregateRoots;
using CleanArchitecture.Infrastructure.Repository;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddScoped<IDomainEventService, DomainEventService>();

            var serviceBus = new ServiceBusRepository(configuration.GetSection("ServiceBusConnectionString").Value);
            services.AddSingleton<IServiceBusRepository>(serviceBus);

            services.AddSingleton(
                InitializeRepository(configuration.GetSection("CosmosDb")).ConfigureAwait(false).GetAwaiter()
                    .GetResult());
            return services;
        }

        private static async Task<IRepository<XAggregateRoot>> InitializeRepository(IConfiguration configuration)
        {
            var databaseName = configuration.GetSection("DatabaseName").Value;
            var containerName = configuration.GetSection("ContainerName").Value;
            var account = configuration.GetSection("Account").Value;
            var key = configuration.GetSection("Key").Value;
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var cosmosDbService = new Repository<XAggregateRoot>(client, databaseName, containerName);
            var
                database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosDbService;
        }
    }
}