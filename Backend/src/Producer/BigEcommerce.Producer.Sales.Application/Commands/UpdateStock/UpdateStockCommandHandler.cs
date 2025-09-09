using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;

namespace BigEcommerce.Producer.Sales.Application.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand>
    {
        private readonly IStockService _stockService;

        public UpdateStockCommandHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            await _stockService.UpdateStockAsync(request.ProductId, request.QuantityChange);
            return Unit.Value;
        }
    }
}