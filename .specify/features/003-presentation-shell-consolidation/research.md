# Phase 0 Research: SPEC-003 Presentation Layer Consolidation

## R1. Blazor Server vs. Blazor WebAssembly vs. mantener el HTML estático

- **Decision**: Blazor Server con render interactivo (`AddInteractiveServerComponents`).
- **Rationale**: El proyecto ya tiene un `ChatterHub` de SignalR (`Hubs/ChatterHub.cs`) y un patrón de estado compartido en memoria de servidor (`OperationalDataStore`, `UiAppShellState`) escrito asumiendo un único proceso de servidor con estado — exactamente el modelo de Blazor Server. Blazor WASM obligaría a exponer ese estado vía HTTP/API antes de tener el backend real, duplicando trabajo que la spec 002 hará de todas formas. El HTML estático (`wwwroot/index.html`) ya demostró el costo de mantener la misma lógica de negocio en dos lenguajes (C# y JS) — se retira.
- **Alternatives considered**: Blazor WASM (rechazado por la razón anterior); continuar con HTML/JS estático (rechazado, es la causa raíz del problema que esta spec resuelve).

## R2. Ámbito de vida de `OperationalDataStore`

- **Decision**: `Scoped` (una instancia por circuito de Blazor Server, equivalente a una pestaña/sesión de navegador).
- **Rationale**: Hoy no está registrado en el contenedor de DI en absoluto (`Program.cs` no tiene ningún `builder.Services.Add...`). Si se registrara como `Singleton` por descuido, dos personas abriendo la demo simultáneamente compartirían y corromperían el mismo pedido de ejemplo — exactamente el edge case que spec.md señala.
- **Alternatives considered**: `Singleton` (rechazado, ya justificado); `Transient` (rechazado, perdería el estado entre interacciones dentro de la misma sesión, que es el requisito central de la User Story 1).

## R3. Qué pasa con `Pages/PedidoForm.razor` y `Pages/ManufacturaForm.razor`

- **Decision**: Se retiran como páginas propias; su disposición visual (hoja de documento + `OdooChatterDrawer` lateral, pipeline de estado en la esquina superior derecha) se funde dentro de las pantallas reales de pedido y de orden de fabricación, que hoy viven en `Components/Poc/PocSalesOrderForm.razor` y `Components/Poc/PocMrpHierarchy.razor` con datos reales pero sin ese acabado visual de "hoja".
- **Rationale**: Ambas versiones tienen mérito distinto — la estática tiene mejor acabado de "hoja de documento", la interactiva tiene lógica real. Mantener ambas por separado es exactamente la duplicación que la spec busca cerrar (FR-007).
- **Alternatives considered**: Eliminarlas sin fundir nada (rechazado, se perdería el acabado visual ya logrado, que sí vale la pena conservar).

## R4. Mapa de rutas reales por fase

- **Decision**:

  | Fase | Ruta | Componente anfitrión |
  |---|---|---|
  | 1-2: Pedido & aprobaciones | `/pedidos/{folio}` | `PocSalesOrderForm` (+ layout de hoja fundido, R3) |
  | 3: Jerarquía MRP & BOM | `/manufactura/{folioOm}` | `PocMrpHierarchy` |
  | 4: Báscula & calidad | `/piso/bascula` | `PocHandheldScaleTerminal` |
  | 5: Balance de masa & cierre | `/manufactura/{folioOf}/cierre` | `PocMassBalanceConsole` |
  | 6: Logística & traspasos | `/logistica` | `PocLogisticsRouting` |
  | 7: Conversión Santa Cruz | `/conversion/{folioOf}` | `PocBaggingConversion` |
  | Inicio | `/` | `Dashboard` |

- **Rationale**: Rutas con nombre de dominio (no `/demo/poc/step/4`) porque esta ya no es "una demo aparte" sino la aplicación real (aunque con datos en memoria); cumple FR-002 (enlaces directos, atrás/adelante del navegador).
- **Alternatives considered**: Mantener `/demo/poc` con navegación interna — rechazado, es lo que hoy impide los enlaces directos y confunde "modo demo" con "la aplicación".

**Output**: Sin incógnitas pendientes en Technical Context.
