using BigEcommerce.Producer.Sales.Infra.Data.Repositories;
using BigEcommerce.Producer.Sales.Domain.Interfaces.Repositories;
using BigEcommerce.Producer.Sales.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using BigEcommerce.Producer.Sales.Domain.Interfaces;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISaleItemRepository, SaleItemRepository>();
            services.AddScoped<IStockRepository, StockRepository>();

            return services;
        }
    }
}