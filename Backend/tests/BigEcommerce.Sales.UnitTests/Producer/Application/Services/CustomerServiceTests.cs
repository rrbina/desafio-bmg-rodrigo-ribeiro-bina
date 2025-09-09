using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using BigEcommerce.Producer.Sales.Application.DTOs;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomerAsync_Should_Create_And_Return_CustomerDto()
        {
            var command = new CreateCustomerCommand("Cliente", "123456", "admin2@admin.com");

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.CreateCustomerAsync(command);

            Assert.NotNull(result);
            Assert.Equal(command.CustomerName, result.CustomerName);
        }

        [Fact]
        public async Task GetAllCustomersAsync_Should_Return_All_Customers()
        {
            var list = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), CustomerName = "A" },
                new Customer { Id = Guid.NewGuid(), CustomerName = "B" }
            };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.GetAllCustomersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCustomerByIdAsync_Should_Return_CustomerDto_When_Found()
        {
            var id = Guid.NewGuid();
            var hasher = new PasswordHasher();
            hasher.CreateHash("123456", out var hash, out var salt);

            var customer = new Customer
            {
                Id = id,
                CustomerName = "Cliente",
                PasswordHash = hash,
                PasswordSalt = salt
            };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);

            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.GetCustomerByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_Should_Return_Null_When_Not_Found()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null!);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.GetCustomerByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCustomerAsync_Should_Update_And_Return_CustomerDto()
        {
            var id = Guid.NewGuid();
            var command = new UpdateCustomerCommand(id, "Novo Nome", "123456");
            var customer = new Customer { Id = id, CustomerName = "Antigo Nome" };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);
            repoMock.Setup(r => r.UpdateAsync(customer)).Returns(Task.CompletedTask);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.UpdateCustomerAsync(command);

            Assert.Equal(command.CustomerName, result.CustomerName);
        }

        [Fact]
        public async Task DeleteCustomerAsync_Should_Delete_And_Return_CustomerDto()
        {
            var id = Guid.NewGuid();
            var customer = new Customer { Id = id, CustomerName = "Cliente" };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);
            repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            var result = await service.DeleteCustomerAsync(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteCustomerAsync_Should_Throw_When_Not_Found()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null!);

            var hasher = new PasswordHasher();
            var service = new CustomerService(repoMock.Object, hasher);

            await Assert.ThrowsAsync<BigEcommerceException>(() => service.DeleteCustomerAsync(Guid.NewGuid()));
        }
    }
}
