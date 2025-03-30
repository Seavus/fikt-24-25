using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class EmailVerificationToken : Entity<EmailVerificationTokenId>
{
    public UserId UserId { get; private set; }
    public Guid Token {  get; private set; }
    public DateTime ExpiredOn { get; private set; }

    private EmailVerificationToken(EmailVerificationTokenId id, UserId userId, Guid token, DateTime expiredOn)
    {
        Id = id; 
        UserId = userId;
        Token = token;
        ExpiredOn = expiredOn;
    }

    public static EmailVerificationToken Create(EmailVerificationTokenId id, UserId userId, Guid token, DateTime expiredOn)
    {
        return new EmailVerificationToken(id, userId, token, expiredOn);
    }
}
