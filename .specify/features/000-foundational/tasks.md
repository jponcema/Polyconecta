# Tasks: SPEC-000 Foundational Baseline

**Feature Branch**: `000-foundational`  
**Date**: 2026-09-15  
**Spec**: [spec.md](./spec.md)

---

## Foundational Tasks

- [x] T001 Setup Clean Architecture C# 5-layer solution (`PolyConecta.Domain`, `PolyConecta.Infrastructure`, `PolyConecta.Api`, `PolyConecta.Presentation`, `PolyConecta.Contpaq`)
- [x] T002 Implement 10 Odoo-Native Domain Entities (`Product`, `StockLot`, `ManufacturingOrder`, `Bom`, `BomLine`, `StockLocation`, `StockPicking`, `StockMove`, `QualityCheck`, `StockScrap`)
- [x] T003 Implement EF Core `PolyDbContext` and Repository Contracts (`IOrderRepository`, `IMaterialRepository`)
- [x] T004 Implement Odoo 19 Enterprise SPA Presentation UI (App Launcher, Kanban Status Pipelines, Form Views, Smart Buttons, Mobile Handheld UI)
- [x] T005 Verify `dotnet build` and `dotnet test` execution (100% Pass)
