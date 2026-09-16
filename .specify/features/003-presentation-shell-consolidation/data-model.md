# Phase 1 "Data Model" (View-State Contracts): SPEC-003

Esta spec no introduce entidades de negocio (esas son de `002-domain-model-redefinition`). Lo que sí define es el contrato de estado de UI que las pantallas consumen, para que ninguna pantalla vuelva a leer un modelo estático directamente.

## `IOperationalWorkflowService`

Interfaz única detrás de la cual vive toda mutación/lectura de las 7 fases. Ver [`contracts/ioperationalworkflowservice.md`](./contracts/ioperationalworkflowservice.md) para la firma completa.

## Estado de navegación vs. estado de negocio (separación de responsabilidades)

| Servicio | Responsabilidad | Ámbito DI |
|---|---|---|
| `UiAppShellState` | Módulo activo, breadcrumb, sidebar colapsado/expandido, texto de búsqueda | `Scoped` |
| `UiViewState` | Estado efímero de una vista puntual (pestaña activa, filtros de lista) | `Scoped` |
| `IOperationalWorkflowService` (impl. `OperationalDataStore`) | Los datos de negocio de la demo: pedido, OM/OF/WO, slots, lotes, quality checks, picking | `Scoped` |

Ningún componente `.razor` MUST mezclar estos tres — un componente de pantalla inyecta `IOperationalWorkflowService` para datos, y opcionalmente `UiAppShellState`/`UiViewState` solo para cosas puramente visuales (qué pestaña está activa, etc.), nunca para decidir reglas de negocio.

## Mapa de rutas (de `research.md` R4)

Ver tabla en `research.md`. Cada fila es una entrada real en `Routes.razor`/`@page`, no una rama de `switch`.
