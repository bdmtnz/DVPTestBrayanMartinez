using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts
{
    public partial class UserDebt
    {
        public UserId UserId { get; private set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime? PaidOnUtc { get; private set; }
    }
}
