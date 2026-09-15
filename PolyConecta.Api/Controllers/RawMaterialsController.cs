using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Api.Controllers;

[ApiController]
[Route("api/v1/raw-materials")]
public class RawMaterialsController : ControllerBase
{
    private readonly PolyDbContext _db;

    public RawMaterialsController(PolyDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetRawMaterials(CancellationToken cancellationToken)
    {
        var items = await _db.RawMaterialCatalogs
                             .Include(x => x.SupplierMappings)
                             .Where(x => x.IsActive)
                             .ToListAsync(cancellationToken);
        return Ok(items);
    }

    public record CreateRawMaterialRequest(
        string InternalSku,
        string Name,
        string Category,
        decimal MfiMeltFlowIndex,
        decimal DensityGcm3,
        string TargetHopper,
        int CidProductoContpaq
    );

    [HttpPost]
    public async Task<IActionResult> CreateRawMaterial([FromBody] CreateRawMaterialRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.InternalSku) || string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { Error = "InternalSku and Name are required fields." });
        }

        var exists = await _db.RawMaterialCatalogs.AnyAsync(x => x.InternalSku == request.InternalSku, cancellationToken);
        if (exists)
        {
            return Conflict(new { Error = $"Raw material with SKU '{request.InternalSku}' already exists." });
        }

        var entity = new RawMaterialCatalog
        {
            InternalSku = request.InternalSku,
            Name = request.Name,
            Category = request.Category,
            MfiMeltFlowIndex = request.MfiMeltFlowIndex,
            DensityGcm3 = request.DensityGcm3,
            TargetHopper = request.TargetHopper,
            CidProductoContpaq = request.CidProductoContpaq,
            IsActive = true
        };

        _db.RawMaterialCatalogs.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetRawMaterials), new { id = entity.Id }, entity);
    }

    public record AddSupplierMappingRequest(
        string SupplierCode,
        string SupplierProductName,
        string SupplierSku
    );

    [HttpPost("{id:guid}/mappings")]
    public async Task<IActionResult> AddSupplierMapping(Guid id, [FromBody] AddSupplierMappingRequest request, CancellationToken cancellationToken)
    {
        var catalogItem = await _db.RawMaterialCatalogs.FindAsync(new object[] { id }, cancellationToken);
        if (catalogItem == null) return NotFound(new { Error = "Raw material catalog item not found." });

        var mapping = new SupplierProductMapping
        {
            RawMaterialCatalogId = id,
            SupplierCode = request.SupplierCode,
            SupplierProductName = request.SupplierProductName,
            SupplierSku = request.SupplierSku
        };

        _db.SupplierProductMappings.Add(mapping);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(mapping);
    }
}
