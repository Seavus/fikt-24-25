using BudgetManager.Domain.Exceptions;
namespace BudgetManager.Domain.Models.ValueObjects;

public record TransactionId
{
    public Guid Value { get; }

    public TransactionId(Guid value)
    {
        Value = value;
    }

    public static TransactionId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Transaction ID cannot be empty!");
        }
        return new TransactionId(value);
    } 
}
