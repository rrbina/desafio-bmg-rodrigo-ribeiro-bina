using Moq;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateProduct;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateProduct;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepo = new();
        private readonly Mock<IStockRepository> _stockRepo = new();

        private ProductService CreateService() =>
            new ProductService(_productRepo.Object, _stockRepo.Object);

        [Fact]
        public async Task CreateProductAsync_Should_Create_And_Return_ProductDto()
        {
            var command = new CreateProductCommand("Produto", 99.9m);

            _productRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _stockRepo.Setup(r => r.AddAsync(It.IsAny<Stock>())).Returns(Task.CompletedTask);

            var service = CreateService();

            var result = await service.CreateProductAsync(command);

            Assert.NotNull(result);
            Assert.Equal(command.ProductName, result.ProductName);
            Assert.Equal(command.UnitPrice, result.UnitPrice);
        }

        [Fact]
        public async Task GetProductByIdAsync_Should_Return_ProductDto_When_Found()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, ProductName = "Produto", UnitPrice = 10 };

            _productRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);

            var service = CreateService();

            var result = await service.GetProductByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Null_When_NotFound()
        {
            _productRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null!);

            var service = CreateService();

            var result = await service.GetProductByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllProductsAsync_Should_Return_All_Products()
        {
            var list = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductName = "P1", UnitPrice = 1 },
                new Product { Id = Guid.NewGuid(), ProductName = "P2", UnitPrice = 2 }
            };

            _productRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var service = CreateService();

            var result = await service.GetAllProductsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateProductAsync_Should_Update_And_Return_ProductDto()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, ProductName = "Antigo", UnitPrice = 10 };

            var command = new UpdateProductCommand(id, "Novo", 20);

            _productRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
            _productRepo.Setup(r => r.UpdateAsync(product)).Returns(Task.CompletedTask);

            var service = CreateService();

            var result = await service.UpdateProductAsync(command);

            Assert.Equal(command.ProductName, result.ProductName);
            Assert.Equal(command.UnitPrice, result.UnitPrice);
        }

        [Fact]
        public async Task UpdateProductAsync_Should_Throw_When_NotFound()
        {
            var command = new UpdateProductCommand(Guid.NewGuid(), "Produto", 10);

            _productRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null!);

            var service = CreateService();

            await Assert.ThrowsAsync<BigEcommerceException>(() => service.UpdateProductAsync(command));
        }

        [Fact]
        public async Task DeleteProductAsync_Should_Delete_And_Return_ProductDto()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, ProductName = "Produto" };
            var stock = new Stock { ProductId = id };

            _productRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
            _productRepo.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);
            _stockRepo.Setup(r => r.GetByProductIdAsync(id)).ReturnsAsync(stock);
            _stockRepo.Setup(r => r.DeleteAsync(stock)).Returns(Task.CompletedTask);

            var service = CreateService();

            var result = await service.DeleteProductAsync(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteProductAsync_Should_Throw_When_NotFound()
        {
            _productRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null!);

            var service = CreateService();

            await Assert.ThrowsAsync<BigEcommerceException>(() => service.DeleteProductAsync(Guid.NewGuid()));
        }
    }
}
