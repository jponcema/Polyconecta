# Tasks: 000 Solution Root Clean Architecture Restructuring

**Feature Branch**: `000-solution-clean-architecture`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md) | **Data Model**: [data-model.md](./data-model.md)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic solution structure

- [x] T001 Review and configure solution project references in `Polyconecta.slnx` and `backend/Directory.Build.props` to enforce unidirectional Clean Architecture dependencies
- [x] T002 [P] Configure NuGet package dependencies (EF Core 8, InMemory, SQLite, OpenAPI, FluentValidation, MediatR, xUnit) across `backend/src/PolyConecta.Domain/PolyConecta.Domain.csproj`, `backend/src/PolyConecta.Infrastructure/PolyConecta.Infrastructure.csproj`, and `backend/src/PolyConecta.Api/PolyConecta.Api.csproj`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T003 Setup EF Core `PolyDbContext` persistence context and seed configurations in `backend/src/PolyConecta.Infrastructure/Persistence/PolyDbContext.cs`
- [x] T004 [P] Implement `OutboxPublisher` transaction queueing service in `backend/src/PolyConecta.Infrastructure/Outbox/OutboxPublisher.cs`
- [x] T005 [P] Configure ASP.NET Core Web API middleware, static files hosting, JSON cycle handling (`ReferenceHandler.IgnoreCycles`), and Swagger/OpenAPI in `backend/src/PolyConecta.Api/Program.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - Clean Architecture Layer Structure & Boundary Enforcement (Priority: P1) 🎯 MVP

**Goal**: Enforce strict layer boundaries and pure domain entities across Domain, Infrastructure, API, Bridge, and Web layers.

**Independent Test**: Execute solution build (`dotnet build`) and domain unit tests (`dotnet test backend/tests/PolyConecta.Domain.Tests/PolyConecta.Domain.Tests.csproj`).

### Implementation for User Story 1

- [x] T006 [P] [US1] Create pure domain entity `MasterOrder` with fields `Id: Guid`, `FolioOm: string ('OM-YYYY-XXXX')`, `CidDocumentoPedido: int`, `CustomerCode: string`, `PtSku: string`, `TargetQuantityKg: decimal`, `Status: string` in `backend/src/PolyConecta.Domain/Entities/MasterOrder.cs`
- [x] T007 [P] [US1] Create pure domain entity `SubOrder` with fields `Id: Guid`, `MasterOrderId: Guid`, `FolioOf: string ('OF-EXT-YYYY-XXXX-N')`, `ProcessType: string ('EXT','IMP','BOL')`, `MachineId: string`, `Status: string ('Borrador','Programado','En_Proceso','Control_Calidad','Finalizado')`, `PlannedQtyKg: decimal` in `backend/src/PolyConecta.Domain/Entities/SubOrder.cs`
- [x] T008 [P] [US1] Create Value Object `Folio` with regex pattern `^EX-[0-9]{2}-[0-9]{6}-[0-9]{6}$` in `backend/src/PolyConecta.Domain/ValueObjects/Folio.cs`
- [x] T009 [P] [US1] Create pure domain entity `RolloMaestro` with fields `Id: Guid`, `SubOrderId: Guid`, `Folio: string`, `LotNumber: string`, `GrossWeightKg: decimal`, `TareWeightKg: decimal`, `NetWeightKg: calculated (Gross - Tare)`, `LengthMeters: decimal`, `GaugeMicron: decimal` in `backend/src/PolyConecta.Domain/Entities/RolloMaestro.cs`
- [x] T010 [P] [US1] Create pure domain entity `PolyLocation` with fields `Id: Guid`, `CidAlmacenContpaq: int`, `LocationCode: string ('PIM/Stock/MP','PIM/Produccion','PIM/Stock/PT','PIM/Cuarentena')`, `LocationName: string`, `PlantCode: string ('PIM','STC','MTM')`, `WarehouseType: string` in `backend/src/PolyConecta.Domain/Entities/PolyLocation.cs`
- [x] T011 [US1] Create EF Core entity configurations for `MasterOrder`, `SubOrder`, `RolloMaestro`, and `PolyLocation` in `backend/src/PolyConecta.Infrastructure/Persistence/Configurations/`
- [x] T012 [US1] Create domain unit test suite `RolloMaestroTests` and `MassBalanceAuditTests` in `backend/tests/PolyConecta.Domain.Tests/`

**Checkpoint**: User Story 1 complete and independently testable (6/6 tests passing)

---

## Phase 4: User Story 2 - Decoupled Odoo 19 Presentation UI & API Gateway Layer (Priority: P2)

**Goal**: Deliver Odoo 19 Enterprise SPA UI Suite (`wwwroot/index.html`) connected to REST API endpoints.

**Independent Test**: Visit `http://localhost:5050` and test navigation across App Launcher grid, Kanban, List, and Form views.

