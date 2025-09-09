using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.SelectSale
{
    [ExcludeFromCodeCoverage]
    public class GetAllSalesCommand : IRequest<IEnumerable<SaleDto>>
    {
    }
}