using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Contracts.Debts;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleVPartners.BackEnd.Application.Debts.Queries.GetById
{
    public class GetDebtByIdQueryHandler(ICache _cache, IUnitOfWork _unit) : IRequestHandler<GetDebtByIdQuery, ErrorOr<DebtResponse>>
    {
        private readonly IGenericRepository<UserDebt> _debt = _unit.GenericRepository<UserDebt>();

        public async Task<ErrorOr<DebtResponse>> Handle(GetDebtByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = $"{nameof(GetDebtQueryHandler)}_{request.Id}";
                var cached = await _cache.Get<DebtResponse>(cacheKey);
                if (!cached.IsError)
                {
                    return cached.Value;
                }

                var debtId = UserDebtId.Create(request.Id);
                var debt = await _debt.FirstOrDefaultAsync(d => d.Id == debtId);
                if (debt is null)
                {
                    return Error.Failure(description: "Debt not found.");
                }

                var response = debt.Adapt<DebtResponse>();
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
