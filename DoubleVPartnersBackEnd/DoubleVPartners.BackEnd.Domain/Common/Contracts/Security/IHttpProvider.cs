namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Security
{
    public interface IHttpProvider
    {
        public string? GetJwtToken();
        public List<string> GetClaimValues(string claimType);
        public string GetSingleClaimValue(string claimType);
        public string GetSingleHeaderValue(string key);
    }
}
