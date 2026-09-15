using PolyConecta.Domain.Common;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Repositories;

/// <summary>
/// Domain Repository for Supplier Entities.
/// </summary>
public interface ISupplierRepository : IRepository<SupplierProductMapping, Guid>
{
    Task<IReadOnlyList<SupplierProductMapping>> GetMappingsBySupplierCodeAsync(string supplierCode, CancellationToken cancellationToken = default);
}
