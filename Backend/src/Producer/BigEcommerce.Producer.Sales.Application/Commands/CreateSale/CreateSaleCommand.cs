using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateSale
{
    [ExcludeFromCodeCoverage]
    public class CreateSaleCommand : IRequest<Guid>
    {
        public SaleDto SaleDto { get; set; }

        public CreateSaleCommand()
        {
        }
    }    
}
