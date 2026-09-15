# Quickstart & Verification Guide: SPEC-002 Core Domain & Data Model

**Feature Branch**: `002-core-domain-data-model`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Data Model**: [data-model.md](./data-model.md) | **Contracts**: [contracts/](./contracts/)

---

## Overview

This guide describes end-to-end validation scenarios for verifying the canonical domain entities, warehouse location mappings, master roll lifecycle, order hierarchy pipelines, and mass balance audit rules defined in **SPEC-002**.

---

## Scenario 1: Verify Warehouse Location Mappings (`admAlmacenes`)

**Objective**: Ensure that 2-step location paths in PolyConecta correctly link to CONTPAQi `admAlmacenes` records.

### Verification Steps
1. Query active locations via API contract: `GET /api/v1/locations` (see [contracts/warehouse-routing-api.json](./contracts/warehouse-routing-api.json)).
2. Verify that response includes standard plant location mappings:
   - `PIM/Stock/MP` → `ALM_PIM_MP`
   - `PIM/Produccion` → `ALM_PIM_PROD`
   - `PIM/Stock/PT` → `ALM_PIM_PT`
   - `PIM/Cuarentena` → `ALM_PIM_CUARENTENA`
3. Validate that no unmapped 2-step location can be created without an explicit CONTPAQi `CIDALMACEN` foreign key.

---

## Scenario 2: Master Order (OM) & Sub-Order (OF) Creation

**Objective**: Verify 3-tier order hierarchy creation from a CONTPAQi Sales Order (`Pedido`).

### Verification Steps
1. Create a Master Order linked to CONTPAQi Sales Order `CIDDOCUMENTO = 421`:
   - Send `POST /api/v1/orders/master` with payload target `5000.0 kg` of PT SKU `PT-BAG-001`.
2. Confirm auto-generation of process sub-orders:
   - `OF-EXT-2026-0421-1` (Extrusión)
   - `OF-IMP-2026-0421-1` (Impresión)
   - `OF-BOL-2026-0421-1` (Bolseo)
3. Check Kanban initial state: All sub-orders MUST initialize in state `Borrador`.

---

## Scenario 3: Rollo Maestro Generation & Weight Capture

**Objective**: Validate immutable Folio generation, net weight calculation, and lot linkage for extruded rolls.

### Verification Steps
1. Execute handheld extrusion roll capture for `OF-EXT-2026-0421-1`:
   - Line: `01`
   - Gross Weight: `155.400 kg`
   - Tare Weight: `5.400 kg`
   - Measured Gauge: `35.0 micras`
   - Dynes: `38.0`
2. Assert generated `RolloMaestro` entity attributes (see [data-model.md](./data-model.md)):
   - Folio matches pattern `EX-01-{YYMMDD}-{HHMMSS}`.
   - Calculated Net Weight is exactly `150.000 kg` ($\text{Gross} - \text{Tare}$).
   - `lot_number` matches `folio` for CONTPAQi `admCapasProducto.cNumeroLote` alignment.
   - Status is `Available` and Location is `PIM/Produccion`.

---

## Scenario 4: Quality Gate Hard-Stop & 2-Step Location Routing

**Objective**: Confirm that rolls held in Quarantine (`ALM_PIM_CUARENTENA`) cannot be transferred or dispatched.

### Verification Steps
1. Set roll status to `Quarantine` and update location to `PIM/Cuarentena`.
2. Attempt material transfer to finished goods `PIM/Stock/PT` via `POST /api/v1/transfers/move`.
3. **Expected Result**: HTTP `422 Unprocessable Entity` with error message `"Quality Gate Hard-Stop: Material in Quarantine cannot be moved to Finished Goods stock."`
4. Execute digital Quality Release signoff.
5. Re-attempt transfer: HTTP `200 OK` with Outbox message queued for CONTPAQi stock transfer update.

---

## Scenario 5: Mass-Balance Audit Verification

**Objective**: Validate mass balance calculation and tolerance alert flagging.

### Verification Steps
1. Complete Sub-Order run with:
   - Raw Material Input: `1000.000 kg`
   - Net Roll Output: `950.000 kg`
   - Scrap Output: `20.000 kg`
   - Unaccounted Loss: `30.000 kg` (3.0% variance)
2. Trigger Sub-Order status transition to `Finalizado`.
3. **Expected Result**: Transition blocked; `poly_mass_balance_audits` records `variance_percentage = 3.000%`, `audit_passed = false`, status flagged as `Mass_Balance_Hold` requiring Supervisor digital approval.
