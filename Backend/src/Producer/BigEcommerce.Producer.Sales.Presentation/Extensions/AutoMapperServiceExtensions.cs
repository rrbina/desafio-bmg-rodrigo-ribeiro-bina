using BigEcommerce.Producer.Sales.Presentation.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var loggerFactory = LoggerFactory.Create(loggingBuilder => {
                loggingBuilder.AddConsole();
            });
            var mapperConfig = AutoMapperFactory.CreateMapperConfiguration(loggerFactory);
            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }
    }
}