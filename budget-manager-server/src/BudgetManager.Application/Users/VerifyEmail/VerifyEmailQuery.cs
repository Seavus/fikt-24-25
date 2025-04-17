namespace BudgetManager.Application.Users.VerifyEmail;

public record VerifyEmailQuery(Guid UserId, Guid Token) : IRequest<VerifyEmailResponse>;