# Tasks: SPEC-002 Core Domain & Data Model

**Feature Branch**: `002-core-domain-data-model`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md) | **Data Model**: [data-model.md](./data-model.md)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Create C# solution and project structure (`PolyConecta.Domain`, `PolyConecta.Infrastructure`, `PolyConecta.Api`) in `backend/src/`
- [x] T002 [P] Configure NuGet package dependencies (EF Core 8, Npgsql, FluentValidation, MediatR, xUnit) in `backend/src/PolyConecta.Infrastructure/PolyConecta.Infrastructure.csproj`
- [x] T003 [P] Configure code style, EditorConfig rules, and build properties in `.editorconfig` and `backend/Directory.Build.props`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T004 Setup PostgreSQL `PolyDbContext` EF Core database context in `backend/src/PolyConecta.Infrastructure/Persistence/PolyDbContext.cs`
- [x] T005 [P] Implement Outbox Message publisher infrastructure for CONTPAQi asynchronous integration in `backend/src/PolyConecta.Infrastructure/Outbox/OutboxPublisher.cs`
- [x] T006 [P] Setup ASP.NET Core API routing, exception handling middleware, and OpenAPI documentation in `backend/src/PolyConecta.Api/Program.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - Multi-Plant & Virtual Location Inventory Management (Priority: P1) 🎯 MVP Component 1

**Goal**: Model physical plants (Apodaca PIM, Santa Cruz STC, Montemorelos MTM) and 2-step location paths (`PIM/Stock/MP`, `PIM/Produccion`, `PIM/Stock/PT`, `PIM/Cuarentena`) aligned 1:1 with CONTPAQi `admAlmacenes` records (`ALM_PIM_MP`, `ALM_PIM_PROD`, `ALM_PIM_PT`, `ALM_PIM_CUARENTENA`, `ALM_STC_MP`, `ALM_STC_PT`, `ALM_MTM_MP`, `ALM_MTM_PT`).

**Independent Test**: Query location catalog via `GET /api/v1/locations` and execute stock transfer via `POST /api/v1/transfers/move`, verifying location routing and CONTPAQi Outbox message queueing.

### Tests for User Story 1

- [x] T007 [P] [US1] Contract and integration tests for location listing and 2-step stock transfers in `backend/tests/PolyConecta.IntegrationTests/LocationsApiTests.cs`

### Implementation for User Story 1

- [x] T008 [P] [US1] Create `PolyLocation` domain entity in `backend/src/PolyConecta.Domain/Entities/PolyLocation.cs` with constraints: `cid_almacen_contpaq INT NOT NULL UNIQUE`, `location_code VARCHAR(50) NOT NULL UNIQUE`, `location_name VARCHAR(100) NOT NULL`, `plant_code VARCHAR(10) NOT NULL ('PIM','STC','MTM')`, `warehouse_type VARCHAR(30) NOT NULL ('RawMaterial','Production','FinishedGoods','Quarantine','Transit')`, `is_active BOOLEAN DEFAULT TRUE`
- [x] T009 [P] [US1] Create `PolyLocation` EF Core entity configuration and plant seed data in `backend/src/PolyConecta.Infrastructure/Persistence/Configurations/PolyLocationConfiguration.cs`
- [x] T010 [US1] Implement `LocationsController` endpoints (`GET /api/v1/locations`, `POST /api/v1/transfers/move`) per API contract in `backend/src/PolyConecta.Api/Controllers/LocationsController.cs`

**Checkpoint**: User Story 1 complete and independently testable

---

## Phase 4: User Story 2 - Rollo Maestro Lifecycle & Physical Identity Tracking (Priority: P1) 🎯 MVP Component 2

**Goal**: Define the canonical `RolloMaestro` entity with immutable Folio (`EX-{Linea:2d}-{YYMMDD}-{HHMMSS}`), net weight calculation ($\text{Net} = \text{Gross} - \text{Tare}$), technical attributes (calibre in micras, metraje, dinas), GS1-128 QR code label, and CONTPAQi `admCapasProducto.cNumeroLote` link.

**Independent Test**: Capture roll weight via handheld API, verify immutable Folio generation, net weight calculation, technical fields, and lot mapping to CONTPAQi `cNumeroLote`.

### Tests for User Story 2

- [x] T011 [P] [US2] Unit tests for `RolloMaestro` weight calculation, gauge validation, and Folio regex format in `backend/tests/PolyConecta.Domain.Tests/RolloMaestroTests.cs`

### Implementation for User Story 2

- [x] T012 [P] [US2] Create `Folio` Value Object with regex validation `^EX-[0-9]{2}-[0-9]{6}-[0-9]{6}$` in `backend/src/PolyConecta.Domain/ValueObjects/Folio.cs`
- [x] T013 [P] [US2] Create `RolloMaestro` domain entity in `backend/src/PolyConecta.Domain/Entities/RolloMaestro.cs` with constraints: `folio VARCHAR(30) UNIQUE NOT NULL`, `lot_number VARCHAR(30) NOT NULL`, `product_sku VARCHAR(30) NOT NULL`, `gross_weight_kg NUMERIC(10,3) NOT NULL > 0`, `tare_weight_kg NUMERIC(10,3) NOT NULL >= 0`, `net_weight_kg GENERATED (gross - tare)`, `length_meters NUMERIC(10,2) NOT NULL > 0`, `gauge_micron NUMERIC(8,2) NOT NULL > 0`, `width_mm NUMERIC(8,2) NOT NULL > 0`, `dynes_cm NUMERIC(5,1) DEFAULT 38.0`, `machine_id VARCHAR(20) NOT NULL`, `shift VARCHAR(10) NOT NULL ('Turno 1','Turno 2','Turno 3')`, `operator_id VARCHAR(50) NOT NULL`, `status VARCHAR(20) NOT NULL ('Available','In_Use','Consumed','Quarantine','Scrap')`
- [x] T014 [US2] Create `RolloMaestro` EF Core entity configuration in `backend/src/PolyConecta.Infrastructure/Persistence/Configurations/RolloMaestroConfiguration.cs`
- [x] T015 [US2] Implement `RollsController` for mobile handheld roll weight registration and roll query endpoints in `backend/src/PolyConecta.Api/Controllers/RollsController.cs`

**Checkpoint**: User Story 2 complete and independently testable

---

## Phase 5: User Story 3 - Hierarchical Manufacturing Orders (OM vs OF-EXT, OF-IMP, OF-BOL) (Priority: P1) 🎯 MVP Component 3

**Goal**: Implement 3-tier Order Hierarchy linking CONTPAQi Sales Orders (`admDocumentos` Pedido) to Master Orders (`MasterOrder` / OM) and Process Sub-Orders (`SubOrder` / `OF-EXT`, `OF-IMP`, `OF-BOL`) with Odoo 19 Kanban status pipelines (`Borrador` → `Programado` → `En_Proceso` → `Control_Calidad` → `Finalizado`).

**Independent Test**: Create Master Order linked to CONTPAQi Pedido `CIDDOCUMENTO`, verify auto-decomposition into process sub-orders, and transition states along the Kanban pipeline.

### Tests for User Story 3

- [x] T016 [P] [US3] Integration tests for Master Order creation and Sub-Order Kanban pipeline transitions in `backend/tests/PolyConecta.IntegrationTests/OrdersApiTests.cs`

### Implementation for User Story 3

- [x] T017 [P] [US3] Create `MasterOrder` domain entity in `backend/src/PolyConecta.Domain/Entities/MasterOrder.cs` with constraints: `folio_om VARCHAR(30) NOT NULL UNIQUE ('OM-YYYY-XXXX')`, `cid_documento_pedido INT NOT NULL`, `customer_code VARCHAR(30) NOT NULL`, `pt_sku VARCHAR(30) NOT NULL`, `target_quantity_kg NUMERIC(12,3) NOT NULL > 0`, `status VARCHAR(20) NOT NULL ('Draft','Approved','In_Progress','Completed','Cancelled')`
- [x] T018 [P] [US3] Create `SubOrder` domain entity in `backend/src/PolyConecta.Domain/Entities/SubOrder.cs` with constraints: `folio_of VARCHAR(35) NOT NULL UNIQUE ('OF-EXT-YYYY-XXXX-N')`, `process_type VARCHAR(10) NOT NULL ('EXT','IMP','BOL')`, `machine_id VARCHAR(20) NOT NULL`, `status VARCHAR(20) NOT NULL ('Borrador','Programado','En_Proceso','Control_Calidad','Finalizado','Scrap')`, `planned_qty_kg NUMERIC(12,3) NOT NULL > 0`, `produced_qty_kg NUMERIC(12,3) DEFAULT 0.0`, `scrap_qty_kg NUMERIC(12,3) DEFAULT 0.0`
- [x] T019 [US3] Create EF Core entity configurations for `MasterOrder` and `SubOrder` in `backend/src/PolyConecta.Infrastructure/Persistence/Configurations/OrderConfigurations.cs`
- [x] T020 [US3] Implement `OrdersController` endpoints (`POST /api/v1/orders/master`, `PATCH /api/v1/orders/sub-orders/{id}/status`) in `backend/src/PolyConecta.Api/Controllers/OrdersController.cs`

**Checkpoint**: User Story 3 complete and independently testable

---

## Phase 6: User Story 4 - Lot Lineage, Independent Inventory Layers & Mass-Balance Audit (Priority: P2)

**Goal**: Implement multi-tier lot lineage DAG tracking (`poly_lot_genealogy`) and automated mass-balance calculation ($\text{Variance} = \frac{\text{Input} - (\text{Output} + \text{Scrap})}{\text{Input}} \times 100$) with a 2.0% audit threshold limit.

**Independent Test**: Complete a sub-order run, audit total raw material input vs roll output and scrap, verify variance calculation and automatic audit flagging.

### Tests for User Story 4

- [x] T021 [P] [US4] Unit tests for Mass Balance variance calculation and 2.0% tolerance audit flagging in `backend/tests/PolyConecta.Domain.Tests/MassBalanceAuditTests.cs`

### Implementation for User Story 4

- [x] T022 [P] [US4] Create `LotGenealogy` domain entity in `backend/src/PolyConecta.Domain/Entities/LotGenealogy.cs` with constraints: `parent_lot_number VARCHAR(30) NOT NULL`, `child_lot_number VARCHAR(30) NOT NULL`, `quantity_consumed_kg NUMERIC(10,3) NOT NULL > 0`
- [x] T023 [P] [US4] Create `MassBalanceAudit` domain entity in `backend/src/PolyConecta.Domain/Entities/MassBalanceAudit.cs` with constraints: `sub_order_id UUID FOREIGN KEY`, `total_mp_input_kg NUMERIC(12,3) NOT NULL`, `total_roll_output_kg NUMERIC(12,3) NOT NULL`, `total_scrap_output_kg NUMERIC(12,3) NOT NULL`, `variance_percentage NUMERIC(6,3) NOT NULL`, `audit_passed BOOLEAN NOT NULL (true if variance <= 2.0%)`, `flag_reason TEXT NULLABLE`
- [x] T024 [US4] Create EF Core entity configurations for `LotGenealogy` and `MassBalanceAudit` in `backend/src/PolyConecta.Infrastructure/Persistence/Configurations/AuditConfigurations.cs`
- [x] T025 [US4] Implement `MassBalanceService` domain service for shop-floor batch auditing in `backend/src/PolyConecta.Domain/Services/MassBalanceService.cs`

**Checkpoint**: User Story 4 complete and independently testable

---

## Phase 7: Polish & Cross-Cutting Concerns

- [x] T026 [P] Generate EF Core database migration scripts in `backend/src/PolyConecta.Infrastructure/Migrations/`
- [x] T027 [P] Update API swagger documentation and OpenAPI spec files in `docs/api/`
- [x] T028 Run end-to-end quickstart validation scenarios defined in `.specify/features/002-core-domain-data-model/quickstart.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Can start immediately.
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all User Stories.
- **User Stories (Phases 3-6)**: Depend on Foundational phase completion. Can proceed sequentially by priority (P1 → P2).
- **Polish (Phase 7)**: Depends on all User Stories completion.

### Parallel Opportunities

- Tasks marked `[P]` within each phase can be developed concurrently in separate files without merge conflicts.
- User Stories 1, 2, and 3 (all Priority P1) can be implemented in parallel by different developers once Phase 2 completes.

---

## Implementation Strategy: MVP Scope

1. **Phase 1 & 2**: Complete Setup and Foundational infrastructure.
2. **Phase 3, 4, 5 (MVP Scope)**: Complete User Stories 1, 2, and 3 (Locations, Rollo Maestro, Order Hierarchy).
3. **Validate MVP**: Execute quickstart scenarios 1, 2, 3, and 4.
4. **Phase 6**: Add User Story 4 (Lot Lineage & Mass Balance Audit).
