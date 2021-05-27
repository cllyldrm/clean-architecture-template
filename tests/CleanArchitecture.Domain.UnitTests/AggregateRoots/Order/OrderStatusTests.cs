using CleanArchitecture.Domain.AggregateRoots.Order;
using CleanArchitecture.Domain.AggregateRoots.Order.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Domain.UnitTests.AggregateRoots.Order
{
    public class OrderStatusTests
    {
        [Test]
        public void ShouldReturnCorrectStatus()
        {
            var value = "COMPLETED";
            var status = OrderStatus.From(value);
            status.Value.Should().Be(value);
        }

        [Test]
        public void ToStringReturnsStatus()
        {
            var status = OrderStatus.Completed;
            status.ToString().Should().Be(status.Value);
        }

        [Test]
        public void ShouldPerformImplicitConversionToStatusString()
        {
            string status = OrderStatus.Completed;
            status.Should().Be("COMPLETED");
        }

        [Test]
        public void ShouldPerformExplicitConversionGivenSupportedStatus()
        {
            var status = (OrderStatus) "COMPLETED";
            status.Should().Be(OrderStatus.Completed);
        }

        [Test]
        public void ShouldThrowUnsupportedStatusExceptionGivenNotSupportedStatus()
        {
            FluentActions.Invoking(() => OrderStatus.From("XX"))
                .Should().Throw<UnsupportedOrderStatusException>();
        }
    }
}