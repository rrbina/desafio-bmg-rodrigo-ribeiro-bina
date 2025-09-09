using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer
{
    [ExcludeFromCodeCoverage]
    public class UpdateCustomerCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerEmail { get; set; }

        public UpdateCustomerCommand() { }

        public UpdateCustomerCommand(Guid id, string customerName, string customerPassword)
        {
            Id = id;
            CustomerName = customerName;
            CustomerPassword = customerPassword;
        }
    }
}