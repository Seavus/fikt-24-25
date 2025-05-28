namespace BudgetManager.Application.Categories.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<CreateCategoryResponse>;