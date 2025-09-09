using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer;
using BigEcommerce.Producer.Sales.Application.Services.Customers;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Application.Services.Sales;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_PublishEvent_AndReturnEventId_When_CustomerIsCreated()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var command = new CreateCustomerCommand("Cliente", "123456", "admin@email.com");

            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock
                .Setup(s => s.CreateCustomerAsync(command))
                .ReturnsAsync(new CustomerDto { Id = customerId, CustomerName = "Cliente", Email = command.CustomerEmail });

            var eventPublisherMock = new Mock<IEventPublisher>();

            var hasher = new PasswordHasher();
            var handler = new CreateCustomerCommandHandler(customerServiceMock.Object, eventPublisherMock.Object, hasher);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<CustomerCreatedEvent>(e =>
                e.CustomerId == customerId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}