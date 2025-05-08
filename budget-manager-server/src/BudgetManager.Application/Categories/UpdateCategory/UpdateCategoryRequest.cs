namespace BudgetManager.Application.Categories.UpdateCategory;

public record UpdateCategoryRequest (Guid CategoryId, string Name);