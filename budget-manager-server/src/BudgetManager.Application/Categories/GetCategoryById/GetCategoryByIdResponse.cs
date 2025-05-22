using BudgetManager.Application.Users.GetUserById;

namespace BudgetManager.Application.Categories.GetCategoryById;

public record GetCategoryByIdResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    
    public required UserModel User { get; init; }
}

public record UserModel(Guid id, string FirstName, string LastName, string Email);