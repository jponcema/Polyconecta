# Feature Specification: 001 - Standalone CONTPAQi Integration Bridge & Real-Time Monitoring Dashboard

**Feature Branch**: `001-integration-bridge-contpaqi`  
**Created**: 2026-09-11  
**Status**: Draft  
**Input**: User description: "001-integration-bridge-contpaqi: Standalone, application-agnostic .NET x86 CONTPAQi Comercial Premium v10+ Integration Bridge with REST/gRPC API, Outbox Pattern, SQL Server Read Pipeline, and Embedded Real-Time Web Dashboard"

---

## Overview

The **CONTPAQi Integration Bridge** is a **completely standalone, application-agnostic integration microservice** designed to bridge external software applications (ERP extensions, shop-floor management tools, e-commerce engines, custom portals) with **CONTPAQi Comercial Premium v10+**. 

It operates independently of any specific client application, exposing a standardized, generic REST/gRPC API contract for ERP write/read commands. It features an embedded **Real-Time Web Monitoring Dashboard** with interactive performance charts, queue statistics, and Dead Letter Queue (DLQ) administrative controls.

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Agnostic ERP Command Ingestion & Local Outbox Persistence (Priority: P1)

As an External Client Application (such as a shop-floor system, e-commerce engine, or custom ERP extension), I want to send generic ERP transaction requests (create documents, add movements, associate lot/pedimento numbers, affect stock balances) to the Bridge via a standardized REST/gRPC API, receiving an immediate acknowledgment while the Bridge guarantees durable, zero-loss processing.

**Why this priority**: Complete decoupling requires the Bridge to act as a generic transaction broker. External applications must not depend on CONTPAQi SDK binaries, STA thread models, or ERP database locks, receiving an immediate API confirmation (`TransactionId`, status `PENDING`) in under 100 ms.

**Independent Test**: Can be tested by posting 100 generic `DOCUMENT_CREATE` and `MOVEMENT_ADD` JSON payloads from a simple HTTP client (e.g. Postman or cURL) to `POST /api/v1/transactions`, verifying that all 100 requests return `202 Accepted` with unique transaction IDs and are safely persisted in the Bridge's local SQLite Outbox database.

**Acceptance Scenarios**:

1. **Given** a valid generic JSON command payload (`DOCUMENT_CREATE`, `MOVEMENT_ADD`, `LOT_ASSOCIATION`, or `DOCUMENT_AFFECT`), **When** an external application sends a request to `POST /api/v1/transactions`, **Then** the Bridge validates the JSON schema, assigns a unique `CorrelationId` and `TransactionId`, saves the record to local SQLite storage with status `PENDING`, and returns `202 Accepted` in under 100 ms.
2. **Given** a batch of pending Outbox transactions, **When** the Bridge worker processes the queue, **Then** it executes the SDK calls sequentially inside a dedicated 32-bit STA thread using a shared session loop (`fInicializaSDK`, `fAbreEmpresa`, `fAltaDocumento`, `fAltaMovimiento`, `fAfectaDocto_Param`), updating transaction status to `COMPLETED` with the resulting CONTPAQi document ID and folio number.
3. **Given** an incoming command containing a `callback_url` parameter, **When** transaction execution completes (or permanently fails), **Then** the Bridge sends an asynchronous HTTP Webhook callback to the specified `callback_url` with the execution outcome.

---

### User Story 2 - Standalone Web Dashboard for Real-Time Visual Monitoring & DLQ Control (Priority: P2)

As a System Administrator or Integration Manager, I want to open a dedicated web dashboard hosted directly by the Bridge (e.g. `http://bridge-host:5055`) to view real-time transaction charts, queue depth, throughput metrics, SDK connection health, and interactively manage failed transactions in the Dead Letter Queue (DLQ).

**Why this priority**: A standalone service must provide its own operational observability and diagnostic tooling. An embedded web dashboard gives administrators instant visual insights into ERP sync health without needing third-party monitoring stacks or client-specific UIs.

**Independent Test**: Can be tested by navigating to the Bridge's web dashboard URL in a browser while sending a batch of transactions, verifying that real-time line charts update smoothly via WebSockets showing throughput (ops/sec), queue depth, SDK response latency, and error counts, and using the DLQ panel to inspect, edit payload, and retry a failed transaction.

**Acceptance Scenarios**:

