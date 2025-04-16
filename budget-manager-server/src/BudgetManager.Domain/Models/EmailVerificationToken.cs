using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class EmailVerificationToken : Entity<EmailVerificationTokenId>
{
    public UserId UserId { get; private set; } = default!;
    public Guid Token { get; private set; } = default!;
    public DateTime ExpiredOn { get; private set; } = default!;

    public static EmailVerificationToken Create(EmailVerificationTokenId emailVerificationTokenId, UserId userId, Guid token)
    {
        ArgumentNullException.ThrowIfNull(emailVerificationTokenId);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(token);

        var utcNow = DateTime.UtcNow;

        var emailToken = new EmailVerificationToken
        {
            Id = emailVerificationTokenId,
            UserId = userId,
            Token = token,
            ExpiredOn = utcNow.AddHours(3)
        };

        return emailToken;
    }
}
