using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer;

namespace BigEcommerce.Producer.Sales.Application.Services.Customers
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command);
        Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command);
        Task<CustomerDto> DeleteCustomerAsync(Guid id);
    }
}