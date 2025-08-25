using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public record AlterDebtCommand(string? Id, string UserId, string Name, decimal Amount) : IRequest<ErrorOr<UserId>>;
}
