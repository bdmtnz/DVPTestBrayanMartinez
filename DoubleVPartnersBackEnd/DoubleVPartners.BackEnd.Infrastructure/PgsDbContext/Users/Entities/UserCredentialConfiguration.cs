using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.BackEnd.Infrastructure.PgsDbContext.Users.Entities
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            throw new NotImplementedException();
        }
    }
}
