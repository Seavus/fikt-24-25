using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.UpdateUser;
public record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : IRequest<UpdateUserResponse>;