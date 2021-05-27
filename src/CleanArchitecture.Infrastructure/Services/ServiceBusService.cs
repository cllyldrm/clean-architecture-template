using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using CleanArchitecture.Application.Common.Interfaces.Services;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public ServiceBusService(string connectionString)
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