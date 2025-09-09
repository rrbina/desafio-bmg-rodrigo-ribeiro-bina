using System;
using System.Threading.Tasks;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Interfaces;
using BigEcommerce.Producer.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BigEcommerce.Producer.Sales.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly SalesDbContext _context;

        public StockRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetByProductIdAsync(Guid productId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Stock stock)
        {
            _context.Stocks.Remove(stock);
            await Task.CompletedTask;
        }
    }
}