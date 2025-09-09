using BigEcommerce.Producer.Sales.Application.Commands.UsePixPayment;
using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Services.Payments;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Commands.PixPayment
{
    public class PixPaymentCommandHandler : IRequestHandler<PixPaymentCommand, PixPaymentResultDto>
    {
        private readonly IPixPaymentService _paymentService;

        public PixPaymentCommandHandler(IPixPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<PixPaymentResultDto> Handle(PixPaymentCommand request, CancellationToken cancellationToken)
        {
            return await _paymentService.GeneratePixAsync(request.Payment);
        }
    }
}
