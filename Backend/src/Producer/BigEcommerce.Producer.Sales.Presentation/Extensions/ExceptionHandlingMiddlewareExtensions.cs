using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Presentation.Middlewares;

[ExcludeFromCodeCoverage]
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseBigEcommerceExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}