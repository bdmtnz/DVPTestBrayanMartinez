using DoubleVPartners.BackEnd.Contracts.Auths;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Auths.Queries.Get
{
    public class GetAuthQueryHandler(IUnitOfWork _unit, IJwtHandler _jwt) : IRequestHandler<GetAuthQuery, ErrorOr<AuthResponse>>
    {
        private readonly IGenericRepository<User> _user = _unit.GenericRepository<User>();
        private readonly IGenericRepository<UserCredential> _credential = _unit.GenericRepository<UserCredential>();

        public async Task<ErrorOr<AuthResponse>> Handle(GetAuthQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userKey = UserCredentialKey.Create(request.Email, request.Password);
                var credential = await _credential.FirstOrDefaultAsync(c => c.Key == userKey);
                if (credential is null)
                {
                    return Error.Failure(description: "Invalid credentials.");
                }

                var user = await _user.FirstOrDefaultAsync(u => u.Id == credential.UserId);
                if (user is null)
                {
                    return Error.Failure(description: "User not found.");
                }

                var jwt = _jwt.Generate(user);
                return new AuthResponse(user.Id.Value, user.Name, jwt);
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
