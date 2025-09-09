using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;

namespace BigEcommerce.Producer.Sales.Application.Commands.DeleteStock
{
    public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand>
    {
        private readonly IStockService _stockService;

        public DeleteStockCommandHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Unit> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            await _stockService.DeleteStockAsync(request.ProductId);
            return Unit.Value;
        }
    }
}