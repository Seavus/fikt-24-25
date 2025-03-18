using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.UpdateUser;
public record UpdateUserCommand(UserId UserId, string FirstName, string LastName) : IRequest<UpdateUserResponse>;