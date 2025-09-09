using BigEcommerce.Producer.Sales.Application.Services.Customers;
using BigEcommerce.Producer.Sales.Application.Services.Jwt;
using BigEcommerce.Producer.Sales.Application.Services.Login;
using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Application.Services.Payments;
using BigEcommerce.Producer.Sales.Application.Services.Products;
using BigEcommerce.Producer.Sales.Application.Services.Sales;
using BigEcommerce.Producer.Sales.Application.Services.Stocks;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPixPaymentService, PixPaymentService>();
            services.AddScoped<ICardPaymentService, CardPaymentService>();

            return services;
        }
    }
}