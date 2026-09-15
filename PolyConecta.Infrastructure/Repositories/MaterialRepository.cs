using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.Repositories;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Infrastructure.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly PolyDbContext _context;

    public MaterialRepository(PolyDbContext context)
    {
        _context = context;
    }

    public async Task<RawMaterialCatalog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RawMaterialCatalogs.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RawMaterialCatalog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RawMaterialCatalogs.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RawMaterialCatalog entity, CancellationToken cancellationToken = default)
    {
        await _context.RawMaterialCatalogs.AddAsync(entity, cancellationToken);
    }

    public void Update(RawMaterialCatalog entity)
    {
        _context.RawMaterialCatalogs.Update(entity);
    }

    public void Delete(RawMaterialCatalog entity)
    {
        _context.RawMaterialCatalogs.Remove(entity);
    }

    public async Task<RawMaterialCatalog?> GetByInternalSkuAsync(string internalSku, CancellationToken cancellationToken = default)
    {
        return await _context.RawMaterialCatalogs
            .FirstOrDefaultAsync(m => m.InternalSku == internalSku, cancellationToken);
    }

    public async Task<IReadOnlyList<RawMaterialCatalog>> GetActiveMaterialsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RawMaterialCatalogs
            .Where(m => m.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<SupplierProductMapping?> GetSupplierMappingAsync(string supplierCode, string supplierSku, CancellationToken cancellationToken = default)
    {
        return await _context.SupplierProductMappings
            .Include(sp => sp.RawMaterialCatalog)
            .FirstOrDefaultAsync(sp => sp.SupplierCode == supplierCode && sp.SupplierSku == supplierSku, cancellationToken);
    }
}
