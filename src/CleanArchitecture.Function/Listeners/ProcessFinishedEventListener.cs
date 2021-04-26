using System.Threading.Tasks;
using CleanArchitecture.Domain.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanArchitecture.Function.Listeners
{
    public class ProcessFinishedEventListener
    {
        [FunctionName("ProcessFinishedEventListener")]
        public async Task Run(
            [ServiceBusTrigger("process-finished", Connection = "ServiceBusConnectionString")]
            string message, ILogger logger)
        {
            var domainEvent = JsonConvert.DeserializeObject<ProcessFinishedEvent>(message);
        }
    }
}