# Feature Specification: 000 Solution Root Clean Architecture Restructuring

**Feature Branch**: `000-solution-clean-architecture`  
**Created**: 2026-09-14  
**Status**: Draft  
**Input**: User description: "/speckit-specify Ayudame con la especificacion 000 para hacer que el proyecto raiz tenga estructura (solucion). Debemos considerar todas las capas de integracion de la solucion. Presentacion, Api, Bridge, etc. Debemos considerar usar arquitectura limpia. Enlista lo que ya existe y estructuremoslo en orden cada capa."

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Clean Architecture Layer Structure & Boundary Enforcement (Priority: P1)

As a Developer and Software Architect, I want the PolyConecta root solution organized into strict Clean Architecture layers with explicit dependency directions, so that business domain rules, infrastructure persistence, API controllers, integration bridges, and presentation UI can evolve independently without tight coupling.

**Why this priority**: Core architectural foundation for the entire project. Prevents circular dependencies, isolates the CONTPAQi SDK Win32 integration, and ensures testability across all domain logic.

**Independent Test**: Can be tested by verifying that each layer compiles in isolation, domain entities contain zero framework dependencies (no HTTP or DB attributes), infrastructure depends on domain interfaces, and API/UI layers consume application services cleanly.

**Acceptance Scenarios**:

1. **Given** the root solution structure, **When** examining dependency references between project layers, **Then** inner layers (Domain/Core) have zero dependencies on outer layers (Infrastructure, API, Presentation).
2. **Given** a change in database persistence mechanism, **When** swapping from InMemory to PostgreSQL or SQL Server, **Then** the Domain entities and Application contracts require zero modifications.
3. **Given** the Win32 SDK integration for CONTPAQi, **When** executing ERP sync commands, **Then** all Win32 native calls remain strictly isolated inside the `PolyConecta.ContpaqBridge` process without polluting the main API or Domain layers.

---

### User Story 2 - Decoupled Odoo 19 Presentation UI & API Gateway Layer (Priority: P2)

As a Plant Operator and System User, I want the Presentation layer (Odoo 19 Enterprise SPA) decoupled from API controllers and domain logic via clean REST contracts, so that UI interaction, Kanban rendering, and Handheld mobile screens operate fast and reliably.

**Why this priority**: Ensures high usability for plant operators (Odoo 19 UX) while keeping the API endpoints testable and independent of UI framework choices.

**Independent Test**: Can be tested by running UI interactions in the web browser against mock REST responses, verifying that UI components interact purely via standardized REST JSON contracts without direct backend state leakage.

**Acceptance Scenarios**:

1. **Given** an operator navigating the Odoo 19 App Launcher, **When** switching between MRP, Inventory, Handheld, Quality, Compras, and Ventas apps, **Then** the UI updates views instantly using standardized REST endpoints (`/api/v1/...`).
2. **Given** a network interruption on shop-floor handheld devices, **When** capturing roll weights, **Then** the presentation layer holds local state offline and syncs with the API gateway asynchronously.

---

### User Story 3 - Outbox Messaging & CONTPAQi Integration Bridge Isolation (Priority: P3)

As a System Integrator, I want the CONTPAQi Integration Bridge isolated in a dedicated 32-bit x86 service communicating via an Outbox queue with the core application backend, so that 64-bit API operations remain responsive even during heavy ERP SDK locks.

**Why this priority**: Guarantees system resilience and CFDI compliance by isolating 32-bit Borland/CLR SDK memory constraints from main HTTP API traffic.

**Independent Test**: Can be tested by pushing synthetic Outbox messages from the API layer and verifying that the Integration Bridge consumes them sequentially without blocking API requests.

**Acceptance Scenarios**:

1. **Given** high-volume roll weighing on the shop floor, **When** multiple weight events occur simultaneously, **Then** events are enqueued in the Outbox database and processed asynchronously by the Integration Bridge without API request timeouts.
2. **Given** an ERP SDK error in CONTPAQi, **When** transaction processing fails, **Then** the Outbox message moves to Dead Letter Queue (DLQ) status without crashing the API or domain state.

---

## Edge Cases

