using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.Repositories;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly PolyDbContext _context;

    public OrderRepository(PolyDbContext context)
    {
        _context = context;
    }

    public async Task<MasterOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.MasterOrders
            .Include(o => o.SubOrders)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MasterOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MasterOrders.Include(o => o.SubOrders).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MasterOrder entity, CancellationToken cancellationToken = default)
    {
        await _context.MasterOrders.AddAsync(entity, cancellationToken);
    }

    public void Update(MasterOrder entity)
    {
        _context.MasterOrders.Update(entity);
    }

    public void Delete(MasterOrder entity)
    {
        _context.MasterOrders.Remove(entity);
    }

    public async Task<MasterOrder?> GetByFolioOmAsync(string folioOm, CancellationToken cancellationToken = default)
    {
        return await _context.MasterOrders
            .Include(o => o.SubOrders)
            .FirstOrDefaultAsync(o => o.FolioOm == folioOm, cancellationToken);
    }

    public async Task<IReadOnlyList<MasterOrder>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MasterOrders
            .Where(o => o.Status == "Sincronizado" || o.Status == "Draft" || o.Status == "Borrador")
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MasterOrder>> GetOrdersByCustomerAsync(string customerCode, CancellationToken cancellationToken = default)
    {
        return await _context.MasterOrders
            .Where(o => o.CustomerCode == customerCode)
            .ToListAsync(cancellationToken);
    }
}
