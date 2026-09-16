# Phase 0 Research: SPEC-002 Domain Model Redefinition

## R1. Tipo de dato para los campos puente hacia CONTPAQi (`erp_*`/`Cid*`)

- **Decision**: Todos los campos que referencian un identificador de CONTPAQi (`erp_product_id`, `erp_warehouse_id`, `erp_document_id`, `erp_customer_id`) se tipan como entero (`int`), nunca `Guid` ni `string`.
- **Rationale**: `docs/contpaq/Referencia_BD_CONTPAQi.md` documenta `CIDPRODUCTO`, `CIDALMACEN`, `CIDDOCUMENTO` y `CIDCLIENTEPROVEEDOR` consistentemente como tipo `I` (entero) en `admProductos`, `admAlmacenes`, `admDocumentos` y `admClientesProveedores`. Ya es lo que hace el código actual (`ContpaqProductId`, `CidAlmacenContpaq`, `CidDocumentoPedido` son todos `int`); la redefinición conserva esa convención (Principio VIII).
- **Alternatives considered**: Usar `string` para tolerar formatos futuros — rechazado porque no hay evidencia en el manual de referencia de que CONTPAQi use identificadores no numéricos, y hacerlo violaría el Principio VII (no especular sin respaldo documental).

## R2. Catálogo de `OperationType.TriggersErpDocumentType`

- **Decision**: Los valores del enumerado se anclan 1:1 a `CIDDOCUMENTODE` de CONTPAQi: `EntradaAlmacen` = 32, `SalidaAlmacen` = 33, `Traspaso` = 34, `Remision` = 3, `None` = sin documento asociado.
- **Rationale**: `Referencia_BD_CONTPAQi.md` (tabla `admDocumentos`, campo `CIDDOCUMENTODE`) enumera exactamente estos códigos. Anclar el enumerado a esos valores permite que `IBridgeSyncService` traduzca la intención de negocio (p. ej. "esta ruta es un traspaso") al tipo de documento real sin tabla de mapeo adicional.
- **Alternatives considered**: Un enumerado propio sin relación declarada con `CIDDOCUMENTODE`, resuelto por convención en la capa de infraestructura — rechazado porque dispersa la regla de negocio ("un traspaso interplanta produce un documento tipo 34") fuera del dominio, donde spec.md la declara (FR-020/FR-022).

## R3. Patrón de herencia para `RawMaterialCatalog` / `RollSpecification` / `BagSpecification`

- **Decision**: Delegación 1:1 sobre `Product` (tabla propia + FK), no extensión de columnas sobre `Product`.
- **Rationale**: Ya justificado en spec.md ("Decisiones de Modelado"); cada especialización aplica solo a un subconjunto de categorías y tiene su propio ciclo de vida (una ficha de rollo puede editarse sin tocar el catálogo base). En EF Core esto se implementa como tabla independiente con `ProductId` como PK y FK a la vez (patrón "table splitting" evitado a propósito — se prefiere tabla separada por tener ciclo de vida propio).
- **Alternatives considered**: TPH (Table-Per-Hierarchy) con columnas nulas por categoría — rechazado por el propio principio de la referencia de modelado (evitar columnas sin sentido para categorías que no las usan).

## R4. Jerarquía tripartita como tres entidades vs. una self-referencing

- **Decision**: Tres entidades (`ManufacturingOrder`, `ProcessOrder`, `WorkOrder`), cada una con FK simple al padre (`ProcessOrder.ManufacturingOrderId`, `WorkOrder.ProcessOrderId`), no una tabla auto-referenciada con `ParentId`.
- **Rationale**: Regla #9 de la Matriz de Discrepancias Validada distingue explícitamente el momento en que cada nivel cambia de estado (la `OF` se confirma con BOM; la `WO` nace en `'Por programar'` independientemente). Una tabla auto-referenciada no puede exigir un conjunto de campos distinto por nivel (`Bom` solo aplica a `ProcessOrder`; `WorkCenterId`/`ScheduledDate` solo a `WorkOrder`) sin columnas condicionales.
- **Alternatives considered**: Mantener `ManufacturingOrder` self-referencing (estado actual) — rechazado, es la causa raíz del gap descrito en spec.md.

## R5. Motor de numeración (`IReferenceSequenceService`)

- **Decision**: Servicio de dominio con un método `Next(documentType) -> DocumentReference`, configurado por tipo de documento (prefijo, ancho de relleno, si reinicia por año). Reemplaza tanto la concatenación manual en `OrdersController` (`$"OM-2026-{sequence:D4}"`) como el value object `Folio` (que solo validaba el formato de rollo).
- **Rationale**: Principio de numeración centralizada de la referencia de modelado; hoy la lógica de folio está repetida en al menos tres lugares (controlador, `Folio.Generate`, `OperationalDataStore` de presentación) con formatos ligeramente distintos.
- **Alternatives considered**: Mantener `Folio` como está y agregar un value object nuevo por cada formato adicional (`LoteReference`, `OmReference`, ...) — rechazado, sería repetir el mismo problema de duplicación que esta spec busca eliminar.

## R6. Framework de pruebas para las nuevas máquinas de estado

- **Decision**: xUnit + FluentAssertions (ya en `tests/PolyConecta.Domain.Tests`), un archivo de pruebas por documento con estado (`ProcessOrderStateTests`, `StockLotQuarantineTests`, etc.), reemplazando `RolloMaestroTests`.
- **Rationale**: Es el framework ya establecido en el proyecto; no hay motivo para introducir uno nuevo.
- **Alternatives considered**: N/A.

**Output**: Todas las incógnitas de Technical Context quedan resueltas; no quedan marcadores `NEEDS CLARIFICATION`.
