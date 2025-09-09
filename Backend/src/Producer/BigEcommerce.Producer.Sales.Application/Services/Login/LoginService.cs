using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Services.Jwt;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginService(
            ICustomerRepository customerRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var customer = await _customerRepository
                .GetByEmailAsync(request.Email);

            if (customer == null)
                return null;

            var isValid = _passwordHasher
                .Verify(request.Password, customer.PasswordHash, customer.PasswordSalt);

            if (!isValid)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.CustomerName),
                new Claim(ClaimTypes.Email, customer.CustomerEmail)
            };

            var token = _jwtService.GenerateToken(claims);

            return new LoginResponse { Token = token };
        }
    }
}
