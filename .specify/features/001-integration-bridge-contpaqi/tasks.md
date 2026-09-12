# Tasks Specification: Standalone CONTPAQi Integration Bridge (.NET x86 Worker)

**Feature**: Standalone CONTPAQi Integration Bridge & Real-Time Monitoring Dashboard  
**Branch**: `001-integration-bridge-contpaqi`  
**Spec**: [.specify/features/001-integration-bridge-contpaqi/spec.md](file:///Users/emilio/Development/Sandbox/Polyconecta/.specify/features/001-integration-bridge-contpaqi/spec.md)  
**Plan**: [.specify/features/001-integration-bridge-contpaqi/plan.md](file:///Users/emilio/Development/Sandbox/Polyconecta/.specify/features/001-integration-bridge-contpaqi/plan.md)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization, directory structure, NuGet dependencies, and compiler settings

- [x] T001 Create project directory layout for `Contpaq.Bridge` solution per implementation plan (`src/Contpaq.Bridge/`, `tests/Contpaq.Bridge.Tests/`)
- [x] T002 Initialize C# .NET 8/9 project file `src/Contpaq.Bridge/Contpaq.Bridge.csproj` targeting `x86` architecture platform (`win-x86`)
- [x] T003 [P] Add NuGet dependencies to `src/Contpaq.Bridge/Contpaq.Bridge.csproj` (`Dapper`, `System.Data.SQLite`, `Microsoft.Data.SqlClient`, `Serilog.AspNetCore`, `Serilog.Sinks.Console`, `Serilog.Sinks.File`)
- [x] T004 [P] Configure C# `.editorconfig` linting rules, code formatting, and strict nullable warnings in `.editorconfig`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core configuration, native P/Invoke bindings, SQLite Outbox storage, and logging infrastructure

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T005 Create application settings configuration file `src/Contpaq.Bridge/appsettings.json` specifying SDK path (`C:\Program Files (x86)\Compac\COMERCIAL`), SQL Server connection string, SQLite path (`bridge_outbox.db`), Dashboard port (`5055`), transaction timeout (8s), max retries (5), and circuit breaker threshold (3)
- [x] T006 Create P/Invoke native SDK interop wrapper `src/Contpaq.Bridge/Infrastructure/Sdk/ContpaqiSdkNative.cs` mapping C++ DLL functions (`fInicializaSDK`, `fTerminaSDK`, `fSetNombrePAQ`, `fAbreEmpresa`, `fCierraEmpresa`, `fAltaDocumento`, `fAltaMovimiento`, `fAltaMovimientoSeriesCapas`, `fAfectaDocto_Param`, `fError`)
- [x] T007 Implement local SQLite Outbox database schema initializer and Dapper migrations script `src/Contpaq.Bridge/Infrastructure/Persistence/DbInitializer.cs` creating `bridge_transactions`, `transaction_logs`, `webhook_deliveries`, and `metric_snapshots` tables with indexes
- [x] T008 Implement structured JSON logging and correlation middleware `src/Contpaq.Bridge/Api/Middleware/CorrelationMiddleware.cs` generating and propagating `CorrelationId` across all HTTP and worker operations
- [x] T009 Implement base Outbox repository `src/Contpaq.Bridge/Infrastructure/Persistence/OutboxRepository.cs` for CRUD operations on `bridge_transactions` and `transaction_logs` SQLite tables

**Checkpoint**: Core foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - Agnostic ERP Command Ingestion & Local Outbox Persistence (Priority: P1) 🎯 MVP

**Goal**: Receive generic ERP write commands via REST API (`POST /api/v1/transactions`), persist in local SQLite Outbox queue with `PENDING` status in < 100 ms, execute via .NET x86 SDK Worker, and deliver async Webhook callbacks

**Independent Test**: Post 100 `DOCUMENT_CREATE` JSON payloads to `POST /api/v1/transactions`, verify HTTP 202 responses with `TransactionId` in < 100 ms, confirm items persisted in SQLite, and verify SDK worker executes items sequentially

- [x] T010 [P] [US1] Create command payload DTOs and JSON schema validators in `src/Contpaq.Bridge/Core/Commands/TransactionCommand.cs` (`DOCUMENT_CREATE`, `MOVEMENT_ADD`, `LOT_ASSOCIATION`, `DOCUMENT_AFFECT`)
- [x] T011 [P] [US1] Create transaction entity model in `src/Contpaq.Bridge/Core/Models/BridgeTransaction.cs` matching SQLite schema
- [x] T012 [US1] Implement REST API controller `src/Contpaq.Bridge/Api/Controllers/TransactionsController.cs` exposing `POST /api/v1/transactions` and `GET /api/v1/transactions/{id}` endpoints
- [x] T013 [US1] Implement asynchronous HTTP Webhook notification engine in `src/Contpaq.Bridge/Infrastructure/Webhooks/WebhookDispatcher.cs` delivering completion notifications to client `callback_url`
- [x] T014 [US1] Add idempotency key checking logic in `src/Contpaq.Bridge/Infrastructure/Persistence/OutboxRepository.cs` to reject duplicate transaction submissions

**Checkpoint**: User Story 1 complete - MVP API ingestion and Outbox queuing fully functional

---

## Phase 4: User Story 2 - Standalone Web Dashboard for Real-Time Visual Monitoring & DLQ Control (Priority: P2)

**Goal**: Embedded Web Dashboard on `http://localhost:5055` displaying real-time visual charts (throughput ops/sec, queue depth, SDK latency, connection health) via WebSockets (SignalR) and an interactive DLQ management panel for inspecting, editing payload inline, retrying, or purging failed transactions

**Independent Test**: Access `http://localhost:5055`, verify real-time visual charts update smoothly via WebSockets while transactions run, trigger a failed transaction, open the DLQ panel, edit its payload inline, click Retry, and verify state transitions to `COMPLETED`

- [x] T015 [P] [US2] Implement WebSockets SignalR hub `src/Contpaq.Bridge/Api/Hubs/DashboardHub.cs` for broadcasting real-time metric snapshots to dashboard clients
- [x] T016 [P] [US2] Create metric snapshot aggregator and background sampling service `src/Contpaq.Bridge/Core/Services/MetricCollectorService.cs` capturing throughput, queue depth, SDK latency, error rate, and connection status every 1 second
- [x] T017 [US2] Implement REST API controller `src/Contpaq.Bridge/Api/Controllers/DlqController.cs` exposing `GET /api/v1/dlq`, `POST /api/v1/dlq/{id}/retry`, `POST /api/v1/dlq/{id}/edit-and-retry`, and `DELETE /api/v1/dlq/{id}` endpoints
- [x] T018 [US2] Create embedded dashboard HTML layout in `src/Contpaq.Bridge/Dashboard/wwwroot/index.html` with navigation tabs for Real-Time Charts, Live Transaction Stream, and DLQ Management Panel
- [x] T019 [US2] Create JavaScript dashboard controller `src/Contpaq.Bridge/Dashboard/wwwroot/js/dashboard.js` connecting to SignalR WebSockets, rendering Chart.js graphs, and providing interactive DLQ JSON modal editing
- [x] T020 [US2] Implement embedded static file controller `src/Contpaq.Bridge/Dashboard/Controllers/DashboardViewController.cs` serving the dashboard UI at root `/`

**Checkpoint**: User Story 2 complete - Real-time visual monitoring dashboard and interactive DLQ management active

---

## Phase 5: User Story 3 - High-Throughput Single-Threaded SDK Execution & Dynamic Session Reuse (Priority: P3)

**Goal**: Execute single-threaded P/Invoke SDK operations in a dedicated STA thread worker with dynamic enterprise session reuse (`fAbreEmpresa` kept open during bursts), strict 8-second execution timeouts, exponential backoff retries with jitter, and an automatic Circuit Breaker

**Independent Test**: Queue 100 transactions, confirm worker processes the burst in a single open SDK session in < 10 seconds (~10-20 ops/sec throughput), simulate a locked database call > 8s, verify timeout cancellation, and confirm Circuit Breaker trips after 3 failures for 15s

- [x] T021 [P] [US3] Implement Circuit Breaker state policy `src/Contpaq.Bridge/Infrastructure/Sdk/CircuitBreakerPolicy.cs` managing `CLOSED`, `OPEN`, `HALF_OPEN` state transitions, 3-failure threshold, and 15-second cooldown timer
- [x] T022 [US3] Implement STA thread execution engine and channel worker in `src/Contpaq.Bridge/Infrastructure/Sdk/ContpaqiSdkGateway.cs` managing `System.Threading.Channels`, `SemaphoreSlim(1,1)`, dedicated STA thread, and 8-second CancellationToken timeout
- [x] T023 [US3] Implement dynamic SDK session lifecycle management in `src/Contpaq.Bridge/Infrastructure/Sdk/ContpaqiSdkGateway.cs` keeping `fAbreEmpresa` open across active queue items and closing after 5 seconds of empty queue inactivity
- [x] T024 [US3] Implement exponential backoff retry engine with random jitter in `src/Contpaq.Bridge/Infrastructure/Sdk/ContpaqiSdkGateway.cs` (retry delays: 1s, 5s, 15s, 60s, 300s) escalating to `DEAD_LETTER_QUEUE` status at 5 retries

**Checkpoint**: User Story 3 complete - High-throughput SDK execution engine, session reuse, and Circuit Breaker operational

---

## Phase 6: User Story 4 - High-Speed Read-Only SQL Pipeline & Decoupled Telemetry (Priority: P4)

**Goal**: Direct SQL Server read queries against `adm*` tables using `READ UNCOMMITTED` hints for master product, client, warehouse, concept catalogs, and stock layer balances with response latency < 50 ms

**Independent Test**: Execute `GET /api/v1/catalogs/products` and `GET /api/v1/inventory/stocks` while an SDK write batch is active, verifying response times < 50 ms without blocking SDK operations

- [x] T025 [P] [US4] Create SQL Server read model DTOs in `src/Contpaq.Bridge/Core/Models/CatalogModels.cs` (`ProductCatalogItem`, `ClientCatalogItem`, `WarehouseCatalogItem`, `ConceptCatalogItem`, `StockLayerItem`)
- [x] T026 [US4] Implement direct Dapper SQL Server read repository `src/Contpaq.Bridge/Infrastructure/Persistence/SqlReadRepository.cs` executing non-blocking `READ UNCOMMITTED` queries against `admProductos`, `admClientes`, `admAlmacenes`, `admConceptos`, `admCapasProducto`, and `admExistenciaCapa`
- [x] T027 [US4] Implement REST API controller `src/Contpaq.Bridge/Api/Controllers/CatalogsController.cs` exposing `GET /api/v1/catalogs/products`, `GET /api/v1/catalogs/clients`, `GET /api/v1/catalogs/warehouses`, and `GET /api/v1/inventory/stocks`

**Checkpoint**: User Story 4 complete - High-speed direct SQL Server catalog and inventory read pipeline active

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Dependency injection wire-up, unit test suite, and end-to-end quickstart validation

- [x] T028 [P] Implement Kestrel startup configuration and ServiceCollection dependency injection wire-up in `src/Contpaq.Bridge/Program.cs`
- [x] T029 Create unit and contract test suite in `tests/Contpaq.Bridge.Tests/TransactionsApiTests.cs` verifying API schema validation and DLQ endpoint contracts
- [x] T030 Execute end-to-end quickstart validation scenarios from `quickstart.md` verifying service health, Outbox processing throughput, real-time dashboard WebSocket streaming, DLQ editing, and SQL read performance

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 (Setup)**: No dependencies - can start immediately.
- **Phase 2 (Foundational)**: Depends on Phase 1 completion - **BLOCKS all user stories**.
- **Phase 3 (User Story 1 - MVP)**: Depends on Phase 2 completion.
- **Phase 4 (User Story 2)**: Depends on Phase 2 completion. Can proceed in parallel with US1 or after US1.
- **Phase 5 (User Story 3)**: Depends on Phase 2 completion and US1 P/Invoke bindings.
- **Phase 6 (User Story 4)**: Depends on Phase 2 completion.
- **Phase 7 (Polish)**: Depends on completion of User Stories 1 through 4.

### Parallel Opportunities

- **Phase 1**: T003 and T004 can run in parallel.
- **Phase 3 (US1)**: T010 and T011 can run in parallel.
- **Phase 4 (US2)**: T015 and T016 can run in parallel.
- **Phase 5 (US3)**: T021 can run in parallel with gateway setup.
- **Phase 6 (US4)**: T025 can run in parallel.

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 (Setup).
2. Complete Phase 2 (Foundational).
3. Complete Phase 3 (User Story 1 - Agnostic Ingestion & Outbox Persistence).
4. **Validate MVP**: Verify REST API ingestion (`POST /api/v1/transactions`), SQLite persistence, and basic SDK dispatch.

### Incremental Delivery

1. Add User Story 2 (Real-time Visual Dashboard & DLQ management).
2. Add User Story 3 (Session reuse, 8s timeouts & Circuit Breaker).
3. Add User Story 4 (Direct SQL read pipeline & Webhook callbacks).
4. Finalize Polish & Quickstart validation.
