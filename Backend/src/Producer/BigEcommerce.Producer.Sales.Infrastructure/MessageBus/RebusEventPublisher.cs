using Rebus.Bus;
using System.Diagnostics.CodeAnalysis;
using BigEcommerce.Producer.Sales.Application.Common;

namespace BigEcommerce.Producer.Sales.Infrastructure.MessageBus
{
    [ExcludeFromCodeCoverage]
    public class RebusEventPublisher : IEventPublisher
    {
        private readonly IBus _bus;

        public RebusEventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T @event)
        {
            await _bus.Send(@event);
        }
    }
}