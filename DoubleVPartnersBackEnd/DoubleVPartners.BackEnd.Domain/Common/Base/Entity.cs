namespace DoubleVPartners.BackEnd.Domain.Common.Base
{
    public abstract partial class Entity<EId>
    {
        public EId Id { get; protected set; }

        protected Entity(EId id)
        {
            Id = id;
        }
    }
}
