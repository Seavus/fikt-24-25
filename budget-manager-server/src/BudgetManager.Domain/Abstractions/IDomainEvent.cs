using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BudgetManager.Domain.Abstractions;

    public interface IDomainEvent : INotification
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string EventType { get; set; } = typeof(IDomainEvent).AssemblyQualifiedName;
    }
