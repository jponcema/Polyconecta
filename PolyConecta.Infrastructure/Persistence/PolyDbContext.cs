using Microsoft.EntityFrameworkCore;
using PolyConecta.Domain.Entities;

namespace PolyConecta.Infrastructure.Persistence;

public class PolyDbContext : DbContext
{
    public PolyDbContext(DbContextOptions<PolyDbContext> options) : base(options) { }

    // Odoo-Native Core DbSets
    public DbSet<Product> Products => Set<Product>();
    public DbSet<StockLot> StockLots => Set<StockLot>();
    public DbSet<ManufacturingOrder> ManufacturingOrders => Set<ManufacturingOrder>();
    public DbSet<Bom> Boms => Set<Bom>();
    public DbSet<BomLine> BomLines => Set<BomLine>();
    public DbSet<StockLocation> StockLocations => Set<StockLocation>();
    public DbSet<StockPicking> StockPickings => Set<StockPicking>();
    public DbSet<StockMove> StockMoves => Set<StockMove>();
    public DbSet<QualityCheck> QualityChecks => Set<QualityCheck>();
    public DbSet<StockScrap> StockScraps => Set<StockScrap>();

    // Additional Entities
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
