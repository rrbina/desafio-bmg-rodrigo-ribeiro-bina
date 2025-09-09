using MediatR;
using BigEcommerce.Producer.Sales.Application.Behaviors;
using System.Diagnostics.CodeAnalysis;
using BigEcommerce.Producer.Sales.Application.Helpers;
namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    public static class MediatRServiceExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationMarker).Assembly;

            services.AddMediatR(applicationAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}