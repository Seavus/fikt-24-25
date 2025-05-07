using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId.Create(request.UserId), cancellationToken);

        if (user == null) {
            throw new NotFoundException("User not found");
        }

        user.SetDeleted();
        await _context.SaveChangesAsync(cancellationToken);
        
        return new DeleteUserResponse(true);
    }
}
