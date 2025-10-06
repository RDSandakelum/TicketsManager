using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsManager.Business.Exceptions;

namespace TicketsManager.API.ExceptionHandlers
{
    public class CommandValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not CommandValidationExeption commandValidationException)
                return false;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
                Detail = commandValidationException.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
