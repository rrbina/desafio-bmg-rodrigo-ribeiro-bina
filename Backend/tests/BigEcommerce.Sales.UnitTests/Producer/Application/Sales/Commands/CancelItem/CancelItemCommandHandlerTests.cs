using Moq;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CancelItem;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.CancelItem
{
    public class CancelItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_CallCancelItemAsync_AndPublishEvent_AndReturnEventId()
        {
            var saleId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var sale = new Sale
            {
                SaleNumber = saleId,
                SaleDate = DateTime.UtcNow,
                Customer = new Customer(),
                Items = new List<SaleItem>()
            };

            var item = new SaleItem
            {
                Id = itemId,
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                Discount = 0,
                Product = new Product { ProductName = "Produto", UnitPrice = 10 }
            };

            var dto = new CancelSaleItemDto(new SaleDto(sale), new SaleItemDto(item));

            var saleServiceMock = new Mock<ISaleService>();
            saleServiceMock
                .Setup(s => s.CancelItemAsync(saleId, itemId))
                .ReturnsAsync(dto);

            var eventPublisherMock = new Mock<IEventPublisher>();

            var handler = new CancelItemCommandHandler(eventPublisherMock.Object, saleServiceMock.Object);
            var result = await handler.Handle(new CancelItemCommand(saleId, itemId), CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<ItemCancelledEvent>(e =>
                e.SaleNumber == saleId &&
                e.ItemId == item.ProductId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}