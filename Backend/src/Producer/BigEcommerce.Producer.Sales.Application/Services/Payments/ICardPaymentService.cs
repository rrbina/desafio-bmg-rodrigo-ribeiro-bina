using BigEcommerce.Producer.Sales.Application.DTOs;

namespace BigEcommerce.Producer.Sales.Application.Services.Payments
{
    public interface ICardPaymentService
    {
        Task<bool> ProcessCardAsync(CardPaymentDto dto);
    }
}