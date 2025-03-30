using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class EmailVerificationToken : Entity<UserId>
{
    public UserId UserId { get; private set; }
    public Guid Token {  get; private set; }
    public DateTime ExpiredOn { get; private set; }

    public EmailVerificationToken(UserId userId, Guid token, DateTime expiredOn)
    {
        UserId = userId;
        Token = token;
        ExpiredOn = expiredOn;
    }

    public static EmailVerificationToken Create(UserId userId)
    {
        return new EmailVerificationToken(userId, Guid.NewGuid(), DateTime.UtcNow.AddHours(24));
    }
}
