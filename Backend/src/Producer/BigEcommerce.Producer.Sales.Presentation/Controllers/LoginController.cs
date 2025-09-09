using MediatR;
using Microsoft.AspNetCore.Mvc;
using BigEcommerce.Producer.Sales.Application.Commands.Login;
using BigEcommerce.Producer.Sales.Application.DTOs;

namespace BigEcommerce.Sales.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _mediator.Send(new LoginCommand(request.Email, request.Password));
                return Ok(new LoginResponse { Token = token });
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}