using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;

namespace BudgetManager.Application.Transactions.CreateTransaction;

internal sealed class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = Transaction.Create(
            TransactionId.Create(Guid.NewGuid()),
            CategoryId.Create(request.CategoryId),
            request.TransactionType,
            request.TransactionDate,
            request.Amount,
            request.Description ?? string.Empty
        );

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse(transaction.Id.Value);
    }
}