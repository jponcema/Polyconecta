using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence.Configurations;

public class MasterOrderConfiguration : IEntityTypeConfiguration<MasterOrder>
{
    public void Configure(EntityTypeBuilder<MasterOrder> builder)
    {
        builder.ToTable("poly_master_orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FolioOm).HasMaxLength(30).IsRequired();
        builder.HasIndex(x => x.FolioOm).IsUnique();

        builder.Property(x => x.CidDocumentoPedido).IsRequired();
        builder.Property(x => x.CustomerCode).HasMaxLength(30).IsRequired();
        builder.Property(x => x.PtSku).HasMaxLength(30).IsRequired();
        builder.Property(x => x.TargetQuantityKg).HasPrecision(12, 3).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();

        builder.HasMany(x => x.SubOrders)
            .WithOne()
            .HasForeignKey(s => s.MasterOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SubOrderConfiguration : IEntityTypeConfiguration<SubOrder>
{
    public void Configure(EntityTypeBuilder<SubOrder> builder)
    {
        builder.ToTable("poly_sub_orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FolioOf).HasMaxLength(35).IsRequired();
        builder.HasIndex(x => x.FolioOf).IsUnique();

        builder.Property(x => x.ProcessType).HasMaxLength(10).IsRequired();
        builder.Property(x => x.MachineId).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();

        builder.Property(x => x.PlannedQtyKg).HasPrecision(12, 3).IsRequired();
        builder.Property(x => x.ProducedQtyKg).HasPrecision(12, 3).HasDefaultValue(0.0m);
        builder.Property(x => x.ScrapQtyKg).HasPrecision(12, 3).HasDefaultValue(0.0m);
    }
}
