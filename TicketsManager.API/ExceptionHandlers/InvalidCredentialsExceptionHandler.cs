using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsManager.Business.Exceptions;

namespace TicketsManager.API.ExceptionHandlers;

public class InvalidCredentialsExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not InvalidCredentialsException)
            return false;

        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Invalid Credentials",
            Detail = exception.Message,
            Instance = httpContext.Request.Path,
        };

        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
