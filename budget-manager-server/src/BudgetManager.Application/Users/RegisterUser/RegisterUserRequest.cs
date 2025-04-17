namespace BudgetManager.Application.Users.RegisterUser;

public record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, decimal Balance);