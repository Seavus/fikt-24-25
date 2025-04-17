using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BudgetManager.Domain.Models;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Infrastructure.Data.Configurations;
public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(emailTokenId => emailTokenId.Value, dbId => EmailVerificationTokenId.Create(dbId));

        builder.Property(x => x.Token).HasMaxLength(50).IsRequired();

        builder.Property(x => x.ExpiredOn).IsRequired();
    }
}