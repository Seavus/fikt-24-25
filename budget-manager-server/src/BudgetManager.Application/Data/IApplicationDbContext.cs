using BudgetManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Data;

public interface IApplicationDbContext
{
	DbSet<User> Users { get; }
	DbSet<Category> Categories { get; }
	DbSet<Transaction> Transactions { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
