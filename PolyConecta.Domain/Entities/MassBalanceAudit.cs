namespace PolyConecta.Domain.Entities;

public class MassBalanceAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubOrderId { get; set; }
    public decimal TotalMpInputKg { get; set; }
    public decimal TotalRollOutputKg { get; set; }
    public decimal TotalScrapOutputKg { get; set; }
    public decimal VariancePercentage { get; set; }
    public bool AuditPassed { get; set; }
    public string? FlagReason { get; set; }
    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;
}
