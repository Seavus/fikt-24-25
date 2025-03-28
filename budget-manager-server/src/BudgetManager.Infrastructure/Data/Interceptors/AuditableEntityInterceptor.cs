using BudgetManager.Application.Services;
using BudgetManager.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BudgetManager.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private ICurrentUser _currentUser;

    public AuditableEntityInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
        {
            UpdateEntities(eventData.Context);
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context != null)
        {
            UpdateEntities(eventData.Context);
        }
        return base.SavedChanges(eventData, result);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IEntity.CreatedOn)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IEntity.CreatedBy)).CurrentValue = _currentUser.Name;
            }
            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IEntity.CreatedOn)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IEntity.CreatedBy)).CurrentValue = _currentUser.Name;
            }
        }

    }
}
