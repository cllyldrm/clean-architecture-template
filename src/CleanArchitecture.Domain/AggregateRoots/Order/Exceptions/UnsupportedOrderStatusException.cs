using System;

namespace CleanArchitecture.Domain.AggregateRoots.Order.Exceptions
{
    public class UnsupportedOrderStatusException : Exception
    {
        public UnsupportedOrderStatusException(string status) : base($"Order status \"{status}\" was unsupported.")
        {
        }
    }
}