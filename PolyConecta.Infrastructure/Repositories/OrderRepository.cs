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

    public async Task<ManufacturingOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders
            .Include(o => o.ChildOrders)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ManufacturingOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders.Include(o => o.ChildOrders).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ManufacturingOrder entity, CancellationToken cancellationToken = default)
    {
        await _context.ManufacturingOrders.AddAsync(entity, cancellationToken);
    }

    public void Update(ManufacturingOrder entity)
    {
        _context.ManufacturingOrders.Update(entity);
    }

    public void Delete(ManufacturingOrder entity)
    {
        _context.ManufacturingOrders.Remove(entity);
    }

    public async Task<ManufacturingOrder?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders
            .Include(o => o.ChildOrders)
            .FirstOrDefaultAsync(o => o.Name == name, cancellationToken);
    }

    public async Task<IReadOnlyList<ManufacturingOrder>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders
            .Where(o => o.State == "Draft" || !o.SalesApproved || !o.CreditApproved)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ManufacturingOrder>> GetOrdersByCustomerAsync(string customerCode, CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders
            .Where(o => o.CustomerCode == customerCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ManufacturingOrder>> GetChildOrdersAsync(Guid parentId, CancellationToken cancellationToken = default)
    {
        return await _context.ManufacturingOrders
            .Where(o => o.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }
}
