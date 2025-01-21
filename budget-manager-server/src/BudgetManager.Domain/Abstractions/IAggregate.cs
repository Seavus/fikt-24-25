using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Domain.Abstractions;

    public interface IAggregate : IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        IDomainEvent[] ClearDomainEvents();
    }

    public interface IAggregate<T> : IAggregate, IEntity<T>;
