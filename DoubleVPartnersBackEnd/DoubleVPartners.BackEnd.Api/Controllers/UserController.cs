using DoubleVPartners.BackEnd.Api.Controllers.Common;
using DoubleVPartners.BackEnd.Application.Users.Commands.Create;
using DoubleVPartners.BackEnd.Application.Users.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ISender _mediator) : ApiController
    {
        [HttpGet("Dashboard", Name = "Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var response = await _mediator.Send(new GetDashboardQuery());
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }
    }
}
