using DoubleVPartners.BackEnd.Contracts.Auths;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Auths.Queries.Get
{
    public record GetAuthQuery(string Email, string Password) : IRequest<ErrorOr<AuthResponse>>;
}
