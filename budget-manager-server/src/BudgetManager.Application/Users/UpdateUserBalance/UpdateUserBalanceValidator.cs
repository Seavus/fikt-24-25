namespace BudgetManager.Application.Users.UpdateUserBalance;

public class UpdateUserBalanceCommandValidator : AbstractValidator<UpdateUserBalanceCommand>
{
    public UpdateUserBalanceCommandValidator()
    {
        RuleFor(x => x.Balance)
           .GreaterThan(0).WithMessage("Balance must not be 0");
    }
}
