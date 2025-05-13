using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Services;
using BudgetManager.Domain.Enums;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Transactions.DeleteTransaction;
internal sealed class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, DeleteTransactionResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public DeleteTransactionHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<DeleteTransactionResponse> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactionId = TransactionId.Create(request.TransactionId);

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);

        if (transaction == null)
            throw new NotFoundException("Transaction not found");

        var userId = UserId.Create(_currentUser.UserId!.Value);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        switch (transaction.TransactionType)
        {
            case TransactionType.Income:
                user.CreditBalance(transaction.Amount);
                break;
            case TransactionType.Expense:
                user.DebitBalance(transaction.Amount);
                break;
        }

        transaction.SetDeleted();
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTransactionResponse(true);
    }
}