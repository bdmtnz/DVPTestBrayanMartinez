namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface ISoftDeletable
    {
        DateTime? DeletedOnUtc { protected set; get; }
        void ApplySoftDelete();
    }
}
