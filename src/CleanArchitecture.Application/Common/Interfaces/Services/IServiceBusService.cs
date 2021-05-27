using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IServiceBusService
    {
        Task Send<TMessage>(TMessage message, string queueName);
    }
}