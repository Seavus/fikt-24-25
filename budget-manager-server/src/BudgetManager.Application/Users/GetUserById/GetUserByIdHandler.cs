using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;

namespace BudgetManager.Application.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private IApplicationDbContext _context;

    public GetUserByIdHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id.Value == request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return new GetUserByIdResponse(user.Id.Value, user.FirstName, user.LastName, user.Email, user.CreatedOn, user.CreatedBy, user.UpdatedOn, user.UpdatedBy);
    }
}
