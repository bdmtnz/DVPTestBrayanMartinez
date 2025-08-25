using DoubleVPartners.BackEnd.Api.Common.Attributes;
using DoubleVPartners.BackEnd.Api.Controllers.Common;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Alter;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Pay;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Remove;
using DoubleVPartners.BackEnd.Application.Debts.Queries.Get;
using DoubleVPartners.BackEnd.Application.Debts.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.BackEnd.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController(ISender _mediator) : ApiController
    {
        [HttpGet(Name = "GetDebts")]
        public async Task<IActionResult> GetDebts()
        {
            var response = await _mediator.Send(new GetDebtQuery());
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpGet("{id}",Name = "GetDebtById")]
        public async Task<IActionResult> GetDebtById(string id)
        {
            var response = await _mediator.Send(new GetDebtByIdQuery(id));
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpPost(Name = "AlterDebt")]
        public async Task<IActionResult> AlterDebt(AlterDebtCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpPatch(Name = "PayDebt")]
        public async Task<IActionResult> PayDebt(string id)
        {
            var response = await _mediator.Send(new PayDebtCommand(id));
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpDelete(Name = "DeleteDebt")]
        public async Task<IActionResult> DeleteDebt(string id)
        {
            var response = await _mediator.Send(new RemoveDebtCommand(id));
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }
    }
}
