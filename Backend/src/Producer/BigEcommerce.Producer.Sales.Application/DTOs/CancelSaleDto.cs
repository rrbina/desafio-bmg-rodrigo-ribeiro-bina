using BigEcommerce.Producer.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CancelSaleDto
    {
        public Sale sale { get; set; }
        public CancelSaleDto(Sale _sale)
        {
            sale = _sale;
        }
    }
}
