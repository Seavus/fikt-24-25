using BudgetManager.Domain.Enums;

namespace BudgetManager.Application.Transactions.CreateTransaction;
public record CreateTransactionCommand(Guid CategoryId, TransactionType TransactionType, DateTime TransactionDate, decimal Amount, string? Description) : IRequest<CreateTransactionResponse>;