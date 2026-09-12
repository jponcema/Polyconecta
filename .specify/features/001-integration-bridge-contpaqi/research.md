# Technical Research: Standalone CONTPAQi Integration Bridge (.NET x86 Worker)

**Feature Branch**: `001-integration-bridge-contpaqi`  
**Date**: 2026-09-11  
**Spec**: [.specify/features/001-integration-bridge-contpaqi/spec.md](file:///Users/emilio/Development/Sandbox/Polyconecta/.specify/features/001-integration-bridge-contpaqi/spec.md)

---

## Executive Summary

This document captures the architectural research, interop strategy, performance optimizations, and technical decisions evaluated for the **Standalone CONTPAQi Comercial Premium Integration Bridge**. 

---

## Research Decisions

### 1. .NET Execution Architecture & Unmanaged P/Invoke Interop

- **Decision**: Build the Integration Bridge as a 32-bit (.NET 8/9 x86) standalone Windows Service / Console Application hosting Kestrel web server, using C# P/Invoke (`DllImport`) to interface directly with `MGW_SDK.dll` and `MGWServicios.dll`.
- **Rationale**:
  - `MGW_SDK.dll` and `MGWServicios` are 32-bit C++ unmanaged dynamic link libraries provided by CONTPAQi. They CANNOT be loaded into a 64-bit (x64) process space.
  - Compiling the Bridge service targeting `x86` platform target allows direct native interop without requiring out-of-process COM surrogate wrappers or expensive inter-process IPC bridges.
  - The C# P/Invoke signature mapping explicitly marshals C++ `Char*` parameters as `[MarshalAs(UnmanagedType.LPStr)] StringBuilder` for output strings and `ref` integers for return codes (`kSIN_ERRORES = 0`).
- **Alternatives Considered**:
  - *Out-of-Process COM/gRPC 64-bit Proxy*: Rejected due to extra IPC overhead (~10-20ms per call) and unnecessary architectural complexity when a native 32-bit .NET process can host both Kestrel Web API and P/Invoke calls natively.

---

### 2. Single-Threaded Apartment (STA) Concurrency & SDK Session Reuse Engine

- **Decision**: Implement a single-threaded execution worker using `System.Threading.Channels.Channel<BridgeTransaction>` and a dedicated `Thread` with STA apartment state (`Thread.SetApartmentState(ApartmentState.STA)`), controlled by `SemaphoreSlim(1,1)` and dynamic session management.
- **Rationale**:
  - `MGW_SDK.dll` is single-threaded and expects sequential stateful function calls (`fInicializaSDK` -> `fAbreEmpresa` -> `fAltaDocumento` -> `fAltaMovimiento` -> `fAfectaDocto_Param` -> `fCierraEmpresa` -> `fTerminaSDK`).
  - Executing concurrent multi-threaded P/Invoke calls directly against `MGW_SDK.dll` causes memory corruption, access violations, or locked company sessions.
  - **Sustained Session Reuse**: Calling `fInicializaSDK()` and `fAbreEmpresa()` per transaction adds 500ms-1000ms latency. The worker keeps the enterprise session open as long as transactions remain in the queue, closing it only after a 5-second idle timeout. This achieves **10-20 ops/sec throughput**.
  - **Timeout Protection**: Each SDK call is wrapped in a CancellationToken-backed task with an **8-second execution limit**. If CONTPAQi Comercial Premium holds a database lock exceeding 8s, the transaction is safely aborted, logged, and re-queued.
- **Alternatives Considered**:
  - *Per-Transaction Init/Close Lifecycle*: Rejected because 100 transactions would take over 100 seconds to process due to DLL load/unload overhead.
  - *BlockingCollection<T> without Timeout*: Rejected because a single hanging table lock would freeze the integration worker indefinitely.

---

### 3. High-Speed Read Pipeline via SQL Server (`adm*` Tables)

- **Decision**: Execute all read-only queries (product catalog, client details, warehouse lists, stock layer balances, document statuses) directly against SQL Server `adm*` tables using `Dapper` or `Microsoft.Data.SqlClient` with non-blocking query hints (`READ UNCOMMITTED` / `WITH (NOLOCK)`).
- **Rationale**:
  - Reading via SDK functions (`fLeeDatoDocumento`, `fBuscaProducto`) forces all reads through the single-threaded SDK lock, bottlenecking throughput.
  - Direct SQL queries bypass the SDK worker completely, returning catalog data in **< 50 ms** for concurrent external clients without holding or acquiring locks.
  - SQL table schemas (`admProductos`, `admClientes`, `admAlmacenes`, `admConceptos`, `admCapasProducto`, `admExistenciaCapa`, `admDocumentos`, `admMovimientos`) are fully documented in `Referencia_BD_CONTPAQi.md` and stable across CONTPAQi v10+ releases.
- **Alternatives Considered**:
  - *Reading via SDK (`fLeeDato`)*: Rejected due to severe throughput bottleneck and lack of set-based batch query capabilities.

---

### 4. Standalone Real-Time Web Dashboard & DLQ Management Panel

- **Decision**: Embed an ASP.NET Core Kestrel web server serving a lightweight single-page visual dashboard with WebSockets (SignalR) and Chart.js, providing live visual charts and an interactive Dead Letter Queue (DLQ) management panel.
- **Rationale**:
  - Standalone architecture demands self-contained operational visibility. Administrators can open `http://bridge-host:5055` to observe live Throughput (ops/sec), Outbox Queue Depth, SDK Latency (ms), Error/Retry Rates, and DLL/SQL Connection Health badges.
  - **DLQ Management**: When an item fails 5 retries, administrators can inspect the exact JSON payload, view error tracebacks, edit the JSON inline (e.g. fix a misspelled SKU code or invalid customer ID), and click **Retry Immediately** or **Purge**.
  - Real-time updates push via WebSockets every 500 ms with minimal overhead (< 1% CPU utilization).
- **Alternatives Considered**:
  - *Prometheus / Grafana Exporter Only*: Rejected because it requires external infrastructure setup and lacks interactive DLQ payload editing/retry capabilities.
  - *Windows Desktop Application (WPF/WinForms)*: Rejected because web dashboard access allows remote server administration without logging into Remote Desktop (RDP).

---

### 5. Outbox Persistence, Retry Strategy & Webhook Callbacks

- **Decision**: Use a local transactional SQLite database (`bridge_outbox.db`) embedded inside the Bridge for durable Outbox queue persistence, exponential backoff retries with jitter (1s, 5s, 15s, 60s, 300s), and asynchronous Webhook notifications to external client callback URLs (`callback_url`).
- **Rationale**:
  - Local SQLite storage ensures zero message loss even if external clients or central network connections disconnect temporarily.
  - Exponential backoff with jitter prevents thundering herd retries when CONTPAQi database locks clear.
  - Webhooks decouple the Bridge from any specific client framework, allowing any system to receive async transaction execution updates.
- **Alternatives Considered**:
  - *In-Memory Queue Only*: Rejected because service restarts or power outages would lose queued transactions.

---

## Technology Stack Summary

| Component | Selected Technology | Rationale |
|-----------|--------------------|-----------|
| **Runtime Target** | .NET 8 / 9 (32-bit x86) | Native interop with 32-bit `MGW_SDK.dll` |
| **Web Server / API Gateway** | ASP.NET Core Kestrel | REST/gRPC endpoints + WebSockets |
| **SDK Interop Layer** | C# P/Invoke (`DllImport`) | Zero-overhead native C++ function mapping |
| **Outbox Persistence** | SQLite (`System.Data.SQLite` / Dapper) | Fast, transactional, embedded local storage |
| **SQL Server Read Engine** | `Microsoft.Data.SqlClient` + Dapper | Non-blocking `READ UNCOMMITTED` queries |
| **Dashboard UI** | HTML5 / Chart.js / SignalR WebSockets | Lightweight, zero-dependency real-time UI |
| **Logging & Telemetry** | Serilog (Structured JSON + Correlation) | Diagnostic traceability across worker loops |
