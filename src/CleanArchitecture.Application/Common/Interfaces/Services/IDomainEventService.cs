using System.Threading.Tasks;
using CleanArchitecture.Domain.Seedwork;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IDomainEventService
    {
        Task Publish(AggregateRoot aggregateRoot);
    }
}