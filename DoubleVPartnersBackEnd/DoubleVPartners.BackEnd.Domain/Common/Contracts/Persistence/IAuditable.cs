namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IAuditable
    {
        DateTime CreatedOnUtc { get; }
        DateTime? UpdatedOnUtc { get; }
    }
}
