using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Contpaq.Bridge.Core.Models;

namespace Contpaq.Bridge.Infrastructure.Webhooks
{
    public interface IWebhookDispatcher
    {
        Task DeliverAsync(BridgeTransaction transaction);
    }

    public class WebhookDispatcher : IWebhookDispatcher
    {
        private static readonly HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

        public async Task DeliverAsync(BridgeTransaction transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.CallbackUrl)) return;

            var payload = new
            {
                event_type = "transaction.status_changed",
                transaction_id = transaction.TransactionId,
                correlation_id = transaction.CorrelationId,
                client_app_id = transaction.ClientAppId,
                status = transaction.Status,
                contpaqi_doc_id = transaction.ContpaqiDocId,
                contpaqi_folio = transaction.ContpaqiFolio,
                error_code = transaction.LastErrorCode,
                error_message = transaction.LastErrorMessage,
                timestamp = DateTime.UtcNow.ToString("o")
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                await HttpClient.PostAsync(transaction.CallbackUrl, content);
            }
            catch
            {
                // Webhook delivery exception swallowed silently to avoid blocking primary bridge processing
            }
        }
    }
}
