using System.Globalization;
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

        var sequence = await _db.ManufacturingOrders.CountAsync(x => x.ProcessType == "Master", cancellationToken) + 1;
        var name = $"OM-2026-{sequence:D4}";

        var parentOrder = new ManufacturingOrder
        {
            Name = name,
            ProcessType = "Master",
            ContpaqDocumentId = request.CidDocumentoPedido,
            CustomerCode = request.CustomerCode,
            ProductQtyTarget = request.TargetQuantityKg,
            State = "Draft"
        };

        var processes = new[] { "Extrusion", "Printing", "Bagging" };
        int procSeq = 1;
        foreach (var proc in processes)
        {
            parentOrder.ChildOrders.Add(new ManufacturingOrder
            {
                Name = $"OF-{proc[..3].ToUpper(CultureInfo.InvariantCulture)}-2026-{sequence:D4}-{procSeq++}",
                ProcessType = proc,
                WorkCenterId = proc == "Extrusion" ? "EXT-01" : proc == "Printing" ? "IMP-01" : "BOL-01",
                State = "Draft",
                ProductQtyTarget = request.TargetQuantityKg
            });
        }

        _db.ManufacturingOrders.Add(parentOrder);
        
        // Also seed legacy entity for backwards compatibility
        var legacyMaster = new MasterOrder
        {
            Id = parentOrder.Id,
            FolioOm = name,
            CidDocumentoPedido = request.CidDocumentoPedido,
            CustomerCode = request.CustomerCode,
            PtSku = request.PtSku,
            TargetQuantityKg = request.TargetQuantityKg,
            Status = "Draft"
        };

        var legacyProcesses = new[] { "EXT", "IMP", "BOL" };
        int legacyProcSeq = 1;
        foreach (var proc in legacyProcesses)
        {
            legacyMaster.SubOrders.Add(new SubOrder
            {
                FolioOf = $"OF-{proc}-2026-{sequence:D4}-{legacyProcSeq++}",
                ProcessType = proc,
                MachineId = proc == "EXT" ? "EXT-01" : proc == "IMP" ? "IMP-01" : "BOL-01",
                Status = "Borrador",
                PlannedQtyKg = request.TargetQuantityKg
            });
        }

        _db.MasterOrders.Add(legacyMaster);

        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetMasterOrderById), new { id = parentOrder.Id }, parentOrder);
    }

    [HttpGet("master/{id}")]
    public async Task<IActionResult> GetMasterOrderById(Guid id, CancellationToken cancellationToken)
    {
        var mo = await _db.ManufacturingOrders
            .Include(x => x.ChildOrders)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
        if (mo == null)
        {
            var legacy = await _db.MasterOrders.Include(x => x.SubOrders).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (legacy == null) return NotFound();
            return Ok(legacy);
        }
        return Ok(mo);
    }

    public record UpdateStatusRequest(string NewStatus, string? Notes);

    [HttpPatch("sub-orders/{id}/status")]
    public async Task<IActionResult> UpdateSubOrderStatus(Guid id, [FromBody] UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var mo = await _db.ManufacturingOrders.FindAsync(new object[] { id }, cancellationToken);
        if (mo != null)
        {
            mo.State = request.NewStatus;
            await _db.SaveChangesAsync(cancellationToken);
            return Ok(mo);
        }

        var subOrder = await _db.SubOrders.FindAsync(new object[] { id }, cancellationToken);
        if (subOrder == null) return NotFound();

        subOrder.Status = request.NewStatus;
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(subOrder);
    }
}
