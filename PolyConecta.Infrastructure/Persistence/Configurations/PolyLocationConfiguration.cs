using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence.Configurations;

public class PolyLocationConfiguration : IEntityTypeConfiguration<PolyLocation>
{
    public void Configure(EntityTypeBuilder<PolyLocation> builder)
    {
        builder.ToTable("poly_locations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CidAlmacenContpaq).IsRequired();
        builder.HasIndex(x => x.CidAlmacenContpaq).IsUnique();

        builder.Property(x => x.LocationCode).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.LocationCode).IsUnique();

        builder.Property(x => x.LocationName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PlantCode).HasMaxLength(10).IsRequired();
        builder.Property(x => x.WarehouseType).HasMaxLength(30).IsRequired();

        builder.HasData(
            new PolyLocation { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), CidAlmacenContpaq = 1, LocationCode = "PIM/Stock/MP", LocationName = "Apodaca MP Stock", PlantCode = "PIM", WarehouseType = "RawMaterial" },
            new PolyLocation { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), CidAlmacenContpaq = 2, LocationCode = "PIM/Produccion", LocationName = "Apodaca WIP Production", PlantCode = "PIM", WarehouseType = "Production" },
            new PolyLocation { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), CidAlmacenContpaq = 3, LocationCode = "PIM/Stock/PT", LocationName = "Apodaca PT Stock", PlantCode = "PIM", WarehouseType = "FinishedGoods" },
            new PolyLocation { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), CidAlmacenContpaq = 4, LocationCode = "PIM/Cuarentena", LocationName = "Apodaca Quality Quarantine", PlantCode = "PIM", WarehouseType = "Quarantine" }
        );
    }
}
