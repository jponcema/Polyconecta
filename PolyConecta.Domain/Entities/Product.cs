namespace PolyConecta.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "RawMaterial"; // "RawMaterial", "FinishedGood", "Wip", "Scrap"
    public string Uom { get; set; } = "KG"; // "KG", "MT", "PZA"
    public int? ContpaqProductId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
