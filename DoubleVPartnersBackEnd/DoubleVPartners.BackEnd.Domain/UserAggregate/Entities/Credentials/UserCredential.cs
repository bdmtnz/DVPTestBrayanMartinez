using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials
{
    public partial class UserCredential
    {
        public UserId UserId { get; private set; }
        public UserCredentialKey Key { get; private set; }
    }
}
