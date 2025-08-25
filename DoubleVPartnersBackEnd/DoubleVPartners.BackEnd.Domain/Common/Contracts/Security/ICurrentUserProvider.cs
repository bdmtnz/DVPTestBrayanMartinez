using DoubleVPartners.BackEnd.Domain.Common.Dtos;

namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Security
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
        string GetCurrentToken();
    }
}
