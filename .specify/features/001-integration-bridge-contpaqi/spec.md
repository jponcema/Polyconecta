# Feature Specification: 001 - Standalone CONTPAQi Integration Bridge & Real-Time Monitoring Dashboard

**Feature Branch**: `001-integration-bridge-contpaqi`  
**Created**: 2026-09-11  
**Last Updated**: 2026-09-14 (Retroalimentado con hallazgos empíricos de sesión)  
**Status**: Approved & Refactored  
**Input**: User description: "001-integration-bridge-contpaqi: Standalone, application-agnostic .NET 8 x86 CONTPAQi Comercial Premium v10+ Integration Bridge with REST API, Outbox Pattern, SQL Server Read Pipeline, and Embedded Real-Time Web Dashboard"

---

## 1. Overview

El **CONTPAQi Integration Bridge** es un microservicio autónomo yagnóstico desarrollado en **.NET 8 (x86 32-bit)** diseñado para servir de puente entre aplicaciones externas de PolyConecta (Handhelds, Portal de Clientes, Motor de Ruteo MES) y **CONTPAQi Comercial Premium v10+**.

Opera desacoplado de la lógica de negocio cliente, exponiendo una API REST/JSON estandarizada para comandos de escritura (vía cola Outbox FIFO) y lecturas directas a SQL Server (`adm*`). Incluye un **Dashboard Web en Tiempo Real** para monitoreo operativo y gestión administrativa de la cola de mensajes fallidos (Dead Letter Queue - DLQ).

---

## 2. Hallazgos Empíricos y Restricciones de Arquitectura (Sesión de Validación)

De acuerdo con las pruebas empíricas y diagnóstico de ejecuciones nativas Win32 en VPS (`vps-innatos`), la especificación incorpora de forma obligatoria los siguientes principios técnicos:

1. **Ciclo de Vida Único del SDK (`fInicializaSDK` / `fTerminaSDK`)**:
   - `fInicializaSDK()` debe llamarse **UNA SOLA VEZ** por ciclo de vida del proceso `Contpaq.Bridge.exe`.
   - `fTerminaSDK()` solo se invoca cuando el proceso se detiene definitivamente. Llamar a `fTerminaSDK()` durante cierres de sesión de empresa corrompe la memoria nativa de Borland/CLR y provoca fallos de acceso `0xc0000005` en `coreclr.dll`.
   - El cierre de sesión (`CloseCompanySession`) debe llamar **únicamente** a `fCierraEmpresa()`.

2. **Exclusividad de Proceso y Entorno de Ejecución (Session 2 Interactive)**:
   - El SDK de CONTPAQi admite **un único proceso activo** que mantenga el handle de licencia.
   - `Contpaq.Bridge.exe` se ejecuta como servicio/tarea en la Sesión de Usuario Interactiva (Session 2 / Session ID 2) bajo arquitectura de 32 bits (`win-x86`) vía `ContpaqBridgeTask`.

3. **Carga Nativa de DLLs (`NativeLibrary.SetDllImportResolver`)**:
   - Se utiliza `NativeLibrary.SetDllImportResolver` para resolver dinámicamente `MGW_SDK.dll` desde la ruta efectiva (`C:\Program Files (x86)\Compac\COMERCIAL` o `AdminPAQSDK`), evitando errores `DllNotFoundException` (Win32 Error 126/127).
   - Se debe establecer `Directory.SetCurrentDirectory` + `SetDllDirectory` antes del primer llamado JIT.
   - Se omite `fSetNombrePAQ` para Comercial Premium, evitando violaciones de acceso de memoria.

4. **Manejo del Código de Retorno `126209` (`fAbreEmpresa`)**:
   - El código `126209` indica que la empresa ya se encuentra abierta en la sesión activa de CONTPAQi. Debe ser interpretado y tratado explícitamente como una **condición de éxito** (`SUCCESS`).

5. **Protección y Tolerancia de Afectación (`fAfectaDocto_Param`)**:
   - La llamada a `fAfectaDocto_Param` debe contar con protección try-catch. Si la afectación reporta advertencias no fatales, la transacción mantiene su estado `COMPLETED` garantizando que el documento y movimiento creados en `fAltaDocumento` / `fAltaMovimiento` conservan su `DocId` y `Folio` asignados.

---

## 3. User Scenarios & Acceptance Criteria

### User Story 1 - Agnostic ERP Command Ingestion & Local Outbox Persistence (Priority: P1)

Como Aplicación Cliente Externa (Handheld, Portal PolyConecta), quiero enviar solicitudes de transacción ERP genéricas (crear documentos, agregar movimientos, asociar lotes, afectar inventarios) al Bridge mediante una API REST/JSON estandarizada, recibiendo una respuesta inmediata `202 Accepted` mientras el Bridge garantiza un procesamiento FIFO durable y libre de pérdidas.

**Acceptance Scenarios**:
1. **Given** un payload JSON válido (`DOCUMENT_CREATE`), **When** la aplicación cliente envía una petición a `POST /api/v1/transactions`, **Then** el Bridge valida la estructura, asigna `TransactionId` y `CorrelationId`, guarda el registro en SQLite Outbox (`bridge_outbox.db`) con estado `PENDING` y retorna `202 Accepted` en < 100 ms.
2. **Given** transacciones pendientes en la cola Outbox, **When** el worker loop secuencial procesa la cola en un hilo de apartamento único (STA thread), **Then** ejecuta la secuencia SDK (`fAbreEmpresa`, `fAltaDocumento`, `fAltaMovimiento`, `fAfectaDocto_Param`), actualizando el estado a `COMPLETED` con el `contpaqi_doc_id` y `contpaqi_folio` resultantes.
3. **Given** una transacción finalizada con `callback_url`, **When** se completa el procesamiento, **Then** el servicio dispara un Webhook POST asíncrono con los metadatos asignados.

