namespace BudgetManager.Application.Transactions.DeleteTransaction;
public record DeleteTransactionCommand(Guid TransactionId) : IRequest<DeleteTransactionResponse>;