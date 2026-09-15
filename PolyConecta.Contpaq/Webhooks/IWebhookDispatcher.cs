namespace PolyConecta.Contpaq.Webhooks;

/// <summary>
/// Interface for forwarding bridge status events and transactions via Webhooks.
/// </summary>
public interface IWebhookDispatcher
{
    Task<bool> DispatchWebhookAsync<TPayload>(string eventName, TPayload payload, CancellationToken cancellationToken = default);
}
