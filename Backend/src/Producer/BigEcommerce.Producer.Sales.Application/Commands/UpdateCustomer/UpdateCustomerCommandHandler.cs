using BigEcommerce.Producer.Sales.Application.Services.Customers;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using System;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Guid>
    {
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;

        public UpdateCustomerCommandHandler(ICustomerService customerService, IEventPublisher eventPublisher)
        {
            _customerService = customerService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerService.UpdateCustomerAsync(request);

            Console.WriteLine($"[EVENTO] CustomerUpdated - CustomerId: {request.Id}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new CustomerUpdatedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                CustomerId = request.Id,
                UpdatedAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}