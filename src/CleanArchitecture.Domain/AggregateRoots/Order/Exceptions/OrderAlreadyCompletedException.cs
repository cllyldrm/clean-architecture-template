using System;

namespace CleanArchitecture.Domain.AggregateRoots.Order.Exceptions
{
    public class OrderAlreadyCompletedException : Exception
    {
        public OrderAlreadyCompletedException(int id) : base($"Order has already completed, OrderId:{id}")
        {
        }
    }
}