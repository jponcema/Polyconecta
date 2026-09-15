# Implementation Plan: 000 Solution Root Clean Architecture Restructuring

**Branch**: `000-solution-clean-architecture` | **Date**: 2026-09-14 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `.specify/features/000-solution-clean-architecture/spec.md`

---

## Summary

Restructure the root PolyConecta project into explicit Clean Architecture layers (`Domain`, `Infrastructure`, `ContpaqBridge`, `Api`, `Presentation`), enforcing unidirectional dependency flow, isolating 32-bit Win32 CONTPAQi SDK interop via Outbox messaging, and delivering an Odoo 19 Enterprise SPA UI Suite (`wwwroot`).

---

## Technical Context

**Language/Version**: C# / .NET 8.0 (64-bit API/Domain, 32-bit x86 Win32 Integration Bridge)  
**Primary Dependencies**: ASP.NET Core, EF Core 8 (InMemory / SQLite / Npgsql), Swashbuckle OpenAPI, Bootstrap 5, Bootstrap Icons  
**Storage**: In-Memory DB (Dev/Test), SQLite `bridge_outbox.db` (Async Outbox Queue), CONTPAQi SQL Server `adm*` (Production ERP)  
**Testing**: xUnit (`PolyConecta.Domain.Tests` & `PolyConecta.IntegrationTests`)  
**Target Platform**: Windows Server / Windows Workstation (Sesión 2 interactiva para Win32 SDK), Cross-platform Linux/macOS para Web API & SPA  
**Project Type**: Multi-project C# Solution with Web REST API and Web SPA Frontend  
**Performance Goals**: <200ms response time on Handheld roll weighing, <1s application module switching  
**Constraints**: Zero circular dependencies between layers, zero framework attributes inside Domain entities, Outbox async processing  
**Scale/Scope**: 5 Solution Layers, 10 SDD Specifications, 6 Odoo 19 App Modules  

---

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- [x] **Principle I (ERP Master Repository & Odoo 19 UI Benchmark)**: CONTPAQi remains master repository of record. UI/UX strictly emulates Odoo 19 Enterprise. -> **PASSED**
- [x] **Principle II (Asynchronous Outbox Sync & Bridge Isolation)**: Win32 SDK calls strictly isolated in `PolyConecta.ContpaqBridge` 32-bit worker with SQLite Outbox queue. -> **PASSED**
- [x] **Principle III (Master Raw Material Catalog)**: Estandarización de materias primas en catálogo maestro `RawMaterialCatalog`. -> **PASSED**
- [x] **Principle IV (Quality Hard-Stop & Mass Balance)**: Bloqueo sistémico de traspasos de material en cuarentena en `PolyConecta.Infrastructure`. -> **PASSED**
- [x] **Principle VII (Reference Manual Backing)**: Decisiones fundamentadas en `Referencia_BD_CONTPAQi.md` y `Referencia_SDK_CONTPAQi.md`. -> **PASSED**
- [x] **Principle IX (Odoo 19 Enterprise UI/UX Standard)**: Grid de Apps, Kanban, Lista, Formulario con Smart Buttons y Statusbar Pipeline. -> **PASSED**

---

## Project Structure

### Documentation (this feature)

```text
.specify/features/000-solution-clean-architecture/
├── spec.md              # Feature specification
├── plan.md              # Implementation plan (this file)
├── research.md          # Architectural decisions & research
├── data-model.md        # Entities, value objects & solution tree mapping
├── quickstart.md        # Runnable validation scenarios
├── contracts/           # Solution & UI component schemas
│   ├── solution-architecture.json
│   └── odoo19-ui-spec.json
└── checklists/
    └── requirements.md  # Quality checklist
```

### Source Code (repository root)

```text
Polyconecta/
├── Polyconecta.slnx
├── backend/
│   ├── Directory.Build.props
│   ├── src/
│   │   ├── PolyConecta.Domain/           # Core Domain Layer
│   │   ├── PolyConecta.Infrastructure/   # Persistence & Outbox Infrastructure Layer
│   │   ├── PolyConecta.ContpaqBridge/    # Win32 x86 CONTPAQi SDK Worker
│   │   └── PolyConecta.Api/              # API Gateway & wwwroot Odoo 19 SPA Presentation
│   └── tests/
│       ├── PolyConecta.Domain.Tests/     # Domain Unit Tests
│       └── PolyConecta.IntegrationTests/ # Integration Tests
└── dist/
    └── win-x86/                          # Compiled Win32 Bridge Assets
```

---

## Complexity Tracking

> **No Constitution violations. No special complexity overrides required.**
