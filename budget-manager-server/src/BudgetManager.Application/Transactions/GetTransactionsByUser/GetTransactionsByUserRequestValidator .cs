
namespace BudgetManager.Application.Transactions.GetTransactionsByUser;

public class GetTransactionsByUserRequestValidator : AbstractValidator<GetTransactionsByUserRequest>
{
    public GetTransactionsByUserRequestValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}