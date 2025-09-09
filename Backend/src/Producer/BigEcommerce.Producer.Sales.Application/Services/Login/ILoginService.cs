using BigEcommerce.Producer.Sales.Application.DTOs;

public interface ILoginService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}