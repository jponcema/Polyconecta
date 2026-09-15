namespace PolyConecta.Domain.Entities;

public class SubOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MasterOrderId { get; set; }
    public string FolioOf { get; set; } = string.Empty; // "OF-EXT-2026-0421-1"
    public string ProcessType { get; set; } = "EXT"; // "EXT", "IMP", "BOL"
    public string MachineId { get; set; } = string.Empty;
    public string Status { get; set; } = "Borrador"; // "Borrador", "Programado", "En_Proceso", "Control_Calidad", "Finalizado", "Scrap"
    public decimal PlannedQtyKg { get; set; }
    public decimal ProducedQtyKg { get; set; } = 0.0m;
    public decimal ScrapQtyKg { get; set; } = 0.0m;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
