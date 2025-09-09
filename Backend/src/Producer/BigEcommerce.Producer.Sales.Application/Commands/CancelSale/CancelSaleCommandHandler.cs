﻿using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using System;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, Guid>
    {
        private readonly ISaleService _saleService;
        private readonly IEventPublisher _eventPublisher;

        public CancelSaleCommandHandler(ISaleService saleService, IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleService.CancelSaleAsync(request.SaleNumber);

            Console.WriteLine($"[EVENTO] SaleCancelled - SaleId: {sale.SaleNumber}");
            var eventId = Guid.NewGuid();
            await _eventPublisher.PublishAsync(new SaleCancelledEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                SaleNumber = sale.SaleNumber ?? Guid.Empty,
                CancelledAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}