namespace PolyConecta.Domain.Events;

/// <summary>
/// Marker interface for all Domain Events.
/// </summary>
public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
}

/// <summary>
/// Handler interface for specific domain events.
/// </summary>
/// <typeparam name="TEvent">Domain event type</typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// Domain Event Publisher abstraction.
/// </summary>
public interface IDomainEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default) where TEvent : IDomainEvent;
}
