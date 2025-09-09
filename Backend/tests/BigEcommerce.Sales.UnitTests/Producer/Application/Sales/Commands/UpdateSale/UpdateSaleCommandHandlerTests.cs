using Moq;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateSale;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.Common;

using BigEcommerce.Sales.Messages.IntegrationEvents;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_UpdateSale_And_PublishEvent_And_ReturnEventId()
        {
            var saleServiceMock = new Mock<ISaleService>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = new()
            };

            var saleDto = new SaleDto
            {
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                BranchName = command.BranchName,
                TotalAmount = 100.0m,
                Items = command.Items
            };

            saleServiceMock
                .Setup(x => x.UpdateSaleAsync(command))
                .ReturnsAsync(saleDto);

            eventPublisherMock
                .Setup(x => x.PublishAsync(It.IsAny<SaleModifiedEvent>()))
                .Returns(Task.CompletedTask);

            var handler = new UpdateSaleCommandHandler(eventPublisherMock.Object, saleServiceMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            saleServiceMock.Verify(x => x.UpdateSaleAsync(command), Times.Once);

            eventPublisherMock.Verify(x => x.PublishAsync(It.Is<SaleModifiedEvent>(e =>
                e.SaleNumber == command.SaleNumber &&
                e.NewTotalAmount == saleDto.TotalAmount &&
                e.EventId == result
            )), Times.Once);
        }
    }
}