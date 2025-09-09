using BigEcommerce.Producer.Sales.Application.Builders;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateSale;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateSale;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;

namespace BigEcommerce.Producer.Sales.Application.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public SaleService(ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IStockRepository stockRepository)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<SaleDto> UpdateSaleAsync(UpdateSaleCommand command)
        {
            var existingSale = await _saleRepository.GetByIdAsync(command.SaleNumber);
            if (existingSale == null)
                throw new BigEcommerceException("Venda não encontrada.");

            foreach (var oldItem in existingSale.Items)
            {
                if (!oldItem.IsCancelled)
                {
                    var stock = await _stockRepository.GetByProductIdAsync(oldItem.ProductId);
                    if (stock == null)
                        throw new BigEcommerceException($"Estoque não encontrado para o produto {oldItem.ProductId}");

                    stock.Quantity += oldItem.Quantity;
                    await _stockRepository.UpdateAsync(stock);
                }
            }

            existingSale.SaleNumber = command.SaleNumber;
            existingSale.SaleDate = command.SaleDate;
            existingSale.CustomerId = command.CustomerId;
            existingSale.Customer.CustomerName = command.CustomerName;
            existingSale.BranchName = command.BranchName;
            existingSale.Items.Clear();

            SalesBuilder.SetSaleItems(existingSale, command.Items);

            foreach (var newItem in existingSale.Items)
            {
                var stock = await _stockRepository.GetByProductIdAsync(newItem.ProductId);
                if (stock == null)
                    throw new BigEcommerceException($"Estoque não encontrado para o produto {newItem.ProductId}");

                if (stock.Quantity < newItem.Quantity)
                    throw new BigEcommerceException($"Estoque insuficiente para o produto {newItem.ProductId}");

                stock.Quantity -= newItem.Quantity;
                await _stockRepository.UpdateAsync(stock);
            }

            await _saleRepository.UpdateAsync(existingSale);
            return new SaleDto(existingSale);
        }


        public async Task<SaleDto> DeleteSaleAsync(Guid SaleId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new BigEcommerceException("Venda não encontrada.");

            if (!sale.IsCancelled)
            {
                foreach (var item in sale.Items.Where(i => !i.IsCancelled))
                {
                    var stock = await _stockRepository.GetByProductIdAsync(item.ProductId);
                    if (stock == null)
                        throw new BigEcommerceException($"Estoque não encontrado para o produto {item.ProductId}");

                    stock.Quantity += item.Quantity;
                    await _stockRepository.UpdateAsync(stock);
                }
            }

            await _saleRepository.DeleteAsync(SaleId);
            return new SaleDto(sale);
        }

        public async Task<CancelSaleItemDto> CancelItemAsync(Guid SaleId, Guid ItemId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new BigEcommerceException("Venda não encontrada.");

            var item = sale.Items.FirstOrDefault(i => i.Id == ItemId);
            if (item == null)
                throw new BigEcommerceException("Item não encontrado.");

            item.IsCancelled = true;

            // 🟢 Repor estoque
            var stock = await _stockRepository.GetByProductIdAsync(item.ProductId);
            if (stock == null)
                throw new BigEcommerceException($"Estoque não encontrado para o produto {item.ProductId}");

            stock.Quantity += item.Quantity;
            await _stockRepository.UpdateAsync(stock);

            // 🟢 Recalcular o total da venda
            sale.TotalAmount = sale.Items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);

            await _saleRepository.UpdateAsync(sale);

            return new CancelSaleItemDto(new SaleDto(sale), new SaleItemDto(item));
        }

        public async Task<SaleDto> CancelSaleAsync(Guid SaleId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new BigEcommerceException("Venda não encontrada.");

            sale.IsCancelled = true;

            foreach (var item in sale.Items)
            {
                item.IsCancelled = true;

                var stock = await _stockRepository.GetByProductIdAsync(item.ProductId);
                if (stock == null)
                    throw new BigEcommerceException($"Estoque não encontrado para o produto {item.ProductId}");

                stock.Quantity += item.Quantity;
                await _stockRepository.UpdateAsync(stock);
            }

            sale.TotalAmount = 0;

            await _saleRepository.UpdateAsync(sale);

            return new SaleDto(sale);
        }


        public async Task<SaleDto> CreateSaleAsync(CreateSaleCommand command)
        {
            var sale = SalesBuilder.CreateSale(command);
            var customer = await _customerRepository.GetByIdAsync(sale.CustomerId);
            sale.Customer = customer ?? new Customer();

            foreach (var item in sale.Items)
            {
                if (item != null)
                    item.Product = await _productRepository.GetByIdAsync(item.ProductId) ?? new Product();
            }

            foreach (var item in sale.Items)
            {
                var stock = await _stockRepository.GetByProductIdAsync(item.ProductId);

                if (stock == null)
                    throw new BigEcommerceException($"Estoque não encontrado para o produto {item.ProductId}");

                if (stock.Quantity < item.Quantity)
                    throw new BigEcommerceException($"Estoque insuficiente para o produto {item.ProductId}");

                stock.Quantity -= item.Quantity;

                await _stockRepository.UpdateAsync(stock);
            }

            await _saleRepository.AddAsync(sale);

            return new SaleDto(sale);
        }


        public async Task<SaleDto?> GetSaleByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return sale == null ? null : new SaleDto(sale);
        }

        async Task<IEnumerable<SaleDto>> ISaleService.GetAllSaleAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            IEnumerable<SaleDto> saleDtos = sales.Select(sale => new SaleDto(sale));

            return saleDtos;
        }
    }
}