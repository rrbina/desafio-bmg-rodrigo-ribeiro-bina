using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Services.Payments
{
    public class CardPaymentService : ICardPaymentService
    {
        private readonly ISaleService _saleService;
        private readonly IEventPublisher _publisher;

        public CardPaymentService(ISaleService saleService, IEventPublisher publisher)
        {
            _saleService = saleService;
            _publisher = publisher;
        }

        public async Task<bool> ProcessCardAsync(CardPaymentDto dto)
        {
            var sale = await _saleService.GetSaleByIdAsync(dto.OrderId);
            if (sale == null)
                throw new Exception("Pedido não encontrado.");

            await _publisher.PublishAsync(new CardPaymentCreatedEvent(dto.OrderId));

            return true;
        }
    }
}