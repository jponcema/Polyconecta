using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Api.Controllers;
using PolyConecta.Domain.Entities;
using PolyConecta.Infrastructure.Outbox;
using PolyConecta.Infrastructure.Persistence;
using Xunit;

namespace PolyConecta.IntegrationTests;

public class LocationsApiTests
{
    private PolyDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PolyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var db = new PolyDbContext(options);
        db.Locations.AddRange(
            new PolyLocation { CidAlmacenContpaq = 1, LocationCode = "PIM/Stock/MP", LocationName = "Apodaca MP", PlantCode = "PIM", WarehouseType = "RawMaterial" },
            new PolyLocation { CidAlmacenContpaq = 2, LocationCode = "PIM/Produccion", LocationName = "Apodaca Production", PlantCode = "PIM", WarehouseType = "Production" },
            new PolyLocation { CidAlmacenContpaq = 4, LocationCode = "PIM/Cuarentena", LocationName = "Apodaca Quarantine", PlantCode = "PIM", WarehouseType = "Quarantine" }
        );
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task Transfer_FromQuarantine_ShouldReturnUnprocessableEntity_HardStopGate()
    {
        var db = GetDbContext();
        var outbox = new OutboxPublisher();
        var controller = new LocationsController(db, outbox);

        var request = new LocationsController.StockTransferRequest("EX-01-260910-042747", "PIM/Cuarentena", "PIM/Stock/PT", "OP-01");
        var result = await controller.ExecuteStockTransfer(request, CancellationToken.None);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.UnprocessableEntityObjectResult>();
    }
}
