# Research & Design Decisions: SPEC-003 Odoo 19 UI/UX Framework

**Feature Branch**: `003-odoo19-ui-ux-framework`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md)

---

## 1. UI Component Architecture & Layout Grid

### Decision
Implement the Odoo 19 Enterprise UI framework using Blazor / Razor Web Components styled with a dedicated Odoo 19 Tailwind CSS theme, based on [mockup-001.png](../../../docs/mockups/mockup-001.png).

### Component Layout Hierarchy

```text
<OdooAppShell>
  ├── <OdooTopbar> (Logo, Breadcrumbs, SpotlightSearch, UserAvatar)
  ├── <OdooSidebar> (Collapsible [<-], Navigation Modules: Inicio, Ventas, Manufactura, Calidad, Config)
  └── <OdooWorkspace>
        ├── <OdooViewHeader> (Title, SearchInput, ViewSwitcher: [kanban] [list])
        └── <OdooViewBody>
              ├── <OdooListView> (Paginator, Sortable Data Table)
              ├── <OdooKanbanView> (Pre-configured Pipeline Stage Columns)
              └── <OdooFormView>
                    ├── <OdooActionBar> (Confirmar, Validar, Subir PDF)
                    ├── <OdooSmartButtonsContainer> (Entrega [1], Manufacturas [3])
                    ├── <OdooStatusPipeline> (Arrow Status Bar)
                    ├── <OdooFormSheet> (Title, Header Fields, Notebook Tabs: Detalle/Observaciones)
                    └── <OdooChatterDrawer> (Side Drawer: Audit Trail, CONTPAQi Sync Logs, User Chat)
```

### Rationale
- Blazor Web Components enable 100% C# type safety and seamless integration with PolyConecta's .NET 8 backend engine.
- Reusable modular UI components guarantee visual consistency across all modules (Ventas, Manufactura, Calidad, Almacenes).

---

## 2. View Switcher Engine & Pre-Configured Kanban Stages

### Decision
Implement a unified `UiViewSwitcher` state component handling seamless switching between List View (`[list]`) and Kanban View (`[kanban]`), preserving search filters and URL query parameters.

### Pre-Configured Kanban Pipelines by Document Type

| Document Type | Pre-configured Kanban Stage Pipeline Columns |
| :--- | :--- |
| **Pedidos (CONTPAQi)** | `Pedido Sincronizado` → `Confirmado` → `En manufactura` → `Hecho` |
| **Órdenes de Manufactura (OF)** | `Borrador` → `Autorizado` → `En progreso` → `Hecho` |
| **Control de Calidad** | `Inspección Pendiente` → `En Revisión` → `Liberado` → `Rechazado` |

### Rationale
- Odoo 19 Kanban views rely on explicit, pre-defined operational pipeline stages. Pre-configuring column structures per document type ensures consistent shop-floor visibility.

---

## 3. Smart Buttons & Status Pipeline Header

### Decision
Create a `<SmartButton>` component container positioned at the top-right of the form header, featuring real-time count badges and direct route navigation.

### Visual Specification

```text
+-------------------------------------------------------------------------+
| [Confirmar] [Validar]                    [ Entrega 1 ] [ Manufacturas 3 ]|
|                                         +-------------------------------+
|                                         | Pedido Sincronizado > Conf >  |
|                                         | En manufactura > Hecho        |
+-------------------------------------------------------------------------+
```

### Rationale
- Smart Buttons provide instant 1-click cross-entity traceability (e.g. Sales Order → Manufacturing Orders → Delivery Notes) without cluttering the main form sheet.

---

## 4. Native Chatter Panel & SignalR Integration

### Decision
Embed `<OdooChatterDrawer>` on the right side of all Form Views. Use SignalR for real-time audit log streaming, CONTPAQi SDK sync notifications, and user comments.

### Rationale
- A dedicated right-side Chatter panel preserves form readability while giving operators and supervisors a live audit trail and communication channel.
