# Research Artifact: 000 Solution Root Clean Architecture Restructuring

**Feature Branch**: `000-solution-clean-architecture`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md)

---

## 1. Architectural Decisions & Layer Boundaries

### Decision 1: Strict Clean Architecture Layer Ordering
* **Chosen Solution**: Organize the root .NET 8 C# solution into 5 explicit layers with strictly unidirectional inward dependencies:
  1. `PolyConecta.Domain` (Core Domain Entities, Value Objects, Domain Services, Repository Interfaces - **0 Framework Dependencies**).
  2. `PolyConecta.Infrastructure` (EF Core `PolyDbContext`, Persistence Configurations, Outbox Publisher, Repository Implementations).
  3. `PolyConecta.ContpaqBridge` (Dedicated .NET 8 x86 Win32 Service, `MGW_SDK.dll` P/Invoke wrapper, SQLite `bridge_outbox.db`, direct SQL `NOLOCK` catalog reads).
  4. `PolyConecta.Api` (ASP.NET Core Web API, REST Controllers, OpenAPI/Swagger Docs, Exception Middleware).
  5. `PolyConecta.Web` / `wwwroot` (Presentation SPA, Odoo 19 Enterprise App Launcher, Kanban / List / Form views, Handheld Barcode screen).
* **Rationale**: Meets Constitution Principle I (CONTPAQi as master ERP, PolyConecta as intelligence layer) and Principle II (Asynchronous Outbox Sync with dedicated 32-bit Win32 Bridge).
* **Alternatives Considered**:
  * Monolithic API with embedded SDK calls: Rejected because `MGW_SDK.dll` is 32-bit (x86) and causes CLR memory crashes (`0xc0000005`) if run inside a 64-bit API process.

### Decision 2: Odoo 19 Enterprise UI/UX Standard Integration
* **Chosen Solution**: Pure HTML5/Bootstrap 5 SPA hosted under `PolyConecta.Api/wwwroot/` emulating Odoo 19 Enterprise UI:
  * **App Launcher (Main Menu Grid)**: Topbar button and home screen with 6 core apps (*MRP*, *Almacenes*, *Handheld Báscula*, *Calidad*, *Compras/MP*, *Ventas/O2C*).
  * **Kanban View**: Drag/Click columns grouped by pipeline status (`Borrador` → `Programado` → `En Proceso` → `Control Calidad` → `Finalizado`).
  * **Form View**: Action buttons, **Smart Buttons Box** with live counters (*12 Rollos*, *Calidad Pass*, *3 Movimientos*), and upper-right **Statusbar Pipeline**.
* **Rationale**: Meets Constitution Principle IX (Mandatory alignment of all UIs and shop-floor interactions with Odoo 19 Enterprise).
* **Alternatives Considered**:
  * Heavy JS framework (React/Angular): Deferred for Phase 1 to maintain zero build setup, instant browser load, and direct alignment with .NET static assets.

### Decision 3: Outbox Asynchronous Queueing for CONTPAQi Sync
* **Chosen Solution**: Shop-floor transactions (Roll Weighing, Stock Transfers, Production Receipts) write domain entities to DB and publish outbox messages. The `ContpaqBridge` worker pulls outbox messages sequentially and executes Win32 SDK calls (`fAltaDocumento`, `fAltaMovimientoSeriesCapas`).
* **Rationale**: Meets Constitution Principle II (Asynchronous ERP Synchronization). Prevents shop-floor handheld latency during CONTPAQi database lock times.

---

## 2. Technology Stack & Environment Matrix

| Component | Selected Technology | Purpose |
| :--- | :--- | :--- |
| **Domain & API Framework** | .NET 8 C# (64-bit) | Web API, Controllers, Business Rules |
| **Integration Bridge** | .NET 8 C# (32-bit x86) | Win32 `MGW_SDK.dll` Interop & Outbox Worker |
| **ORM & Database Context** | Entity Framework Core 8 | `PolyDbContext` with InMemory DB for Dev/Test |
| **Outbox Storage** | EF Core / SQLite (`bridge_outbox.db`) | Durable asynchronous transaction queue |
| **Presentation SPA** | HTML5 / Bootstrap 5 / JS (Vanilla) | Odoo 19 Enterprise UI Suite |
| **API Documentation** | Swashbuckle OpenAPI / Swagger | Interactive API testing and contracts |
