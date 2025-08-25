using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Users.Commands.Create
{
    public record CreateUserCommand(string Name, string Email, string Password) : IRequest<ErrorOr<UserId>>;
}
