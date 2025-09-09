using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BigEcommerce.Producer.Sales.Domain.Entities;

namespace BigEcommerce.Producer.Sales.Application.Services.Stocks
{
    public interface IStockService
    {
        Task<Stock?> GetByProductIdAsync(Guid productId);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task AddStockAsync(Stock stock);
        Task UpdateStockAsync(Guid productId, int quantityChange);
        Task DeleteStockAsync(Guid productId);
    }
}