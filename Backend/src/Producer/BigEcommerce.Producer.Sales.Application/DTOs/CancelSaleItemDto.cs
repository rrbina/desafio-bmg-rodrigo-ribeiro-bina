﻿using BigEcommerce.Producer.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CancelSaleItemDto
    {
        public SaleDto Sale { get; set; }
        public SaleItemDto Item { get; set; }

        public CancelSaleItemDto(SaleDto sale, SaleItemDto item)
        {
            Sale = sale;
            Item = item;
        }

        public CancelSaleItemDto()
        {
                
        }
    }
}
