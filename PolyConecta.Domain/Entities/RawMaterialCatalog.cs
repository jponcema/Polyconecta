using System.Text.Json.Serialization;

namespace PolyConecta.Domain.Entities;

public class RawMaterialCatalog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string InternalSku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // VirginResin, Additive, Pigment, Recycled
    public decimal MfiMeltFlowIndex { get; set; }
    public decimal DensityGcm3 { get; set; }
    public string TargetHopper { get; set; } = "Cualquiera"; // Tolva A, Tolva B, Tolva C, Cualquiera
    public int CidProductoContpaq { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SupplierProductMapping> SupplierMappings { get; set; } = new List<SupplierProductMapping>();
}

public class SupplierProductMapping
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RawMaterialCatalogId { get; set; }
    public string SupplierCode { get; set; } = string.Empty; // e.g., DOW, BRASKEM, ALSEA
    public string SupplierProductName { get; set; } = string.Empty;
    public string SupplierSku { get; set; } = string.Empty;
    public DateTime MappedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public RawMaterialCatalog? RawMaterialCatalog { get; set; }
}
