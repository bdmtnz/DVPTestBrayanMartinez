using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Application.Debts.Queries.GetById;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Pay
{
    public class PayDebtCommandHandler(ICache _cache, IUnitOfWork _unit, ICurrentUserProvider _current) : IRequestHandler<PayDebtCommand, ErrorOr<Success>>
    {
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        public async Task<ErrorOr<Success>> Handle(PayDebtCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var current = _current.GetCurrentUser();
                if (current is null)
                {
                    return Error.Failure(description: "Unauthorized.");
                }

                var debt = await _debt.FirstOrDefaultAsync(d => d.Id == UserDebtId.Create(request.Id));
                if (debt is null)
                {
                    return Error.Failure(description: "Debt not found.");
                }

                debt.Pay();
                _debt.Update(debt);
                await _unit.SaveChangesAsync(cancellationToken);

                await _cache.Remove($"{nameof(GetDebtQueryHandler)}_{current.Id}");
                await _cache.Remove($"{nameof(GetDebtByIdQueryHandler)}_{request.Id}");

                return Result.Success;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
