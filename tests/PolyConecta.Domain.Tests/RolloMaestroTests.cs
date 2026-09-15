using FluentAssertions;
using PolyConecta.Domain.Entities;
using PolyConecta.Domain.ValueObjects;
using Xunit;

namespace PolyConecta.Domain.Tests;

public class RolloMaestroTests
{
    [Fact]
    public void Folio_Generate_ShouldMatchRegexPattern()
    {
        var timestamp = new DateTime(2026, 9, 10, 4, 27, 47);
        var folio = Folio.Generate(1, timestamp);

        folio.Value.Should().Be("EX-01-260910-042747");
    }

    [Fact]
    public void RolloMaestro_NetWeight_ShouldEqualGrossMinusTare()
    {
        var roll = new RolloMaestro
        {
            GrossWeightKg = 155.400m,
            TareWeightKg = 5.400m
        };

        roll.NetWeightKg.Should().Be(150.000m);
    }
}
