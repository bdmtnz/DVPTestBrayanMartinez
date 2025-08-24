using DoubleVPartners.BackEnd.Domain.Common.Base;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects
{
    public class UserDebtId : ValueObject
    {
        public string Value { get; protected set; }

        [Obsolete("Only for reflection", true)]
        protected UserDebtId() { }
        private UserDebtId(Ulid value)
        {
            Value = value.ToString();
        }

        public static UserDebtId CreateUnique()
        {
            return new UserDebtId(Ulid.NewUlid());
        }

        public static UserDebtId Create(Ulid value)
        {
            return new UserDebtId(value);
        }

        public static UserDebtId Create(string value)
        {
            return new UserDebtId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
