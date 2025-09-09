using System.Security.Claims;

namespace BigEcommerce.Producer.Sales.Application.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
