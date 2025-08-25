using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Application.Debts.Queries.GetById;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public class AlterDebtCommandHandler(ICache _cache, AlterDebtFactory _factory, IUnitOfWork _unit, ICurrentUserProvider _current) : IRequestHandler<AlterDebtCommand, ErrorOr<UserDebtId>>
    {
        public async Task<ErrorOr<UserDebtId>> Handle(AlterDebtCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var current = _current.GetCurrentUser();
                if (current is null)
                {
                    return Error.Failure(description: "Unauthorized.");
                }


                var result = await _factory.GetProcessor(request);
                if (result.IsError)
                {
                    return result.Errors;
                }

                result.Value.Processor(result.Value.Debt);
                await _unit.SaveChangesAsync(cancellationToken);

                await _cache.Remove($"{nameof(GetDebtQueryHandler)}_{current.Id}");
                await _cache.Remove($"{nameof(GetDebtByIdQueryHandler)}_{request.Id}");

                return result.Value.Debt.Id;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
