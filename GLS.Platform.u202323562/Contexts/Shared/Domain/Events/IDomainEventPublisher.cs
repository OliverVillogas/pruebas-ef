namespace GLS.Platform.u202323562.Contexts.Shared.Domain.Events;


public interface IDomainEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : class;
}