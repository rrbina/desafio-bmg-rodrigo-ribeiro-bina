using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;
using BigEcommerce.Producer.Sales.Domain.Entities;

namespace BigEcommerce.Producer.Sales.Application.Commands.CreateStock
{
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand>
    {
        private readonly IStockService _stockService;

        public CreateStockCommandHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Unit> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _stockService.AddStockAsync(stock);
            return Unit.Value;
        }
    }
}