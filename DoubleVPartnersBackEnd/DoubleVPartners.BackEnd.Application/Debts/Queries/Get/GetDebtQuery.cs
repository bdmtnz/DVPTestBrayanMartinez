using DoubleVPartners.BackEnd.Contracts.Debts;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleVPartners.BackEnd.Application.Debts.Queries.Get
{
    public record GetDebtQuery(): IRequest<ErrorOr<List<DebtResponse>>>;
}
