using DoubleVPartners.BackEnd.Domain.Common.Base;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate
{
    public partial class User : AggregateRoot<UserId>, IAuditable
    {
        private List<UserDebt> _debts = [];
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? UpdatedOnUtc { get; private set; }

        public User(UserId id, string name, DateTime createdOnUtc) : base(id)
        {
            Name = name;
            CreatedOnUtc = createdOnUtc;
        }

        public static User Create(string name, string email, string password)
        {
            return new User(UserId.CreateUnique(), name, DateTime.UtcNow)
                .SetCredential(email, password);
        }

        public User SetCredential(string email, string password)
        {
            Credential = UserCredential.Create(Id, email, password);
            return this;
        }

        public User AddDebt(decimal amount, string description)
        {
            var debt = UserDebt.Create(Id, amount, description);
            _debts.Add(debt);
            UpdatedOnUtc = DateTime.UtcNow;
            return this;
        }

        public User AlterDebt(UserDebtId debtId, string name, decimal amount)
        {
            var debt = _debts.FirstOrDefault(d => d.Id == debtId);
            if (debt is not null)
            {
                debt.SetName(name)
                    .SetAmount(amount);
            }
            return this;
        }

        public User RemoveDebt(UserDebtId debtId)
        {
            var debt = _debts.FirstOrDefault(d => d.Id == debtId);
            if (debt is not null)
            {
                _debts.Remove(debt);
            }
            return this;
        }

        public User PayDebt(UserDebtId debtId)
        {
            var debt = _debts.FirstOrDefault(d => d.Id == debtId);
            if (debt is not null)
            {
                debt.Pay();
            }
            return this;
        }
    }
}
