# Implementation Plan: SPEC-003: Presentation Layer Consolidation

**Branch**: `003-presentation-shell-consolidation` | **Date**: 2026-09-15 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `.specify/features/003-presentation-shell-consolidation/spec.md`

---

## Summary

Convertir `PolyConecta.Presentation` en una aplicación Blazor Server real (hoy sirve un HTML estático vía `MapFallbackToFile`, sin ningún host de Blazor registrado), consolidando las tres superficies de UI hoy desconectadas (`wwwroot/index.html`, el shell Odoo con páginas estáticas, y `PocDemoSuite`/`Components/Poc/*`) en un único shell navegable por rutas reales, con la lógica de datos detrás de una interfaz reemplazable (`IOperationalWorkflowService`).

## Technical Context

**Language/Version**: C# 12 / .NET 8, Razor Components

**Primary Dependencies**: `Microsoft.AspNetCore.Components` (Blazor Server, ya incluido en `Microsoft.NET.Sdk.Web`), `Microsoft.AspNetCore.SignalR` (para `ChatterHub`, ya presente)

**Storage**: N/A — estado en memoria por sesión (`Scoped`), sin base de datos en esta spec

**Testing**: Verificación manual en navegador por historia de usuario (FR-011/FR-012); opcionalmente `bunit` para pruebas de componentes si el equipo lo adopta más adelante — no se introduce en esta spec para no ampliar el alcance

**Target Platform**: Navegador vía Blazor Server (render interactivo del lado del servidor)

**Project Type**: Web — capa de presentación de la solución multi-proyecto existente

**Performance Goals**: N/A (demo de un solo usuario/sesión concurrente moderado)

**Constraints**: Debe seguir el benchmark visual Odoo 19 Enterprise (Constitución Principio IX); no debe requerir conexión real a `PolyConecta.Api` ni a CONTPAQi

**Scale/Scope**: 1 host Blazor, 1 shell, ~7 rutas (una por fase operativa), retiro de 1 archivo HTML estático y 2 páginas con datos quemados

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principio | Gate | Estado |
|---|---|---|
| I / IX (benchmark Odoo 19 Enterprise) | Shell único con topbar/sidebar/status pipeline/smart buttons ya construidos en `Components/Shell` y `Components/Forms` | ✅ Pass — se reutilizan, no se rehacen |
| VI (alcance Fase 1: 100% Handheld) | La captura de peso sigue siendo un formulario mediado por el Planner, no hardware real | ✅ Pass |
| II (no SQL/SDK directo desde UI) | La UI no debe llamar a CONTPAQi directamente; toda esa responsabilidad queda del lado de `IOperationalWorkflowService`, hoy en memoria | ✅ Pass |

No hay violaciones que requieran "Complexity Tracking".

## Project Structure

### Documentation (this feature)

```text
.specify/features/003-presentation-shell-consolidation/
├── spec.md
├── plan.md              # This file
├── research.md
├── data-model.md         # Contratos de vista/estado, no entidades de negocio
├── contracts/
│   └── ioperationalworkflowservice.md
├── quickstart.md
└── checklists/
    └── requirements.md
```

### Source Code (repository root)

```text
PolyConecta.Presentation/
├── App.razor                          # NUEVO — componente raíz Blazor
├── Routes.razor                       # NUEVO — tabla de rutas real (reemplaza el switch de OdooAppShell)
├── _Imports.razor                     # NUEVO
├── Program.cs                         # REESCRITO — AddRazorComponents + AddInteractiveServerComponents + MapHub<ChatterHub>
├── Components/
│   ├── Shell/OdooAppShell.razor       # Se convierte en el MainLayout único (ya existe, se re-cablea)
│   └── Poc/*.razor                    # Se re-ubican bajo rutas reales (una por fase), ya no bajo /demo/poc aislado
├── Pages/
│   ├── PedidoForm.razor               # RETIRADO como página propia; su layout se funde en la pantalla real de pedido
│   ├── ManufacturaForm.razor          # RETIRADO igual, fundido en la pantalla real de OF/OM
│   └── PocDemoSuite.razor             # RETIRADO — su stepper se reemplaza por el sidebar del shell único
├── Services/
│   ├── IOperationalWorkflowService.cs # NUEVO — la costura hacia el backend real
│   └── OperationalDataStore.cs        # Pasa a implementar IOperationalWorkflowService, registrado Scoped
└── wwwroot/
    └── index.html                     # RETIRADO como página servida por defecto (se puede archivar fuera de wwwroot si se quiere conservar de referencia)
```

**Structure Decision**: Blazor Server interactivo dentro del mismo proyecto `PolyConecta.Presentation` ya existente — no se crea un proyecto nuevo. Se reutiliza el 90% de los componentes ya escritos (`Components/Poc/*`, `Components/Shell/*`, `Components/Chatter/*`, `Components/Forms/*`); el trabajo es de *cableado y consolidación*, no de reescritura desde cero.

## Complexity Tracking

*Sin violaciones que justificar.*
