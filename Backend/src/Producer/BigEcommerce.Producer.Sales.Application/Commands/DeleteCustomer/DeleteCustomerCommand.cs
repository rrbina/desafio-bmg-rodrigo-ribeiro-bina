using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteCustomer
{
    [ExcludeFromCodeCoverage]
    public class DeleteCustomerCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; }

        public DeleteCustomerCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}