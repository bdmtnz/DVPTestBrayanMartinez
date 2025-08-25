namespace DoubleVPartners.BackEnd.Contracts.Debts
{
    public record DebtResponse(
        string Id, 
        string UserId,
        string Name,
        decimal Amount,
        DateTime CreatedOnUtc,
        DateTime? UpdatedOnUtc,
        DateTime? PaidOnUtc);
}
