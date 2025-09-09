using System.Security.Claims;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Services.Jwt;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;

namespace BigEcommerce.Producer.Sales.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICustomerRepository _customerRepository;

        public LoginCommandHandler(
            IJwtService jwtService,
            IPasswordHasher passwordHasher,
            ICustomerRepository customerRepository)
        {
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByEmailAsync(request.Email);
            if (customer == null)
                throw new BigEcommerceException("Email ou senha inválidos.");

            var isValid = _passwordHasher.Verify(
                request.Password,
                customer.PasswordHash,
                customer.PasswordSalt);

            if (!isValid)
                throw new BigEcommerceException("Email ou senha inválidos.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.CustomerName),
                new Claim(ClaimTypes.Email, customer.CustomerEmail)
            };

            return _jwtService.GenerateToken(claims);
        }
    }
}
