using BudgetManager.Application.Data;
using BudgetManager.Application.Services;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Transactions.GetTransactionStatistics;
internal sealed class GetTransactionStatisticsHandler : IRequestHandler<GetTransactionStatisticsQuery, GetTransactionStatisticsResponse>
{
    private IApplicationDbContext _context;
    private ICurrentUser _currentUser;

    public GetTransactionStatisticsHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<GetTransactionStatisticsResponse> Handle(GetTransactionStatisticsQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(_currentUser.UserId!.Value);
        var month = request.Month;
        var year = request.Year;

        var query = _context.Transactions
            .AsNoTracking()
            .Where (t => t.TransactionDate.Month == month && t.TransactionDate.Year == year)
            .Join(_context.Categories.AsNoTracking().Where(c => c.UserId == userId),

                t => t.CategoryId,
                c => c.Id,
                   (t, c) => new { Transaction = t, Category = c });


        var transactionsByCategory = await query
            .GroupBy(x => new { x.Category.Id, x.Category.Name })
            .Select(g => new TransactionsByCategory(
                g.Key.Id.Value,
                g.Key.Name,
                g.Count()))
            .ToListAsync(cancellationToken);

        var transactionsByDay = await query
            .GroupBy(x => x.Transaction.TransactionDate.Date)
            .Select(g => new TransactionsByDay(
                DateOnly.FromDateTime(g.Key),
                g.Count(),
                g.Sum(x => x.Transaction.Amount)))
            .ToListAsync(cancellationToken);
        
        return new GetTransactionStatisticsResponse(transactionsByCategory, transactionsByDay);
    }
}