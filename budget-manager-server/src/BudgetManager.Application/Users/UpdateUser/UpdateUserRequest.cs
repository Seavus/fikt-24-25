namespace BudgetManager.Application.Users.UpdateUser;
public record UpdateUserRequest(Guid UserId, string FirstName, string LastName);