using System;
using System.Threading.Tasks;
using Contpaq.Bridge.Core.Services;
using Contpaq.Bridge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/metrics")]
    public class MetricsController : ControllerBase
    {
        private readonly IOutboxRepository _outboxRepository;

        public MetricsController(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        [HttpGet("snapshot")]
        public async Task<IActionResult> GetSnapshot()
        {
            var queueDepth = await _outboxRepository.GetQueueDepthAsync();

            return Ok(new
            {
                timestamp = DateTime.UtcNow.ToString("o"),
                reads_per_sec = 0.0,
                writes_per_sec = 0.0,
                total_reads = PerformanceMetrics.TotalReadOps,
                total_writes = PerformanceMetrics.TotalWriteOps,
                avg_query_latency_ms = Math.Round(PerformanceMetrics.AvgQueryLatencyMs, 2),
                avg_sdk_latency_ms = Math.Round(MetricCollectorService.AverageSdkLatencyMs, 2),
                queue_depth = queueDepth,
                error_rate_percent = MetricCollectorService.ErrorRatePercent,
                active_sdk_session = MetricCollectorService.IsSdkSessionActive,
                circuit_state = MetricCollectorService.CircuitState
            });
        }
    }
}
