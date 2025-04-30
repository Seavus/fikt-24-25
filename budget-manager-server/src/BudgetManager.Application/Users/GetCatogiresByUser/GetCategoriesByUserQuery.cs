using BudgetManager.Application.Common.Responses;

namespace BudgetManager.Application.Users.GetCategoriesByUser;

public record GetCategoriesByUserQuery(int PageIndex, int PageSize) : IRequest<PaginatedResponse<GetCategoriesByUserResponse>>;