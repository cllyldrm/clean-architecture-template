using System.Net;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}