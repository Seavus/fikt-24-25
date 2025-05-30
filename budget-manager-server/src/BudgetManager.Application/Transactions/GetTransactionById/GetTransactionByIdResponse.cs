using BudgetManager.Application.Transactions.GetTransactionsByUser;

namespace BudgetManager.Application.Transactions.GetTransactionById;

public record GetTransactionByIdResponse(
    Guid Id,
    CategoryModel Category,
    string TransactionType,
    string TransactionDate,
    decimal Amount,
    string? Description
)
{
    public GetTransactionByIdResponse() : this(
      Guid.Empty,
      new CategoryModel(Guid.Empty, string.Empty),
      string.Empty,
      string.Empty,
      0,
      null
  )
    { }
}