- What happens when a project layer introduces a forbidden reverse dependency (e.g. Domain referencing EF Core or Web UI)? The solution build MUST fail with explicit dependency validation rules.
- How does the system handle concurrent Outbox message reads when the Integration Bridge service restarts? The Outbox worker MUST acquire a single-instance lock to ensure thread-safe sequential processing.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The root solution MUST be structured into strict Clean Architecture layers with explicit dependency directions: `Domain` (Core) $\rightarrow$ `Application` $\rightarrow$ `Infrastructure` $\rightarrow$ `ContpaqBridge` $\rightarrow$ `Api` $\rightarrow$ `Presentation (Web)`.
- **FR-002**: The `Domain` layer (`PolyConecta.Domain`) MUST contain pure business entities (`MasterOrder`, `SubOrder`, `RolloMaestro`, `PolyLocation`, `RawMaterialCatalog`, `MassBalanceAudit`), value objects (`Folio`), and domain services without external framework dependencies.
- **FR-003**: The `Infrastructure` layer (`PolyConecta.Infrastructure`) MUST encapsulate all EF Core database contexts (`PolyDbContext`), migration scripts, and the Outbox publisher interface implementation.
- **FR-004**: The `ContpaqBridge` layer (`PolyConecta.ContpaqBridge`) MUST operate as a dedicated .NET 8 x86 process encapsulating `MGW_SDK.dll` P/Invoke calls and direct SQL `NOLOCK` catalog queries.
- **FR-005**: The `Api` layer (`PolyConecta.Api`) MUST expose RESTful controllers for Orders, Rolls, Locations, and Raw Materials with OpenAPI/Swagger specifications and exception handling middleware.
- **FR-006**: The `Presentation` layer (`PolyConecta.Web` / static assets in `wwwroot`) MUST deliver an Odoo 19 Enterprise SPA with App Launcher grid, Kanban, List, Form views, and Handheld mobile screens.
- **FR-007**: The system MUST enforce the Outbox pattern for asynchronous ERP synchronization, decoupling shop-floor execution from CONTPAQi SDK latency.
- **FR-008**: The system MUST implement Quality Gate Hard-Stops in the Infrastructure persistence layer, blocking inventory transfers of quarantined lots.
- **FR-009**: The solution structure MUST list and organize all existing codebase assets cleanly into their respective architectural layers without orphan files.
- **FR-010**: All layers MUST pass isolated unit and integration test suites (`PolyConecta.Domain.Tests` and `PolyConecta.IntegrationTests`).

### Key Entities & Layer Mapping

#### Existing & Restructured Layers Map

| Layer Order | Layer Name | Project / Directory Path | Responsibilities & Tech Stack |
| :--- | :--- | :--- | :--- |
| **1. Core / Domain** | `Domain Layer` | `backend/src/PolyConecta.Domain/` | Entities, Value Objects (`Folio`), Domain Services (`MassBalanceService`), Interfaces. **Zero external dependencies.** |
| **2. Infrastructure** | `Persistence & Outbox` | `backend/src/PolyConecta.Infrastructure/` | EF Core `PolyDbContext`, EF Configurations, `OutboxPublisher`, Migrations, Repository implementations. |
| **3. Integration Bridge** | `CONTPAQi Win32 Bridge` | `backend/src/PolyConecta.ContpaqBridge/` (or `dist/win-x86/`) | Dedicated .NET 8 x86 worker, P/Invoke to `MGW_SDK.dll`, SQLite Outbox DB (`bridge_outbox.db`), direct SQL reads (`adm*`). |
| **4. API Gateway** | `REST API Backend` | `backend/src/PolyConecta.Api/` | ASP.NET Core Web API, Controllers (`Orders`, `Rolls`, `Locations`, `RawMaterials`), Middleware, Swagger UI. |
| **5. Presentation** | `Odoo 19 Web SPA` | `backend/src/PolyConecta.Api/wwwroot/` | Single Page App (HTML/CSS/JS), Odoo 19 App Launcher, Kanban / List / Form Views, Handheld Mobile Screen. |

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of solution projects compile cleanly with zero circular dependencies across Clean Architecture layers.
- **SC-002**: 100% of automated unit and integration tests pass cleanly across Domain and API layers.
- **SC-003**: System processes shop-floor roll weighing requests in under 200ms on Handheld devices regardless of ERP sync locks.
- **SC-004**: Users can seamlessly switch between all 6 Odoo 19 App Launcher modules (MRP, Inventory, Handheld, Quality, Compras, Ventas) in under 1 second.

---

## Assumptions

- Target environment supports .NET 8 SDK and x86 runtime execution for the CONTPAQi SDK bridge worker.
- SQLite or SQL Server InMemory database context is used during local development and testing, easily configurable to PostgreSQL or SQL Server for production.
- Existing database tables in CONTPAQi (`admAlmacenes`, `admProductos`, `admCapasProducto`, `admDocumentos`) remain read-only for direct SQL queries and writable exclusively through the Integration Bridge SDK worker.
