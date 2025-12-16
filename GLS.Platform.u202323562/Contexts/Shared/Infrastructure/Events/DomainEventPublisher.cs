using GLS.Platform.u202323562.Contexts.Shared.Domain.Events;

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Events;

public class DomainEventPublisher(IServiceProvider serviceProvider) : IDomainEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : class
    {
        var handlerType = typeof(IEnumerable<>).MakeGenericType(
            typeof(Func<,>).MakeGenericType(typeof(TEvent), typeof(Task))
        );

        if (serviceProvider.GetService(handlerType) is IEnumerable<Func<TEvent, Task>> handlers)
        {
            foreach (var handler in handlers)
            {
                await handler(domainEvent);
            }
        }
    }
}