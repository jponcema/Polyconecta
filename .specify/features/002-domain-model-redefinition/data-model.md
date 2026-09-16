# Phase 1 Data Model: SPEC-002 Domain Model Redefinition

El diagrama de entidades y sus campos ya está publicado en [`spec.md`](./spec.md#key-entities--data-model) — este documento no lo repite. Aquí se agregan las dos cosas que el diagrama no expresa: **las reglas de validación por entidad** (derivadas de los Functional Requirements) y **las tablas de transición de estado** de los siete documentos con ciclo de vida.

## Mixins (pseudocódigo agnóstico de ORM)

```text
AuditableEntity:
    Id: Guid (technical PK, immutable)
    CreatedAt: DateTime (set once, on insert)
    CreatedBy: string
    UpdatedAt: DateTime (set on every update)
    UpdatedBy: string

ArchivableEntity:
    IsActive: bool = true   // archivar, nunca DELETE físico de un registro referenciado

StatefulDocument<TState>:
    State: TState (enum cerrado)
    // Toda transición pasa por un método con nombre de acción; escribe una fila en StateTransitionLog
```

## Tablas de transición de estado

### `SalesOrder`

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| `Draft` | `Confirm()` | Producto y cantidad solicitada presentes | `Confirmed` |
| `Confirmed` | `ValidateSales(user)` | — | `Confirmed` (marca `sales_approved=true`; si `credit_approved` ya es true, dispara `Authorized`) |
| `Confirmed` | `ValidateCredit(user)` | — | `Confirmed` (marca `credit_approved=true`; si `sales_approved` ya es true, dispara `Authorized`) |
| `Confirmed` | *(automática)* | `sales_approved && credit_approved` | `Authorized` |
| `Authorized` | *(disparada por `ManufacturingOrder` al programar su primera `WorkOrder`)* | — | `InProgress` |
| `InProgress` | *(disparada al cerrar la última `ProcessOrder` hija)* | — | `Done` |
| `Draft`/`Confirmed`/`Authorized` | `Cancel()` | Ninguna `ProcessOrder` hija en `InProgress`/`Done` | `Cancelled` |

### `ManufacturingOrder` (OM)

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| `Draft` | *(automática al sincronizar `SalesOrder.Authorized`, una por cada `SalesOrderLine` del pedido)* | — | `Approved` |
| `Approved` | *(automática)* | Al menos una `ProcessOrder` hija en `InProgress` | `InProgress` |
| `InProgress` | `Close()` | Todas las `ProcessOrder` hijas en `Done` | `Done` |
| Cualquiera excepto `Done` | `Cancel()` | Ninguna `ProcessOrder` hija en `InProgress`/`Done` | `Cancelled` |

### `ProcessOrder` (OF)

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| `Draft` | `LoadBom(file)` | Solo si `process_type == Extrusion` | `Draft` (con `bom_id` asignado, insumos reservados) |
| `Draft` | `Confirm()` | Si `process_type == Extrusion`: `bom_id` no nulo (FR-004). Precarga `ProductionSlot`s + `QualityCheck`s pendientes (FR-014) | `Confirmed` |
| `Confirmed` | *(automática al asignar la primera `WorkOrder`)* | — | `InProgress` |
| `InProgress` | `Close()` | Todos los `ProductionSlot` de la orden tienen `QualityCheck` en estado terminal (FR-019) | `TechnicalClosure` → (tras generar `MassBalanceAudit`) → `Done` |
| `Draft`/`Confirmed` | `Cancel()` | — | `Cancelled` |

### `WorkOrder` (WO)

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| *(creación)* | — | Se crea automáticamente al confirmar la `ProcessOrder` (FR-005) | `PendingSchedule` |
| `PendingSchedule` | `Schedule(workCenterId, date)` | `workCenterId` activo y del mismo `Plant` que la `ProcessOrder` | `Scheduled` |
| `Scheduled` | *(inicio de turno)* | — | `InProgress` |
| `InProgress` | `Complete()` | — | `Done` |
| `Scheduled`/`InProgress` | `Reschedule(workCenterId, date)` | — | Permanece en su estado, pero queda una fila nueva en `StateTransitionLog` (edge case de spec.md) |

### `StockLot`

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| *(creación)* | `Weigh(gross, tare)` sobre un `ProductionSlot.Empty` | El `ProductionSlot` pertenece a una `ProcessOrder` en `InProgress` | `Available` (provisional, pendiente de `QualityCheck`) |
| `Available` (provisional) | `QualityCheck.Approve()` | — | `Available` (definitivo); `Name` = `{Folio}-R{SequenceNumber:000}` |
| `Available` (provisional) | `QualityCheck.Reject()` | — | `Quarantine`; `Name` = `{Folio}-R{SequenceNumber:000}.S`; `CurrentLocationId` = ubicación de cuarentena de la planta; **el `ProductionSlot` vuelve a `Empty` conservando `SequenceNumber`** (FR-016) |
| `Available` | *(consumo en conversión o despacho)* | — | `Consumed` |
| `Quarantine` | *(decisión de Dirección/Planner: retrabajo, reasignación o scrap)* | — | `Consumed` o `ScrappedOut` según decisión (fuera del alcance de detalle de esta spec — ver `INFORME_VALIDACION_DIAGRAMA_OPERATIVO.md` §5.2) |

### `QualityCheck`

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| *(creación)* | — | Se precarga junto con su `ProductionSlot` al confirmar la `ProcessOrder` (FR-014) | `Pending` |
| `Pending` | `Approve()` | El `StockLot` asociado tiene peso capturado | `Passed` |
| `Pending` | `Reject()` | El `StockLot` asociado tiene peso capturado | `Failed` (dispara `StockLot` → `Quarantine`) |

### `StockPicking`

| Desde | Operación | Precondición | Hacia |
|---|---|---|---|
| `Draft` | *(automática al reservar `StockMove`s)* | — | `Waiting` |
| `Waiting` | *(todos los `StockMove` con `StockLot` asignado)* | — | `Ready` |
| `Ready` | `ValidateDispatch()` | Ningún `StockLot` incluido está en `Quarantine` ni sin `QualityCheck.Passed` vigente (FR-021, hard-stop Principio IV) | `Done` (dispara `IBridgeSyncService` según `OperationType.TriggersErpDocumentType`) |
| `Ready` (solo `OperationType` de 2 pasos) | `ValidateReceipt()` | Ejecutado en la planta destino, no en origen (FR-022) | `Done` (recién aquí se dispara la sincronización ERP) |
| Cualquiera excepto `Done` | `Cancel()` | — | `Cancelled` |

## Validaciones por entidad (derivadas de Functional Requirements)

| Entidad | Regla | FR |
|---|---|---|
| `Product` | `Sku` único y obligatorio; `Category` determina qué especialización delegada puede existir | FR-007, FR-008 |
| `RollSpecification` / `PtSpecification` | Solo una instancia por `Product`; `PtSpecification.RelatedRollSpecificationId` es **obligatoria** (nunca nula) — apunta al rollo del que se deriva el PT, sea el mismo rollo vendido tal cual o uno distinto de un segundo proceso | FR-009 |
| `SalesOrderLine` | `TargetProductionKg` y `TolerancePercentageOverride` son propios de cada línea/corrida, nunca copiados desde `RollSpecification` | FR-010b |
| `Product` | `BaseUom` siempre `KG` — ningún cálculo de MRP/balance de masa MUST leer otra unidad | FR-010c |
| `PackagingUnit` / `SalesOrderLine` | La unidad de venta es configurable por producto y elegible por línea; `RequestedQtyKg` MUST ser siempre un valor calculado (`RequestedQty × PackagingUnit.ConversionToKg`), nunca capturado independientemente | FR-010d |
| `Bom` / `BomLine` | `Sum(ComponentPercentage)` por capa debe poder validarse contra 100% antes de permitir `ProcessOrder.Confirm()` | FR-004 |
| `ProductionSlot` | `SlotNumber` único dentro de su `ProcessOrder`; no se elimina al pasar a cuarentena, solo cambia de estado | FR-016 |
| `StockLot` | `Name` es campo calculado, nunca editable directamente (FR-015); `NetWeightKg` es calculado en memoria (`Gross - Tare`), no almacenado |
| `ScrapEntry` | `ScrapReasonCodeId` obligatorio; no se acepta motivo en texto libre como único dato (FR-017) |
| `MassBalanceAudit` | Se genera una sola vez por `ProcessOrder.Close()`; `TolerancePercentageApplied` se lee de configuración, no se hardcodea (FR-018) |
| `OperationType` | `RequiresQualityRelease = true` para `PIM-OUT-DIR` y `PIM-TR-OUT`; `false` solo para movimientos internos sin salida física | FR-020, FR-021 |
| `StockLocation` | `PlantId` obligatorio; `LegalEntity` nunca se asigna directamente, se resuelve vía `Plant` | FR-023 |

## Relaciones — resumen de cardinalidad

- `LegalEntity` 1—N `Plant` 1—N `{WorkCenter, StockLocation, Operator}`
- `Customer` 1—N `SalesOrder` 1—N `SalesOrderLine` 1—1 `ManufacturingOrder` 1—N `ProcessOrder` 1—N `WorkOrder`
- `Product` 1—0/1 `RollSpecification`; `Product` (categoría `FinishedGood`) 1—0/1 `PtSpecification` 1—1 `RollSpecification` (referencia obligatoria, nunca opcional — ver spec.md FR-009)
- `ProcessOrder` 1—N `ProductionSlot` 1—1(opcional) `StockLot` 1—1 `QualityCheck`
- `Product` 1—0/1 `RawMaterialCatalog` | `RollSpecification` | `PtSpecification` (mutuamente excluyentes por categoría)
- `OperationType` 1—N `StockPicking` 1—N `StockMove` N—1 `StockLot`

**Output**: data-model.md completo — no quedan entidades del diagrama de `spec.md` sin regla de validación o transición documentada.
