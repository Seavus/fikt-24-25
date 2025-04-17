using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Events;

public sealed record UserCreatedEvent(UserId UserId, string Email, Guid EmailToken) : IDomainEvent;