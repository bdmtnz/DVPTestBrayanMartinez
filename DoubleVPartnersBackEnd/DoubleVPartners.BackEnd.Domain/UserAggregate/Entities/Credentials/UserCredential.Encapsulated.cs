using DoubleVPartners.BackEnd.Domain.Common.Base;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials
{
    public partial class UserCredential : Entity<UserCredentialId>, IAuditable
    {
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? UpdatedOnUtc { get; private set; }

        [Obsolete("Only for reflection", true)]
        protected UserCredential() : base(default) { }

        private UserCredential(UserCredentialId id, UserId userId, UserCredentialKey key, DateTime createdOnUtc) : base(id)
        {
            CreatedOnUtc = createdOnUtc;
            UserId = userId;
            Key = key;
        }

        public static UserCredential Create(UserId userId, string email, string password)
        {
            var _id = UserCredentialId.CreateUnique();
            var key = UserCredentialKey.Create(email, password);
            return new UserCredential(_id, userId, key, DateTime.UtcNow);
        }
    }
}
