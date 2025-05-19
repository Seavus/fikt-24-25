namespace BudgetManager.Application.Transactions.GetTransactionStatistics;

public record GetTransactionStatisticsResponse(
    List<TransactionsByCategory> TransactionsByCategory,
    List<TransactionsByDay> TransactionsByDay
    );

public record TransactionsByCategory(Guid CategoryId, string CategoryName, int Count);

public record TransactionsByDay(DateOnly Date, int Count, decimal Amount);