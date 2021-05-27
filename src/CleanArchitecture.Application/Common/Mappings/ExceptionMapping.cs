using System;
using System.Net;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.AggregateRoots.Order.Exceptions;

namespace CleanArchitecture.Application.Common.Mappings
{
    public static class ExceptionMapping
    {
        public static int ToHttpStatusCode(this Exception exception)
        {
            if (exception is ExceptionBase exceptionBase)
            {
                return (int) exceptionBase.StatusCode;
            }

            if (exception is OrderAlreadyCompletedException or UnsupportedOrderStatusException)
            {
                return (int) HttpStatusCode.BadRequest;
            }

            return (int) HttpStatusCode.InternalServerError;
        }
    }
}