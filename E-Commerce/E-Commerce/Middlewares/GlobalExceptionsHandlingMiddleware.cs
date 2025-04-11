using System.Net;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Middlewares
{
    public class GlobalExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsHandlingMiddleware> _logger;

        public GlobalExceptionsHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundEndPointException(httpContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong{ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleNotFoundEndPointException(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The endpoint {httpContext.Request.Path} is not found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        //Handle Exception
        public async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            //Set Status Code To 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //Set Content Type application/json
            httpContext.Response.ContentType = "application/json";
            //C#09
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            //Return Standard Response
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = exception.Message
            }.ToString();
            await httpContext.Response.WriteAsync(response);
            
        }
    }
}
