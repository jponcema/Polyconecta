# Quickstart: Validating SPEC-002 Domain Model Redefinition

## Prerrequisitos

- .NET 8 SDK
- PostgreSQL accesible (o `dotnet ef migrations` en modo `InMemory` para pruebas unitarias, que no requieren base real)

## Pasos de validación (una vez implementada esta spec)

1. **Compilación limpia sin el modelo legado**:
   ```bash
   dotnet build PolyConecta.Domain/PolyConecta.Domain.csproj
   dotnet build PolyConecta.Infrastructure/PolyConecta.Infrastructure.csproj
   ```
   No debe haber ninguna referencia a `MasterOrder`, `SubOrder` ni `RolloMaestro` en el árbol de compilación (`grep -r "MasterOrder\|SubOrder\|RolloMaestro" PolyConecta.Domain PolyConecta.Infrastructure PolyConecta.Api` debe devolver vacío).

2. **Pruebas de las máquinas de estado**:
   ```bash
   dotnet test tests/PolyConecta.Domain.Tests
   ```
   Debe incluir, como mínimo, un caso por cada fila de las tablas de transición de [`data-model.md`](./data-model.md) que tiene una precondición (rechazo de `ProcessOrder.Confirm()` sin BOM, rechazo de `StockPicking.ValidateDispatch()` con lote en cuarentena, renombrado `.S` + liberación de slot en `StockLot` tras `QualityCheck.Reject()`).

3. **Migración de base de datos**:
   ```bash
   dotnet ef migrations add DomainModelRedefinition --project PolyConecta.Infrastructure --startup-project PolyConecta.Api
   dotnet ef database update --project PolyConecta.Infrastructure --startup-project PolyConecta.Api
   ```
   Revisar el script generado para confirmar que elimina las tablas legado (`MasterOrders`, `SubOrders`, `MasterRolls`) en vez de dejarlas huérfanas.

4. **Trazabilidad 1:1 con la Matriz de Discrepancias Validada** (SC-001 de spec.md): para cada uno de los 17 puntos de `INFORME_VALIDACION_DIAGRAMA_OPERATIVO.md` §4, confirmar que existe al menos una entidad, campo o regla de transición que lo implementa — usar la tabla de "Validaciones por entidad" y las tablas de transición de `data-model.md` como checklist.

## Fuera de este quickstart

- Probar la sincronización real con CONTPAQi (`IBridgeSyncService`) — depende de `PolyConecta.Contpaq`, no cambia en esta spec.
- Probar la capa de presentación — cubierto por `003-presentation-shell-consolidation/quickstart.md`.
