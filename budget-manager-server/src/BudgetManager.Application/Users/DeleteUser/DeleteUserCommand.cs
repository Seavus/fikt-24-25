namespace BudgetManager.Application.Users.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest;