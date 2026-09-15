namespace PolyConecta.Domain.Entities;

public class RolloMaestro
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubOrderId { get; set; }
    public string Folio { get; set; } = string.Empty; // "EX-01-260910-042747"
    public string LotNumber { get; set; } = string.Empty; // "cNumeroLote" in CONTPAQi
    public string ProductSku { get; set; } = string.Empty;

    public decimal GrossWeightKg { get; set; }
    public decimal TareWeightKg { get; set; }
    public decimal NetWeightKg => GrossWeightKg - TareWeightKg;

    public decimal LengthMeters { get; set; }
    public decimal GaugeMicron { get; set; }
    public decimal WidthMm { get; set; }
    public decimal DynesCm { get; set; } = 38.0m;

    public string MachineId { get; set; } = string.Empty;
    public string Shift { get; set; } = "Turno 1"; // "Turno 1", "Turno 2", "Turno 3"
    public string OperatorId { get; set; } = string.Empty;
    public string Status { get; set; } = "Available"; // "Available", "In_Use", "Consumed", "Quarantine", "Scrap"
    public string LocationCode { get; set; } = "PIM/Produccion";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
