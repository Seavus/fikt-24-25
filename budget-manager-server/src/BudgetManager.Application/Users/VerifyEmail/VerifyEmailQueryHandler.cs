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
        var user = await _context.Users
            .Include(u => u.EmailVerificationTokens)
            .FirstOrDefaultAsync(u => u.Id == UserId.Create(request.UserId), cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found.");

        var success = user.VerifyEmail(request.Token);

        await _context.SaveChangesAsync(cancellationToken);

        return new VerifyEmailResponse(success);
    }
}