1. **Given** the Bridge service is running, **When** an administrator accesses the web dashboard port (e.g., `http://localhost:5055`), **Then** the dashboard displays live visual charts (throughput ops/sec, Outbox queue depth, average SDK latency, error/retry rates, and DLL/SQL connection status badges) updated in real time via WebSockets.
2. **Given** an Outbox transaction that has reached `DEAD_LETTER_QUEUE` status after maximum retries, **When** the administrator opens the DLQ Management panel, **Then** the dashboard presents full transaction details (JSON payload, error history, stack trace, timestamps) with interactive buttons to **Retry Immediately**, **Edit Payload & Retry**, or **Purge**.
3. **Given** an administrator using the DLQ Management panel, **When** they click "Edit Payload & Retry" on a failed item, **Then** the UI provides a JSON editor modal, validates the modified payload, updates the transaction record, resets `retry_count` to 0, and re-queues it for execution.

---

### User Story 3 - High-Throughput Single-Threaded SDK Execution & Dynamic Session Reuse (Priority: P3)

As a System Integrator, I want the Bridge Worker engine to process high-volume transaction bursts efficiently using continuous SDK session reuse and tight execution timeouts (5-10s per transaction), ensuring high throughput (~10-20 ops/sec) while preventing ERP table locks from stalling the service.

**Why this priority**: Initializing and closing the CONTPAQi SDK per transaction adds ~500ms-1s overhead per call. Maintaining an active enterprise session across queue bursts with strict transaction timeouts and Circuit Breaker protection guarantees high performance and fault isolation.

**Independent Test**: Can be tested by queueing 100 transactions into the Bridge, verifying that the worker processes the burst in a single open SDK session (`fAbreEmpresa`) in under 10 seconds, and that if 1 transaction hits a table lock timeout (> 8s), the worker cancels the call, triggers a Circuit Breaker after 3 consecutive failures, and resumes queue processing safely after cooling down.

**Acceptance Scenarios**:

1. **Given** multiple pending transactions in the Outbox queue, **When** the Bridge worker is active, **Then** it maintains a single open SDK enterprise session across consecutive items, closing the session only after a configurable idle threshold (default 5 seconds of empty queue) or upon encountering a fatal DLL error.
2. **Given** an SDK function call that encounters a database lock in CONTPAQi Comercial Premium, **When** execution exceeds the 8-second transaction timeout limit, **Then** the worker cancels the transaction attempt, logs an `SDK_TIMEOUT` error, increments the item retry count, and releases the execution semaphore.
3. **Given** 3 consecutive transaction failures due to DLL crashes or persistent database locks, **When** the threshold is reached, **Then** the Circuit Breaker trips to `OPEN` state for 15 seconds, pausing further SDK execution, updating the connection health badge on the Dashboard to `DEGRADED/PAUSED`, and auto-testing recovery in `HALF-OPEN` state.

---

### User Story 4 - High-Speed Read-Only SQL Pipeline & Decoupled Telemetry (Priority: P4)

As an External Client Application, I want to query CONTPAQi master catalogs (products, clients, warehouses, concepts) and inventory balances directly via high-speed read endpoints on the Bridge, and receive decoupled Webhook / WebSocket notifications for async writes.

**Why this priority**: Read operations represent the majority of ERP interactions. Direct SQL reads with non-blocking hints bypass SDK thread locks, returning catalog and stock data in milliseconds without interfering with write transactions.

**Independent Test**: Can be tested by executing concurrent HTTP `GET` requests to `/api/v1/catalogs/products` and `/api/v1/inventory/stocks` while an SDK write batch is processing, verifying that read responses return in under 50 ms without blocking or being blocked by SDK operations.

**Acceptance Scenarios**:

1. **Given** a read query request for product catalog or stock availability, **When** an external application calls `GET /api/v1/catalogs/*` or `GET /api/v1/inventory/*`, **Then** the Bridge executes a direct read query against SQL Server `adm*` tables using `READ UNCOMMITTED` hints and returns formatted JSON in under 50 ms.
2. **Given** an external client application configured with Webhook subscriptions, **When** an Outbox transaction transitions to `COMPLETED` or `DEAD_LETTER_QUEUE`, **Then** the Bridge delivers an asynchronous HTTP POST notification containing the `TransactionId`, `ClientAppId`, final status, and CONTPAQi document metadata to the client's registered callback URL.

---

### Edge Cases

- **What happens when the CONTPAQi server reboots mid-transaction?**  
  The current in-flight SDK call times out after 8 seconds. The transaction is rolled back, the SDK session is reset, and the item remains in `PENDING` state in the local SQLite Outbox for retry upon worker auto-reconnect.
