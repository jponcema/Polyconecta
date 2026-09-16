# Feature Specification: SPEC-000: Foundational Baseline (Clean Architecture, Odoo-Native Domain Model & Odoo 19 SPA)

**Feature Branch**: `000-foundational`  
**Created**: 2026-09-15  
**Status**: Validated & Implemented Baseline  
**Input**: Compactación de especificaciones de fundación (Clean Architecture, Modelo de Datos Odoo-Native y Suite UI/UX Odoo 19 SPA) para establecer el punto de partida del nuevo Roadmap basado en `INFORME_VALIDACION_DIAGRAMA_OPERATIVO.md`.

---

## Executive Summary & Architectural Vision

This specification defines the **Foundational Technical Baseline** for **PolyConecta**, uniting into a single, cohesive foundation:
1. **Clean Architecture C# Solution (`backend/`):** 5-layer decoupled architecture (`PolyConecta.Domain`, `PolyConecta.Infrastructure`, `PolyConecta.Api`, `PolyConecta.Presentation`, `PolyConecta.Contpaq`).
2. **Odoo-Native Domain Data Model (`PolyConecta.Domain`):** 10 canonical base entities (`Product`, `StockLot`, `ManufacturingOrder`, `Bom`, `BomLine`, `StockLocation`, `StockPicking`, `StockMove`, `QualityCheck`, `StockScrap`) fully aligned with Odoo 19 Enterprise ORM and CONTPAQi `adm*` tables.
3. **Odoo 19 SPA Framework (`PolyConecta.Presentation`):** Complete frontend SPA suite featuring dynamic Kanban, List, and Form views, status pipeline headers, Smart Buttons, App Launcher grid, and Mobile Handheld Terminal interface.

---

## User Scenarios & Testing

### User Story 1 - Multi-Layer Clean Architecture & Solution Foundation (Priority: P1)

As a Developer and System Administrator, I need the solution to enforce strict layer independence with unidirectional dependencies, exposing RESTful endpoints via ASP.NET Core API and asynchronous CONTPAQi Win32 integration through a transactional Outbox pattern.

### User Story 2 - Unified Odoo-Native Data Model (Priority: P1)

As a Shop-Floor Operator and Production Planner, I need products, extruded rolls, manufacturing orders, locations, movements, and quality checks to be governed by unified Odoo-Native domain models extending physical extrusion properties and mapping to CONTPAQi `admProductos`, `admCapasProducto`, `admAlmacenes`, and `admDocumentos`.

### User Story 3 - Odoo 19 SPA User Experience & Navigation (Priority: P1)

As a User across Customer Service, Planning, Quality, and Logistics, I need the interface to emulate Odoo 19 Enterprise with responsive Kanban status pipelines, interactive Smart Buttons, Form views, and Mobile Handheld scanner interfaces for zero-latency plant operations.

---

## Functional Requirements

- **FR-001**: Solution MUST structure C# projects under clean 5-layer boundaries with EF Core 8 and PostgreSQL integration.
- **FR-002**: Domain layer MUST expose the 10 Odoo-Native base entities (`Product`, `StockLot`, `ManufacturingOrder`, `Bom`, `BomLine`, `StockLocation`, `StockPicking`, `StockMove`, `QualityCheck`, `StockScrap`).
- **FR-003**: Presentation layer MUST render Odoo 19 Enterprise SPA UI components with App Launcher, Kanban pipelines, Form views, Smart Buttons, and Handheld scanner layout.

---

## Key Entities & Data Model

```mermaid
erDiagram
    Product ||--o{ StockLot : "instantiates lot / roll"
    Product ||--o{ BomLine : "used as component"
    
    ManufacturingOrder ||--o{ ManufacturingOrder : "parent_id (OM -> OF)"
    ManufacturingOrder ||--o| Bom : "uses recipe"
    ManufacturingOrder ||--o{ StockLot : "produces lots"
    ManufacturingOrder ||--o{ StockScrap : "generates scrap"
    
    Bom ||--|{ BomLine : "contains lines"
    BomLine }|--|| Product : "consumes resin/additive"
    
    StockLot ||--o{ QualityCheck : "inspected by"
    StockLot ||--o{ StockMove : "moved by"
    
    StockPicking ||--|{ StockMove : "groups moves"
    StockLocation ||--o{ StockMove : "source/destination"

    Product {
        uuid id PK
        string sku UK
        string name
        string category "RawMaterial | FinishedGood | Scrap"
        string uom "KG | MT | PZA"
        int contpaq_product_id FK
    }

    StockLot {
        uuid id PK
        uuid product_id FK
        string name UK "Folio / Lote (IV214-26-R001)"
        string contpaq_lot_number "cNumeroLote CONTPAQi"
        decimal gross_weight_kg
        decimal tare_weight_kg
        decimal net_weight_kg
        decimal length_meters
        decimal gauge_micron
        decimal width_mm
        decimal dynes_cm
        string shift
        string operator_id
        string status "Available | Quarantine | Consumed | Scrap"
        uuid current_location_id FK
    }

    ManufacturingOrder {
        uuid id PK
        uuid parent_id FK "Reference to Parent OM"
        string name UK "Folio OM / OF"
        string process_type "Master | Extrusion | Printing | Bagging"
        int contpaq_document_id FK "Pedido CONTPAQi"
        uuid product_id FK
        decimal product_qty_target
        decimal product_qty_produced
        decimal scrap_qty
        string state "Draft | Approved | Progress | To_Close | Done | Cancel"
        boolean sales_approved
        boolean credit_approved
        string workcenter_id
    }

    Bom {
        uuid id PK
        uuid product_id FK
        string code
        decimal layer_a_pct
        decimal layer_b_pct
        decimal layer_c_pct
    }

    BomLine {
        uuid id PK
        uuid bom_id FK
        uuid product_id FK "Insumo Resina/Aditivo"
        string layer "A | B | C"
        decimal component_pct
    }

    StockLocation {
        uuid id PK
        string name UK "PIM/Stock/MP"
        string plant_code "PIM | STC | MTM"
        string usage "Internal | Production | Transit | Inventory | Customer"
        int contpaq_warehouse_id FK
    }

    StockPicking {
        uuid id PK
        string name UK "PIM-TR-OUT-001"
        string picking_type "MO | Internal_Transfer | Customer_Shipment"
        uuid location_id FK "Source"
        uuid location_dest_id FK "Destination"
        string state "Draft | Waiting | Ready | Done"
    }

    StockMove {
        uuid id PK
        uuid picking_id FK
        uuid manufacturing_order_id FK
        uuid product_id FK
        uuid lot_id FK "StockLot / Roll"
        decimal product_uom_qty
        uuid location_id FK
        uuid location_dest_id FK
        string state "Draft | Reserved | Done"
    }

    QualityCheck {
        uuid id PK
        uuid lot_id FK
        uuid manufacturing_order_id FK
        decimal gauge_measured
        decimal dynes_measured
        string quality_state "None | Pass | Fail"
        string inspector_id
    }

    StockScrap {
        uuid id PK
        uuid manufacturing_order_id FK
        uuid product_id FK
        decimal scrap_qty
        uuid location_id FK
        string scrap_reason
    }
```

---

## Success Criteria

- **SC-001**: 100% clean compilation (`dotnet build`) across all 5 C# project layers with 0 errors.
- **SC-002**: 100% of domain entities align with Odoo-Native ORM naming standards.
- **SC-003**: Frontend SPA serves Odoo 19 navigation, App Launcher, Kanban pipelines, and Handheld interface on port 9000.
