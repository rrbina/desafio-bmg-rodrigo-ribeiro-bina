using BigEcommerce.Producer.Sales.Application.Services.Customers;
using MediatR;
using BigEcommerce.Producer.Sales.Application.Common;
using BigEcommerce.Sales.Messages.IntegrationEvents;
using System;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Guid>
    {
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;

        public DeleteCustomerCommandHandler(ICustomerService customerService, IEventPublisher eventPublisher)
        {
            _customerService = customerService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerService.DeleteCustomerAsync(request.CustomerId);

            Console.WriteLine($"[EVENTO] CustomerDeleted - CustomerId: {request.CustomerId}");
            var eventId = Guid.NewGuid();

            try
            {
                await _eventPublisher.PublishAsync(new CustomerDeletedEvent
                {
                    EventId = eventId,
                    EventDateTime = DateTime.UtcNow,
                    CustomerId = request.CustomerId,
                    DeletedAt = DateTime.UtcNow
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return eventId;
        }
    }
}