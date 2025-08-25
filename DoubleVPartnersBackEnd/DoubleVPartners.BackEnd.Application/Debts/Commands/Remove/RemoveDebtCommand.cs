using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Remove
{
    public record RemoveDebtCommand(string Id) : IRequest<ErrorOr<Success>>;
}
