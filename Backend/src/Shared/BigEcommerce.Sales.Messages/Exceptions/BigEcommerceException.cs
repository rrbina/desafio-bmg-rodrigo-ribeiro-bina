using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class BigEcommerceException : Exception
{
    public BigEcommerceException(string message) : base(message) { }
}