using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class ServiceBusRepository : IServiceBusRepository
    {
        private readonly ServiceBusClient _serviceBusClient;

        public ServiceBusRepository(string connectionString)
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
        }

        public async Task Send<TMessage>(TMessage message, string queueName)
        {
            var serviceBusSender = _serviceBusClient.CreateSender(queueName);

            var serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(message))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}