using MediatR;
using System;

namespace BigEcommerce.Producer.Sales.Application.Commands.DeleteStock
{
    public class DeleteStockCommand : IRequest
    {
        public Guid ProductId { get; set; }

        public DeleteStockCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}