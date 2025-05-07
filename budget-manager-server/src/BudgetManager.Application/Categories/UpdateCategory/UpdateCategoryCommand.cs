namespace BudgetManager.Application.Categories.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Unit>;