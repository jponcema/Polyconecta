namespace PolyConecta.Infrastructure.Outbox;

public interface IOutboxProcessor
{
    Task ProcessPendingMessagesAsync(CancellationToken cancellationToken = default);
}
