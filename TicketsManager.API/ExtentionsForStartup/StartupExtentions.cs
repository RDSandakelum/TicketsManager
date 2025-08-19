using TicketsManager.API.ExceptionHandlers;

namespace TicketsManager.API.ExtentionsForStartup;

public static class StartupExtentions
{
    public static IServiceCollection AddCustomExceptionsHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<CommandValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
        return services;
    }
}
