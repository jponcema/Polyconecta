using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.Repositories;
using PolyConecta.Infrastructure.Persistence;

namespace PolyConecta.Infrastructure.Repositories;

public class OutboxRepository : IOutboxRepository
{
    private readonly PolyDbContext _context;

    public OutboxRepository(PolyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await _context.Set<OutboxMessage>().AddAsync(message, cancellationToken);
    }

    public async Task<IReadOnlyList<OutboxMessage>> GetUnprocessedMessagesAsync(int batchSize = 50, CancellationToken cancellationToken = default)
    {
        return await _context.Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsProcessedAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<OutboxMessage>().FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            message.ProcessedOnUtc = DateTime.UtcNow;
            message.Error = null;
        }
    }

    public async Task MarkAsFailedAsync(Guid messageId, string error, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<OutboxMessage>().FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            message.Error = error;
        }
    }
}
