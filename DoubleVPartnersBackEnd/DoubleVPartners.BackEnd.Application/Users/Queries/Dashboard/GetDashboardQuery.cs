using DoubleVPartners.BackEnd.Contracts.Users;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Users.Queries.Dashboard
{
    public record GetDashboardQuery() : IRequest<ErrorOr<DashboardResponse>>;
}
