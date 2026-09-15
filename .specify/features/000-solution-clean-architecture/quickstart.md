# Quickstart Validation Guide: 000 Solution Clean Architecture

**Feature Branch**: `000-solution-clean-architecture`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

---

## 1. Prerequisites

- **.NET 8 SDK** installed (`dotnet --version` >= 8.0.100).
- Modern web browser (Chrome, Edge, Safari, Firefox).
- Solution source code located in `/Users/emilio/Development/Sandbox/Polyconecta/`.

---

## 2. Validation Scenarios

### Scenario 1: Automated Solution Build, Test & Launch Script
* **Goal**: Validate that all Clean Architecture layers compile without errors and run tests automatically.
* **Commands**:
  ```bash
  cd /Users/emilio/Development/Sandbox/Polyconecta
  ./run.sh 5060
  ```
* **Expected Outcome**:
  * Solution builds (`PolyConecta.slnx`) with **0 Errors**.
  * All domain unit tests (`6/6`) and integration tests (`2/2`) pass **100%**.
  * Server launches and displays the runner banner.

---

### Scenario 2: Live Prototype Web Server & Odoo 19 UI Launch
* **Goal**: Start the local backend API server hosting the Odoo 19 Enterprise Web SPA directly from the root project.
* **Command**:
  ```bash
  dotnet run --project PolyConecta.Api/PolyConecta.Api.csproj --urls http://localhost:5060
  ```
* **Expected Outcome**:
  * Server logs: `Now listening on: http://localhost:5060`.
  * Open browser at `http://localhost:5060` to view the Odoo 19 App Launcher.

---

### Scenario 3: Operating the Odoo 19 Enterprise App Launcher
* **Goal**: Test navigation between all 6 core applications.
* **Steps**:
  1. Visit `http://localhost:5060`.
  2. Click on **Manufactura (MRP)** $\rightarrow$ Verify Kanban View loads with OF status columns (`Borrador`, `Programado`, `En Proceso`, `Calidad`, `Finalizado`).
  3. Click on the App Switcher icon (<i class="bi bi-grid-3x3-gap-fill"></i>) $\rightarrow$ Select **Almacenes (WMS)**.
  4. Switch view mode to **Lista (☰)** $\rightarrow$ Verify locations (`PIM/Stock/MP`, `PIM/Produccion`, `PIM/Stock/PT`, `PIM/Cuarentena`) are displayed.
  5. Select **Handheld Báscula** app $\rightarrow$ Simulate roll weighing, capture weight, and generate GS1-128 QR code label.
  6. Select **Compras (P2P)** $\rightarrow$ Check Raw Material master catalog items (`MP-RES-HD-001`) and supplier mappings.

---

### Scenario 4: Quality Gate Hard-Stop Enforcement
* **Goal**: Verify that stock transfers of quarantined material fail with a Quality Gate Hard-Stop alert.
* **Steps**:
  1. In the **Almacenes (WMS)** app, switch to **Formulario (📄)**.
  2. Select Source Location: `PIM/Cuarentena (Quality Gate Quarantine)`.
  3. Select Target Location: `PIM/Stock/PT (Apodaca Stock PT)`.
  4. Click **Simular Traspaso de Stock**.
* **Expected Outcome**:
  * Red alert box displays: `BLOQUEO HARD-STOP ACTIVADO: Quality Gate Hard-Stop: Material in Quarantine cannot be moved to Finished Goods stock without a verified Digital Quality Release.`
