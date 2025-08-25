using DoubleVPartners.BackEnd.Domain.UserAggregate;
using System.Security.Claims;

namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Security
{
    public interface IJwtHandler
    {
        string Generate(User user);
        List<Claim> Validate(string token);
    }
}
