using BudgetManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Data;

public interface IApplicationDbContext
{
	DbSet<User> Users { get; }
	DbSet<Category> Categories { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}