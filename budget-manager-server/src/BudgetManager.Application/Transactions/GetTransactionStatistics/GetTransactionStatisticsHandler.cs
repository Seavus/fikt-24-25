using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Transactions.GetTransactionStatistics;
public class GetTransactionStatisticsHandler : IRequestHandler<GetTransactionStatisticsQuery, List<GetTransactionStatisticsResponse>>
{
    private IApplicationDbContext _context;
    private ICurrentUser _currentUser;

    public GetTransactionStatisticsHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<List<GetTransactionStatisticsResponse>> Handle(GetTransactionStatisticsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId!.Value;

        var transactionsWithCategories = await (
                    from transaction in _context.Transactions
                    join category in _context.Categories on transaction.CategoryId equals category.Id
                    where transaction.TransactionDate.Month == request.Month &&
                        transaction.TransactionDate.Year == request.Year &&
                        !transaction.IsDeleted &&
                        !category.IsDeleted
                    select new
                    {
                        transaction,
                        category
                    }
                    ).ToListAsync(cancellationToken);

        var grouped = transactionsWithCategories
            .Where(x => x.category.UserId.Value == userId)
            .GroupBy(x => new { x.category.Id, x.category.Name })
            .Select(g => new GetTransactionStatisticsResponse(
                g.Key.Id.Value,
                g.Key.Name,
                g.Count()
                ))
            .ToList();
        
        return grouped;
    }
}