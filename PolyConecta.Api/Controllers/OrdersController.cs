using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly PolyDbContext _db;

    public OrdersController(PolyDbContext db)
    {
        _db = db;
    }

    public record CreateMasterOrderRequest(
        int CidDocumentoPedido,
        string CustomerCode,
        string PtSku,
        decimal TargetQuantityKg
    );

    [HttpPost("master")]
    public async Task<IActionResult> CreateMasterOrder([FromBody] CreateMasterOrderRequest request, CancellationToken cancellationToken)
    {
        if (request.TargetQuantityKg <= 0)
        {
            return BadRequest(new { Error = "Target quantity must be greater than zero." });
        }

        var sequence = await _db.MasterOrders.CountAsync(cancellationToken) + 1;
        var folioOm = $"OM-2026-{sequence:D4}";

        var masterOrder = new MasterOrder
        {
            FolioOm = folioOm,
            CidDocumentoPedido = request.CidDocumentoPedido,
            CustomerCode = request.CustomerCode,
            PtSku = request.PtSku,
            TargetQuantityKg = request.TargetQuantityKg,
            Status = "Draft"
        };

        var processes = new[] { "EXT", "IMP", "BOL" };
        int procSeq = 1;
        foreach (var proc in processes)
        {
            masterOrder.SubOrders.Add(new SubOrder
            {
                FolioOf = $"OF-{proc}-2026-{sequence:D4}-{procSeq++}",
                ProcessType = proc,
                MachineId = proc == "EXT" ? "EXT-01" : proc == "IMP" ? "IMP-01" : "BOL-01",
                Status = "Borrador",
                PlannedQtyKg = request.TargetQuantityKg
            });
        }

        _db.MasterOrders.Add(masterOrder);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetMasterOrderById), new { id = masterOrder.Id }, masterOrder);
    }

    [HttpGet("master/{id}")]
    public async Task<IActionResult> GetMasterOrderById(Guid id, CancellationToken cancellationToken)
    {
        var mo = await _db.MasterOrders.Include(x => x.SubOrders).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (mo == null) return NotFound();
        return Ok(mo);
    }

    public record UpdateStatusRequest(string NewStatus, string? Notes);

    [HttpPatch("sub-orders/{id}/status")]
    public async Task<IActionResult> UpdateSubOrderStatus(Guid id, [FromBody] UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var validStatuses = new[] { "Borrador", "Programado", "En_Proceso", "Control_Calidad", "Finalizado", "Scrap" };
        if (!validStatuses.Contains(request.NewStatus))
        {
            return BadRequest(new { Error = $"Invalid status '{request.NewStatus}'." });
        }

        var subOrder = await _db.SubOrders.FindAsync(new object[] { id }, cancellationToken);
        if (subOrder == null) return NotFound();

        subOrder.Status = request.NewStatus;
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(subOrder);
    }
}
