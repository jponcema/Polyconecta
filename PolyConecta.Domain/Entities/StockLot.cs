namespace PolyConecta.Domain.Entities;

public class StockLot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public string Name { get; set; } = string.Empty; // "EX-01-260910-042747"
    public string ContpaqLotNumber { get; set; } = string.Empty; // "cNumeroLote" CONTPAQi

    public decimal GrossWeightKg { get; set; }
    public decimal TareWeightKg { get; set; }
    public decimal NetWeightKg => GrossWeightKg - TareWeightKg;

    public decimal LengthMeters { get; set; }
    public decimal GaugeMicron { get; set; }
    public decimal WidthMm { get; set; }
    public decimal DynesCm { get; set; } = 38.0m;

    public string MachineId { get; set; } = string.Empty;
    public string Shift { get; set; } = "Turno 1";
    public string OperatorId { get; set; } = string.Empty;
    public string Status { get; set; } = "Available"; // "Available", "Quarantine", "Consumed", "Scrap"
    public string CurrentLocationCode { get; set; } = "PIM/Stock/PT";

    public DateTime ProducedAt { get; set; } = DateTime.UtcNow;
}
