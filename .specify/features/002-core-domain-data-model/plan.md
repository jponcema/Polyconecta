# Implementation Plan: SPEC-002 Core Domain & Data Model

**Branch**: `002-core-domain-data-model` | **Date**: 2026-09-14 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `.specify/features/002-core-domain-data-model/spec.md`

---

## Summary

This plan defines the architectural design, database schemas, API contracts, and validation rules for **SPEC-002: Core Domain & Data Model**. It establishes the canonical domain model for PolyConecta, covering multi-plant virtual location routing 1:1 aligned with CONTPAQi `admAlmacenes`, the `RolloMaestro` entity with immutable Folio generation (`EX-01-260910-042747`), a 3-tier Order Hierarchy (CONTPAQi Pedido → Master Order OM → Process Sub-Orders OF-EXT, OF-IMP, OF-BOL) with Odoo 19 Kanban status pipelines, multi-tier lot lineage DAGs, and mandatory mass-balance audit gates.

---

## Technical Context

**Language/Version**: C# / .NET 8 (PolyConecta Web API & Operational Engine) + .NET Framework 4.8 x86 Worker (CONTPAQi SDK Integration Bridge)  
**Primary Dependencies**: ASP.NET Core, Entity Framework Core 8, Npgsql PostgreSQL provider, FluentValidation, MediatR (Outbox Pattern)  
**Storage**: PostgreSQL (PolyConecta Operational Engine DB) + Integration Outbox Tables + CONTPAQi MS SQL Server (`adm*` tables via SDK Worker)  
**Testing**: xUnit, FluentAssertions, Moq, Testcontainers for PostgreSQL  
**Target Platform**: Linux Docker Containers (PolyConecta Web & Engine API), Windows Server x64/x86 (CONTPAQi Bridge Worker), Mobile Web / Handheld Scanners (Shop-Floor UI)  
**Project Type**: Specialized MES / Operational Routing Engine (Web Application & Microservices)  
**Performance Goals**: Handheld roll barcode weight capture & GS1-128 QR code label render in <1.5 seconds; stock movement outbox enqueue in <50ms; real-time Kanban pipeline UI rendering for 100+ active sub-orders  
**Constraints**: Asynchronous Outbox Pattern for CONTPAQi updates (strictly no direct SQL writes to `adm*` tables); 100% mobile handheld scanner usability; mandatory quality hard-stop gate  
**Scale/Scope**: 3 physical plants (Apodaca PIM, Santa Cruz STC, Montemorelos MTM); 8+ virtual warehouse locations; ~500+ master rolls produced daily; ~50+ concurrent mobile devices  

---

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-checked after Phase 1 design.*

| Principle | Description | Compliance Status | Justification / Mechanism |
| :--- | :--- | :---: | :--- |
| **Principle I** | ERP Master of Record & Odoo 19 UI Benchmark | **PASS** | CONTPAQi remains ERP master of record. PolyConecta implements custom domain engine with Odoo 19 Kanban/List/Form UI. |
| **Principle II** | Asynchronous Outbox Pattern & SDK Worker | **PASS** | PolyConecta domain mutations write to local DB and enqueue Outbox messages for .NET x86 Worker with `SDK_CONTPAQ.dll`. |
| **Principle III** | SKU Catalog Boundaries | **PASS** | Master MP SKU catalog unified; PT SKUs retain CONTPAQi `admProductos` customer mappings linked to internal technical specs. |
| **Principle IV** | Mass-Balance Audit & Quality Hard-Stop | **PASS** | Formula enforced ($\text{Consumo MP} = \text{Rollos} + \text{Scrap}$); 2.0% tolerance audit; Quarantine holds block stock movements. |
| **Principle V** | Governance & Multi-Plant Routes | **PASS** | Role-based digital signoffs; 2-step location routing across PIM, STC, and MTM plants. |
| **Principle VI** | Phase 1 Scope Boundaries | **PASS** | Focused on 100% handheld scan roll capture, order-to-cash sync, and CONTPAQi SDK integration. |
| **Principle VII & VIII** | Technical Backing & CONTPAQi Schema Reality | **PASS** | Grounded in actual `admAlmacenes`, `admCapasProducto`, `admDocumentos`, `admMovimientos`, `admProductos` schema fields. |
| **Principle IX** | Odoo 19 Enterprise UI/UX Benchmark | **PASS** | Kanban status headers, Smart Buttons for roll lineage navigation, 2-step location routing, and low-friction mobile scanner views. |

---

## Project Structure

### Documentation (this feature)

```text
.specify/features/002-core-domain-data-model/
├── spec.md              # Feature specification
├── plan.md              # Implementation plan (this file)
├── research.md          # Phase 0 design decisions & rationale
├── data-model.md        # Phase 1 canonical database schemas & state rules
├── quickstart.md        # Phase 1 runnable validation scenarios & guide
└── contracts/           # Phase 1 interface definitions & schemas
    ├── domain-schema.json
    ├── warehouse-routing-api.json
    └── order-hierarchy-api.json
```

### Source Code (repository root)

```text
backend/
├── src/
│   ├── PolyConecta.Domain/
│   │   ├── Entities/       # RolloMaestro, MasterOrder, SubOrder, PolyLocation, LotGenealogy
│   │   ├── ValueObjects/   # Folio, Weight, MicronGauge, Dynes
│   │   └── Events/         # RollCreatedEvent, StockTransferredEvent, QualityStatusChangedEvent
│   ├── PolyConecta.Infrastructure/
│   │   ├── Persistence/    # EF Core DbContext, PostgreSQL Configurations, Migrations
│   │   └── Outbox/         # OutboxMessage, OutboxPublisher
│   └── PolyConecta.Api/
│       ├── Controllers/    # OrdersController, LocationsController, TransfersController
│       └── Dtos/           # API Contract DTOs
└── tests/
    ├── PolyConecta.Domain.Tests/
    └── PolyConecta.IntegrationTests/
```

**Structure Decision**: Multi-project Web Application layout (`backend/` API & Domain services) with clean separation between Domain entities, Infrastructure EF Core persistence, and API contract controllers.

---

## Complexity Tracking

> **No violations present. All Constitution gates passed.**
