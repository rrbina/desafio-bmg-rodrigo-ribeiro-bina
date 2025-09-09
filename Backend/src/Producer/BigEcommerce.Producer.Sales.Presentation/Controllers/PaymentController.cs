using BigEcommerce.Producer.Sales.Application.Commands.CardPayment;
using BigEcommerce.Producer.Sales.Application.Commands.UsePixPayment;
using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigEcommerce.Producer.Sales.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("pix")]
        public async Task<IActionResult> PayWithPix([FromBody] PixPaymentDto dto)
        {
            var result = await _mediator.Send(new PixPaymentCommand(dto));
            return Ok(result);
        }

        [HttpPost("card")]
        public async Task<IActionResult> PayWithCard([FromBody] CardPaymentDto dto)
        {
            var result = await _mediator.Send(new CardPaymentCommand(dto));
            return Ok(result);
        }
    }
}