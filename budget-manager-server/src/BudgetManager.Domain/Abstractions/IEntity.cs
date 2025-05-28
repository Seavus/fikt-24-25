namespace BudgetManager.Domain.Abstractions;

public interface IEntity
{
    DateTime CreatedOn { get; set; }
    string CreatedBy { get; set; }
    DateTime? UpdatedOn { get; set; }
    string? UpdatedBy { get; set; }
    bool IsDeleted { get; }
}

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}
