using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.Id)
            .HasConversion(categoryId => categoryId.Value, dbId => CategoryId.Create(dbId));

        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
    }
}