using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.BackEnd.Infrastructure.PgsDbContext.Users.Entities
{
    public class UserDebtConfiguration : IEntityTypeConfiguration<UserDebt>
    {
        public void Configure(EntityTypeBuilder<UserDebt> builder)
        {
            // Table name (optional, can be omitted if default is fine)
            builder.ToTable("UserDebts");

            // Key configuration for ValueObject Id
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value, // to db
                    value => UserDebtId.Create(value) // from db
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

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.PaidOnUtc)
                .HasColumnName("PaidOnUtc");

            // Auditing
            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder.Property(x => x.UpdatedOnUtc);

            // Soft delete
            builder.Property(x => x.DeletedOnUtc);

            // Indexes (optional, but common for foreign keys)
            builder.HasIndex(x => x.UserId);
        }
    }
}
