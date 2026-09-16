# Implementation Plan: SPEC-002: Domain Model Redefinition

**Branch**: `002-domain-model-redefinition` | **Date**: 2026-09-15 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `.specify/features/002-domain-model-redefinition/spec.md`

---

## Summary

Reemplazar `PolyConecta.Domain` (entidades duplicadas, máquinas de estado inconsistentes, sin ficha técnica ni jerarquía tripartita real) por el modelo redefinido en `spec.md`: 29 entidades/servicios organizados en catálogos archivables, documentos con flujo de estado auditado, y dos especializaciones por delegación sobre `Product`. La migración es **entidad por entidad, sin convivencia con el modelo legado** (FR-006): no se repite el patrón actual de `OrdersController` escribiendo simultáneamente en `ManufacturingOrder` y en `MasterOrder`/`SubOrder`.

## Technical Context

**Language/Version**: C# 12 / .NET 8

**Primary Dependencies**: Entity Framework Core 8 (`Npgsql.EntityFrameworkCore.PostgreSQL` 8.0.4), MediatR 12.4 (para publicar eventos de transición de estado), FluentValidation 11.9 (para las precondiciones de las operaciones de transición)

**Storage**: PostgreSQL (vía `PolyDbContext`, ya configurado en `PolyConecta.Infrastructure`)

**Testing**: xUnit 2.9 + FluentAssertions 6.12 (`tests/PolyConecta.Domain.Tests`)

**Target Platform**: Servidor Linux/Windows, ASP.NET Core 8 (`PolyConecta.Api`)

**Project Type**: Solución multi-proyecto (Clean Architecture: Domain / Infrastructure / Api / Presentation / Contpaq)

**Performance Goals**: N/A — no hay requisito de throughput distinto al actual; el cambio es de forma del modelo, no de volumen

**Constraints**: Ninguna migración MUST requerir downtime de CONTPAQi (Principio II); ninguna operación de dominio MUST invocar el SDK/SQL de CONTPAQi directamente

**Scale/Scope**: ~29 entidades/servicios de dominio, 3 entidades legado retiradas (`MasterOrder`, `SubOrder`, `RolloMaestro`), 1 migración de EF Core

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principio | Gate | Estado |
|---|---|---|
| II (Outbox / no SQL directo) | Ninguna entidad/servicio de dominio referencia `SDK_CONTPAQ.dll` ni ejecuta SQL contra `adm*` | ✅ Pass — `IBridgeSyncService` sigue siendo el único punto de salida, vía Outbox |
| III (fronteras MP/PT) | `RawMaterialCatalog` sigue siendo el catálogo único de MP; `Product`/PT admite código por cliente | ✅ Pass — ver FR-007/FR-010 |
| IV (balance de masa, hard-stop) | Tolerancia configurable, auditoría obligatoria, hard-stop antes de `PIM-TR-OUT`/`PIM-OUT-DIR` | ✅ Pass — FR-018, FR-019, FR-021 |
| V (multiplanta/multiempresa) | `LegalEntity`/`Plant` como límite derivado, sin mezcla intercompany automática todavía | ✅ Pass — FR-023, mezcla completa diferida explícitamente |
| VI (alcance Fase 1) | No se modela peletizado como jerarquía propia, ni báscula IoT, ni P2P de 3 firmas | ✅ Pass — sección "Fuera de alcance" de spec.md |
| VIII (alineación con CONTPAQi real) | Campos `erp_*`/`Cid*` se conservan como puente hacia `admProductos`/`admAlmacenes`/`admDocumentos` | ⚠️ Verificar en Fase 0 contra `docs/contpaq/Referencia_BD_CONTPAQi.md` antes de fijar tipos de esos campos |

No hay violaciones que requieran la tabla de "Complexity Tracking".

## Project Structure

### Documentation (this feature)

```text
.specify/features/002-domain-model-redefinition/
├── spec.md
├── plan.md              # This file
├── research.md
├── data-model.md
├── contracts/
│   └── repository-contracts.md
├── quickstart.md
└── checklists/
    └── requirements.md
```

### Source Code (repository root)

```text
PolyConecta.Domain/
├── Common/                # AuditableEntity, ArchivableEntity, StatefulDocument<T> mixins (nuevo)
├── Entities/              # Reemplazo entidad por entidad (ver data-model.md)
├── ValueObjects/           # DocumentReference (sustituye a Folio de formato único)
├── Repositories/           # Interfaces ampliadas: ISalesOrderRepository, IProcessOrderRepository, etc.
├── Services/               # IReferenceSequenceService, IMassBalanceService, IIntercompanyMirrorService (stub)
└── Events/                 # StateTransitionOccurred (evento de dominio genérico)

PolyConecta.Infrastructure/
├── Persistence/
│   ├── PolyDbContext.cs           # DbSets actualizados a las 29 entidades redefinidas
│   └── Configurations/            # Fluent API por entidad (reemplaza convención implícita actual)
└── Repositories/                   # Implementaciones EF Core de las interfaces ampliadas

PolyConecta.Api/
└── Controllers/OrdersController.cs # Reescrito para usar IOrderRepository/servicios de dominio,
                                     # no PolyDbContext directo ni doble-escritura legado

tests/PolyConecta.Domain.Tests/     # RolloMaestroTests se retira; nuevas suites por documento con estado
```

**Structure Decision**: Se conserva la topología de 5 proyectos ya existente (Clean Architecture). El cambio vive dentro de `PolyConecta.Domain` + `PolyConecta.Infrastructure`, con un ajuste puntual en `PolyConecta.Api` para eliminar el acceso directo a `PolyDbContext` desde el controlador (violación de capas detectada en la revisión previa).

## Complexity Tracking

*Sin violaciones que justificar.*
