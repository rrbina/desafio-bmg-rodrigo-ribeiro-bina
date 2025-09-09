using BigEcommerce.Producer.Sales.Application.DTOs;
using BigEcommerce.Producer.Sales.Application.Sales.Commands.SelectProduct;
using BigEcommerce.Producer.Sales.Application.Services.Products;
using MediatR;

namespace BigEcommerce.Producer.Sales.Application.Sales.Handlers
{
    public class SelectProductHandler :
        IRequestHandler<GetAllProductsCommand, IEnumerable<ProductDto>>,
        IRequestHandler<GetProductByIdCommand, ProductDto?>
    {
        private readonly IProductService _productService;

        public SelectProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProductsAsync();
        }

        public async Task<ProductDto?> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductByIdAsync(request.ProductId);
        }
    }
}