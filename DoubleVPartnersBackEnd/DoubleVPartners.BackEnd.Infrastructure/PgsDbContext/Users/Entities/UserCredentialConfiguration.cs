using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.BackEnd.Infrastructure.PgsDbContext.Users.Entities
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            // Table name (optional)
            builder.ToTable("UserCredentials");

            // Key configuration for ValueObject Id
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value, // to db
                    value => UserCredentialId.Create(value) // from db
                )
                .HasColumnName("Id")
                .IsRequired();

            // UserId as ValueObject
            builder.Property(x => x.UserId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                )
                .HasColumnName("UserId")
                .IsRequired();

            // UserCredentialKey as ValueObject (store encrypted string)
            builder.Property(x => x.Key)
                .HasConversion(
                    key => key.Encrypted,
                    encrypted => UserCredentialKey.Create(encrypted)
                )
                .HasColumnName("Key")
                .HasMaxLength(512)
                .IsRequired();

            // Auditing
            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder.Property(x => x.UpdatedOnUtc);

            // Indexes (optional, but common for foreign keys)
            builder.HasIndex(x => x.UserId);
        }
    }
}
