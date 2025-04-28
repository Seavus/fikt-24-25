namespace BudgetManager.Application.Users.UpdateUserBalance;

internal class UpdateUserBalanceCommandValidator : AbstractValidator<UpdateUserBalanceCommand>
{
    public UpdateUserBalanceCommandValidator()
    {
        RuleFor(x => x.Balance)
           .GreaterThanOrEqualTo(0).WithMessage("Balance must be zero or greater");
    }
}
