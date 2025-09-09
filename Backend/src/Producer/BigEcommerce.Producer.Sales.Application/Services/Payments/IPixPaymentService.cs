using BigEcommerce.Producer.Sales.Application.DTOs;

namespace BigEcommerce.Producer.Sales.Application.Services.Payments
{
    public interface IPixPaymentService
    {
        Task<PixPaymentResultDto> GeneratePixAsync(PixPaymentDto dto);
    }
}