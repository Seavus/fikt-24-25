namespace BudgetManager.Application.Users.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<LoginUserResponse>;  