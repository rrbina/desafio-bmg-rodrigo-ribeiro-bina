using System.Collections.Generic;
using MediatR;
using BigEcommerce.Producer.Sales.Domain.Entities;

namespace BigEcommerce.Producer.Sales.Application.Commands.SelectStock
{
    public class GetAllStocksCommand : IRequest<IEnumerable<Stock>>
    {
    }
}