using System;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Seedwork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(AggregateRoot aggregateRoot)
        {
            foreach (var domainEvent in aggregateRoot.DomainEvents.Where(_ => !_.IsPublished))
            {
                _logger.LogInformation($"Publishing domain event. Event - {domainEvent.GetType().Name}");
                domainEvent.IsPublished = true;
                await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
            }
        }

        private static INotification GetNotificationCorrespondingToDomainEvent(object domainEvent)
        {
            return (INotification) Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}