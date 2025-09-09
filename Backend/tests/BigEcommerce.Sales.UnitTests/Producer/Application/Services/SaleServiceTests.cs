using Moq;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateSale;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateSale;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepo = new();
        private readonly Mock<ICustomerRepository> _customerRepo = new();
        private readonly Mock<IProductRepository> _productRepo = new();
        private readonly Mock<IStockRepository> _stockRepo = new();
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _service = new SaleService(
                _saleRepo.Object,
                _customerRepo.Object,
                _productRepo.Object,
                _stockRepo.Object
            );
        }


        [Fact]
        public async Task GetSaleByIdAsync_Returns_Null_WhenNotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            var result = await _service.GetSaleByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }       
        

        [Fact]
        public async Task DeleteSaleAsync_Throws_When_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<BigEcommerceException>(() => _service.DeleteSaleAsync(Guid.NewGuid()));
        }
        

        [Fact]
        public async Task CancelSaleAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<BigEcommerceException>(() => _service.CancelSaleAsync(Guid.NewGuid()));
        }

        

        [Fact]
        public async Task CancelItemAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<BigEcommerceException>(() =>
                _service.CancelItemAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task CancelItemAsync_Throws_When_Item_NotFound()
        {
            var sale = new Sale { SaleNumber = Guid.NewGuid(), Items = new List<SaleItem>() };

            _saleRepo.Setup(r => r.GetByIdAsync(sale.SaleNumber)).ReturnsAsync(sale);

            await Assert.ThrowsAsync<BigEcommerceException>(() =>
                _service.CancelItemAsync(sale.SaleNumber, Guid.NewGuid()));
        }    

        [Fact]
        public async Task UpdateSaleAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                Items = new List<SaleItemDto>()
            };

            await Assert.ThrowsAsync<BigEcommerceException>(() => _service.UpdateSaleAsync(command));
        }
        
        [Fact]
        public async Task CreateSaleAsync_Creates_And_Returns_Sale()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "Cliente",
                    BranchName = "Filial",
                    SaleDate = DateTime.UtcNow,
                    Items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10 }
            }
                }
            };

            var product = new Product { Id = command.SaleDto.Items.First().ProductId, UnitPrice = 10 };
            var stock = new Stock { ProductId = product.Id, Quantity = 10 };

            _customerRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Customer());
            _productRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            _stockRepo.Setup(r => r.GetByProductIdAsync(product.Id)).ReturnsAsync(stock);
            _stockRepo.Setup(r => r.UpdateAsync(It.IsAny<Stock>())).Returns(Task.CompletedTask);

            var result = await _service.CreateSaleAsync(command);

            Assert.NotNull(result);
            Assert.Equal(command.SaleDto.CustomerId, result.CustomerId);
        }
    }
}