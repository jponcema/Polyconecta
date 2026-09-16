namespace PolyConecta.Domain.Entities;

public class Bom
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public string Code { get; set; } = string.Empty;
    public decimal LayerAPercentage { get; set; } = 33.33m;
    public decimal LayerBPercentage { get; set; } = 33.34m;
    public decimal LayerCPercentage { get; set; } = 33.33m;

    public List<BomLine> Lines { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class BomLine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BomId { get; set; }
    
    public Guid ProductId { get; set; } // Raw Material Product
    public Product? Product { get; set; }

    public string Layer { get; set; } = "A"; // "A", "B", "C"
    public decimal ComponentPercentage { get; set; }
}
