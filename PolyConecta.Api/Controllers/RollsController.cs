using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.ValueObjects;
using PolyConecta.Infrastructure.Outbox;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Api.Controllers;

[ApiController]
[Route("api/v1/rolls")]
public class RollsController : ControllerBase
{
    private readonly PolyDbContext _db;
    private readonly IOutboxPublisher _outbox;

    public RollsController(PolyDbContext db, IOutboxPublisher outbox)
    {
        _db = db;
        _outbox = outbox;
    }

    public record CaptureRollRequest(
        Guid ManufacturingOrderId,
        int LineId,
        string ProductSku,
        decimal GrossWeightKg,
        decimal TareWeightKg,
        decimal LengthMeters,
        decimal GaugeMicron,
        decimal WidthMm,
        decimal DynesCm,
        string MachineId,
        string Shift,
        string OperatorId
    );

    [HttpPost("capture")]
    public async Task<IActionResult> CaptureRoll([FromBody] CaptureRollRequest request, CancellationToken cancellationToken)
    {
        if (request.GrossWeightKg <= request.TareWeightKg)
        {
            return BadRequest(new { Error = "Gross weight must be greater than tare weight." });
        }

        var folioObj = Folio.Generate(request.LineId, DateTime.UtcNow);
        var roll = new StockLot
        {
            ManufacturingOrderId = request.ManufacturingOrderId,
            Name = folioObj.Value,
            ContpaqLotNumber = folioObj.Value, // 1:1 mapping to CONTPAQi cNumeroLote
            ProductSku = request.ProductSku,
            GrossWeightKg = request.GrossWeightKg,
            TareWeightKg = request.TareWeightKg,
            LengthMeters = request.LengthMeters,
            GaugeMicron = request.GaugeMicron,
            WidthMm = request.WidthMm,
            DynesCm = request.DynesCm,
            MachineId = request.MachineId,
            Shift = request.Shift,
            OperatorId = request.OperatorId,
            Status = "Available",
            CurrentLocationCode = "PIM/Produccion"
        };

        _db.StockLots.Add(roll);
        await _db.SaveChangesAsync(cancellationToken);

        await _outbox.EnqueueAsync("RollCreated", new
        {
            RollId = roll.Id,
            Folio = roll.Name,
            LotNumber = roll.ContpaqLotNumber,
            NetWeightKg = roll.NetWeightKg
        }, cancellationToken);

        return CreatedAtAction(nameof(GetRollByFolio), new { folio = roll.Name }, roll);
    }

    [HttpGet("{folio}")]
    public async Task<IActionResult> GetRollByFolio(string folio, CancellationToken cancellationToken)
    {
        var roll = await _db.StockLots.FirstOrDefaultAsync(r => r.Name == folio, cancellationToken);
        if (roll == null) return NotFound();
        return Ok(roll);
    }
}
