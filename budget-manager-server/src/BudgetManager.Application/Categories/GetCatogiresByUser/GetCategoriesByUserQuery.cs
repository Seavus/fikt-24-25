using BudgetManager.Application.Common.Responses;

namespace BudgetManager.Application.Categories.GetCatogiresByUser;

public record GetCategoriesByUserQuery(int PageIndex, int PageSize) : IRequest<PaginatedResponse<GetCategoriesByUserResponse>>;