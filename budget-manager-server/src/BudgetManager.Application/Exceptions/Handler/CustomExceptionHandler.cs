using BudgetManager.Domain.Exceptions;

namespace BudgetManager.Application.Exceptions.Handler;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occured at {Time}", DateTime.UtcNow);

        var statusCode = exception switch
        {
            InternalServerException => StatusCodes.Status500InternalServerError,
            BadRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            DomainException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Detail = exception.GetType().Name,
            Status = statusCode,
            Instance = context.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(e => e.ErrorMessage).ToArray()
                );

            problemDetails.Extensions["validationErrors"] = errors;
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }       
}