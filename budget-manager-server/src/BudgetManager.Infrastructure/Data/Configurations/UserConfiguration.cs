using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(userId => userId.Value, dbId => UserId.Create(dbId));

        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();

        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();

        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Password).HasMaxLength(50).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.EmailVerificationTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId);
    }
}