using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Events;
using BudgetManager.Domain.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class User : Aggregate<UserId>
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public decimal Balance { get; private set; }

    public bool EmailVerified { get; private set; } = false;

    private readonly List<EmailVerificationToken> _emailVerificationTokens = new();

    private User() { }

    public IReadOnlyCollection<EmailVerificationToken> EmailVerificationTokens => _emailVerificationTokens.AsReadOnly();

    public static User Create(UserId id, string firstName, string lastName, string email, string password, decimal balance, bool emailVerified = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(password);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(balance, 0);

        var user = new User()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            EmailVerified = emailVerified,
            Balance = balance
        };

        var emailToken = user.AddEmailVerificationToken();
        user.AddDomainEvent(new UserCreatedEvent(user.Id, user.Email, emailToken));

        return user;
    }

    private Guid AddEmailVerificationToken()
    {
        var emailToken = EmailVerificationToken.Create(EmailVerificationTokenId.Create(Guid.NewGuid()), Id, Guid.NewGuid());
        _emailVerificationTokens.Add(emailToken);

        return emailToken.Token;
    }

    public bool VerifyEmail(Guid emailToken)
    {
        var token = _emailVerificationTokens.FirstOrDefault(x => x.Token.Equals(emailToken))
            ?? throw new DomainException("Email token does not exists");

        if (token.ExpiredOn < DateTime.UtcNow)
        {
            throw new DomainException("Expired email verification token.");
        }

        EmailVerified = true;
        _emailVerificationTokens.Remove(token);

        return true;
    }

    public void Update(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);

        FirstName = firstName;
        LastName = lastName;
    }
}
