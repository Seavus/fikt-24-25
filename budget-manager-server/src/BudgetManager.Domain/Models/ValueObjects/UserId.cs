using System;
using BudgetManager.Domain.Exceptions;

namespace BudgetManager.Domain.Models.ValueObjects;

public class UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        Value = value;
    }
    public static UserId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Guid cannot be empty!");
        }
        return new UserId(value);
    }
}

