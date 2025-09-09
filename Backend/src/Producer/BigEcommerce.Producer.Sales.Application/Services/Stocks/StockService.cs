using System;
using System.Threading.Tasks;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigEcommerce.Producer.Sales.Application.Services.Stocks
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task UpdateStockAsync(Guid productId, int quantityChange)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId);

            if (stock == null)
                throw new InvalidOperationException("Stock not found for this product.");

            var newQuantity = stock.Quantity + quantityChange;

            if (newQuantity < 0)
                throw new InvalidOperationException("Insufficient stock.");

            stock.Quantity = newQuantity;

            await _stockRepository.UpdateAsync(stock);
        }

        public async Task DeleteStockAsync(Guid productId)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId);

            if (stock == null)
                return;

            await _stockRepository.DeleteAsync(stock);
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _stockRepository.GetAllAsync();
        }

        public async Task<Stock?> GetByProductIdAsync(Guid productId)
        {
            return await _stockRepository.GetByProductIdAsync(productId);
        }
    }
}