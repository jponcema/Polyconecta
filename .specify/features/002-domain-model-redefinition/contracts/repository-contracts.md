# Contracts: Domain Repository & Service Interfaces (SPEC-002)

Estos son los contratos internos que `PolyConecta.Api`/`PolyConecta.Presentation` consumirán del dominio redefinido. No son endpoints HTTP (esos se derivan de aquí en la fase de tasks/implementación); son las interfaces de `PolyConecta.Domain.Repositories` y `.Services` que reemplazan el acceso directo a `PolyDbContext` desde `OrdersController` (violación de capas detectada en la revisión previa).

## Repositorios

```csharp
public interface ISalesOrderRepository : IRepository<SalesOrder, Guid>
{
    // Carga SalesOrder junto con su colección de SalesOrderLine (maestro + detalle, como en CONTPAQi)
    Task<SalesOrder?> GetByErpDocumentIdAsync(int erpDocumentId, CancellationToken ct = default);
    Task<IReadOnlyList<SalesOrder>> GetPendingApprovalsAsync(CancellationToken ct = default);
}

public interface ISalesOrderLineRepository : IRepository<SalesOrderLine, Guid>
{
    Task<IReadOnlyList<SalesOrderLine>> GetBySalesOrderAsync(Guid salesOrderId, CancellationToken ct = default);
}

public interface IManufacturingOrderRepository : IRepository<ManufacturingOrder, Guid>
{
    Task<ManufacturingOrder?> GetBySalesOrderLineIdAsync(Guid salesOrderLineId, CancellationToken ct = default);
}

public interface IProcessOrderRepository : IRepository<ProcessOrder, Guid>
{
    Task<IReadOnlyList<ProcessOrder>> GetByManufacturingOrderAsync(Guid manufacturingOrderId, CancellationToken ct = default);
    Task<IReadOnlyList<ProcessOrder>> GetPendingTechnicalClosureAsync(Guid plantId, CancellationToken ct = default);
}

public interface IWorkOrderRepository : IRepository<WorkOrder, Guid>
{
    Task<IReadOnlyList<WorkOrder>> GetPendingScheduleAsync(Guid plantId, CancellationToken ct = default);
}

public interface IStockLotRepository : IRepository<StockLot, Guid>
{
    Task<StockLot?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<StockLot>> GetByProcessOrderAsync(Guid processOrderId, CancellationToken ct = default);
}
```

`IOrderRepository` (actual, sobre `ManufacturingOrder` mezclado con el legado) se retira; se sustituye por los cuatro repositorios de arriba, uno por documento con ciclo de vida propio — evita el problema actual de que un solo repositorio intente cubrir `OM`, `OF` y el modelo legado a la vez.

## Servicios de dominio

```csharp
public interface IReferenceSequenceService
{
    // documentType: "OM" | "OF-EXT" | "OF-IMP" | "OF-BOL" | "WO" | "LOTE"
    Task<string> NextAsync(string documentType, CancellationToken ct = default);
}

public interface IMassBalanceService
{
    MassBalanceAudit AuditProcessOrderClosure(
        Guid processOrderId,
        decimal totalInputKg,
        decimal totalGoodRollsKg,
        decimal totalQuarantineKg,
        decimal totalScrapKg,
        decimal tolerancePercentage);
}

public interface IIntercompanyMirrorService
{
    // Stub — Fase 2 (Constitución Principio VI). Sin implementación en esta spec.
    Task MirrorAsync(Guid stockPickingId, CancellationToken ct = default);
}
```

## Regla de consumo (Api)

`PolyConecta.Api.Controllers.*` MUST depender únicamente de estas interfaces (inyectadas por DI), nunca de `PolyDbContext` directamente. Esto cierra el gap detectado en `OrdersController.CreateMasterOrder`, que hoy escribe en `PolyDbContext` sin pasar por `IOrderRepository`.
