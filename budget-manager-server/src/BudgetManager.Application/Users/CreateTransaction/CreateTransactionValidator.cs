namespace BudgetManager.Application.Users.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required.");

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