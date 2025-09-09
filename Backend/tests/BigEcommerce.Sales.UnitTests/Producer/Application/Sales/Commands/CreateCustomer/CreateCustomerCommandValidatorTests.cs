using BigEcommerce.Producer.Sales.Application.Sales.Commands.CreateCustomer;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests
    {
        [Fact]
        public void Validate_Should_HaveError_When_CustomerNameIsEmpty()
        {
            var validator = new CreateCustomerCommandValidator();
            var command = new CreateCustomerCommand("", "123456", "admin@email.com");

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Pass_When_CustomerNameIsValid()
        {
            var validator = new CreateCustomerCommandValidator();
            var command = new CreateCustomerCommand("Cliente válido", "123456", "admin@email.com");

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
