namespace BudgetManager.Application.Transactions.GetTransactionStatistics;

public record GetTransactionStatisticsRequest(int Month, int Year) : IRequest<List<GetTransactionStatisticsResponse>>;