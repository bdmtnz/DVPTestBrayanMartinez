using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.BackEnd.Infrastructure.PgsDbContext.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name (optional)
            builder.ToTable("Users");

            // Key configuration for ValueObject Id
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                )
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Auditing
            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder.Property(x => x.UpdatedOnUtc);

            // Soft delete
            builder.Property(x => x.DeletedOnUtc);

            // One-to-one: User <-> UserCredential
            builder.HasOne(x => x.Credential)
                .WithOne()
                .HasForeignKey<UserCredential>(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: User <-> UserDebts
            builder.HasMany(x => x.Debts)
                .WithOne()
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
