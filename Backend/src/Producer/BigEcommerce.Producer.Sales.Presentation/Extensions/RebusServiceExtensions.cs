using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Producer.Sales.Infrastructure.MessageBus;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System.Diagnostics.CodeAnalysis;
using BigEcommerce.Sales.Messages.IntegrationEvents;

namespace BigEcommerce.Producer.Sales.Presentation.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RebusServiceExtensions
    {
        public static IServiceCollection AddRebusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["RabbitMq:ConnectionString"];
            var queueName = configuration["RabbitMq:QueueName"];

            services.AddRebus(configure =>
                configure
                    .Transport(t => t.UseRabbitMq(connectionString, queueName))
                    .Routing(r => r.TypeBased()
                        .Map<CustomerCreatedEvent>(queueName)
                        .Map<CustomerDeletedEvent>(queueName)
                        .Map<CustomerUpdatedEvent>(queueName)
                        .Map<ItemCancelledEvent>(queueName)
                        .Map<ProductCreatedEvent>(queueName)
                        .Map<ProductDeletedEvent>(queueName)
                        .Map<ProductUpdatedEvent>(queueName)
                        .Map<SaleCancelledEvent>(queueName)
                        .Map<SaleCreatedEvent>(queueName)
                        .Map<SaleDeletedEvent>(queueName)
                        .Map<SaleModifiedEvent>(queueName)
                        .Map<PixPaymentCreatedEvent>(queueName)
                        .Map<CardPaymentCreatedEvent>(queueName)
                        
                    )
            );

            services.AddScoped<IEventPublisher, RebusEventPublisher>();
            return services;
        }
    }
}