namespace BudgetManager.Application.Categories.UpdateCategory;

public record UpdateCategoryCommand(Guid CategoryId, string Name) : IRequest<UpdateCategoryResponse>;