using BudgetManager.Application.Common.Responses;
namespace BudgetManager.Application.Transactions.GetTransactionsByUser;
public record GetTransactionsByUserQuery(int PageIndex, int PageSize)
    : IRequest<PaginatedResponse<GetTransactionsByUserResponse>>;
