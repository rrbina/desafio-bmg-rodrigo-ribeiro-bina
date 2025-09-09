﻿using BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteProduct;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteProduct
{
    public class DeleteProductCommandValidatorTests
    {
        private readonly DeleteProductCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteProductCommand(Guid.Empty);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Valid()
        {
            var command = new DeleteProductCommand(Guid.NewGuid());
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
