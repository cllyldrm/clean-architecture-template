using CleanArchitecture.Domain.AggregateRoots.Order.Events;
using CleanArchitecture.Domain.AggregateRoots.Order.Exceptions;
using CleanArchitecture.Domain.Common.Models;

namespace CleanArchitecture.Domain.AggregateRoots.Order
{
    public class OrderEntity : AggregateRoot
    {
        public OrderEntity(string name, OrderStatus status)
        {
            Name = name;
            Status = status;
        }

        private OrderEntity()
        {
        }

        public int? Id { get; private set; }
        public string Name { get; }
        public OrderStatus Status { get; private set; }

        public void SetId(int id)
        {
            if (Id != null)
            {
                return;
            }

            Id = id;
            DomainEvents.Add(new OrderCreatedEvent(id));
        }

        public void Complete()
        {
            if (Equals(Status, OrderStatus.Completed)) throw new OrderAlreadyCompletedException(Id.Value);
            Status = OrderStatus.Completed;
        }
    }
}