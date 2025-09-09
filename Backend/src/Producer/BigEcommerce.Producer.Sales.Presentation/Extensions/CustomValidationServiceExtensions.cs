using BigEcommerce.Producer.Sales.Application.Helpers;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CustomValidationServiceExtensions
    {
        public static IServiceCollection AddCustomValidation(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationMarker).Assembly;
            services.AddValidatorsFromAssembly(applicationAssembly);
            return services;
        }
    }
}
