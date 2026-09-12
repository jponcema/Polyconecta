# Quickstart & End-to-End Validation Guide: Standalone CONTPAQi Integration Bridge

**Feature Branch**: `001-integration-bridge-contpaqi`  
**Date**: 2026-09-11  
**Spec**: [.specify/features/001-integration-bridge-contpaqi/spec.md](file:///Users/emilio/Development/Sandbox/Polyconecta/.specify/features/001-integration-bridge-contpaqi/spec.md)

---

## Overview

This guide provides runnable end-to-end validation scenarios for testing the **Standalone CONTPAQi Integration Bridge** (`Contpaq.Bridge`) and its embedded **Real-Time Web Dashboard**.

---

## Prerequisites

1. **Operating System**: Windows Server / Windows 10/11 x64 (Bridge executes in 32-bit `x86` mode).
2. **CONTPAQi Installation**: CONTPAQi Comercial Premium v10+ installed with valid license files.
3. **SDK Libraries**: `MGW_SDK.dll` and `MGWServicios.dll` present in `C:\Program Files (x86)\Compac\COMERCIAL` or specified path.
4. **SQL Server Access**: SQL Server connection credentials to the active `adm*` company database.
5. **Runtime Environment**: .NET 8 or .NET 9 SDK (x86 build target enabled).

---

## Scenario 1: Launching Standalone Bridge & Health Check

### 1. Launch Service

Build the solution `Polyconecta.slnx` and run the `Contpaq.Bridge` web service:

```bash
# Build Polyconecta solution
dotnet build Polyconecta.slnx -c Release

# Start standalone Contpaq.Bridge web service
dotnet run --project src/Contpaq.Bridge/Contpaq.Bridge.csproj -c Release
```

### 2. Verify Service Health

Execute an HTTP `GET` request to verify Kestrel web server and SDK initialization status:

```bash
curl -X GET http://localhost:5055/health
```

**Expected Response**:
```json
{
  "status": "Healthy",
  "worker_architecture": "x86",
  "sdk_initialized": true,
  "sql_connected": true,
  "circuit_state": "CLOSED",
  "timestamp": "2026-09-11T12:55:00Z"
}
```

---

## Scenario 2: Submitting a Generic ERP Transaction Command

Submit a generic `DOCUMENT_CREATE` command via REST API to verify Outbox acceptance:

```bash
curl -X POST http://localhost:5055/api/v1/transactions \
  -H "Content-Type: application/json" \
  -d '{
    "client_app_id": "test-client",
    "correlation_id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "idempotency_key": "TEST-REQ-001",
    "callback_url": "http://localhost:9090/webhook-test",
    "command_type": "DOCUMENT_CREATE",
    "payload": {
      "codigo_concepto": "3",
      "codigo_cliente_proveedor": "CLI001",
      "fecha": "2026-09-11",
      "referencia": "TEST-PO-100",
      "observaciones": "Prueba de Integracion Standalone",
      "movimientos": [
        {
          "codigo_producto": "MP-RESINA-01",
          "unidades": 100.0,
          "precio": 50.0,
          "codigo_almacen": "1",
          "lote": {
            "numero_lote": "LOT-2026-A1",
            "fecha_caducidad": "2027-09-11",
            "pedimento": "PED-99001"
          }
        }
      ]
    }
  }'
```

**Expected Response** (`202 Accepted` in < 100 ms):
```json
{
  "transaction_id": "c1f3a2b4-5678-4e9b-8123-abcdef123456",
  "correlation_id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "status": "PENDING",
  "created_at": "2026-09-11T12:55:01Z"
}
```

---

## Scenario 3: Real-Time Web Dashboard Monitoring

1. Open your web browser and navigate to: **`http://localhost:5055`**
2. **Visual Verification**:
   - **Throughput Chart**: Observe real-time ops/sec line graph updating via WebSockets.
   - **Queue Depth Chart**: Monitor pending/processing Outbox queue items.
   - **SDK Response Latency**: View average latency (ms) for SDK function execution.
   - **Connection Health Badge**: Confirm `SDK Session: Active` and `SQL Status: Connected`.

---

## Scenario 4: Simulating Transaction Failure & Interactive DLQ Remediation

### 1. Submit Intentionally Invalid SKU Payload

```bash
curl -X POST http://localhost:5055/api/v1/transactions \
  -H "Content-Type: application/json" \
  -d '{
    "client_app_id": "test-client",
    "command_type": "DOCUMENT_CREATE",
    "payload": {
      "codigo_concepto": "3",
      "codigo_cliente_proveedor": "CLI001",
      "fecha": "2026-09-11",
      "movimientos": [
        {
          "codigo_producto": "SKU-NONEXISTENT-999",
          "unidades": 1.0,
          "codigo_almacen": "1"
        }
      ]
    }
  }'
```

### 2. Verify Escalation to Dead Letter Queue (DLQ)

After 5 retries, verify the transaction moves to DLQ state:

```bash
curl -X GET http://localhost:5055/api/v1/dlq
```

### 3. Remediate via Dashboard UI

1. On the Web Dashboard (`http://localhost:5055`), click on the **DLQ Management** tab.
2. Select the failed transaction item.
3. Click **Edit Payload & Retry**.
4. In the JSON editor modal, correct `"codigo_producto": "SKU-NONEXISTENT-999"` to a valid SKU code (e.g. `"codigo_producto": "MP-RESINA-01"`).
5. Click **Submit & Execute**.
6. Verify the transaction status transitions to `COMPLETED` and the assigned CONTPAQi `contpaqi_folio` is displayed.

---

## Scenario 5: Direct SQL Catalog Read Verification

Execute a direct SQL Server catalog query to verify high-speed read latency (< 50 ms):

```bash
curl -X GET "http://localhost:5055/api/v1/catalogs/products?search=RESINA&limit=10"
```

**Expected Response**:
```json
{
  "total": 1,
  "products": [
    {
      "id_producto": 101,
      "codigo_producto": "MP-RESINA-01",
      "nombre_producto": "Resina Polietileno HDPE",
      "tipo_producto": 1,
      "control_existencia": 2
    }
  ],
  "query_time_ms": 12
}
```
