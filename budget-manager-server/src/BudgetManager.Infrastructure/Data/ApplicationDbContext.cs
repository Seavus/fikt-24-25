using BudgetManager.Domain.Models;
using BudgetManager.Application.Data;
using Microsoft.EntityFrameworkCore;
using BudgetManager.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var categoryIdConverter = new ValueConverter<CategoryId, Guid>(
        id => id.Value,
        value => new CategoryId(value)
    );

        var userIdConverter = new ValueConverter<UserId, Guid>(
            id => id.Value,
            value => new UserId(value)
        );

        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .HasConversion(categoryIdConverter);

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasConversion(userIdConverter);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}