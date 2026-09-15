using PolyConecta.Domain.Events;
using Xunit;

namespace PolyConecta.Domain.Tests;

public class InterfaceContractsTests
{
    private class TestDomainEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }

    [Fact]
    public void DomainEvent_Initialization_SetsValidMetadata()
    {
        var evt = new TestDomainEvent();
        Assert.NotEqual(Guid.Empty, evt.EventId);
        Assert.True(evt.OccurredOnUtc <= DateTime.UtcNow);
    }
}
