using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Debts.Commands.Remove
{
    public record RemoveDebtCommand(string Id, string UserId) : IRequest<ErrorOr<Success>>;
}
