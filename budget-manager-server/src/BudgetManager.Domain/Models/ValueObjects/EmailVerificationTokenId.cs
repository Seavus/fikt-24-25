using BudgetManager.Domain.Exceptions;

namespace BudgetManager.Domain.Models.ValueObjects;

public record EmailVerificationTokenId
{
    public Guid Value { get; }

    private EmailVerificationTokenId (Guid value)
    {
        this.Value = value;
    }

    public static EmailVerificationTokenId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Email verification token ID cannot be empty!");
        }
        return new EmailVerificationTokenId (value);
    }
}
