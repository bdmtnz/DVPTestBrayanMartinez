using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate
{
    public partial class User
    {
        public string Name { get; private set; }
        public UserCredential Credential { get; private set; }
        public IReadOnlyList<UserDebt> Debts => _debts.AsReadOnly();
    }
}