- **What happens if an external application sends an invalid JSON command?**  
  The Bridge's API gateway rejects the request immediately with HTTP `400 Bad Request` and structured validation error details, without persisting invalid items into the Outbox queue.
- **What happens if the client's Webhook endpoint is unreachable when a transaction finishes?**  
  Webhook deliveries use an independent retry queue with exponential backoff (up to 3 retries). Webhook failures do not impact the core ERP transaction state or Outbox queue.
- **What happens if a duplicate transaction command is submitted?**  
  Every command accepts an optional `idempotency_key`. The Bridge checks the local Outbox database before inserting; if the key exists, it returns the existing transaction status without re-queueing SDK execution.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The System MUST operate as a completely standalone, application-agnostic integration microservice, exposing a standardized REST/gRPC API contract for ERP write and read commands decoupled from any specific client business logic.
- **FR-002**: The System MUST host an embedded Real-Time Web Monitoring Dashboard (built on Kestrel + WebSockets) accessible on a configurable port (e.g. `http://host:5055`), providing live charts for Throughput (ops/sec), Queue Depth, SDK Latency, Error/Retry Rates, and Connection Health.
- **FR-003**: The System MUST include an interactive Dead Letter Queue (DLQ) Management Panel within the Web Dashboard allowing administrators to inspect failed transaction payloads, edit JSON payloads inline, re-queue items for immediate execution, or purge items.
- **FR-004**: The System MUST isolate all CONTPAQi SDK write invocations (`MGW_SDK.dll` / `MGWServicios`) inside a dedicated 32-bit (.NET x86) Worker Process executing on a single-threaded apartment (STA) thread pool controlled by `SemaphoreSlim(1,1)` and `System.Threading.Channels`.
- **FR-005**: The System MUST strictly enforce that ALL database write operations (creating documents, adding movements, associating lot numbers, affecting stock balances) occur exclusively through official CONTPAQi SDK functions (`fInicializaSDK`, `fAbreEmpresa`, `fAltaDocumento`, `fAltaMovimiento`, `fAltaMovimientoSeriesCapas`, `fAfectaDocto_Param`/`fAfectaDocto`, `fCierraEmpresa`, `fTerminaSDK`). Direct SQL `INSERT`, `UPDATE`, or `DELETE` statements on CONTPAQi `adm*` tables are strictly prohibited.
- **FR-006**: The System MUST perform all read-only catalog, document, and stock layer lookups directly against SQL Server tables (`admProductos`, `admClientes`, `admAlmacenes`, `admConceptos`, `admCapasProducto`, `admExistenciaCapa`, `admDocumentos`, `admMovimientos`) using non-blocking SQL read hints (`READ UNCOMMITTED` / `WITH (NOLOCK)`).
- **FR-007**: The System MUST persist all incoming transaction commands in a local transactional SQLite database (`bridge_outbox.db`) to guarantee durable Outbox queuing, zero message loss during network partitions, and full audit logging.
- **FR-008**: The System MUST support SDK session reuse during queue processing bursts, maintaining an open enterprise session (`fAbreEmpresa`) while items exist in the queue and closing it only after a 5-second idle threshold or upon unrecoverable DLL error.
- **FR-009**: The System MUST enforce a strict per-transaction SDK execution timeout of 8 seconds (configurable between 5 and 10 seconds). If an SDK function execution exceeds this timeout, the operation MUST be aborted, logged, and queued for retry.
- **FR-010**: The System MUST implement an automatic Circuit Breaker that transitions to `OPEN` state for 15 seconds after 3 consecutive transaction timeouts or SDK exception crashes, pausing queue execution and updating Dashboard health badges.
- **FR-011**: The System MUST execute an exponential backoff retry strategy with random jitter (retry delays: 1s, 5s, 15s, 60s, 300s) for transient SDK errors (e.g. table locks, temporary concurrency collisions) up to a maximum of 5 retry attempts.
- **FR-012**: The System MUST transition any transaction failing all 5 retry attempts to `DEAD_LETTER_QUEUE` status, preserving full execution history, error codes, and original payload for inspection in the Web Dashboard.
- **FR-013**: The System MUST deliver asynchronous Webhook notifications to client-specified callback URLs (`callback_url`) upon transaction completion or DLQ transition, accompanied by live WebSocket telemetry streaming to the Web Dashboard.
- **FR-014**: The System MUST load all operational parameters (SDK directory path, SQL Server connection strings, Dashboard HTTP port, timeout limits, max retry counts) from a centralized configuration file (`appsettings.json`) or Environment Variables.
- **FR-015**: The System MUST log all API requests, SDK function calls, and error events in structured JSON format (Serilog) with a unique `CorrelationId` and `TransactionId`.

