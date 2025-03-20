using BudgetManager.Application.Common.Responses;

namespace BudgetManager.Application.Users.GetUsers;
public record GetUsersQuery(int PageIndex, int PageSize) : IRequest<PaginatedResponse<GetUsersResponse>>;