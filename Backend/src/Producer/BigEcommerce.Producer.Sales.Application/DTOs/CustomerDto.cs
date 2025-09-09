using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using static System.Formats.Asn1.AsnWriter;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CustomerDto() { }

        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            CustomerName = customer.CustomerName;
            Email = customer.CustomerEmail;
        }
    }
}