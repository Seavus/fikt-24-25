using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Transactions.GetTransactionsByUser;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Transactions.GetTransactionById;

internal sealed class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdResponse>
{
    private readonly IApplicationDbContext _context;
    

    public GetTransactionByIdHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
        
    }

    public async Task<GetTransactionByIdResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transactionId = TransactionId.Create(request.Id);

        var transaction = await _context.Transactions
            .AsNoTracking()
            .Join(_context.Categories.AsNoTracking(),
                t => t.CategoryId,
                c => c.Id,
                (t, c) => new { Transaction = t, Category = c })
            .FirstOrDefaultAsync(x => x.Transaction.Id == transactionId, cancellationToken);

        if (transaction == null)
            throw new NotFoundException("Transaction not found.");

        var t = transaction.Transaction;
        var c = transaction.Category;

        var response = new GetTransactionByIdResponse(
            t.Id.Value,
            new CategoryModel(c.Id.Value, c.Name),
            t.TransactionType.ToString(),
            t.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
            t.Amount,
            t.Description
        );

        return response;
    }
}
