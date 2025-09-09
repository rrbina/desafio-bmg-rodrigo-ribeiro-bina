using System;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public Customer()
        {
            
        }
    }
}