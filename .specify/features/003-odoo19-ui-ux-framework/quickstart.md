# Quickstart & Verification Guide: SPEC-003 Odoo 19 UI/UX Framework

**Feature Branch**: `003-odoo19-ui-ux-framework`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Data Model**: [data-model.md](./data-model.md) | **Contracts**: [contracts/](./contracts/)

---

## Overview

This guide describes validation scenarios for testing the Odoo 19 Enterprise UI/UX Design & Component Framework in PolyConecta, based on [mockup-001.png](../../../docs/mockups/mockup-001.png).

---

## Scenario 1: App Shell Navigation & Sidebar Collapse Toggle

**Objective**: Verify Application Shell layout, dynamic breadcrumbs, Spotlight Search, and sidebar collapse behavior.

### Verification Steps
1. Open PolyConecta home dashboard: `http://localhost:5000/`.
2. Verify Topbar elements: Logo, Breadcrumbs (`Inicio / Dashboard`), Spotlight search, User Avatar `A`.
3. Click the sidebar toggle button `[<-]` on the top-left of the main workspace.
4. **Expected Result**: Sidebar collapses smoothly into icon mode, expanding the main workspace width. Clicking `[<-]` again expands the sidebar back to full width.
5. Type `3192389` in the Spotlight search input. Click the matching dropdown item to open `Formulario Pedido 3192389`.

---

## Scenario 2: Dual View Switcher (Kanban vs List Views)

**Objective**: Validate view mode switching and pre-configured pipeline columns.

### Verification Steps
1. Navigate to Manufacturing Orders list: `/manufactura/orders`.
2. Click the `[kanban]` view button in the top header switcher.
3. **Expected Result**: Orders render as Kanban cards grouped under 4 pre-configured pipeline columns:
   - `Borrador`
   - `Autorizado`
   - `En progreso`
   - `Hecho`
4. Click the `[list]` view button.
5. **Expected Result**: Orders switch instantly (<200ms) to a tabular data grid view without losing search query filters.

---

## Scenario 3: Smart Buttons & Arrow Status Pipeline

**Objective**: Validate Form View Smart Button counter badges and visual arrow status header.

### Verification Steps
1. Open Sales Order `Formulario Pedido 3192389`.
2. Inspect top-right header area:
   - Verify Smart Buttons: `[ Entrega 1 ]`, `[ Manufacturas 3 ]`.
   - Verify Arrow Status Pipeline header: `Pedido Sincronizado` → `Confirmado` → `En manufactura` → `Hecho`.
3. Click `[ Manufacturas 3 ]` Smart Button.
4. **Expected Result**: Navigates directly to `/manufactura/orders?pedido=3192389` showing the 3 linked manufacturing sub-orders.

---

## Scenario 4: Side Chatter & Audit Log Panel

**Objective**: Validate native right-side Chatter drawer, automated CONTPAQi sync logs, and user comments.

### Verification Steps
1. Open Form View `Formulario Manufactura IV214-26`.
2. Observe right-side Chatter drawer:
   - Verify automated log: `"Se carga el pedido de venta polyconecta"`.
3. Type `"Inspección de extrusión aprobada"` in the Chatter text input.
4. Click `[Send]`.
5. **Expected Result**: Comment is appended immediately to the Chatter thread with timestamp and author name, broadcasting to linked observers via SignalR.
