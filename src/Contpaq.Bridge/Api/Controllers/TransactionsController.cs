using System;
using System.Text.Json;
using System.Threading.Tasks;
using Contpaq.Bridge.Core.Commands;
using Contpaq.Bridge.Core.Models;
using Contpaq.Bridge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly IOutboxRepository _outboxRepository;

        public TransactionsController(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(request.IdempotencyKey))
            {
                var existing = await _outboxRepository.GetByIdempotencyKeyAsync(request.IdempotencyKey);
                if (existing != null)
                {
                    return Accepted(new
                    {
                        transaction_id = existing.TransactionId,
                        correlation_id = existing.CorrelationId,
                        status = existing.Status,
                        created_at = existing.CreatedAt,
                        is_duplicate = true
                    });
                }
            }

            var correlationId = request.CorrelationId ?? HttpContext.Items["X-Correlation-ID"]?.ToString() ?? Guid.NewGuid().ToString();

            var transaction = new BridgeTransaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                CorrelationId = correlationId,
                ClientAppId = request.ClientAppId,
                IdempotencyKey = request.IdempotencyKey,
                CommandType = request.CommandType,
                PayloadJson = JsonSerializer.Serialize(request.Payload),
                CallbackUrl = request.CallbackUrl,
                Status = "PENDING",
                CreatedAt = DateTime.UtcNow.ToString("o"),
                UpdatedAt = DateTime.UtcNow.ToString("o")
            };

            var success = await _outboxRepository.AddTransactionAsync(transaction);
            if (!success)
            {
                return Conflict(new { error = "Duplicate transaction idempotency key" });
            }

            Contpaq.Bridge.Core.Services.PerformanceMetrics.RecordWriteOp();

            return Accepted(new
            {
                transaction_id = transaction.TransactionId,
                correlation_id = transaction.CorrelationId,
                status = transaction.Status,
                created_at = transaction.CreatedAt
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionStatus(string id)
        {
            var transaction = await _outboxRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound(new { error = $"Transaction {id} not found" });
            }

            return Ok(new
            {
                transaction_id = transaction.TransactionId,
                correlation_id = transaction.CorrelationId,
                client_app_id = transaction.ClientAppId,
                command_type = transaction.CommandType,
                status = transaction.Status,
                retry_count = transaction.RetryCount,
                contpaqi_doc_id = transaction.ContpaqiDocId,
                contpaqi_folio = transaction.ContpaqiFolio,
                last_error_code = transaction.LastErrorCode,
                last_error_message = transaction.LastErrorMessage,
                created_at = transaction.CreatedAt,
                updated_at = transaction.UpdatedAt
            });
        }

        [HttpDelete("pending")]
        public async Task<IActionResult> DeletePendingTransactions()
        {
            var deletedCount = await _outboxRepository.DeletePendingTransactionsAsync();
            return Ok(new { message = $"Purged {deletedCount} pending transactions from Outbox", deleted_count = deletedCount });
        }

        [HttpDelete("purge-all")]
        public async Task<IActionResult> PurgeAllTransactions()
        {
            var deletedCount = await _outboxRepository.PurgeAllTransactionsAsync();
            return Ok(new { message = $"Purged all {deletedCount} transactions from Outbox", deleted_count = deletedCount });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(string id)
        {
            var success = await _outboxRepository.DeleteTransactionAsync(id);
            if (!success) return NotFound(new { error = $"Transaction {id} not found" });
            return Ok(new { message = $"Transaction {id} deleted successfully" });
        }
    }
}
