using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateProduct;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateProduct;

namespace BigEcommerce.Producer.Sales.Application.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> UpdateProductAsync(UpdateProductCommand command);
        Task<ProductDto> DeleteProductAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductCommand command);
    }
}