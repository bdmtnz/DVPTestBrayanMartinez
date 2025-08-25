using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Pay
{
    public record PayDebtCommand(string Id) : IRequest<ErrorOr<Success>>;
}
