using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BudgetManager.Domain.Abstractions;

    public interface IDomainEvent : INotification
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime CreatedOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;
    }
