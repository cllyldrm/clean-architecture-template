using System;
using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase(string message) : base(message)
        {
        }

        public abstract HttpStatusCode StatusCode { get; }
    }
}