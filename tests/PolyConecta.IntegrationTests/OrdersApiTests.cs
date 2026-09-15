using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Api.Controllers;
using PolyConecta.Infrastructure.Persistence;
using Xunit;

namespace PolyConecta.IntegrationTests;

public class OrdersApiTests
{
    private PolyDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PolyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new PolyDbContext(options);
    }

    [Fact]
    public async Task CreateMasterOrder_ShouldAutoDecomposeProcessSubOrders()
    {
        var db = GetDbContext();
        var controller = new OrdersController(db);

        var request = new OrdersController.CreateMasterOrderRequest(421, "CLI-100", "PT-BAG-001", 5000.0m);
        var result = await controller.CreateMasterOrder(request, CancellationToken.None);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>();

        var masterOrders = await db.MasterOrders.Include(x => x.SubOrders).ToListAsync();
        masterOrders.Should().HaveCount(1);
        masterOrders[0].SubOrders.Should().HaveCount(3);
        masterOrders[0].SubOrders.Select(s => s.ProcessType).Should().Contain(new[] { "EXT", "IMP", "BOL" });
        masterOrders[0].SubOrders.All(s => s.Status == "Borrador").Should().BeTrue();
    }
}