---

### User Story 2 - Standalone Web Dashboard & Real-Time Monitoring (Priority: P2)

Como Administrador del Sistema, quiero acceder a un Dashboard Web embebido (`http://localhost:5005`) para supervisar métricas de procesamiento (Throughput ops/sec, profundidad de cola Outbox, latencia promedio del SDK, badges de salud de conexión) y gestionar elementos en la cola de errores (DLQ).

**Acceptance Scenarios**:
1. **Given** el servicio Bridge en ejecución, **When** se navega al puerto 5005, **Then** el dashboard despliega gráficos interactivos actualizados vía WebSockets / SignalR.
2. **Given** transacciones fallidas agotadas tras 5 reintentos en estado `DEAD_LETTER_QUEUE`, **When** el administrador abre el panel DLQ, **Then** puede inspeccionar el error, editar el payload JSON en un modal y reencolar la transacción a estado `PENDING`.

---

### User Story 3 - High-Throughput Worker Loop & Circuit Breaker (Priority: P3)

Como Integrador de Sistemas, quiero que el motor de ejecución del SDK mantenga la sesión de empresa abierta entre ráfagas de transacciones, con timeout de 8s por transacción y protección de Circuit Breaker.

**Acceptance Scenarios**:
1. **Given** ráfagas consecutivas en la cola, **When** el worker las procesa, **Then** reutiliza la sesión abierta de empresa (`_isCompanyOpen = true`), cerrándola únicamente tras 3600s de inactividad o al detener la aplicación.
2. **Given** 3 fallos o bloqueos de tabla seguidos en CONTPAQi, **When** se alcanza el umbral, **Then** el Circuit Breaker conmuta a estado `OPEN` durante 15 segundos deteniendo el consumo de cola y actualizando los indicadores de salud.

---

### User Story 4 - High-Speed Read-Only SQL Pipeline (Priority: P4)

Como Aplicación Cliente, quiero consultar catálogos de productos, clientes, almacenes y existencias directamente por SQL sin pasar por el SDK nativo de escritura.

**Acceptance Scenarios**:
1. **Given** una consulta GET a `/api/v1/catalogs/*` o `/api/v1/inventory/*`, **When** se ejecuta la llamada, **Then** el Bridge realiza una lectura directa en SQL Server `adm*` usando sugerencias no bloqueantes (`READ UNCOMMITTED` / `NOLOCK`), retornando el resultado en < 50 ms.

---

## 4. Requirements & Data Contracts

### Functional Requirements

- **FR-001**: Microservicio autónomo e independiente en .NET 8 (x86), ejecutable en Sesión 2 interactiva.
- **FR-002**: Dashboard Web integrado en puerto 5005 mediante Kestrel + SignalR.
- **FR-003**: Panel de administración de Dead Letter Queue (DLQ) para edición JSON y reintento manual.
- **FR-004**: Hilo de trabajo único de apartamento STA (`ApartmentState.STA`) para la ejecución nativa P/Invoke de `MGW_SDK.dll`.
- **FR-005**: Escrituras prohibidas en SQL directo; todas las altas de documentos y movimientos DEBEN usar el SDK oficial (`fAltaDocumento`, `fAltaMovimiento`, etc.).
- **FR-006**: Lecturas directas de catálogos y existencias vía SQL Server (`admProductos`, `admClientes`, `admAlmacenes`, `admExistenciaCapa`) con `NOLOCK`.
- **FR-007**: Persistencia Outbox durable en SQLite local (`bridge_outbox.db`).
- **FR-008**: Manejo de `fInicializaSDK` al inicio del servicio y `fTerminaSDK` al apagarlo.
- **FR-009**: Inclusión de `NativeLibrary.SetDllImportResolver` para carga robusta de DLLs en Windows.
- **FR-010**: Manejo explícito de error `126209` como éxito en `fAbreEmpresa`.

### Core Data Payload Contract (`DOCUMENT_CREATE`)

```json
{
  "client_app_id": "polyconecta-app",
  "command_type": "DOCUMENT_CREATE",
  "payload": {
    "codigo_concepto": "1",
    "codigo_cliente_proveedor": "2441MXN",
    "fecha": "09/14/2026",
    "referencia": "COT-2026-001",
    "observaciones": "Cotización emitida desde PolyConecta",
    "movimientos": [
      {
        "codigo_producto": "SOPORTETEC",
        "unidades": 1.0,
        "precio": 250.00,
        "codigo_almacen": "1"
      }
    ]
  }
}
```

---

## 5. Success Criteria

- **SC-001**: Respuesta del endpoint POST `/api/v1/transactions` en < 100 ms (`202 Accepted`).
- **SC-002**: Procesamiento en cola Outbox a velocidad de 10-20 operaciones por segundo.
- **SC-003**: Invocación nativa del SDK de CONTPAQi sin cierres inesperados por `fTerminaSDK` o `0xc0000005`.
- **SC-004**: Apertura exitosa de empresa reconociendo el código `126209` como sesión activa válida.
- **SC-005**: Monitoreo continuo de salud vía GET `/health` reportando `status: Healthy`, `sdk_initialized: true`, `sql_connected: true`.
