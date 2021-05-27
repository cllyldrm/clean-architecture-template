using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public ValidationException(string message) : base(message)
        {
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}