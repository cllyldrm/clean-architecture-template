namespace CleanArchitecture.Application.Common.Models
{
    public class Response
    {
        public Response(object data)
        {
            Data = data;
        }

        public Response()
        {
        }

        public string Message { get; set; }

        public object Data { get; }
    }
}