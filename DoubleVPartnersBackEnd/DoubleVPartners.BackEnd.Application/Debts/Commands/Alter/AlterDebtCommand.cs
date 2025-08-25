using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Alter
{
    public record AlterDebtCommand(string? Id, string Name, decimal Amount) : IRequest<ErrorOr<UserDebtId>>;
}
