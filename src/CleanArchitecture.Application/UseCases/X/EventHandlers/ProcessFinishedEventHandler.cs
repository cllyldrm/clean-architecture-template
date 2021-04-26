using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.UseCases.X.EventHandlers
{
    public class ProcessFinishedEventHandler : INotificationHandler<DomainEventNotification<ProcessFinishedEvent>>
    {
        private readonly IServiceBusRepository _serviceBusRepository;

        public ProcessFinishedEventHandler(IServiceBusRepository serviceBusRepository)
        {
            _serviceBusRepository = serviceBusRepository;
        }

        public async Task Handle(DomainEventNotification<ProcessFinishedEvent> notification,
            CancellationToken cancellationToken)
        {
            await _serviceBusRepository.Send(notification.DomainEvent, Constants.Exchanges.ProcessFinished);
        }
    }
}