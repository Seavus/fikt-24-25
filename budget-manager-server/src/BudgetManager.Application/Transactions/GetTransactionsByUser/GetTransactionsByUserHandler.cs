using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Transactions.GetTransactionsByUser;

public class GetTransactionsByUserQueryHandler
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
        var userId = _currentUser.UserId;

        var query = _context.Transactions
            .AsNoTracking()
            .Join(_context.Categories,
                t => t.CategoryId,
                c => c.Id,
                (t, c) => new { Transaction = t, Category = c })
            .Where(x => x.Category.UserId.Value == userId)
            .OrderByDescending(x => x.Transaction.TransactionDate)
            .Select(x => new GetTransactionsByUserResponse(
                x.Transaction.Id.Value,
                new CategoryResponse(x.Category.Id.Value, x.Category.Name),
                x.Transaction.TransactionType.ToString(),
                x.Transaction.TransactionDate,
                x.Transaction.Amount,
                x.Transaction.Description
            ));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResponse<GetTransactionsByUserResponse>(items,request.PageIndex,request.PageSize,totalCount);
    }
}