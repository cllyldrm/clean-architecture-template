using CleanArchitecture.Domain.Common.Models;

namespace CleanArchitecture.Domain.AggregateRoots.Order.Events
{
    public class OrderCreatedEvent : DomainEvent
    {
        public int Id { get; }

        public OrderCreatedEvent(int id)
        {
            Id = id;
        }
    }
}