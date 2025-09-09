using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer;
using BigEcommerce.Producer.Sales.Application.Services.Customers;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Services.Sales
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CustomerService(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command)
        {            
            var customerExists = await _customerRepository.ExistsByNameAsync(command.CustomerName);
            if (customerExists)
            {
                throw new BigEcommerceException($"Customer with name '{command.CustomerName}' already exists.");
            }

            _passwordHasher.CreateHash(command.CustomerPassword, out var hash, out var salt);
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CustomerName = command.CustomerName,
                CustomerEmail = command.CustomerEmail,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _customerRepository.AddAsync(customer);
            return new CustomerDto(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto(c));
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer is null ? null : new CustomerDto(customer);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command)
        {
            var customer = await _customerRepository.GetByIdAsync(command.Id);
            if (customer is null)
                throw new Exception("Cliente não encontrado.");

            _passwordHasher.CreateHash(command.CustomerPassword, out var hash, out var salt);

            customer.CustomerName = command.CustomerName;
            customer.CustomerEmail = command.CustomerEmail;
            customer.PasswordSalt = salt;
            customer.PasswordHash = hash;

            await _customerRepository.UpdateAsync(customer);

            return new CustomerDto(customer);
        }

        public async Task<CustomerDto> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is null)
                throw new BigEcommerceException("Cliente não encontrado.");

            await _customerRepository.DeleteAsync(id);
            return new CustomerDto(customer);
        }
    }
}