using System.Threading.Tasks;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IDomainEventService
    {
        Task Publish(AggregateRoot aggregateRoot);
    }
}