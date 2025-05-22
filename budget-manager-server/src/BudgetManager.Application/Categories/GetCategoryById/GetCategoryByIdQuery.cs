namespace BudgetManager.Application.Categories.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<GetCategoryByIdResponse>;