using Microsoft.EntityFrameworkCore;
using BudgetManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
            transactionId => transactionId.Value,
            guid => TransactionId.Create(guid));

        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.TransactionDate)
            .IsRequired();

        builder.Property(t => t.CategoryId)
            .HasConversion(
            categoryId => categoryId.Value,
            dbId => CategoryId.Create(dbId));

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(t => t.CategoryId);
    }
}
