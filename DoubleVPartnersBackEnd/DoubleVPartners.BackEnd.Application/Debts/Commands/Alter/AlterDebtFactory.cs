using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public class AlterDebtFactory(IUnitOfWork _unit)
    {
        private readonly IGenericRepository<User> _user = _unit.GenericRepository<User>();

        private User Create(User user, AlterDebtCommand request)
        {
            return user.AddDebt(request.Amount, request.Name);
        }

        private User Update(User user, AlterDebtCommand request)
        {
            var userDebtId = UserDebtId.Create(request.Id);
            return user.AlterDebt(userDebtId, request.Name, request.Amount);
        }

        internal async Task<ErrorOr<(Action<User> Processor, User User)>> GetProcessor(AlterDebtCommand request)
        {
            try
            {
                var userId = UserId.Create(request.UserId);
                var user = await _user.FirstOrDefaultAsync(u => u.Id == userId);
                if (user is null)
                {
                    return Error.NotFound(description: "User not found.");
                }

                if (string.IsNullOrEmpty(request.Id))
                {
                    return (_user.Add, Create(user, request));
                }

                return (_user.Update, Update(user, request));
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
