using DoubleVPartners.BackEnd.Domain.Common.Base;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts
{
    public partial class UserDebt : Entity<UserDebtId>, IAuditable, ISoftDeletable
    {
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? UpdatedOnUtc { get; private set; }
        public DateTime? DeletedOnUtc { get; set; }

        [Obsolete("For ORM use only", true)]
        protected UserDebt() { }
        private UserDebt(UserDebtId id, UserId userId, string name, decimal amount, DateTime createdOnUtc) : base(id)
        {
            UserId = userId;
            Name = name;
            Amount = amount;
            CreatedOnUtc = createdOnUtc;
        }

        public void ApplySoftDelete()
        {
            DeletedOnUtc = DeletedOnUtc ?? DateTime.UtcNow;
        }

        public static UserDebt Create(UserId userId, decimal amount, string description)
            => new UserDebt(UserDebtId.CreateUnique(), userId, description, amount, DateTime.UtcNow);

        public UserDebt SetName(string name)
        {
            Name = name;
            return this;
        }

        public UserDebt SetAmount(decimal amount)
        {
            Amount = amount;
            return this;
        }

        public ErrorOr<UserDebt> Pay()
        {
            if (PaidOnUtc is not null)
            {
                return Error.Failure(description: "Debt is already paid.");
            }
            PaidOnUtc = DateTime.UtcNow;
            return this;
        }
    }
}
