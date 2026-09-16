namespace PolyConecta.Domain.Entities;

public class StockPicking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // e.g. "PIM-TR-OUT-001"
    public string PickingType { get; set; } = "Internal_Transfer"; // "MO", "Internal_Transfer", "Customer_Shipment"

    public Guid? LocationId { get; set; } // Source Location
    public StockLocation? SourceLocation { get; set; }

    public Guid? LocationDestId { get; set; } // Destination Location
    public StockLocation? DestinationLocation { get; set; }

    public string State { get; set; } = "Draft"; // "Draft", "Waiting", "Ready", "Done", "Cancelled"
    public DateTime ScheduledDate { get; set; } = DateTime.UtcNow;

    public List<StockMove> Moves { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class StockMove
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? StockPickingId { get; set; }
    public Guid? ManufacturingOrderId { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid? StockLotId { get; set; }
    public StockLot? StockLot { get; set; }

    public decimal ProductUomQty { get; set; }

    public Guid LocationId { get; set; }
    public StockLocation? SourceLocation { get; set; }

    public Guid LocationDestId { get; set; }
    public StockLocation? DestinationLocation { get; set; }

    public string State { get; set; } = "Draft"; // "Draft", "Reserved", "Done"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