### Implementation for User Story 2

- [x] T013 [P] [US2] Implement `OrdersController` REST API endpoints (`POST /api/v1/orders/master`, `GET /api/v1/orders/master/{id}`, `PATCH /api/v1/orders/sub-orders/{id}/status`) in `backend/src/PolyConecta.Api/Controllers/OrdersController.cs`
- [x] T014 [P] [US2] Implement `RollsController` REST API endpoint (`POST /api/v1/rolls/capture`) in `backend/src/PolyConecta.Api/Controllers/RollsController.cs`
- [x] T015 [P] [US2] Implement `LocationsController` REST API endpoints (`GET /api/v1/locations`, `POST /api/v1/transfers/move`) in `backend/src/PolyConecta.Api/Controllers/LocationsController.cs`
- [x] T016 [US2] Build Odoo 19 Enterprise Web SPA with App Launcher Grid, Topbar, Kanban/List/Form views, and Handheld Barcode screen in `backend/src/PolyConecta.Api/wwwroot/index.html`
- [x] T017 [US2] Create integration test suite `LocationsApiTests` and `OrdersApiTests` in `backend/tests/PolyConecta.IntegrationTests/`

**Checkpoint**: User Story 2 complete and independently testable (Odoo 19 SPA running live)

---

## Phase 5: User Story 3 - Outbox Messaging & CONTPAQi Integration Bridge Isolation (Priority: P3)

**Goal**: Isolate 32-bit Win32 CONTPAQi SDK worker and Outbox transaction queueing.

**Independent Test**: Enqueue `RollCreated` and `StockTransferred` events and verify async Outbox processing without blocking API HTTP responses.

### Implementation for User Story 3

- [x] T018 [P] [US3] Create domain entity `RawMaterialCatalog` and `SupplierProductMapping` with `[JsonIgnore]` on cyclic references in `backend/src/PolyConecta.Domain/Entities/RawMaterialCatalog.cs`
- [x] T019 [P] [US3] Implement `RawMaterialsController` REST API endpoints (`GET /api/v1/raw-materials`, `POST /api/v1/raw-materials`) in `backend/src/PolyConecta.Api/Controllers/RawMaterialsController.cs`
- [x] T020 [US3] Configure `ContpaqBridge` 32-bit Win32 worker project and outbox polling loop in `dist/win-x86/` and `backend/src/PolyConecta.ContpaqBridge/`

**Checkpoint**: User Story 3 complete and independently testable

---

## Phase 6: Polish & Cross-Cutting Concerns

- [x] T021 [P] Update solution architecture documentation and SDD roadmap in `docs/sdd/ROADMAP_ESPECIFICACIONES_POLYCONECTA.md`
- [x] T022 Run `quickstart.md` validation scenarios (build, test, run API, browser navigation, quality hard-stop test)

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - completed.
- **Foundational (Phase 2)**: Depends on Setup completion - completed.
- **User Stories (Phase 3+)**: Depend on Foundational phase completion - completed.
- **Polish (Phase 6)**: Completed.

### Parallel Opportunities

- All Setup tasks marked `[P]` run in parallel.
- Domain entities T006, T007, T008, T009, T010 run in parallel.
- API Controllers T013, T014, T015 run in parallel.

---

## Implementation Strategy

### MVP First (User Story 1 Only)
1. Phase 1 Setup & Phase 2 Foundational infrastructure.
2. Phase 3 User Story 1 (Core Domain & Clean Architecture).
3. Validate Domain Unit Tests (`dotnet test PolyConecta.Domain.Tests.csproj`).

### Incremental Delivery
1. Add User Story 2: Decoupled API Controllers & Odoo 19 Web SPA (`wwwroot/index.html`).
2. Add User Story 3: Master MP Catalogs & CONTPAQi Integration Bridge.
