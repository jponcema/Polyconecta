using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence;

public class PolyDbContext : DbContext
{
    public PolyDbContext(DbContextOptions<PolyDbContext> options) : base(options) { }

    public DbSet<PolyLocation> Locations => Set<PolyLocation>();
    public DbSet<RolloMaestro> MasterRolls => Set<RolloMaestro>();
    public DbSet<MasterOrder> MasterOrders => Set<MasterOrder>();
    public DbSet<SubOrder> SubOrders => Set<SubOrder>();
    public DbSet<LotGenealogy> LotGenealogies => Set<LotGenealogy>();
    public DbSet<MassBalanceAudit> MassBalanceAudits => Set<MassBalanceAudit>();
    public DbSet<RawMaterialCatalog> RawMaterialCatalogs => Set<RawMaterialCatalog>();
    public DbSet<SupplierProductMapping> SupplierProductMappings => Set<SupplierProductMapping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PolyDbContext).Assembly);
    }
}
