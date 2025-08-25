using DoubleVPartners.BackEnd.Api.Common.Attributes;
using DoubleVPartners.BackEnd.Api.Controllers.Common;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Alter;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Pay;
using DoubleVPartners.BackEnd.Application.Debts.Commands.Remove;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.BackEnd.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController(ISender _mediator) : ApiController
    {
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
        public async Task<IActionResult> PayDebt(string id, string debtId)
        {
            var response = await _mediator.Send(new PayDebtCommand(id, debtId));
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpDelete(Name = "DeleteDebt")]
        public async Task<IActionResult> DeleteDebt(string id, string debtId)
        {
            var response = await _mediator.Send(new RemoveDebtCommand(id, debtId));
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }
    }
}
