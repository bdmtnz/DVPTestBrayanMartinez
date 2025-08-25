using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Remove
{
    public class RemoveDebtCommandHandler(IUnitOfWork _unit) : IRequestHandler<RemoveDebtCommand, ErrorOr<Success>>
    {
        private readonly IGenericRepository<User> _user = _unit.GenericRepository<User>();

        public async Task<ErrorOr<Success>> Handle(RemoveDebtCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = UserId.Create(request.UserId);
                var user = await _user.FirstOrDefaultAsync(u => u.Id == userId);
                if (user is null)
                {
                    return Error.NotFound(description: "User not found.");
                }

                user.RemoveDebt(UserDebtId.Create(request.Id));
                _user.Update(user);
                await _unit.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