---

### Key Entities

- **BridgeTransaction**: Primary record representing an ERP write or read command received by the Bridge.
  - *Attributes*: `TransactionId` (UUID), `CorrelationId` (UUID), `ClientAppId` (String), `IdempotencyKey` (String, Nullable), `CommandType` (Enum: `DOCUMENT_CREATE`, `MOVEMENT_ADD`, `LOT_ASSOCIATION`, `DOCUMENT_AFFECT`), `PayloadJson` (Text), `CallbackUrl` (String, Nullable), `Status` (Enum: `PENDING`, `PROCESSING`, `COMPLETED`, `FAILED`, `DEAD_LETTER_QUEUE`), `RetryCount` (Integer), `MaxRetries` (Integer), `NextAttemptAt` (Timestamp), `ContpaqiDocId` (Integer, Nullable), `ContpaqiFolio` (String, Nullable), `CreatedAt` (Timestamp), `UpdatedAt` (Timestamp).
- **TransactionLog**: Audit entry capturing every SDK execution step or error.
  - *Attributes*: `LogId` (UUID), `TransactionId` (UUID), `CorrelationId` (UUID), `AttemptNumber` (Integer), `SdkFunctionName` (String), `SdkErrorCode` (Integer), `ErrorMessage` (Text), `ExecutionDurationMs` (Long), `Timestamp` (Timestamp).
- **MetricSnapshot**: Aggregated real-time metrics captured for Web Dashboard visual charts.
  - *Attributes*: `SnapshotId` (Long), `Timestamp` (Timestamp), `ThroughputOpsPerSec` (Double), `QueueDepth` (Integer), `AverageSdkLatencyMs` (Double), `ErrorRatePercent` (Double), `ActiveSdkSession` (Boolean), `CircuitState` (Enum: `CLOSED`, `OPEN`, `HALF_OPEN`).
- **WebhookDelivery**: Record of callback notifications dispatched to external client applications.
  - *Attributes*: `DeliveryId` (UUID), `TransactionId` (UUID), `CallbackUrl` (String), `HttpStatus` (Integer, Nullable), `ResponseBody` (Text, Nullable), `AttemptCount` (Integer), `DeliveredAt` (Timestamp, Nullable).

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: **Agnostic API Acknowledgment Latency**: 100% of valid transaction requests sent to `POST /api/v1/transactions` return HTTP `202 Accepted` confirmations with unique transaction IDs in under 100 ms.
- **SC-002**: **Outbox Processing Throughput**: During queue processing bursts with an active SDK session, the Bridge processes queued transactions at a minimum throughput of 10 operations per second (>= 600 operations / minute).
- **SC-003**: **Real-Time Dashboard Streaming Latency**: The embedded Web Dashboard updates visual charts (throughput, queue depth, latency, health status) via WebSockets within 500 ms of underlying state changes.
- **SC-004**: **Transaction Timeout Enforcement**: No single hanging or blocked SDK function call exceeds 8 seconds before being safely cancelled and scheduled for retry.
- **SC-005**: **Read Query Latency**: Direct SQL catalog and inventory read queries (`GET /api/v1/catalogs/*`, `GET /api/v1/inventory/*`) return structured JSON results in under 50 ms for 95% of requests.
- **SC-006**: **Zero Message Loss & DLQ Auditability**: 100% of validated transactions submitted to the Bridge are either processed into CONTPAQi or held in the DLQ with full diagnostic history and inline editing capabilities.
- **SC-007**: **Webhook Delivery Reliability**: 99.9% of Webhook callback notifications are delivered to client callback URLs within 2 seconds of transaction completion.

---

## Assumptions

- **Target ERP Version**: CONTPAQi Comercial Premium v10.0.0 or higher installed on Windows Server / OS with valid license file and network access to SQL Server.
- **Service Deployment**: The Bridge runs as a standalone 32-bit (.NET 8/9 x86) Windows Service or console process hosting Kestrel web server for API endpoints and Web Dashboard.
- **Database Access**: Direct read-only SQL Server connection credentials to CONTPAQi `adm*` company databases.
- **Decoupled Architecture**: External client applications interface with the Bridge exclusively via HTTP/REST endpoints or Webhook callbacks, with no direct shared codebase or binary dependency.
