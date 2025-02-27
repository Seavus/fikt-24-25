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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}