using DoubleVPartners.BackEnd.Infrastructure.Security;
using System.Security.Claims;

namespace DoubleVPartners.BackEnd.Api.Common.Middleware
{
    public class JwtMiddleware(JwtHandler _jwt) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Items.Clear();

            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                List<Claim> claims = new List<Claim>();
                claims.AddRange(_jwt.Validate(token));
                context.Items["Claims"] = claims;
            }
            catch { }

            await next(context);
        }
    }
}
