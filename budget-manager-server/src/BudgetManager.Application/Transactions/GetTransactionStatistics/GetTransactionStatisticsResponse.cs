namespace BudgetManager.Application.Transactions.GetTransactionStatistics;

public record GetTransactionStatisticsResponse(Guid CategoryId, string CategoryName, int Transactions);