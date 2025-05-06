namespace BudgetManager.Application.Users.LoginUser;

public record LoginUserResponse(string AccessToken, Guid Id, string Name, string Email);