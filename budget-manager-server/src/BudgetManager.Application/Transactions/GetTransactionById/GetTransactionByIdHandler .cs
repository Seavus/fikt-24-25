using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Transactions.GetTransactionsByUser;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Transactions.GetTransactionById;

internal sealed class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        var response = _mapper.Map<GetTransactionByIdResponse>(
            transaction.Transaction,
            opt => opt.Items["category"] = transaction.Category);

        return response;
    }
}
