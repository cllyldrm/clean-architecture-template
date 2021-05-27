using System;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var httpStatusCode = exception.ToHttpStatusCode();
                _logger.LogError($"{exception.Message}");
                await WriteExceptionAsync(context, exception, httpStatusCode);
            }
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, int code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = code;

            var bodyText = JsonSerializer.Serialize(new Response {Message = exception.Message});
            await response.WriteAsync(bodyText).ConfigureAwait(false);
        }
    }
}