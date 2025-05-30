using BudgetManager.Application.Transactions.GetTransactionsByUser;

namespace BudgetManager.Application.Transactions.GetTransactionById;
public record GetTransactionByIdResponse
{
    public Guid Id { get; set; }
    public required CategoryModel? Category { get; set; }
    public required string TransactionType { get; set; }
    public required string TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}