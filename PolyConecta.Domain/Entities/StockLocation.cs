namespace PolyConecta.Domain.Entities;

public class StockLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // e.g. "PIM/Stock/MP"
    public string PlantCode { get; set; } = "PIM"; // "PIM", "STC", "MTM"
    public string Usage { get; set; } = "Internal"; // "Internal", "Production", "Transit", "Inventory", "Customer", "Quarantine"
    public int? ContpaqWarehouseId { get; set; } // CONTPAQi admAlmacenes CIDALMACEN
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
