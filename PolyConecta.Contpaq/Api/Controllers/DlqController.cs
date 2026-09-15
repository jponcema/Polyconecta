using System;
using System.Text.Json;
using System.Threading.Tasks;
using Contpaq.Bridge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/dlq")]
    public class DlqController : ControllerBase
    {
        private readonly IOutboxRepository _outboxRepository;

        public DlqController(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDlqItems()
        {
            var items = await _outboxRepository.GetDlqTransactionsAsync();
            return Ok(items);
        }

        [HttpPost("{id}/retry")]
        public async Task<IActionResult> RetryDlqItem(string id)
        {
            var item = await _outboxRepository.GetByIdAsync(id);
            if (item == null || item.Status != "DEAD_LETTER_QUEUE")
            {
                return NotFound(new { error = "Item not found in DLQ" });
            }

            await _outboxRepository.IncrementRetryAsync(id, 0, DateTime.UtcNow, null, null);
            return Ok(new { message = $"Transaction {id} re-queued for execution" });
        }

        public class EditPayloadRequest
        {
            public JsonElement Payload { get; set; }
        }

        [HttpPost("{id}/edit-and-retry")]
        public async Task<IActionResult> EditAndRetryDlqItem(string id, [FromBody] EditPayloadRequest request)
        {
            var item = await _outboxRepository.GetByIdAsync(id);
            if (item == null || item.Status != "DEAD_LETTER_QUEUE")
            {
                return NotFound(new { error = "Item not found in DLQ" });
            }

            item.PayloadJson = request.Payload.GetRawText();
            item.Status = "PENDING";
            item.RetryCount = 0;
            item.NextAttemptAt = DateTime.UtcNow.ToString("o");
            item.LastErrorCode = null;
            item.LastErrorMessage = null;
            item.UpdatedAt = DateTime.UtcNow.ToString("o");

            await _outboxRepository.UpdateStatusAsync(id, "PENDING", null, null, null, null);
            await _outboxRepository.IncrementRetryAsync(id, 0, DateTime.UtcNow, null, null);

            return Ok(new { message = $"Transaction {id} updated and re-queued" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PurgeDlqItem(string id)
        {
            var success = await _outboxRepository.DeleteDlqTransactionAsync(id);
            if (!success)
            {
                return NotFound(new { error = "Item not found in DLQ" });
            }
            return Ok(new { message = $"Transaction {id} purged from DLQ" });
        }
    }
}
