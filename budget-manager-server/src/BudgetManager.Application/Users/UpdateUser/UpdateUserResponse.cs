using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.UpdateUser;
public record UpdateUserResponse(UserId Id, string FirstName, string LastName, string Email);