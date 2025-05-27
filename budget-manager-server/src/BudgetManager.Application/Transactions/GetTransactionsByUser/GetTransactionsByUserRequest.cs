namespace BudgetManager.Application.Transactions.GetTransactionsByUser;
public record GetTransactionsByUserRequest(int PageIndex = 1, int PageSize = 10);