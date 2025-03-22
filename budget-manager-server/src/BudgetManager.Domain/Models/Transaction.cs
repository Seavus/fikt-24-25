using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class Transaction : Aggregate<TransactionId>
{
    public CategoryId CategoryId { get; private set; }
    public string TransactionType { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public decimal Amount { get; private set; }
    public string? Description { get; private set; }

    private Transaction(TransactionId id, CategoryId categoryId, string transactionType, DateTime transactionDate, decimal amount, string description)
    {
        Id = id;
        CategoryId = categoryId;
        TransactionType = transactionType;
        TransactionDate = transactionDate;
        Amount = amount;
        Description = description;
        CreatedOn = DateTime.UtcNow;
        CreatedBy = "System";
    }

    public static Transaction Create (TransactionId id, CategoryId categoryId, string transactionType, DateTime transactionDate, decimal amount, string description)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        return new Transaction(id, categoryId, transactionType, transactionDate, amount, description);
    }

    public void Update(CategoryId categoryId, string transactionType, DateTime transactionDate, decimal amount, string description)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        CategoryId = categoryId;
        TransactionType = transactionType;
        TransactionDate = transactionDate;
        Amount = amount;
        Description = description;
        UpdatedOn = DateTime.UtcNow;
        UpdatedBy = "System";
    }
}