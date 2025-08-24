using DoubleVPartners.BackEnd.Domain.Common.Base;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects
{
    public class UserId : AggregateRootId<string>
    {
        public override string Value { get; protected set; }

        private UserId() { }
        private UserId(Ulid value)
        {
            Value = value.ToString();
        }

        public static UserId CreateUnique()
        {
            return new UserId(Ulid.NewUlid());
        }

        public static UserId Create(Ulid value)
        {
            return new UserId(value);
        }

        public static UserId Create(string value)
        {
            return new UserId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
