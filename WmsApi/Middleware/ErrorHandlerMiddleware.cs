using System.Net;
using WmsApi.Exceptions;

namespace WmsApi.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BaseException exception)
        {
            var response = context.Response;
            
            switch (exception)
            {
                case NotFoundException notFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    await response.WriteAsJsonAsync(new { error = notFoundException.Message });
                    break;
                default:
                    throw new ArgumentException("Invalid exception type", nameof(exception));
            }
        }
    }
}