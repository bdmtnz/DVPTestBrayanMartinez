using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.BackEnd.Infrastructure.PgsDbContext.Users.Entities
{
    public class UserDebtConfiguration : IEntityTypeConfiguration<UserDebt>
    {
        public void Configure(EntityTypeBuilder<UserDebt> builder)
        {
            throw new NotImplementedException();
        }
    }
}
