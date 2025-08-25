using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Users.Commands.Create
{
    public class CreateCommandHandler(IUnitOfWork _unit) : IRequestHandler<CreateUserCommand, ErrorOr<UserId>>
    {
        private readonly IGenericRepository<User> _user = _unit.GenericRepository<User>();

        public async Task<ErrorOr<UserId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.Create(request.Name, request.Email, request.Password);
                _user.Add(user);
                await _unit.SaveChangesAsync(cancellationToken);
                return user.Id;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
