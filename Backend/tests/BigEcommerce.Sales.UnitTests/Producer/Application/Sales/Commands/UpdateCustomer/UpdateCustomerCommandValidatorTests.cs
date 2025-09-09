using BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer;

namespace BigEcommerce.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidatorTests
    {
        private readonly UpdateCustomerCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateCustomerCommand(Guid.Empty, "Cliente", "123456");
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "", "123456");
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Too_Long()
        {
            var command = new UpdateCustomerCommand(Guid.NewGuid(), new string('A', 101), "123456");
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "Cliente Válido", "123456");
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
