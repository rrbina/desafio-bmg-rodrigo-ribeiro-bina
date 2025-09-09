using Moq;
using BigEcommerce.Producer.Sales.Application.Services.Customers;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteCustomer;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Call_DeleteCustomerAsync_And_PublishEvent_And_Return_EventId()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            var customerId = Guid.NewGuid();
            var command = new DeleteCustomerCommand(customerId);

            var handler = new DeleteCustomerCommandHandler(customerServiceMock.Object, eventPublisherMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<CustomerDeletedEvent>(e =>
                e.CustomerId == customerId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}