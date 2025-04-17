using BudgetManager.Domain.Events;
using FluentEmail.Core;

namespace BudgetManager.Application.Users.EventHandlers;

internal sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IFluentEmail _fluentEmail;

    public UserCreatedEventHandler(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var verificationUrl = $"https://localhost:7188/api/account/{notification.UserId.Value}/verify-email/{notification.EmailToken}";

        await _fluentEmail
            .To(notification.Email)
            .Subject("Verify your email")
            .Body($"Please verify your email by clicking the following link: <a href=\"{verificationUrl}\">{verificationUrl}</a>", true)
            .SendAsync(cancellationToken);
    }
}