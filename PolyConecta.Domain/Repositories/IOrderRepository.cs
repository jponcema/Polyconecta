using PolyConecta.Domain.Common;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Repositories;

/// <summary>
/// Domain Repository for Manufacturing Orders (Master and Sub-Orders).
/// </summary>
public interface IOrderRepository : IRepository<ManufacturingOrder, Guid>
{
    Task<ManufacturingOrder?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ManufacturingOrder>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ManufacturingOrder>> GetOrdersByCustomerAsync(string customerCode, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ManufacturingOrder>> GetChildOrdersAsync(Guid parentId, CancellationToken cancellationToken = default);
}
