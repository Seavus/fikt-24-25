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
        var problemDetails = new
        {
            Title = exception.Message,
            Detail = exception.GetType().Name,
            Status = statusCode,
            Instance = context.Request.Path
        };
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors;

           var validationProblemDetails = new
            {
                Title = problemDetails.Title,
                Detail = problemDetails.Detail,
                Status = problemDetails.Status,
                Instance = problemDetails.Instance,
                Errors = errors
            };
        }
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
     }
}