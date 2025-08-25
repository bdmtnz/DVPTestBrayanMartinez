using DoubleVPartners.BackEnd.Api.Controllers.Common;
using DoubleVPartners.BackEnd.Application.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.BackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ISender _mediator) : ApiController
    {
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
