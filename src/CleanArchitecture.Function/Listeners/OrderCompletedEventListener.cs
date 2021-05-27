using System.Threading.Tasks;
using CleanArchitecture.Domain.AggregateRoots.Order.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanArchitecture.Function.Listeners
{
    public class OrderCompletedEventListener
    {
        [FunctionName("OrderCompletedEventListener")]
        public async Task Run([ServiceBusTrigger("order-completed-queue", Connection = "ServiceBusConnectionString")] string message, ILogger logger)
        {
            var domainEvent = JsonConvert.DeserializeObject<OrderCompletedEvent>(message);
        }
    }
}