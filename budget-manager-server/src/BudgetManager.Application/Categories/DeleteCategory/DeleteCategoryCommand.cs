namespace BudgetManager.Application.Categories.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest<DeleteCategoryResponse>;