using Contpaq.Bridge.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/logs")]
    public class LogsController : ControllerBase
    {
        private readonly ConsoleLogStreamService _logStreamService;

        public LogsController(ConsoleLogStreamService logStreamService)
        {
            _logStreamService = logStreamService;
        }

        [HttpGet("recent")]
        public IActionResult GetRecentLogs([FromQuery] int limit = 100)
        {
            var logs = _logStreamService.GetRecentLogs(limit);
            return Ok(logs);
        }
    }
}
