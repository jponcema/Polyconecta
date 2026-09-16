using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Services;

public class MassBalanceAuditResult
{
    public Guid ManufacturingOrderId { get; set; }
    public decimal TotalMpInputKg { get; set; }
    public decimal TotalRollOutputKg { get; set; }
    public decimal TotalScrapOutputKg { get; set; }
    public decimal VariancePercentage { get; set; }
    public bool AuditPassed { get; set; }
    public string? FlagReason { get; set; }
}

public class MassBalanceService
{
    public static MassBalanceAuditResult AuditManufacturingRun(Guid manufacturingOrderId, decimal mpInputKg, decimal rollOutputKg, decimal scrapOutputKg, decimal maxTolerancePercent = 2.0m)
    {
        if (mpInputKg <= 0)
        {
            throw new ArgumentException("Input raw material weight must be greater than zero.", nameof(mpInputKg));
        }

        var totalOutput = rollOutputKg + scrapOutputKg;
        var variancePercentage = Math.Abs((mpInputKg - totalOutput) / mpInputKg) * 100.0m;
        var auditPassed = variancePercentage <= maxTolerancePercent;

        return new MassBalanceAuditResult
        {
            ManufacturingOrderId = manufacturingOrderId,
            TotalMpInputKg = mpInputKg,
            TotalRollOutputKg = rollOutputKg,
            TotalScrapOutputKg = scrapOutputKg,
            VariancePercentage = Math.Round(variancePercentage, 3),
            AuditPassed = auditPassed,
            FlagReason = auditPassed ? null : $"Mass Balance Audit Failed: Variance of {variancePercentage:F2}% exceeds allowed tolerance of {maxTolerancePercent:F1}%."
        };
    }

    public static MassBalanceAudit AuditSubOrderRun(Guid subOrderId, decimal mpInputKg, decimal rollOutputKg, decimal scrapOutputKg, decimal maxTolerancePercent = 2.0m)
    {
        var result = AuditManufacturingRun(subOrderId, mpInputKg, rollOutputKg, scrapOutputKg, maxTolerancePercent);
        return new MassBalanceAudit
        {
            SubOrderId = subOrderId,
            TotalMpInputKg = result.TotalMpInputKg,
            TotalRollOutputKg = result.TotalRollOutputKg,
            TotalScrapOutputKg = result.TotalScrapOutputKg,
            VariancePercentage = result.VariancePercentage,
            AuditPassed = result.AuditPassed,
            FlagReason = result.FlagReason
        };
    }
}
