using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;
using BigEcommerce.Producer.Sales.Domain.Entities;

namespace BigEcommerce.Producer.Sales.Application.Commands.SelectStock
{
    public class GetAllStocksHandler : IRequestHandler<GetAllStocksCommand, IEnumerable<Stock>>
    {
        private readonly IStockService _stockService;

        public GetAllStocksHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IEnumerable<Stock>> Handle(GetAllStocksCommand request, CancellationToken cancellationToken)
        {
            return await _stockService.GetAllAsync();
        }
    }
}