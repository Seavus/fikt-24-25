using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;

namespace BudgetManager.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<GetUsersResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaginatedResponse<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        int totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(u => u.CreatedOn)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items =_mapper.Map<List<GetUsersResponse>>(users);

        return new PaginatedResponse<GetUsersResponse>(items, request.PageIndex, request.PageSize, totalCount);
    }
}