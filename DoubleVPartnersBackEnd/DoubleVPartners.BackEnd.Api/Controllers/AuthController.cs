using DoubleVPartners.BackEnd.Api.Controllers.Common;
using DoubleVPartners.BackEnd.Application.Auths.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleVPartners.BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _mediator) : ApiController
    {
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(GetAuthQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Match(
                id => Ok(id),
                err => Problem(err)
            );
        }
    }
}
