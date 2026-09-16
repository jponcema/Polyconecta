# Tasks: SPEC-001 Interactive End-to-End Operational Flow PoC SPA

**Feature Branch**: `001-poc-end-to-end-operational-flow`  
**Date**: 2026-09-15  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

---

## Phase 1: Setup & PoC Navigation Suite Framework

- [x] T001 Create unified interactive PoC Navigation layout and step navigation bar in `PolyConecta.Presentation/Pages/PocDemoSuite.razor`
- [x] T002 Configure route `/demo/poc` and sub-routes for the 7 operational phases in `PolyConecta.Presentation/App.razor`

---

## Phase 2: User Story 1 - Phase 1 & 2: Sales Order & 2-Signature Approvals UI

- [x] T003 Implement Sales Order Form component with structured ERP User Fields for Extrusion & Converting in `PolyConecta.Presentation/Components/Poc/PocSalesOrderForm.razor`
- [x] T004 Implement Odoo 19 Header Status Pipeline (`Borrador` → `Confirmado` → `Autorizado`) with interactive `Confirmar` (AC), `Validar Ventas` and `Validar Crédito` buttons in `PolyConecta.Presentation/Components/Poc/PocOrderHeaderPipeline.razor`

---

## Phase 3: User Story 2 - Phase 3: MRP Hierarchy & Dynamic BoM UI

- [x] T005 Implement MRP Hierarchy component displaying Master Order (`OM`) and child sub-orders (`OF-EXT`, `OF-IMP`, `OF-BOL`) with work orders (`WO`) in `'Por programar'` state in `PolyConecta.Presentation/Components/Poc/PocMrpHierarchy.razor`
- [x] T006 Implement 3-Hopper Co-Extrusion BoM Recipe uploader (Tolvas A/B/C %) and extruder machine/date scheduler in `PolyConecta.Presentation/Components/Poc/PocBomUploader.razor`

---

## Phase 4: User Story 3 - Phase 4: Mobile Handheld Weighing, Slots & Quarantine `.S` Recycling UI

- [x] T007 Implement Mobile Handheld Scale Terminal with pre-loaded slots (`Rollo 1`... `Rollo N`) in `PolyConecta.Presentation/Components/Poc/PocHandheldScaleTerminal.razor`
- [x] T008 Implement `QualityCheck` modal with automatic lot assignment (`IV214-26-R001`), rejection simulation to `IV214-26-R001.S` in `PIM/Stock/Cuarentena`, and slot recycling logic in `PolyConecta.Presentation/Components/Poc/PocQualityCheckModal.razor`

---

## Phase 5: User Story 4 - Phase 5 & 6: Mass Balance Closure & 2-Step Logistics UI

- [x] T009 Implement Mass Balance Closure Console calculating Total Extruded Mass ($\sum \text{Rollos} + \text{Scrap}$) with consolidated CONTPAQi stock deduction trigger in `PolyConecta.Presentation/Components/Poc/PocMassBalanceConsole.razor`
- [x] T010 Implement Odoo Smart Buttons (`Entregas / Envíos` vs `Operaciones de Traslado`) with 2-step inter-plant transfer simulator (`PIM-TR-OUT` $\rightarrow$ `TRANSIT/PIM-STC` $\rightarrow$ `STC-TR-IN`) in `PolyConecta.Presentation/Components/Poc/PocLogisticsRouting.razor`

---

## Phase 6: User Story 5 - Phase 7: Santa Cruz Bagging Conversion & Dual Metric Audit UI

- [x] T011 Implement Santa Cruz Bagging Conversion view (`OF-BOL`) enforcing 4 Universal Manufacturing Rules, Dual Metric Capture (Thousands + Kg), Real Conversion Factor ($\text{Kg/Millar real}$), suaje scrap, and order completion to `Hecho` in `PolyConecta.Presentation/Components/Poc/PocBaggingConversion.razor`

---

## Phase 7: Verification & Build Validation

- [x] T012 Run `dotnet build` across solution and verify clean compilation
- [x] T013 Verify interactive PoC SPA functionality under `/demo/poc`
