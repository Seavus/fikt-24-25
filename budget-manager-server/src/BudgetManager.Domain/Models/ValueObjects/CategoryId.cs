using BudgetManager.Domain.Exceptions;

namespace BudgetManager.Domain.Models.ValueObjects;

public class CategoryId
{
    public Guid Value { get; }

    public CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Guid cannot be empty!");
        }
        return new CategoryId(value);
    }
}