using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace DoubleVPartners.BackEnd.Infrastructure.Security.Providers
{
    public class HttpProvider(IHttpContextAccessor _httpContextAccessor) : IHttpProvider
    {
        public string? GetJwtToken()
        {
            return _httpContextAccessor.HttpContext!.Request.Headers["Authorization"];
        }

        public List<string> GetClaimValues(string claimType)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return [];
            }

            if (!_httpContextAccessor.HttpContext!.Items.ContainsKey("Claims"))
            {
                return [];
            }

            if (_httpContextAccessor.HttpContext!.Items["Claims"] is List<Claim> claims)
            {
                return claims
                    .Where(claim => claim.Type == claimType)
                    .Select(claim => claim.Value)
                    .ToList();
            }

            return [];
        }

        public string GetSingleClaimValue(string claimType)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return string.Empty;
            }

            if (!_httpContextAccessor.HttpContext!.Items.ContainsKey("Claims"))
            {
                return string.Empty;
            }

            if (_httpContextAccessor.HttpContext!.Items["Claims"] is List<Claim> claims)
            {
                return claims
                    .Single(claim => claim.Type == claimType)
                    .Value;
            }

            return string.Empty;
        }

        public string GetSingleHeaderValue(string key)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return string.Empty;
            }

            if (!_httpContextAccessor.HttpContext!.Request.Headers.ContainsKey(key))
            {
                return string.Empty;
            }

            StringValues stringValues = string.Empty;
            _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(key, out stringValues);

            var response = stringValues.FirstOrDefault() ?? string.Empty;
            return response;
        }
    }
}
