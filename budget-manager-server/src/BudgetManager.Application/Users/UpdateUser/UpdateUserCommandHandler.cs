using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

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
        var userId = UserId.Create(request.UserId);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        user.Update(request.FirstName, request.LastName);

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse(user.Id, user.FirstName, user.LastName, user.Email);
    }
}