using BigEcommerce.Producer.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CustomerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(c => c.Sales)
                   .WithOne(s => s.Customer)
                   .HasForeignKey(s => s.CustomerId);

            builder.Property(c => c.CustomerEmail)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(c => c.PasswordHash)
                   .IsRequired();

            builder.Property(c => c.PasswordSalt)
                   .IsRequired();

        }
    }
}