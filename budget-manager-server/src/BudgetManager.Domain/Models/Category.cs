using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class Category : Aggregate<CategoryId>
{
    public string Name { get; private set; }
    public UserId UserId { get; private set; }
    

    private Category(CategoryId id, string name,UserId userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
        CreatedOn = DateTime.UtcNow;
        CreatedBy = "System";
    }

    public static Category Create(CategoryId id, string name, UserId userId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new Category(id, name, userId);
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        Name = name;
        UpdatedOn = DateTime.UtcNow;
        UpdatedBy = "System";
    }

    public static Category Create(CategoryId categoryId, string name, Guid userId)
    {
        throw new NotImplementedException();
    }
}