using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateProduct;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateProduct;
using BigEcommerce.Producer.Sales.Application.Services.Products;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;

namespace BigEcommerce.Producer.Sales.Application.Services.Sales
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public ProductService(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = command.ProductName,
                UnitPrice = command.UnitPrice
            };

            await _productRepository.AddAsync(product);

            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = 0
            };

            await _stockRepository.AddAsync(stock);

            return new ProductDto(product);
        }


        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product is null ? null : new ProductDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto(p));
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null)
                throw new BigEcommerceException("Produto não encontrado.");

            product.ProductName = command.ProductName;
            product.UnitPrice = command.UnitPrice;

            await _productRepository.UpdateAsync(product);
            return new ProductDto(product);
        }

        public async Task<ProductDto> DeleteProductAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new BigEcommerceException("Produto não encontrado.");

            var stock = await _stockRepository.GetByProductIdAsync(productId);
            if (stock != null)
                await _stockRepository.DeleteAsync(stock);

            await _productRepository.DeleteAsync(productId);

            return new ProductDto(product);
        }

    }
}