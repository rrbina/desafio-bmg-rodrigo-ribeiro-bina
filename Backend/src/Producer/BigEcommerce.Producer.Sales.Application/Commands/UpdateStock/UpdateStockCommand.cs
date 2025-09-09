using MediatR;
using System;

namespace BigEcommerce.Producer.Sales.Application.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public int QuantityChange { get; set; } // positivo ou negativo

        public UpdateStockCommand(Guid productId, int quantityChange)
        {
            ProductId = productId;
            QuantityChange = quantityChange;
        }
    }
}