# Canonical Data Model Specification: SPEC-002 Core Domain & Data Model

**Feature Branch**: `002-core-domain-data-model`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Research**: [research.md](./research.md)

---

## 1. Domain ER Diagram

```mermaid
erDiagram
    PolyLocation ||--o{ RolloMaestro : "holds stock of"
    CONTPAQ_admAlmacenes ||--|| PolyLocation : "maps 1:1 via cIdAlmacen"
    CONTPAQ_admDocumentos ||--o{ MasterOrder : "references Sales Pedido"
    MasterOrder ||--|{ SubOrder : "decomposes into"
    SubOrder ||--o{ RolloMaestro : "produces"
    RolloMaestro ||--o{ LotGenealogy : "parent/child lineage"
    SubOrder ||--o{ MassBalanceAudit : "evaluated by"

    PolyLocation {
        uuid id PK
        int cid_almacen_contpaq FK "admAlmacenes.CIDALMACEN"
        string location_code UK "PIM/Stock/MP"
        string location_name
        string plant_code "PIM|STC|MTM"
        string warehouse_type "RawMaterial|Production|FinishedGoods|Quarantine|Transit"
    }

    MasterOrder {
        uuid id PK
        string folio_om UK "OM-2026-0421"
        int cid_documento_pedido_contpaq FK "admDocumentos.CIDDOCUMENTO"
        string customer_code
        string pt_sku
        float target_quantity_kg
        string status "Draft|Approved|In_Progress|Completed|Cancelled"
        datetime created_at
    }

    SubOrder {
        uuid id PK
        uuid master_order_id FK
        string folio_of UK "OF-EXT-2026-0421-1"
        string process_type "EXT|IMP|BOL"
        string machine_id
        string status "Borrador|Programado|En_Proceso|Control_Calidad|Finalizado"
        float planned_qty_kg
        float produced_qty_kg
        float scrap_qty_kg
    }

    RolloMaestro {
        uuid id PK
        uuid sub_order_id FK
        string folio UK "EX-01-260910-042747"
        string lot_number "admCapasProducto.cNumeroLote"
        string product_sku
        float gross_weight_kg
        float tare_weight_kg
        float net_weight_kg
        float length_meters
        float gauge_micron
        float width_mm
        float dynes_cm
        string machine_id
        string shift
        string operator_id
        string status "Available|In_Use|Consumed|Quarantine|Scrap"
        uuid location_id FK
        datetime created_at
    }

    LotGenealogy {
        uuid id PK
        string parent_lot_number
        string child_lot_number
        uuid source_roll_id FK
        uuid target_roll_id FK
        float quantity_consumed_kg
        datetime created_at
    }

    MassBalanceAudit {
        uuid id PK
        uuid sub_order_id FK
        float total_mp_input_kg
        float total_roll_output_kg
        float total_scrap_output_kg
        float variance_percentage
        boolean audit_passed
        string flag_reason
        datetime audited_at
    }
```

---

## 2. Table Schemas & Constraints

### 2.1 `poly_locations`

Stores internal 2-step location paths mapped 1:1 to CONTPAQi `admAlmacenes` IDs.

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Unique location identifier |
| `cid_almacen_contpaq` | `INT` | `NOT NULL, UNIQUE` | CONTPAQi `admAlmacenes.CIDALMACEN` |
| `location_code` | `VARCHAR(50)` | `NOT NULL, UNIQUE` | 2-step path (e.g. `PIM/Stock/MP`) |
| `location_name` | `VARCHAR(100)`| `NOT NULL` | Descriptive name |
| `plant_code` | `VARCHAR(10)` | `NOT NULL` | Plant identifier (`PIM`, `STC`, `MTM`) |
| `warehouse_type` | `VARCHAR(30)` | `NOT NULL` | `RawMaterial`, `Production`, `FinishedGoods`, `Quarantine`, `Transit` |
| `is_active` | `BOOLEAN` | `DEFAULT TRUE` | Location active status |

---

### 2.2 `poly_master_orders`

Stores parent Master Orders (OM) linked to CONTPAQi Sales Orders (`admDocumentos`).

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Master Order ID |
| `folio_om` | `VARCHAR(30)` | `NOT NULL, UNIQUE` | Master Order Folio (e.g. `OM-2026-0421`) |
| `cid_documento_pedido`| `INT` | `NOT NULL` | CONTPAQi Sales Order `CIDDOCUMENTO` |
| `customer_code` | `VARCHAR(30)` | `NOT NULL` | Customer code (`admClientes.CCODIGOCLIENTES`) |
| `pt_sku` | `VARCHAR(30)` | `NOT NULL` | Finished Product SKU |
| `target_quantity_kg` | `NUMERIC(12,3)`| `NOT NULL, > 0` | Total scheduled output weight |
| `status` | `VARCHAR(20)` | `NOT NULL` | `Draft`, `Approved`, `In_Progress`, `Completed`, `Cancelled` |
| `created_at` | `TIMESTAMPTZ` | `NOT NULL` | Creation timestamp |

---

### 2.3 `poly_sub_orders`

