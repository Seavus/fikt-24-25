using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;

namespace BudgetManager.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        user.Update(request.FirstName, request.LastName);

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse(user.Id, user.FirstName, user.LastName, user.Email);
    }
}