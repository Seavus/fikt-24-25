using Microsoft.EntityFrameworkCore;
using BudgetManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Enums;

namespace BudgetManager.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(tId => tId.Value, dbId => TransactionId.Create(dbId));

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId);

        builder.Property(x => x.TransactionType)
            .HasConversion(typeId => Convert.ToInt32(typeId), dbId => (TransactionType)Enum.Parse(typeof(TransactionType), dbId.ToString()));

        builder.Property(x => x.TransactionDate).IsRequired();

        builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);

        builder.Property(x => x.Description).HasMaxLength(200);
    }
}
