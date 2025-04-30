using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Enums;
using BudgetManager.Domain.Exceptions;

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

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == CategoryId.Create(request.CategoryId), cancellationToken);

        if (category == null)
            throw new NotFoundException("Category not found.");

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == category.UserId, cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found for the category.");

        if (request.TransactionType == TransactionType.Expense)
        {
            user.DebitBalance(request.Amount);
        }
        else if (request.TransactionType == TransactionType.Income)
        {
            user.CreditBalance(request.Amount);
        }
        else
        {
            throw new DomainException("Unsupported transaction type.");
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse(transaction.Id.Value);
    }
}