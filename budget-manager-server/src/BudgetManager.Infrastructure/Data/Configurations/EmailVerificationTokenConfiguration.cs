using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Infrastructure.Data.Configurations;
    public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,         
                dbId => EmailVerificationTokenId.Create(dbId) 
            );

            builder.Property(e => e.Token)
                .IsRequired();

            builder.Property(e => e.ExpiredOn)
                .IsRequired();
        }
    }