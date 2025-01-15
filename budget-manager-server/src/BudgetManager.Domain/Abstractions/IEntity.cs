using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Domain.Abstractions
{
    public interface IEntity
    {
        DateTime CreatedOn { get; }
        string CreatedBy { get; }
        DateTime? UpdatedOn { get; }
        string? UpdatedBy { get; }
    }
    public interface IEntity<T> : IEntity
    {
        T Id { get; }
    }
}
