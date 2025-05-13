namespace BudgetManager.Application.Transactions.GetTransactionStatistics;

public class GetTransactionStatisticsValidator : AbstractValidator<GetTransactionStatisticsQuery>
{
    public GetTransactionStatisticsValidator()
    {
        RuleFor(x => x.Month).InclusiveBetween(1, 12);
        RuleFor(x => x.Year).GreaterThan(2000);

    }
}
