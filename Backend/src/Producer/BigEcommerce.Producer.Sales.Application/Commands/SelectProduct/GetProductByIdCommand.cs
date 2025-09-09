using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.SelectProduct
{
    [ExcludeFromCodeCoverage]
    public class GetProductByIdCommand : IRequest<ProductDto?>
    {
        public Guid ProductId { get; }

        public GetProductByIdCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}