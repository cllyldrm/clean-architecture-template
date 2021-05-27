using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Domain.AggregateRoots.Order.Exceptions;
using CleanArchitecture.Domain.Common.Models;

namespace CleanArchitecture.Domain.AggregateRoots.Order
{
    public class OrderStatus : ValueObject
    {
        static OrderStatus()
        {
        }

        private OrderStatus()
        {
        }

        private OrderStatus(string status)
        {
            Value = status;
        }

        public string Value { get; private set; }

        public static OrderStatus From(string value)
        {
            var status = new OrderStatus {Value = value};

            if (!SupportedStatuses.Contains(status))
            {
                throw new UnsupportedOrderStatusException(status);
            }

            return status;
        }

        public static OrderStatus Created => new("CREATED");

        public static OrderStatus Completed => new("COMPLETED");

        public static implicit operator string(OrderStatus status)
        {
            return status.ToString();
        }

        public static explicit operator OrderStatus(string status)
        {
            return From(status);
        }

        public override string ToString()
        {
            return Value;
        }

        protected static IEnumerable<OrderStatus> SupportedStatuses
        {
            get
            {
                yield return Created;
                yield return Completed;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}