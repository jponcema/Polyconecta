namespace PolyConecta.Infrastructure.Common;

/// <summary>
/// Unit of Work contract for atomic EF Core transactions.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
