# Implementation Plan: Standalone CONTPAQi Integration Bridge (.NET x86 Worker)

**Branch**: `001-integration-bridge-contpaqi` | **Date**: 2026-09-11 | **Spec**: [.specify/features/001-integration-bridge-contpaqi/spec.md](file:///Users/emilio/Development/Sandbox/Polyconecta/.specify/features/001-integration-bridge-contpaqi/spec.md)

**Input**: Feature specification from `.specify/features/001-integration-bridge-contpaqi/spec.md`

---

## Summary

The **Standalone CONTPAQi Integration Bridge** is a completely application-agnostic integration microservice designed to connect external client systems with CONTPAQi Comercial Premium v10+. It executes as a 32-bit (.NET x86) standalone web service hosting Kestrel, exposing a generic REST/gRPC API, managing an asynchronous SQLite Outbox queue, executing SDK write functions sequentially in an STA thread pool, providing high-speed direct SQL Server read lookups, and hosting an embedded **Real-Time Visual Web Dashboard** with live metric streaming and Dead Letter Queue (DLQ) administrative controls.

---

## Technical Context

**Language/Version**: C# / .NET 8.0 or .NET 9.0 (targeting `win-x86` 32-bit process execution mode)  
**Primary Dependencies**: ASP.NET Core Kestrel (REST API + SignalR WebSockets), Dapper / `System.Data.SQLite` (Local Outbox persistence), `Microsoft.Data.SqlClient` (Direct SQL Server read queries), Serilog (Structured JSON logging with CorrelationId)  
**Storage**: Embedded SQLite database (`bridge_outbox.db`) for Outbox queue, execution logs, Webhook delivery tracking, and metrics snapshots  
**Testing**: xUnit, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing, Moq for P/Invoke SDK wrappers  
**Target Platform**: Windows Server 2016+ / Windows 10/11 x64 (running in 32-bit `x86` execution mode as a Windows Service or Console Application)  
**Project Type**: Standalone Integration Microservice / Web Service with Embedded Visual Web Dashboard  
**Performance Goals**: Outbox throughput >= 10 ops/sec (>= 600 ops/min), API response < 100 ms, Direct SQL read latency < 50 ms, Dashboard WebSocket update < 500 ms  
**Constraints**: 8-second max execution timeout per SDK transaction, single-threaded apartment (STA) thread isolation for C++ `MGW_SDK.dll`, Circuit Breaker after 3 consecutive failures  
**Scale/Scope**: Standalone microservice supporting 100+ queued transactions per burst, 5 max retries with exponential backoff + jitter  

---

## Constitution Check

*GATE: Passed before Phase 0 research. Re-evaluated post Phase 1 design.*

1. **Principle I: ERP Repository of Record**  
   *Status*: PASSED. CONTPAQi Comercial Premium v10+ remains the single master repository of record for billing, accounting, and official stock balances.
2. **Principle II: Asynchronous ERP Synchronization & Outbox Resilience**  
   *Status*: PASSED. Integration strictly uses an asynchronous Outbox Pattern. All write operations occur exclusively through official CONTPAQi SDK functions (`fInicializaSDK`, `fAbreEmpresa`, `fAltaDocumento`, `fAltaMovimiento`, `fAltaMovimientoSeriesCapas`, `fAfectaDocto_Param`). Direct SQL `INSERT` or `UPDATE` queries to CONTPAQi database tables (`adm*`) are strictly prohibited.
3. **Principle VII: Authoritative Technical Backing**  
   *Status*: PASSED. Technical design is explicitly grounded in [Referencia_SDK_CONTPAQi.md](file:///Users/emilio/Development/Sandbox/Polyconecta/docs/contpaq/Referencia_SDK_CONTPAQi.md) and [Referencia_BD_CONTPAQi.md](file:///Users/emilio/Development/Sandbox/Polyconecta/docs/contpaq/Referencia_BD_CONTPAQi.md).
4. **Principle VIII: Alignment with Existing CONTPAQi Operational Database Reality**  
   *Status*: PASSED. Entities and read schemas directly map to `admProductos`, `admClientes`, `admAlmacenes`, `admConceptos`, `admDocumentos`, `admMovimientos`, `admCapasProducto`, and `admExistenciaCapa`.

---

## Project Structure

### Documentation (this feature)

```text
.specify/features/001-integration-bridge-contpaqi/
├── spec.md              # Feature Specification (User Stories, FRs, Success Criteria)
├── plan.md              # Implementation Plan (This file)
├── research.md          # Phase 0 Output (Technical research & architecture decisions)
├── data-model.md        # Phase 1 Output (SQLite schema, SQL read models, state flow)
├── quickstart.md        # Phase 1 Output (Validation scenarios & testing guide)
├── contracts/           # Phase 1 Output (OpenAPI / JSON Schema specifications)
│   ├── transactions-api.json
│   └── catalogs-api.json
└── checklists/
    └── requirements.md  # Specification Quality Checklist
```

### Source Code Structure (Standalone Bridge Microservice)

```text
src/Contpaq.Bridge/
├── Api/
│   ├── Controllers/
│   │   ├── TransactionsController.cs   # POST /api/v1/transactions, GET status
│   │   ├── DlqController.cs            # GET /api/v1/dlq, Retry, Edit, Purge
│   │   └── CatalogsController.cs       # GET /api/v1/catalogs/*, GET /api/v1/inventory/*
│   └── Hubs/
│       └── DashboardHub.cs             # Real-time WebSocket telemetry for Dashboard
├── Core/
│   ├── Commands/                       # Generic ERP command DTOs & JSON validators
│   ├── Interfaces/                     # IContpaqiSdkGateway, IOutboxRepository, ISqlReadRepository
│   └── Models/                         # BridgeTransaction, TransactionLog, MetricSnapshot
├── Infrastructure/
│   ├── Sdk/
│   │   ├── ContpaqiSdkNative.cs        # P/Invoke DllImport declarations for MGW_SDK.dll
│   │   ├── ContpaqiSdkGateway.cs       # STA thread worker loop with 8s timeout & session reuse
│   │   └── CircuitBreakerPolicy.cs     # 3-failure threshold, 15s cooldown circuit breaker
│   ├── Persistence/
│   │   ├── OutboxRepository.cs         # SQLite Dapper data access & queue management
│   │   └── SqlReadRepository.cs        # Direct SQL Server READ UNCOMMITTED Dapper queries
│   └── Webhooks/
│       └── WebhookDispatcher.cs        # Async HTTP POST callback delivery engine
└── Dashboard/                          # Embedded Real-Time Web Monitoring UI
    ├── wwwroot/
    │   ├── index.html                  # Dashboard SPA layout
    │   ├── js/dashboard.js             # SignalR client + Chart.js real-time graphs
    │   └── css/dashboard.css           # UI styling & status badges
    └── Controllers/
        └── DashboardViewController.cs  # Serves embedded dashboard HTML
```

---

## Complexity Tracking

> **No Constitution violations present. Gates are fully satisfied.**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| *None* | N/A | N/A |
