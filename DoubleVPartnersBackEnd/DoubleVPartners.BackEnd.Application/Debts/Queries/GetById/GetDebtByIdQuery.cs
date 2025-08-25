using DoubleVPartners.BackEnd.Contracts.Debts;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleVPartners.BackEnd.Application.Debts.Queries.GetById
{
    public record GetDebtByIdQuery(string Id) : IRequest<ErrorOr<DebtResponse>>;
}
