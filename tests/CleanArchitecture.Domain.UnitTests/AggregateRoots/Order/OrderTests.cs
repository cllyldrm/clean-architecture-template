using CleanArchitecture.Domain.AggregateRoots.Order;
using CleanArchitecture.Domain.AggregateRoots.Order.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Domain.UnitTests.AggregateRoots.Order
{
    public class OrderTests
    {
        [Test]
        public void Complete_WhenOrderStatusCompleted_ShouldThrowOrderAlreadyCompletedException()
        {
            var order = new OrderEntity("order-name", OrderStatus.Completed);
            order.SetId(1);

            FluentActions.Invoking(() => order.Complete())
                .Should().Throw<OrderAlreadyCompletedException>();
        }

        [Test]
        public void Complete_WhenOrderStatusCreated_ShouldSetToCompleted()
        {
            var order = new OrderEntity("order-name", OrderStatus.Created);
            order.SetId(1);
            order.Complete();
            order.Status.Should().Be(OrderStatus.Completed);
        }
    }
}