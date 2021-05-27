using CleanArchitecture.Domain.Common.Models;

namespace CleanArchitecture.Domain.AggregateRoots.Order.Events
{
    public class OrderCompletedEvent : DomainEvent
    {
        public int Id { get; }

        public OrderCompletedEvent(int id)
        {
            Id = id;
        }
    }
}