using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
            
        builder.Property(u => u.Id)
                .HasConversion(
                    userId => userId.Value,
                    dbId => UserId.Create(dbId)
                );

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(u => u.EmailVerified)
            .IsRequired();

        builder.HasMany(u => u.EmailVerificationTokens)
            .WithOne()
            .HasForeignKey(e => e.UserId);


    }
}