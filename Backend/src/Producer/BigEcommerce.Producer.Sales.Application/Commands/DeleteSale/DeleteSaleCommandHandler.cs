using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Producer.Sales.Application.Services;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using MediatR;
using System;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Guid>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaleService _saleService;

        public DeleteSaleCommandHandler(IEventPublisher eventPublisher, ISaleService saleService)
        {
            _eventPublisher = eventPublisher;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleService.DeleteSaleAsync(request.SaleNumber);
            Console.WriteLine($"[EVENTO] SaleCancelled - SaleId: {sale.SaleNumber}");
            var eventId = Guid.NewGuid();
            await _eventPublisher.PublishAsync(new SaleDeletedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                SaleNumber = sale.SaleNumber ?? Guid.Empty,
                DeletedAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}