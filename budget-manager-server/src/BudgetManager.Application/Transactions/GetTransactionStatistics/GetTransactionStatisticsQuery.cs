namespace BudgetManager.Application.Transactions.GetTransactionStatistics;

public record GetTransactionStatisticsQuery(int Month, int Year) : IRequest<GetTransactionStatisticsResponse>;