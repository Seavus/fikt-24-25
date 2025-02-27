using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Id)
                .HasConversion(
                    categoryId => categoryId.Value, 
                    dbId => CategoryId.Create(dbId) 
                );

            builder.Property(c => c.UserId)
                .HasConversion(
                    userId => userId.Value, 
                    dbId => UserId.Create(dbId)  
                );

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}