Stores process execution sub-orders (`OF-EXT`, `OF-IMP`, `OF-BOL`).

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Sub-Order ID |
| `master_order_id` | `UUID` | `FOREIGN KEY` | Parent Master Order ID |
| `folio_of` | `VARCHAR(35)` | `NOT NULL, UNIQUE` | Sub-Order Folio (e.g. `OF-EXT-2026-0421-1`) |
| `process_type` | `VARCHAR(10)` | `NOT NULL` | `EXT` (Extrusión), `IMP` (Impresión), `BOL` (Bolseo) |
| `machine_id` | `VARCHAR(20)` | `NOT NULL` | Work Center / Extrusion Line ID |
| `status` | `VARCHAR(20)` | `NOT NULL` | `Borrador`, `Programado`, `En_Proceso`, `Control_Calidad`, `Finalizado`, `Scrap` |
| `planned_qty_kg` | `NUMERIC(12,3)`| `NOT NULL, > 0` | Planned batch weight |
| `produced_qty_kg` | `NUMERIC(12,3)`| `DEFAULT 0.0` | Accumulated roll output weight |
| `scrap_qty_kg` | `NUMERIC(12,3)`| `DEFAULT 0.0` | Accumulated scrap weight |
| `created_at` | `TIMESTAMPTZ` | `NOT NULL` | Creation timestamp |

---

### 2.4 `poly_master_rolls`

Stores physical master roll inventory and technical parameters.

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Rollo Maestro ID |
| `sub_order_id` | `UUID` | `FOREIGN KEY` | Originating Sub-Order ID |
| `folio` | `VARCHAR(30)` | `NOT NULL, UNIQUE` | Immutable Folio (`EX-01-260910-042747`) |
| `lot_number` | `VARCHAR(30)` | `NOT NULL` | CONTPAQi Lot (`admCapasProducto.cNumeroLote`) |
| `product_sku` | `VARCHAR(30)` | `NOT NULL` | Master SKU |
| `gross_weight_kg` | `NUMERIC(10,3)`| `NOT NULL, > 0` | Measured gross weight |
| `tare_weight_kg` | `NUMERIC(10,3)`| `NOT NULL, >= 0` | Measured core/pallet tare |
| `net_weight_kg` | `NUMERIC(10,3)`| `GENERATED` | Gross minus Tare |
| `length_meters` | `NUMERIC(10,2)`| `NOT NULL, > 0` | Total meterage |
| `gauge_micron` | `NUMERIC(8,2)` | `NOT NULL, > 0` | Calibre in micras |
| `width_mm` | `NUMERIC(8,2)` | `NOT NULL, > 0` | Width in mm |
| `dynes_cm` | `NUMERIC(5,1)` | `DEFAULT 38.0` | Corona treatment energy |
| `machine_id` | `VARCHAR(20)` | `NOT NULL` | Line ID |
| `shift` | `VARCHAR(10)` | `NOT NULL` | Work shift |
| `operator_id` | `VARCHAR(50)` | `NOT NULL` | Operator user ID |
| `status` | `VARCHAR(20)` | `NOT NULL` | `Available`, `In_Use`, `Consumed`, `Quarantine`, `Scrap` |
| `location_id` | `UUID` | `FOREIGN KEY` | Current location ID (`poly_locations.id`) |
| `created_at` | `TIMESTAMPTZ` | `NOT NULL` | Timestamp |

---

### 2.5 `poly_lot_genealogy`

Tracks DAG transformational relationships between lots.

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Lineage record ID |
| `parent_lot_number` | `VARCHAR(30)` | `NOT NULL` | Parent lot number (e.g. Resin Lot) |
| `child_lot_number` | `VARCHAR(30)` | `NOT NULL` | Child lot number (e.g. Master Roll Lot) |
| `source_roll_id` | `UUID` | `NULLABLE FK` | Source roll ID |
| `target_roll_id` | `UUID` | `NULLABLE FK` | Target roll ID |
| `quantity_consumed_kg`| `NUMERIC(10,3)`| `NOT NULL, > 0` | Weight consumed in transition |
| `created_at` | `TIMESTAMPTZ` | `NOT NULL` | Timestamp |

---

### 2.6 `poly_mass_balance_audits`

Tracks shop-floor mass balance calculations and tolerance audits.

| Field Name | Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `id` | `UUID` | `PRIMARY KEY` | Audit record ID |
| `sub_order_id` | `UUID` | `FOREIGN KEY` | Sub-order audited |
| `total_mp_input_kg` | `NUMERIC(12,3)`| `NOT NULL` | Total raw material input |
| `total_roll_output_kg`| `NUMERIC(12,3)`| `NOT NULL` | Total roll output |
| `total_scrap_output_kg`| `NUMERIC(12,3)`| `NOT NULL` | Total scrap output |
| `variance_percentage` | `NUMERIC(6,3)` | `NOT NULL` | Calculated variance % |
| `audit_passed` | `BOOLEAN` | `NOT NULL` | `True` if variance <= 2.0% |
| `flag_reason` | `TEXT` | `NULLABLE` | Explanation if failed |
| `audited_at` | `TIMESTAMPTZ` | `NOT NULL` | Audit timestamp |

---

## 3. State Transition Rules

### Sub-Order Pipeline (`poly_sub_orders.status`)

```
[Borrador] --(Schedule & Assign)--> [Programado] --(Start Scan)--> [En_Proceso]
[En_Proceso] --(Finish Batch & Weigh)--> [Control_Calidad]
[Control_Calidad] --(Quality Approval & Pass Audit)--> [Finalizado]
[Control_Calidad] --(Quality Reject)--> [Scrap]
```

### Rollo Maestro Lifecycle (`poly_master_rolls.status`)

```
[Available] --(Scan to Print/Bagging)--> [In_Use] --(Fully Consumed)--> [Consumed]
[Available] --(Quality Audit Hold)--> [Quarantine]
[Quarantine] --(Quality Release)--> [Available]
[Quarantine] --(Quality Reject)--> [Scrap]
```
