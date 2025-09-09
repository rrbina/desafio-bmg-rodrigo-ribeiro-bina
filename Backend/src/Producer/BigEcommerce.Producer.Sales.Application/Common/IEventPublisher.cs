using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BigEcommerce.Producer.Sales.Application.Common
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }
}