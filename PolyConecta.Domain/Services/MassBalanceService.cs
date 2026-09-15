using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Services;

public class MassBalanceService
{
    public static MassBalanceAudit AuditSubOrderRun(Guid subOrderId, decimal mpInputKg, decimal rollOutputKg, decimal scrapOutputKg, decimal maxTolerancePercent = 2.0m)
    {
        if (mpInputKg <= 0)
        {
            throw new ArgumentException("Input raw material weight must be greater than zero.", nameof(mpInputKg));
        }

        var totalOutput = rollOutputKg + scrapOutputKg;
        var variancePercentage = Math.Abs((mpInputKg - totalOutput) / mpInputKg) * 100.0m;
        var auditPassed = variancePercentage <= maxTolerancePercent;

        return new MassBalanceAudit
        {
            SubOrderId = subOrderId,
            TotalMpInputKg = mpInputKg,
            TotalRollOutputKg = rollOutputKg,
            TotalScrapOutputKg = scrapOutputKg,
            VariancePercentage = Math.Round(variancePercentage, 3),
            AuditPassed = auditPassed,
            FlagReason = auditPassed ? null : $"Mass Balance Audit Failed: Variance of {variancePercentage:F2}% exceeds allowed tolerance of {maxTolerancePercent:F1}%."
        };
    }
}
