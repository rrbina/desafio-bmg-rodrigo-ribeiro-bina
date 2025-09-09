using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Services.Payments
{
    public class PixPaymentService : IPixPaymentService
    {
        private readonly ISaleService _saleService;
        private readonly IEventPublisher _publisher;

        public PixPaymentService(ISaleService saleService, IEventPublisher publisher)
        {
            _saleService = saleService;
            _publisher = publisher;
        }

        public async Task<PixPaymentResultDto> GeneratePixAsync(PixPaymentDto dto)
        {
            var sale = await _saleService.GetSaleByIdAsync(dto.OrderId);
            if (sale == null)
                throw new Exception("Pedido não encontrado.");

            var pixCode = Guid.NewGuid();

            try
            {
                await _publisher.PublishAsync(new PixPaymentCreatedEvent(dto.OrderId, pixCode));
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }            

            return new PixPaymentResultDto(pixCode);
        }
    }
}