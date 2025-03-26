namespace BudgetManager.Application.Categories;

public record CreateCategoryCommand(string Name, Guid UserId): IRequest<CreateCategoryResponse>;