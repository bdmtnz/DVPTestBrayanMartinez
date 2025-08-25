namespace DoubleVPartners.BackEnd.Contracts.Users
{
    public record DashboardResponse(int Debts, int Paids, int Unpaids, decimal Pendient);
}
