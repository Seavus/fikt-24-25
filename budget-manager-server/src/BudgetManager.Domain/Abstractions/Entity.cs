using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; protected set; } = default!;
        public DateTime CreatedOn { get; private set; }  = DateTime.UtcNow;
        public string CreatedBy { get; private set; } = "System";
        public DateTime? UpdatedOn { get; private set; }
        public string? UpdatedBy { get; private set;}

        protected void SetAuditFields(string createdBy = "System")
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        protected void UpdateAuditFields(string updatedBy = "System")
        {
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
}