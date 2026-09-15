# Implementation Plan: SPEC-003 Odoo 19 UI/UX Design & Component Framework

**Branch**: `003-odoo19-ui-ux-framework` | **Date**: 2026-09-14 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `.specify/features/003-odoo19-ui-ux-framework/spec.md`

---

## Summary

This plan defines the technical implementation architecture for **SPEC-003: Odoo 19 Enterprise UI/UX Design & Component Framework**. Grounded in [mockup-001.png](../../../docs/mockups/mockup-001.png), it specifies reusable Blazor / Razor Web Components styled with an Odoo 19 Tailwind CSS theme. Key components include the global Application Shell (collapsible sidebar `[<-]`, Topbar with Spotlight search), View Switcher (`[kanban] [list]`) with pre-configured document status pipeline columns, Form Views with Smart Buttons and visual Arrow Status Header, Notebook Tabs (`[Detalle] [Observaciones]`), and a native right-side Chatter panel powered by SignalR for real-time audit logs and comments.

---

## Technical Context

**Language/Version**: C# / .NET 8 (Blazor Server / WebAssembly or Razor Components) + HTML5/CSS3  
**Primary Dependencies**: Tailwind CSS (Odoo 19 theme palette), SignalR Core (for live Chatter updates), Lucide / Heroicons  
**Storage**: In-Memory / PostgreSQL for UI state preferences, active view modes, chatter messages, and follower subscriptions  
**Testing**: bunit for Blazor component unit testing, xUnit, Playwright / Selenium for E2E UI testing  
**Target Platform**: Desktop Web Browsers (Chrome, Edge, Safari, Firefox), Tablet / Mobile Handheld Scanners (Android WebView)  
**Project Type**: Web Application Frontend UI Framework & Component Library  
**Performance Goals**: View mode switching between List and Kanban in <200ms; Smart Button count badge refresh in <500ms; Chatter SignalR message broadcast in <100ms  
**Constraints**: 100% visual fidelity to [mockup-001.png](../../../docs/mockups/mockup-001.png); responsive sidebar collapse; auto-stacking Chatter drawer on screens <768px  
**Scale/Scope**: 5 main UI module screens (Dashboard, Pedidos, Formulario Pedido, Manufactura, Formulario Manufactura); reusable component library  

---

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-checked after Phase 1 design.*

| Principle | Description | Compliance Status | Justification / Mechanism |
| :--- | :--- | :---: | :--- |
| **Principle IX** | UI/UX & MRP Operational Benchmark: Odoo 19 Enterprise | **PASS** | 100% compliant. Implements Odoo 19 visual patterns (Kanban/List views, Smart Buttons, Arrow Status Header, Chatter drawer, 2-step location routing). |
| **Principle I** | ERP Master of Record | **PASS** | PolyConecta provides the UI intelligence layer; CONTPAQi remains financial ERP master of record. |
| **Principle II** | Asynchronous ERP Synchronization | **PASS** | Form actions enqueue Outbox messages; CONTPAQi SDK sync logs stream live into Chatter. |
| **Principle III–VIII** | Domain, Governance & Technical Reality | **PASS** | Data bindings align with canonical entities defined in SPEC-002. |

---

## Project Structure

### Documentation (this feature)

```text
.specify/features/003-odoo19-ui-ux-framework/
├── spec.md              # Feature specification
├── plan.md              # Implementation plan (this file)
├── research.md          # Phase 0 design decisions & layout grid
├── data-model.md        # Phase 1 UI state schemas & component data models
├── quickstart.md        # Phase 1 runnable UI validation scenarios & guide
└── contracts/           # Phase 1 interface definitions & schemas
    ├── ui-component-contracts.json
    └── chatter-api.json
```

### Source Code (repository root)

```text
frontend/
├── src/
│   ├── PolyConecta.Ui/
│   │   ├── Components/
│   │   │   ├── Shell/         # OdooAppShell.razor, OdooTopbar.razor, OdooSidebar.razor
│   │   │   ├── Views/         # OdooViewSwitcher.razor, OdooListView.razor, OdooKanbanView.razor
│   │   │   ├── Forms/         # OdooFormSheet.razor, OdooSmartButtons.razor, OdooStatusPipeline.razor
│   │   │   └── Chatter/       # OdooChatterDrawer.razor, OdooChatterMessage.razor
│   │   ├── Pages/             # Dashboard.razor, PedidosList.razor, PedidoForm.razor, ManufacturasList.razor, ManufacturaForm.razor
│   │   └── wwwroot/
│   │       ├── css/           # odoo19-theme.css (Tailwind build output)
│   │       └── js/            # chatter-scroll.js, spotlight.js
└── tests/
    └── PolyConecta.Ui.Tests/  # bunit component tests
```

**Structure Decision**: Web Application UI Framework layout (`frontend/src/PolyConecta.Ui/`) with modular Razor components organized into Shell, Views, Forms, and Chatter subfolders.

---

## Complexity Tracking

> **No violations present. All Constitution gates passed.**
