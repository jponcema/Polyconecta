using PolyConecta.Domain.Common;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Repositories;

/// <summary>
/// Domain Repository for Product Catalog (Raw Materials, Finished Goods, Scrap).
/// </summary>
public interface IMaterialRepository : IRepository<Product, Guid>
{
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
}
