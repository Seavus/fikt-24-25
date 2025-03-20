namespace BudgetManager.Application.Users.GetUserById;

public record GetUserByIdResponse(Guid Id, string FirstName, string LastName, string Email, DateTime CreatedOn, string CreatedBy, DateTime? UpdatedOn, string? UpdatedBy);