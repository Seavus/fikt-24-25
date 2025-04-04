namespace BudgetManager.Application.Categories;

public record CreateCategoryCommand(string Name) : IRequest<CreateCategoryResponse>;