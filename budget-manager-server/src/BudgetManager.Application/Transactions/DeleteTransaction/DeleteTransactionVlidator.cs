namespace BudgetManager.Application.Transactions.DeleteTransaction;
internal class DeleteTransactionValidator : AbstractValidator<DeleteTransactionCommand>
{
    public DeleteTransactionValidator()
    {
        RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId is required.");
    }
}