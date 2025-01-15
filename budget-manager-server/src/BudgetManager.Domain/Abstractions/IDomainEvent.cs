using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BudgetManager.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }
        DateTime CreatedOn { get; }
        string EventType { get; }
    }
}
