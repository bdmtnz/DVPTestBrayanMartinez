namespace DoubleVPartners.BackEnd.Infrastructure.Security
{
    public class JwtSettings
    {
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public int TokenExpirationInMinutes { get; set; } = 60;
    }
}
