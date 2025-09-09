using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Services.Payments;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Commands.CardPayment
{
    public class CardPaymentCommandHandler : IRequestHandler<CardPaymentCommand, bool>
    {
        private readonly ICardPaymentService _paymentService;

        public CardPaymentCommandHandler(ICardPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(CardPaymentCommand request, CancellationToken cancellationToken)
        {
            return await _paymentService.ProcessCardAsync(request.Payment);
        }
    }
}