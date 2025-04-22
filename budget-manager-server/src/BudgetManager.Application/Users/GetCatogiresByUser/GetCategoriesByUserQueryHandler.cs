using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Users.GetCategoriesByUser;

internal class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, PaginatedResponse<GetCategoriesByUserResponse>>
{
    private IApplicationDbContext _context;
    private ICurrentUser _currentUser;

    public GetCategoriesByUserQueryHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<PaginatedResponse<GetCategoriesByUserResponse>> Handle(GetCategoriesByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User is not authenticated");

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