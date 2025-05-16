namespace BudgetManager.Application.Transactions.GetTransactionById;

public record GetTransactionByIdResponse
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string TransactionType { get; init; } = string.Empty;
}