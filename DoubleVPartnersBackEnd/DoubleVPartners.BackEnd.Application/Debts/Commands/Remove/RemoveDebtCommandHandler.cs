using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Application.Debts.Queries.GetById;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Remove
{
    public class RemoveDebtCommandHandler(ICache _cache, IUnitOfWork _unit, ICurrentUserProvider _current) : IRequestHandler<RemoveDebtCommand, ErrorOr<Success>>
    {
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        public async Task<ErrorOr<Success>> Handle(RemoveDebtCommand request, CancellationToken cancellationToken)
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

                if (debt.PaidOnUtc is not null)
                {
                    return Error.Failure(description: "Cannot alter a paid debt.");
                }

                _debt.Delete(debt);
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
