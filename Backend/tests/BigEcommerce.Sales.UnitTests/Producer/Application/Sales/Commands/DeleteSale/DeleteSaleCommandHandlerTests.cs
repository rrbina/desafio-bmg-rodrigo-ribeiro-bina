using Moq;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteSale;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using BigEcommerce.Producer.Sales.Domain.Entities;


namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_CallDeleteSaleAsync_And_PublishEvent_And_ReturnEventId()
        {
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);

            var sale = new Sale
            {
                SaleNumber = saleId,
                SaleDate = DateTime.UtcNow,
                Customer = new Customer(),
                Items = new List<SaleItem>()
            };

            var saleServiceMock = new Mock<ISaleService>();
            saleServiceMock
                .Setup(s => s.DeleteSaleAsync(saleId))
                .ReturnsAsync(new SaleDto(sale));

            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock
                .Setup(e => e.PublishAsync(It.IsAny<SaleDeletedEvent>()))
                .Returns(Task.CompletedTask);

            var handler = new DeleteSaleCommandHandler(eventPublisherMock.Object, saleServiceMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            saleServiceMock.Verify(s => s.DeleteSaleAsync(saleId), Times.Once);

            eventPublisherMock.Verify(e => e.PublishAsync(It.Is<SaleDeletedEvent>(ev =>
                ev.SaleNumber == saleId &&
                ev.EventId == result
            )), Times.Once);
        }
    }
}