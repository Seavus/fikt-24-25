using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;

namespace BudgetManager.Application.Users.GetCategoriesByUser;

internal class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, PaginatedResponse<GetCategoriesByUserResponse>>
{
    private IApplicationDbContext _context;

    public GetCategoriesByUserQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<PaginatedResponse<GetCategoriesByUserResponse>> Handle(GetCategoriesByUserQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories.AsQueryable();

        int totalCount = await query.CountAsync(cancellationToken);

        var categories = await query
            .OrderBy(c => c.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new GetCategoriesByUserResponse(c.Id.Value, c.Name))
            .ToListAsync(cancellationToken);

        return new PaginatedResponse<GetCategoriesByUserResponse>(categories, request.PageIndex, request.PageSize, totalCount);

    }
}