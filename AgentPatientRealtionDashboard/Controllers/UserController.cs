using App.Core.Apps.User.Command;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentPatientRealtionDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto register)
        {
            var result = await _mediator.Send(new RegisterUserCommand { register = register });
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _mediator.Send(new LoginUserCommand { login = login });
            return Ok(result);
        }
    }
}
