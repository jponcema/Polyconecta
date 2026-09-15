using PolyConecta.Domain.Common;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Repositories;

/// <summary>
/// Domain Repository for Master Orders and SubOrders.
/// </summary>
public interface IOrderRepository : IRepository<MasterOrder, Guid>
{
    Task<MasterOrder?> GetByFolioOmAsync(string folioOm, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MasterOrder>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MasterOrder>> GetOrdersByCustomerAsync(string customerCode, CancellationToken cancellationToken = default);
}
