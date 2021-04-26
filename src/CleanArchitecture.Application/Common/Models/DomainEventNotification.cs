using MediatR;

namespace CleanArchitecture.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}