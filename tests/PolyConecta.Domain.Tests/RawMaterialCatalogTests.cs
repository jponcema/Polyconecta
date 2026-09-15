using PolyConecta.Domain.Entities;
using Xunit;

namespace PolyConecta.Domain.Tests;

public class RawMaterialCatalogTests
{
    [Fact]
    public void RawMaterialCatalog_Initialization_ShouldDefaultsCorrectly()
    {
        var rawMat = new RawMaterialCatalog
        {
            InternalSku = "MP-RES-HD-001",
            Name = "Resina Polietileno Alta Densidad",
            Category = "VirginResin",
            MfiMeltFlowIndex = 0.95m,
            DensityGcm3 = 0.958m,
            TargetHopper = "Tolva A",
            CidProductoContpaq = 101
        };

        Assert.NotEqual(Guid.Empty, rawMat.Id);
        Assert.True(rawMat.IsActive);
        Assert.Equal("MP-RES-HD-001", rawMat.InternalSku);
        Assert.Empty(rawMat.SupplierMappings);
    }

    [Fact]
    public void SupplierProductMapping_Association_ShouldLinkToParent()
    {
        var rawMat = new RawMaterialCatalog
        {
            InternalSku = "MP-RES-HD-001",
            Name = "Resina Polietileno Alta Densidad"
        };

        var mapping = new SupplierProductMapping
        {
            RawMaterialCatalogId = rawMat.Id,
            SupplierCode = "DOW",
            SupplierProductName = "Dowlex 2045G",
            SupplierSku = "DOW-2045G"
        };

        rawMat.SupplierMappings.Add(mapping);

        Assert.Single(rawMat.SupplierMappings);
        Assert.Equal("DOW", rawMat.SupplierMappings.First().SupplierCode);
    }
}
