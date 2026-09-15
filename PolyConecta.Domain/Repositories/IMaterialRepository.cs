using PolyConecta.Domain.Common;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Repositories;

/// <summary>
/// Domain Repository for Raw Material Catalog and Supplier Mappings.
/// </summary>
public interface IMaterialRepository : IRepository<RawMaterialCatalog, Guid>
{
    Task<RawMaterialCatalog?> GetByInternalSkuAsync(string internalSku, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RawMaterialCatalog>> GetActiveMaterialsAsync(CancellationToken cancellationToken = default);
    Task<SupplierProductMapping?> GetSupplierMappingAsync(string supplierCode, string supplierSku, CancellationToken cancellationToken = default);
}
