using MediatR;
using System;

namespace BigEcommerce.Producer.Sales.Application.Commands.CreateStock
{
    public class CreateStockCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public CreateStockCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}