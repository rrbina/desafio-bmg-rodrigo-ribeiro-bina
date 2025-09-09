using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Commands.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; }
        public string Password { get; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}