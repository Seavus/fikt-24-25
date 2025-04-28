using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Services;
using BudgetManager.Domain.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;


namespace BudgetManager.Application.Users.UpdateUserBalance;

internal class UpdateUserBalanceCommandHandler : IRequestHandler<UpdateUserBalanceCommand, UpdateUserBalanceResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public UpdateUserBalanceCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<UpdateUserBalanceResponse> Handle(UpdateUserBalanceCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(_currentUser.UserId!.Value);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found.");
        }

        var isUpdated = user.UpdateBalance(request.Balance);

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserBalanceResponse(isUpdated);
    }
}

