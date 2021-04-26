using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface IServiceBusRepository
    {
        Task Send<TMessage>(TMessage message, string queueName);
    }
}