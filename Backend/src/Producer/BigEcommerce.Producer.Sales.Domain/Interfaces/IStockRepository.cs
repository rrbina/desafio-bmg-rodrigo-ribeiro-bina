using System;
using System.Threading.Tasks;
using BigEcommerce.Producer.Sales.Domain.Entities;

namespace BigEcommerce.Producer.Sales.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock?> GetByProductIdAsync(Guid productId);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(Stock stock);
    }
}