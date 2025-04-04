namespace BudgetManager.Application.Users.RegisterUser;
public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<RegisterUserResponse>;