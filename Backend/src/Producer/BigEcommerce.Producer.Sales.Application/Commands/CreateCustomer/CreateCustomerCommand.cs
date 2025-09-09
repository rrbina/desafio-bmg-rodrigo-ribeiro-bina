using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer
{
    [ExcludeFromCodeCoverage]
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string CustomerName { get; set; }
        public string CustomerPassword{ get; set; }
        public string CustomerEmail { get; set; }        

        public CreateCustomerCommand() { }

        public CreateCustomerCommand(string customerName, string customerPassword, string customerEmail)
        {
            CustomerName = customerName;
            CustomerPassword = customerPassword;
            CustomerEmail = customerEmail;
        }
    }
}