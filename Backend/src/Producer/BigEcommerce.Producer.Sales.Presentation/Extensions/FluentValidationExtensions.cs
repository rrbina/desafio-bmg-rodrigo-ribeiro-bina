using FluentValidation.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
