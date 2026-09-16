# Implementation Plan: SPEC-001: Interactive End-to-End Operational Flow PoC SPA

**Feature Branch**: `001-poc-end-to-end-operational-flow`  
**Date**: 2026-09-15  
**Spec**: [spec.md](./spec.md) | **Checklist**: [checklists/requirements.md](./checklists/requirements.md)

---

## Technical Stack & Architecture

- **Frontend Platform:** Blazor WebAssembly / SPA (`PolyConecta.Presentation`)
- **UI Design System:** Odoo 19 Enterprise (Kanban Pipelines, App Launcher, Header Action Buttons, Smart Buttons, Form & List Views, Mobile Handheld Terminal)
- **State Management:** Blazor In-Memory Component State & State Services (`UiViewState.cs`, `UiAppShellState.cs`)
- **Backend API:** ASP.NET Core 8 (`PolyConecta.Api`)
- **Core Domain Entities:** `Product`, `StockLot`, `ManufacturingOrder`, `Bom`, `StockLocation`, `StockPicking`, `QualityCheck`, `StockScrap`

---

## Component Breakdown

1. `PolyConecta.Presentation/Pages/PocDemoSuite.razor`: Unified PoC Demo Suite container with top navigation bar, active phase indicator, and step switcher.
2. `PolyConecta.Presentation/Components/Poc/PocSalesOrderForm.razor`: Phase 1 & 2 Sales Order Form with ERP User Fields & 2-Signature header pipeline (`Borrador` → `Confirmado` → `Autorizado`).
3. `PolyConecta.Presentation/Components/Poc/PocMrpHierarchy.razor`: Phase 3 MRP Hierarchy (`OM` $\rightarrow$ `OF` $\rightarrow$ `WO`) and 3-hopper co-extrusion recipe Excel uploader.
4. `PolyConecta.Presentation/Components/Poc/PocHandheldScaleTerminal.razor`: Phase 4 Scale terminal with pre-loaded slots (`Rollo 1`... `Rollo N`), `QualityCheck` modal, lotification `IV214-26-R001`, and Quarantine `.S` recycling rule (`IV214-26-R001.S` moves to quarantine, recycling `R001` slot).
5. `PolyConecta.Presentation/Components/Poc/PocMassBalanceConsole.razor`: Phase 5 Mass Balance Console ($\sum \text{Rollos} + \text{Scrap}$) and single consolidated CONTPAQi stock deduction trigger.
6. `PolyConecta.Presentation/Components/Poc/PocLogisticsRouting.razor`: Phase 6 Smart Buttons (`Entregas / Envíos` vs `Operaciones de Traslado`) with 2-step inter-plant transfer simulator (`PIM-TR-OUT` $\rightarrow$ `TRANSIT` $\rightarrow$ `STC-TR-IN`).
7. `PolyConecta.Presentation/Components/Poc/PocBaggingConversion.razor`: Phase 7 Santa Cruz Bagging Conversion view (`OF-BOL`) with Dual Metric Capture (Thousands + Kg), Real Conversion Factor ($\text{Kg/Millar real}$), suaje scrap, and order completion to `Hecho`.
