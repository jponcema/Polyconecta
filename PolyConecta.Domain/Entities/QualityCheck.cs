namespace PolyConecta.Domain.Entities;

public class QualityCheck
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid? StockLotId { get; set; }
    public StockLot? StockLot { get; set; }

    public Guid? ManufacturingOrderId { get; set; }
    public ManufacturingOrder? ManufacturingOrder { get; set; }

    public decimal GaugeMeasured { get; set; }
    public decimal DynesMeasured { get; set; } = 38.0m;
    public bool VisualInspectionPassed { get; set; } = true;

    public string QualityState { get; set; } = "None"; // "None", "Pass", "Fail", "Quarantine"
    public string InspectorId { get; set; } = string.Empty;
    public DateTime InspectedAt { get; set; } = DateTime.UtcNow;
}

public class StockScrap
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid? ManufacturingOrderId { get; set; }
    public ManufacturingOrder? ManufacturingOrder { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public decimal ScrapQty { get; set; }
    public Guid? LocationId { get; set; }
    public string ScrapReason { get; set; } = "Extrusion Startup / Edge Trimming";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
