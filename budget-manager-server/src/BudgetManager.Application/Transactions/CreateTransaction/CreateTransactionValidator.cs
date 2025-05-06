using BudgetManager.Application.Data;
using BudgetManager.Application.Services;
using BudgetManager.Domain.Enums;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Transactions.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator(IApplicationDbContext context, ICurrentUser currentUser)
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required.")
            .MustAsync(async (id, cancellation) =>
                await context.Categories.AnyAsync(c => c.Id == CategoryId.Create(id) &&
                c.UserId == UserId.Create(currentUser.UserId!.Value), cancellation))
            .WithMessage("Categori does not exist.");

        RuleFor(x => x.TransactionType)
            .IsInEnum()
            .WithMessage("Invalid transaction type.");

        RuleFor(x => x.TransactionDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Transaction date cannot be in the future.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");
    }
}