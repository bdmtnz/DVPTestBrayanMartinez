using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.Common.Dtos;

namespace DoubleVPartners.BackEnd.Infrastructure.Security.Providers
{
    public class CurrentUserProvider(IHttpProvider _httpAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            ArgumentNullException.ThrowIfNull(_httpAccessor, nameof(_httpAccessor));

            string id = _httpAccessor.GetSingleClaimValue("id");
            string name = _httpAccessor.GetSingleClaimValue("name");

            return new CurrentUser(id, name);
        }

        public string GetCurrentToken()
            => _httpAccessor.GetJwtToken()!;
    }
}
