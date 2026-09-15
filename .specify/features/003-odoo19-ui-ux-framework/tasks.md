# Tasks: SPEC-003 Odoo 19 UI/UX Design & Component Framework

**Feature Branch**: `003-odoo19-ui-ux-framework`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md) | **Data Model**: [data-model.md](./data-model.md)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Create Blazor / Razor Web Components project structure in `frontend/src/PolyConecta.Ui/PolyConecta.Ui.csproj`
- [x] T002 [P] Configure Tailwind CSS build pipeline and Odoo 19 theme styles in `frontend/src/PolyConecta.Ui/wwwroot/css/odoo19-theme.css`
- [x] T003 [P] Configure bunit test project structure in `frontend/tests/PolyConecta.Ui.Tests/PolyConecta.Ui.Tests.csproj`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T004 Create `UiAppShellState` domain state service in `frontend/src/PolyConecta.Ui/Services/UiAppShellState.cs` with constraints: `activeModule VARCHAR ('Inicio','Ventas','Manufactura','Calidad','Configuracion')`, `breadcrumbPath VARCHAR`, `isSidebarCollapsed BOOLEAN DEFAULT false`, `spotlightSearchQuery VARCHAR NULLABLE`
- [x] T005 [P] Create `UiViewState` domain state service in `frontend/src/PolyConecta.Ui/Services/UiViewState.cs` with constraints: `documentType VARCHAR ('Pedido','Manufactura','Calidad')`, `viewMode VARCHAR ('Kanban','List')`, `searchQuery VARCHAR`, `preconfiguredStages LIST`
- [x] T006 [P] Setup SignalR Chat Hub for Chatter messaging in `frontend/src/PolyConecta.Ui/Hubs/ChatterHub.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - App Shell, Navigation & Spotlight Search (Priority: P1) 🎯 MVP Component 1

**Goal**: Global Application Shell with collapsible sidebar `[<-]`, Topbar breadcrumbs (`Inicio / Dashboard`), Spotlight search, and user avatar matching [mockup-001.png](../../../docs/mockups/mockup-001.png).

**Independent Test**: Click sidebar toggle button `[<-]`, verify sidebar collapse, type query in Spotlight search bar, and verify instant navigation.

### Tests for User Story 1

- [x] T007 [P] [US1] Component unit test for App Shell sidebar toggle and breadcrumb rendering in `frontend/tests/PolyConecta.Ui.Tests/OdooAppShellTests.cs`

### Implementation for User Story 1

- [x] T008 [P] [US1] Create `<OdooTopbar>` component in `frontend/src/PolyConecta.Ui/Components/Shell/OdooTopbar.razor` rendering logo, breadcrumbs, Spotlight search input, and user avatar
- [x] T009 [P] [US1] Create `<OdooSidebar>` component in `frontend/src/PolyConecta.Ui/Components/Shell/OdooSidebar.razor` with `[<-]` toggle button and module links (`Inicio`, `Ventas`, `Manufactura`, `Calidad`, `Configuracion`)
- [x] T010 [US1] Create `<OdooAppShell>` layout frame combining Topbar, Sidebar, and main workspace container in `frontend/src/PolyConecta.Ui/Components/Shell/OdooAppShell.razor`

**Checkpoint**: User Story 1 complete and independently testable

---

## Phase 4: User Story 2 - Dual View Switcher (List & Pre-configured Kanban Stages) (Priority: P1) 🎯 MVP Component 2

**Goal**: Header View Switcher `[kanban] [list]` with pre-configured pipeline stage columns (`Borrador` → `Autorizado` → `En progreso` → `Hecho` for Manufacturing; `Pedido Sincronizado` → `Confirmado` → `En manufactura` → `Hecho` for Sales Orders).

**Independent Test**: Toggle between `[kanban]` and `[list]` view buttons, verifying instant card rendering in pre-configured status columns.

### Tests for User Story 2

- [x] T011 [P] [US2] Component unit test for View Switcher mode toggle in `frontend/tests/PolyConecta.Ui.Tests/OdooViewSwitcherTests.cs`

### Implementation for User Story 2

- [x] T012 [P] [US2] Create `<OdooViewSwitcher>` header control in `frontend/src/PolyConecta.Ui/Components/Views/OdooViewSwitcher.razor` with `[kanban]` and `[list]` buttons
- [x] T013 [P] [US2] Create `<OdooListView>` tabular data grid component in `frontend/src/PolyConecta.Ui/Components/Views/OdooListView.razor`
- [x] T014 [US2] Create `<OdooKanbanView>` component in `frontend/src/PolyConecta.Ui/Components/Views/OdooKanbanView.razor` with pre-configured stage column model constraints: `stageId VARCHAR REQUIRED`, `stageName VARCHAR REQUIRED`, `sequence INT REQUIRED >= 1`, `cardCount INT DEFAULT 0`

**Checkpoint**: User Story 2 complete and independently testable

---

## Phase 5: User Story 3 - Odoo 19 Form View with Smart Buttons & Status Pipeline Header (Priority: P1) 🎯 MVP Component 3

**Goal**: Form View layout (`Formulario Pedido`, `Formulario Manufactura`) featuring Action Bar (`Confirmar`, `Validar`, `Subir PDF`), Smart Buttons with live count badges (`Entrega 1`, `Manufacturas 3`), Arrow Status Pipeline header, primary header fields (`Cliente`, `Fecha`, `Ubicacion`), and Notebook Tabs (`[Detalle] [Observaciones]`).

**Independent Test**: Open `Formulario Pedido 3192389`, verify Smart Buttons, click `[ Manufacturas 3 ]`, verify Arrow Pipeline highlighting.

### Tests for User Story 3

- [x] T015 [P] [US3] Component unit test for Smart Button count rendering and navigation in `frontend/tests/PolyConecta.Ui.Tests/OdooSmartButtonsTests.cs`

### Implementation for User Story 3

- [x] T016 [P] [US3] Create `<OdooSmartButtons>` container component in `frontend/src/PolyConecta.Ui/Components/Forms/OdooSmartButtons.razor` with `SmartButtonModel` constraints: `id VARCHAR REQUIRED`, `label VARCHAR REQUIRED`, `countBadge INT DEFAULT 0 >= 0`, `targetRoute VARCHAR REQUIRED`
- [x] T017 [P] [US3] Create `<OdooStatusPipeline>` arrow header component in `frontend/src/PolyConecta.Ui/Components/Forms/OdooStatusPipeline.razor`
- [x] T018 [US3] Create `<OdooFormSheet>` form sheet layout component with Notebook Tabs in `frontend/src/PolyConecta.Ui/Components/Forms/OdooFormSheet.razor`

**Checkpoint**: User Story 3 complete and independently testable

---

## Phase 6: User Story 4 - Native Side Chatter & Audit Log Panel (Priority: P2)

**Goal**: Native right-side Chatter panel embedded in all Form Views, displaying real-time user notes, CONTPAQi SDK sync logs (`"Se carga el pedido de venta polyconecta"`), and interactive comment submission.

**Independent Test**: Open Form View, post comment via Chatter input box, verify real-time append to audit trail via SignalR.

### Tests for User Story 4

- [x] T019 [P] [US4] Component unit test for Chatter message posting and audit log rendering in `frontend/tests/PolyConecta.Ui.Tests/OdooChatterTests.cs`

### Implementation for User Story 4

- [x] T020 [P] [US4] Create `<OdooChatterMessage>` entry component in `frontend/src/PolyConecta.Ui/Components/Chatter/OdooChatterMessage.razor` with `ChatterMessageModel` constraints: `messageId UUID REQUIRED`, `documentId VARCHAR REQUIRED`, `authorName VARCHAR REQUIRED`, `messageType VARCHAR REQUIRED ENUM ('UserComment','SystemAudit','ErpSyncLog')`, `bodyText VARCHAR REQUIRED`, `timestamp DATETIME REQUIRED`
- [x] T021 [US4] Create `<OdooChatterDrawer>` side drawer component with SignalR hub connection in `frontend/src/PolyConecta.Ui/Components/Chatter/OdooChatterDrawer.razor`

**Checkpoint**: User Story 4 complete and independently testable

---

## Phase 7: Polish & Cross-Cutting Concerns

- [x] T022 [P] Create mockup page views (`Dashboard.razor`, `PedidosList.razor`, `PedidoForm.razor`, `ManufacturasList.razor`, `ManufacturaForm.razor`) matching [mockup-001.png](../../../docs/mockups/mockup-001.png) in `frontend/src/PolyConecta.Ui/Pages/`
- [x] T023 [P] Update API swagger and UI contract documentation in `docs/api/`
- [x] T024 Run end-to-end quickstart validation scenarios defined in `.specify/features/003-odoo19-ui-ux-framework/quickstart.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Can start immediately.
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all User Stories.
- **User Stories (Phases 3-6)**: Depend on Foundational phase completion. Can proceed sequentially by priority (P1 → P2).
- **Polish (Phase 7)**: Depends on all User Stories completion.

### Parallel Opportunities

- Tasks marked `[P]` within each phase can be developed concurrently in separate files without merge conflicts.
- User Stories 1, 2, and 3 (all Priority P1) can be implemented in parallel by different developers once Phase 2 completes.

---

## Implementation Strategy: MVP Scope

1. **Phase 1 & 2**: Complete Setup and Foundational infrastructure.
2. **Phase 3, 4, 5 (MVP Scope)**: Complete User Stories 1, 2, and 3 (App Shell, View Switcher, Form View & Smart Buttons).
3. **Validate MVP**: Execute quickstart scenarios 1, 2, and 3.
4. **Phase 6**: Add User Story 4 (Side Chatter Drawer & SignalR Audit Stream).
