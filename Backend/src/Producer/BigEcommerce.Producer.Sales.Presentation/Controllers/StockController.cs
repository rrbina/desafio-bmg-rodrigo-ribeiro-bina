using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Commands.CreateStock;
using BigEcommerce.Producer.Sales.Application.Commands.UpdateStock;
using BigEcommerce.Producer.Sales.Application.Commands.DeleteStock;
using BigEcommerce.Producer.Sales.Application.Commands.SelectStock;
using Microsoft.AspNetCore.Authorization;

namespace BigEcommerce.Producer.Sales.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStockCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            await _mediator.Send(new DeleteStockCommand(productId));
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStocksCommand());
            return Ok(result);
        }
    }
}