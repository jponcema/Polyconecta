using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.Repositories;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly PolyDbContext _context;

    public SupplierRepository(PolyDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierProductMapping?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SupplierProductMappings.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SupplierProductMapping>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SupplierProductMappings.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SupplierProductMapping entity, CancellationToken cancellationToken = default)
    {
        await _context.SupplierProductMappings.AddAsync(entity, cancellationToken);
    }

    public void Update(SupplierProductMapping entity)
    {
        _context.SupplierProductMappings.Update(entity);
    }

    public void Delete(SupplierProductMapping entity)
    {
        _context.SupplierProductMappings.Remove(entity);
    }

    public async Task<IReadOnlyList<SupplierProductMapping>> GetMappingsBySupplierCodeAsync(string supplierCode, CancellationToken cancellationToken = default)
    {
        return await _context.SupplierProductMappings
            .Where(s => s.SupplierCode == supplierCode)
            .ToListAsync(cancellationToken);
    }
}
