using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.SelectCustomer
{
    [ExcludeFromCodeCoverage]
    public class GetCustomerByIdCommand : IRequest<CustomerDto?>
    {
        public Guid CustomerId { get; }

        public GetCustomerByIdCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}