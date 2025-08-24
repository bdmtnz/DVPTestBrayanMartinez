using DoubleVPartners.BackEnd.Domain.Common.Base;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects
{
    public class UserCredentialId : ValueObject
    {
        public string Value { get; protected set; }

        [Obsolete("Only for reflection", true)]
        protected UserCredentialId() { }
        private UserCredentialId(Ulid value)
        {
            Value = value.ToString();
        }

        public static UserCredentialId CreateUnique()
        {
            return new UserCredentialId(Ulid.NewUlid());
        }

        public static UserCredentialId Create(Ulid value)
        {
            return new UserCredentialId(value);
        }

        public static UserCredentialId Create(string value)
        {
            return new UserCredentialId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
