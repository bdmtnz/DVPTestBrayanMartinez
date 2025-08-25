using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public class AlterDebtCommandHandler(AlterDebtFactory _factory, IUnitOfWork _unit) : IRequestHandler<AlterDebtCommand, ErrorOr<UserId>>
    {
        public async Task<ErrorOr<UserId>> Handle(AlterDebtCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _factory.GetProcessor(request);
                if (result.IsError)
                {
                    return result.Errors;
                }

                result.Value.Processor(result.Value.User);
                await _unit.SaveChangesAsync(cancellationToken);

                return result.Value.User.Id;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
