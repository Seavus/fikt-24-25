using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;

namespace BudgetManager.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<GetUsersResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<PaginatedResponse<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        int totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(u => u.LastName)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new GetUsersResponse(u.Id.Value, u.FirstName, u.LastName, u.Email))
            .ToListAsync(cancellationToken);

        return new PaginatedResponse<GetUsersResponse>(users, request.PageIndex, request.PageSize, totalCount);
    }
}