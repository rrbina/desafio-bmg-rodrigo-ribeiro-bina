using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.SelectCustomer
{
    [ExcludeFromCodeCoverage]
    public class GetAllCustomersCommand : IRequest<IEnumerable<CustomerDto>>
    {
    }
}