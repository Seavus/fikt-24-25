using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Transactions.GetTransactionsByUser;

internal sealed class GetTransactionsByUserQueryHandler
    : IRequestHandler<GetTransactionsByUserQuery, PaginatedResponse<GetTransactionsByUserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public GetTransactionsByUserQueryHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<PaginatedResponse<GetTransactionsByUserResponse>> Handle(
        GetTransactionsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId.Value;

        var query = await _context.Transactions
    .AsNoTracking()
    .Join(_context.Categories.AsNoTracking(),
        t => t.CategoryId,
        c => c.Id,
        (t, c) => new { Transaction = t, Category = c })
    .ToListAsync(cancellationToken);  

        var filtered = query
            .Where(x => x.Category.UserId.Value == userId)
            .OrderByDescending(x => x.Transaction.TransactionDate)
            .Select(x => new GetTransactionsByUserResponse(
                x.Transaction.Id.Value,
                new CategoryModel(x.Category.Id.Value, x.Category.Name),
                x.Transaction.TransactionType.ToString(),
                x.Transaction.TransactionDate,
                x.Transaction.Amount,
                x.Transaction.Description
            ));

        var totalCount = filtered.Count();

        var items = filtered
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new PaginatedResponse<GetTransactionsByUserResponse>(items, request.PageIndex, request.PageSize, totalCount);
    }
}