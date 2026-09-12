using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Contpaq.Bridge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CatalogsController : ControllerBase
    {
        private readonly ISqlReadRepository _sqlReadRepository;

        public CatalogsController(ISqlReadRepository sqlReadRepository)
        {
            _sqlReadRepository = sqlReadRepository;
        }

        [HttpGet("catalogs/products")]
        public async Task<IActionResult> GetProducts([FromQuery] string? search, [FromQuery] int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var products = await _sqlReadRepository.GetProductsAsync(search, limit);
                sw.Stop();
                return Ok(new { products, query_time_ms = sw.ElapsedMilliseconds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }

        [HttpGet("catalogs/clients")]
        public async Task<IActionResult> GetClients([FromQuery] string? search, [FromQuery] int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var clients = await _sqlReadRepository.GetClientsAsync(search, limit);
                sw.Stop();
                return Ok(new { clients, query_time_ms = sw.ElapsedMilliseconds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }

        [HttpGet("catalogs/warehouses")]
        public async Task<IActionResult> GetWarehouses()
        {
            try
            {
                var warehouses = await _sqlReadRepository.GetWarehousesAsync();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }

        [HttpGet("catalogs/concepts")]
        public async Task<IActionResult> GetConcepts()
        {
            try
            {
                var concepts = await _sqlReadRepository.GetConceptsAsync();
                return Ok(concepts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }

        [HttpGet("inventory/stocks")]
        public async Task<IActionResult> GetProductStock([FromQuery] string codigo_producto, [FromQuery] string? codigo_almacen)
        {
            if (string.IsNullOrEmpty(codigo_producto))
            {
                return BadRequest(new { error = "codigo_producto query parameter is required" });
            }

            var sw = Stopwatch.StartNew();
            try
            {
                var stock = await _sqlReadRepository.GetProductStockAsync(codigo_producto, codigo_almacen);
                sw.Stop();
                if (stock == null) return NotFound(new { error = $"Product {codigo_producto} stock not found" });
                return Ok(new { stock, query_time_ms = sw.ElapsedMilliseconds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> GetInvoices([FromQuery] string? search, [FromQuery] int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var invoices = await _sqlReadRepository.GetInvoicesAsync(search, limit);
                sw.Stop();
                return Ok(new { invoices, query_time_ms = sw.ElapsedMilliseconds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Database read error", details = ex.Message });
            }
        }
    }
}
