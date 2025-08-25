using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Contracts.Debts;
using DoubleVPartners.BackEnd.Contracts.Users;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace DoubleVPartners.BackEnd.Application.Users.Queries.Dashboard
{
    public class GetDashboardQueryHandler(ICache _cache, IUnitOfWork _unit, ICurrentUserProvider _current) : IRequestHandler<GetDashboardQuery, ErrorOr<DashboardResponse>>
    {
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        public async Task<ErrorOr<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var current = _current.GetCurrentUser();
                if (current is null)
                {
                    return Error.Failure(description: "Unauthorized.");
                }

                var cacheKey = $"{nameof(GetDashboardQueryHandler)}_{current.Id}";
                var cached = await _cache.Get<DashboardResponse>(cacheKey);
                if (!cached.IsError)
                {
                    return cached.Value;
                }

                var userId = UserId.Create(current.Id);
                var debts = await _debt.Count(d => d.UserId == userId);
                var unpaids = await _debt.Count(d => d.UserId == userId && d.PaidOnUtc == null);
                var paids = await _debt.Count(d => d.UserId == userId && d.PaidOnUtc != null);
                var pendient = await _debt.Sum(d => d.Amount, d => d.UserId == userId && d.PaidOnUtc == null);

                var response = new DashboardResponse(debts, paids, unpaids, pendient);
                await _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

                return response;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
