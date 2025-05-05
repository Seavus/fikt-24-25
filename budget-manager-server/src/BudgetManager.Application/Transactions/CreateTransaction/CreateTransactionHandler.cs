using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Enums;
using BudgetManager.Domain.Exceptions;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Transactions.CreateTransaction;

internal sealed class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public CreateTransactionHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
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

        var userId = UserId.Create(_currentUser.UserId!.Value);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken) ?? throw new NotFoundException("User not found for the category.");
        
        switch (request.TransactionType)
        {
            case TransactionType.Expense:
                user.CreditBalance(request.Amount); 
                break;

            case TransactionType.Income:
                user.DebitBalance(request.Amount); 
                break;

            default:
                throw new DomainException("Unsupported transaction type.");
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse(transaction.Id.Value);
    }
}