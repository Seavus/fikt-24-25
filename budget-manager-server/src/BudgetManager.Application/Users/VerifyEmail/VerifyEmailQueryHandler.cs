using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.VerifyEmail;

public class VerifyEmailQueryHandler : IRequestHandler<VerifyEmailQuery, VerifyEmailResponse>
{
    private IApplicationDbContext _context;

    public VerifyEmailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VerifyEmailResponse> Handle(VerifyEmailQuery request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);
        var user = await _context.Users
            .Include("EmailVerificationTokens")
            .FirstOrDefaultAsync(u => u.Id ==userId , cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found.");

        var success = user.VerifyEmail(request.Token);

        await _context.SaveChangesAsync(cancellationToken);

        return new VerifyEmailResponse(success);
    }
}
