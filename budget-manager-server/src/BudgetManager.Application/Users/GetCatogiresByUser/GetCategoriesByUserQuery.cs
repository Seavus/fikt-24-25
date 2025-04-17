using BudgetManager.Application.Common.Responses;

namespace BudgetManager.Application.Users.GetCategoriesByUser;

public record GetCategoriesByUserQuery(Guid UserId, int PageIndex, int PageSize) : IRequest<PaginatedResponse<GetCategoriesByUserResponse>>;