using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence.Configurations;

public class RolloMaestroConfiguration : IEntityTypeConfiguration<RolloMaestro>
{
    public void Configure(EntityTypeBuilder<RolloMaestro> builder)
    {
        builder.ToTable("poly_master_rolls");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Folio).HasMaxLength(30).IsRequired();
        builder.HasIndex(x => x.Folio).IsUnique();

        builder.Property(x => x.LotNumber).HasMaxLength(30).IsRequired();
        builder.Property(x => x.ProductSku).HasMaxLength(30).IsRequired();

        builder.Property(x => x.GrossWeightKg).HasPrecision(10, 3).IsRequired();
        builder.Property(x => x.TareWeightKg).HasPrecision(10, 3).IsRequired();
        builder.Ignore(x => x.NetWeightKg); // Computed property

        builder.Property(x => x.LengthMeters).HasPrecision(10, 2).IsRequired();
        builder.Property(x => x.GaugeMicron).HasPrecision(8, 2).IsRequired();
        builder.Property(x => x.WidthMm).HasPrecision(8, 2).IsRequired();
        builder.Property(x => x.DynesCm).HasPrecision(5, 1).HasDefaultValue(38.0m);

        builder.Property(x => x.MachineId).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Shift).HasMaxLength(10).IsRequired();
        builder.Property(x => x.OperatorId).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();
        builder.Property(x => x.LocationCode).HasMaxLength(50).IsRequired();
    }
}
