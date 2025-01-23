using System;

namespace BudgetManager.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; }  = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set;}
}
