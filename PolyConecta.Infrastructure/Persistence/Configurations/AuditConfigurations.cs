using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence.Configurations;

public class LotGenealogyConfiguration : IEntityTypeConfiguration<LotGenealogy>
{
    public void Configure(EntityTypeBuilder<LotGenealogy> builder)
    {
        builder.ToTable("poly_lot_genealogy");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ParentLotNumber).HasMaxLength(30).IsRequired();
        builder.Property(x => x.ChildLotNumber).HasMaxLength(30).IsRequired();
        builder.Property(x => x.QuantityConsumedKg).HasPrecision(10, 3).IsRequired();
    }
}

public class MassBalanceAuditConfiguration : IEntityTypeConfiguration<MassBalanceAudit>
{
    public void Configure(EntityTypeBuilder<MassBalanceAudit> builder)
    {
        builder.ToTable("poly_mass_balance_audits");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SubOrderId).IsRequired();
        builder.Property(x => x.TotalMpInputKg).HasPrecision(12, 3).IsRequired();
        builder.Property(x => x.TotalRollOutputKg).HasPrecision(12, 3).IsRequired();
        builder.Property(x => x.TotalScrapOutputKg).HasPrecision(12, 3).IsRequired();
        builder.Property(x => x.VariancePercentage).HasPrecision(6, 3).IsRequired();
        builder.Property(x => x.AuditPassed).IsRequired();
    }
}
