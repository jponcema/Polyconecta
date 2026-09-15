using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Outbox;

public interface IOutboxPublisher
{
    Task EnqueueAsync(string messageType, object payload, CancellationToken cancellationToken = default);
}

public class OutboxPublisher : IOutboxPublisher
{
    private readonly List<OutboxMessage> _messages = new();

    public Task EnqueueAsync(string messageType, object payload, CancellationToken cancellationToken = default)
    {
        var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);
        var msg = new OutboxMessage
        {
            MessageType = messageType,
            Payload = jsonPayload
        };
        _messages.Add(msg);
        return Task.CompletedTask;
    }

    public IReadOnlyList<OutboxMessage> GetUnprocessedMessages() => _messages.Where(m => !m.Processed).ToList();
}
