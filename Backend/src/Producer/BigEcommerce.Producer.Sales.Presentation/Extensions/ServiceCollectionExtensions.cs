using BigEcommerce.Producer.Sales.Application.Common.Settings;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            return services;
        }
    }
}
