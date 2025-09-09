using BigEcommerce.Producer.Sales.Application.Services.PasswordHasher;
using BigEcommerce.Producer.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Infrastructure.Persistence
{
    [ExcludeFromCodeCoverage]
    public static class SalesDbContextSeed
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            context.Database.Migrate();

            if (!context.Sales.Any())
            {
                passwordHasher.CreateHash("123456", out var passwordHash, out var passwordSalt);

                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerName = "Cliente Teste",
                    CustomerEmail = "admin@admin.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var product1 = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Produto A",
                    UnitPrice = 50
                };

                var product2 = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Produto B",
                    UnitPrice = 30
                };

                var stock1 = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = product1.Id,
                    Quantity = 10
                };

                var stock2 = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = product2.Id,
                    Quantity = 10
                };

                var sale = new Sale
                {
                    SaleNumber = Guid.NewGuid(),
                    SaleDate = DateTime.UtcNow,
                    CustomerId = customer.Id,
                    BranchName = "Filial Central",
                    TotalAmount = 270,
                    TotalDiscount = 10,
                    IsCancelled = false,
                    Items = new List<SaleItem>
                    {
                        new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product1.Id,
                            Quantity = 3,
                            Discount = 0,
                            TotalAmount = 150,
                            IsCancelled = false
                        },
                        new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product2.Id,
                            Quantity = 4,
                            Discount = 10,
                            TotalAmount = 120,
                            IsCancelled = false
                        }
                    }
                };

                context.Customers.Add(customer);
                context.Products.AddRange(product1, product2);
                context.Stocks.AddRange(stock1, stock2);
                context.Sales.Add(sale);

                context.SaveChanges();
            }
        }
    }
}