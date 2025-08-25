using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public class AlterDebtFactory(IUnitOfWork _unit, ICurrentUserProvider _current)
    {
        private readonly IGenericRepository<User> _user = _unit.GenericRepository<User>();
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        private UserDebt Create(User user, AlterDebtCommand request)
        {
            return user.AddDebt(request.Amount, request.Name);
        }

        private UserDebt Update(User user, AlterDebtCommand request)
        {
            var userDebtId = UserDebtId.Create(request.Id);
            return user.AlterDebt(userDebtId, request.Name, request.Amount);
        }

        internal async Task<ErrorOr<(Action<UserDebt> Processor, UserDebt Debt)>> GetProcessor(AlterDebtCommand request)
        {
            try
            {
                var current = _current.GetCurrentUser();
                var userId = UserId.Create(current.Id);
                var user = await _user.FirstOrDefaultAsync(u => u.Id == userId, "Debts");
                if (user is null)
                {
                    return Error.NotFound(description: "User not found.");
                }

                if (string.IsNullOrEmpty(request.Id))
                {
                    return (_debt.Add, Create(user, request));
                }

                return (_debt.Update, Update(user, request));
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
