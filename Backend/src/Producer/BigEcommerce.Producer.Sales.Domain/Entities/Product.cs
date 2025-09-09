using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        public Guid Id { get; set; }        
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Stock Stock { get; set; }

    }
}
