using DoubleVPartners.BackEnd.Contracts.Debts;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleVPartners.BackEnd.Application.Debts.Queries.Get
{
    public class GetDebtQueryHandler(IMapper _mapper, ICache _cache, IUnitOfWork _unit, ICurrentUserProvider _current) : IRequestHandler<GetDebtQuery, ErrorOr<List<DebtResponse>>>
    {
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        public async Task<ErrorOr<List<DebtResponse>>> Handle(GetDebtQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var current = _current.GetCurrentUser();
                if (current is null)
                {
                    return Error.Failure(description: "Unauthorized.");
                }

                var cacheKey = $"{nameof(GetDebtQueryHandler)}_{current.Id}";
                var cached = await _cache.Get<List<DebtResponse>>(cacheKey);
                if (!cached.IsError)
                {
                    return cached.Value;
                }

                var userId = UserId.Create(current.Id);
                var debts = await _debt.Where(d => d.UserId == userId);

                List<DebtResponse> response = debts.ToList().ConvertAll(_mapper.Map<DebtResponse>);
                await _cache.Set(cacheKey, response);

                return response;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
