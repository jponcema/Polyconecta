# Feature Specification: SPEC-003: Presentation Layer Consolidation into One Operable Proof of Concept

**Feature Branch**: `003-presentation-shell-consolidation`

**Created**: 2026-09-15

**Status**: Draft

**Input**: User description: "Reestructurar `PolyConecta.Presentation` para tener, de verdad, una prueba de concepto operable — hoy conviven tres superficies de UI desconectadas entre sí y ninguna corre realmente como una SPA navegable."

---

## Executive Summary & Architectural Vision

Esta spec parte de una verificación directa del estado actual de `PolyConecta.Presentation`, no de lo que documentaba la spec anterior (`001-poc-end-to-end-operational-flow`). Esa verificación encontró tres superficies de interfaz que **no se comunican entre sí**, y ninguna corre como una aplicación Blazor real hoy:

1. **`wwwroot/index.html`** (1076 líneas de HTML/JS vanilla con Bootstrap por CDN): es la única superficie que el servidor sirve de verdad — `Program.cs` no registra Blazor en absoluto, solo `UseStaticFiles()` + `MapFallbackToFile("index.html")`. Duplica en JavaScript la misma lógica de negocio (pesaje, cuarentena `.S`, cierre técnico, traspaso en 2 pasos) que ya existe, mejor escrita, en C#.
2. **`Components/Shell/OdooAppShell.razor`** + `Pages/Dashboard.razor` / `Pages/PedidoForm.razor` / `Pages/ManufacturaForm.razor`: un shell con topbar/sidebar tipo Odoo que navega por `switch` interno entre tres pantallas con **datos quemados en el markup** (folios, clientes y montos fijos, sin `@code` de estado real). No tiene ninguna ruta ASP.NET que lo monte.
3. **`Pages/PocDemoSuite.razor`** (`@page "/demo/poc"`) + `Components/Poc/*.razor` + `Services/OperationalDataStore.cs`: la superficie más completa funcionalmente — sí tiene estado real en C# (`OperationalDataStore`) y reproduce correctamente las 7 fases y varias de las 17 reglas validadas — pero tiene su propia barra de navegación oscura independiente del shell Odoo, y como no hay ningún host de Blazor configurado, su `@page` tampoco es alcanzable.

Adicionalmente, `tasks.md` de `001-poc-end-to-end-operational-flow` marca como completadas (`[x]`) tareas que referencian archivos que **no existen en el repositorio** (`PocBomUploader.razor`, `PocQualityCheckModal.razor`) y una verificación de build/ejecución que no es posible porque no hay ni siquiera un archivo de solución `.sln` en la raíz del repositorio. Esta spec no repite ese patrón: cada requisito funcional exige evidencia de ejecución real en navegador, no solo la existencia de un archivo `.razor`.

**Decisión de arquitectura**: se consolida sobre **Blazor Server con render interactivo**, no sobre Blazor WebAssembly ni sobre HTML/JS estático. Razón (una línea, per Constitución Principio IX/UX): ya existe un `ChatterHub` de SignalR y un patrón de estado compartido en memoria de servidor (`OperationalDataStore`, `UiAppShellState`) que calzan naturalmente con Blazor Server; migrar a WASM implicaría rehacer ese estado como llamadas HTTP sin necesidad, y el HTML estático ya demostró no ser mantenible (duplica lógica en dos lenguajes).

---

## User Scenarios & Testing

### User Story 1 - Una sola aplicación navegable de verdad (Priority: P1)

Como stakeholder de negocio, necesito abrir una URL y recorrer las 7 fases operativas validadas usando botones y formularios reales del navegador (URL, atrás/adelante, enlaces directos), sin que cambiar de pantalla borre lo que ya había capturado, para poder validar el flujo completo en una sola sesión de demo.

**Why this priority**: Es el problema raíz — hoy no existe ninguna superficie Blazor realmente montada; sin esto, ninguna otra historia de usuario es alcanzable en el navegador.

**Independent Test**: Ejecutar `dotnet run` sobre `PolyConecta.Presentation`, abrir la URL base en un navegador, navegar a cada una de las 7 fases mediante enlaces reales (no un `switch` interno), recargar la pestaña a mitad del flujo y confirmar que la navegación por URL sigue funcionando.

**Acceptance Scenarios**:

1. **Given** el proyecto compilado y corriendo, **When** se abre la URL raíz en un navegador, **Then** se renderiza el shell de navegación con contenido real (no la página de bienvenida por defecto de ASP.NET ni un 404).
2. **Given** el usuario está en la fase 4 (báscula), **When** usa el botón "Atrás" del navegador, **Then** regresa a la fase 3 sin perder los datos ya capturados en la fase 4 dentro de esa sesión.
3. **Given** un enlace directo a una fase específica (p. ej. `/piso/bascula`), **When** se abre en una pestaña nueva, **Then** carga esa pantalla directamente, sin pasar primero por la fase 1.

---

### User Story 2 - Un único shell de navegación, no tres (Priority: P1)

Como usuario y como desarrollador, necesito que todas las pantallas compartan el mismo shell (topbar, sidebar, breadcrumb, estilo Odoo 19 Enterprise) para no mantener tres sistemas de navegación distintos ni decidir cuál es "el real" cada vez que se agrega una pantalla.

**Why this priority**: Sin un shell único, cada pantalla nueva vuelve a bifurcar el problema que esta spec busca cerrar.

**Independent Test**: Recorrer las 7 fases y verificar visualmente que el topbar, el sidebar y el estilo de cabecera (pipeline de estado, smart buttons) son exactamente los mismos componentes compartidos en cada pantalla.

**Acceptance Scenarios**:

1. **Given** cualquier pantalla de la aplicación, **When** se inspecciona su composición, **Then** todas usan el mismo componente de shell raíz; ninguna pantalla define su propia barra de navegación duplicada.
2. **Given** el shell único ya construido, **When** se agrega una pantalla nueva, **Then** hereda topbar/sidebar/breadcrumb sin código adicional de navegación.

---

### User Story 3 - Costura de reemplazo hacia el dominio real (Priority: P2)

Como equipo de producto, necesito que la lógica de datos de la POC viva detrás de una interfaz de servicio reemplazable, para poder conectarla a `PolyConecta.Api` sobre el dominio redefinido (`002-domain-model-redefinition`) más adelante sin rehacer las vistas `.razor`.

**Why this priority**: Evita que el trabajo de esta spec quede varado de nuevo cuando el dominio se implemente — protege la inversión.

**Independent Test**: Registrar una segunda implementación *fake* de la interfaz de servicio (sin tocar ningún `.razor`) e intercambiarla por configuración; confirmar que las pantallas siguen renderizando sin cambios de marcado.

**Acceptance Scenarios**:

1. **Given** la interfaz `IOperationalWorkflowService` implementada hoy por un almacén en memoria, **When** se registra una implementación alternativa en el contenedor de DI, **Then** ningún componente `.razor` requiere modificación para seguir funcionando.

---

### User Story 4 - "Hecho" significa ejecutable, no solo compilado (Priority: P2)

Como responsable de validar el trabajo, necesito que cada tarea de implementación declare su evidencia de ejecución real en navegador como criterio de cierre, para no repetir la experiencia de tareas marcadas `[x]` que referenciaban archivos inexistentes o rutas inalcanzables.

**Why this priority**: Es la lección directa del intento anterior; sin este requisito, el riesgo de completions falsos se repite.

**Independent Test**: Revisar `tasks.md` de esta feature una vez ejecutada la fase de implementación y confirmar que cada tarea cerrada referencia un archivo existente y una ruta verificablemente alcanzable en el navegador (captura de pantalla o pasos de reproducción), no solo `dotnet build`.

**Acceptance Scenarios**:

1. **Given** una tarea marcada como completada, **When** se le pide reproducir el paso independiente descrito en su historia de usuario, **Then** es reproducible tal cual está escrito, en el navegador, sin ajustes adicionales.

### Edge Cases

- ¿Qué pasa si dos personas abren la demo al mismo tiempo desde navegadores distintos? El estado de `OperationalDataStore` MUST ser por sesión/circuito (`Scoped`), no compartido globalmente (`Singleton`) — hoy no está registrado en el contenedor de DI en absoluto, así que ambos casos son posibles por accidente; esta spec fija el ámbito explícitamente.
- ¿Qué pasa si el usuario refresca la pestaña completa (no solo navega) a mitad del flujo? Es aceptable perder el estado de esa sesión (no hay persistencia en base de datos en esta spec) — se documenta como límite conocido, no como bug.
- ¿Qué pasa con `wwwroot/index.html` y con `Pages/PedidoForm.razor`/`ManufacturaForm.razor` una vez consolidado el shell? Se retiran del árbol de rutas activo; su valor visual (la maqueta de "hoja" con chatter lateral) se reutiliza dentro de las pantallas reales en vez de mantenerse como una superficie paralela.

---

## Requirements

### Functional Requirements

**Host y enrutamiento reales**
- **FR-001**: `PolyConecta.Presentation` MUST registrar un host de Blazor Server con render interactivo (componente raíz, tabla de rutas, `_Imports.razor`) y mapear el `ChatterHub` existente — ya no MUST depender de `MapFallbackToFile` sirviendo un HTML estático como sustituto de la aplicación.
- **FR-002**: Cada una de las 7 fases operativas validadas MUST tener una ruta propia y navegable por URL (no un `switch` interno de cadenas), soportando enlaces directos y los botones atrás/adelante del navegador.
- **FR-003**: El estado de la demo (`OperationalDataStore` y equivalentes) MUST registrarse con ciclo de vida `Scoped` (por sesión/circuito), nunca `Singleton`, para que dos usuarios concurrentes no compartan ni corrompan el mismo estado.

**Shell único**
- **FR-004**: MUST existir un único componente de shell raíz (topbar + sidebar + breadcrumb, estilo Odoo 19 Enterprise per Constitución Principio IX) que envuelva las 7 pantallas; ninguna pantalla MUST definir su propia barra de navegación o botón de "volver al dashboard" duplicado.
- **FR-005**: La cabecera de cada documento (pipeline de estado, smart buttons) MUST ser el mismo componente compartido (`OdooStatusPipeline`, `OdooSmartButtons`) en las 7 pantallas, no una reimplementación por pantalla.

**Consolidación y retiro de duplicados**
- **FR-006**: `wwwroot/index.html` MUST dejar de ser la superficie servida por defecto; su lógica de negocio en JavaScript MUST retirarse una vez que la funcionalidad equivalente exista en las pantallas Blazor reales — no MUST mantenerse en paralelo "por si acaso".
- **FR-007**: Los valores hoy quemados en el markup de `Pages/PedidoForm.razor` y `Pages/ManufacturaForm.razor` MUST reemplazarse por datos que vienen del estado inyectado (la misma fuente que usan las pantallas de `Components/Poc/*`); su disposición visual (hoja de documento + chatter lateral) MUST conservarse dentro de las pantallas reales en lugar de descartarse.
- **FR-008**: MUST existir una única fuente de datos de demostración (un solo servicio de estado) para las 7 fases; no MUST haber dos catálogos de datos de ejemplo divergentes (el de `Presentation.Models` y el hardcodeado en `wwwroot/index.html`) describiendo el mismo pedido con valores distintos.

**Costura de reemplazo hacia el backend real**
- **FR-009**: Toda pantalla MUST leer y mutar datos exclusivamente a través de una interfaz de servicio (`IOperationalWorkflowService` o equivalente por dominio: pedidos, MRP, piso de planta, logística), nunca instanciando `OperationalDataStore` directamente en el marcado ni leyendo modelos estáticos por defecto.
- **FR-010**: La implementación concreta de esa interfaz MUST quedar aislada en `Services/`, de forma que sustituirla por una que llame a `PolyConecta.Api` no requiera cambios en ningún archivo `.razor`.

**Verificación honesta**
- **FR-011**: Ninguna tarea de implementación derivada de esta spec MUST marcarse como completada sin haber ejecutado la aplicación (`dotnet run`) y verificado en navegador el paso de prueba independiente descrito en su historia de usuario.
- **FR-012**: `tasks.md` de esta feature MUST referenciar únicamente archivos que existen en el repositorio al momento de marcarse `[x]`; toda tarea que dependa de un archivo por crear MUST permanecer abierta hasta que ese archivo exista y compile.

### Fuera de alcance

- Conexión real a `PolyConecta.Api`/persistencia en base de datos — esta spec entrega la POC operable en memoria con la costura de reemplazo lista; conectar el backend real depende de que `002-domain-model-redefinition` esté implementada.
- Autenticación/autorización de usuarios reales — la demo asume un usuario único por sesión de navegador.
- Integración directa de báscula/hardware (Principio VI, Fase 2 de la Constitución) — la captura de peso sigue siendo un formulario mediado por el Planner.

---

## Key Entities (Presentation State & Seams)

- **`IOperationalWorkflowService`** (nueva interfaz): expone las operaciones de las 7 fases (confirmar pedido, validar ventas/crédito, cargar BOM, programar WO, pesar rollo, aprobar/rechazar calidad, cerrar técnicamente, despachar/recibir traspaso, cerrar conversión) como métodos async, ocultando si la implementación es en memoria o vía HTTP a la API real.
- **`OperationalDataStore`** (existente, ajustado): pasa a ser la implementación en memoria de `IOperationalWorkflowService`, registrada `Scoped`; deja de instanciarse implícitamente y pasa a inyectarse.
- **`UiAppShellState` / `UiViewState`** (existentes): se conservan como estado de navegación/UI (módulo activo, breadcrumb, sidebar colapsado); no MUST mezclarse con el estado de negocio de `OperationalDataStore` (separación de responsabilidades).
- **Mapa de rutas de las 7 fases**: una ruta real por fase (p. ej. `/pedidos/{folio}`, `/manufactura/{folioOm}`, `/piso/bascula`, `/logistica`, `/conversion/{folioOf}`), cada una montada dentro del shell único.

---

## Success Criteria

### Measurable Outcomes

- **SC-001**: Un usuario puede abrir la aplicación en un navegador y completar las 7 fases operativas usando únicamente el mouse/teclado y URLs reales, sin recargar manualmente el estado, en una sola sesión.
- **SC-002**: El 100% de las pantallas comparte el mismo shell de navegación; cero pantallas con barra de navegación propia duplicada.
- **SC-003**: Cero archivos `.razor` huérfanos — todo componente existente queda alcanzable desde al menos una ruta real, o se retira si ya no aplica.
- **SC-004**: Cero fuentes de datos de demostración divergentes — una sola implementación de `IOperationalWorkflowService` en memoria describe cada documento de ejemplo.
- **SC-005**: El 100% de las tareas cerradas en `tasks.md` de esta feature tienen evidencia de ejecución real en navegador, no solo de compilación.

## Assumptions

- Blazor Server (render interactivo) es la arquitectura elegida sobre Blazor WebAssembly, dado el `ChatterHub` de SignalR y el patrón de estado en memoria de servidor ya presentes en el proyecto; es una decisión reversible si el usuario prefiere explícitamente WASM.
- El backend real (`PolyConecta.Api` sobre el dominio redefinido) no se conecta en esta spec; se deja la costura (`IOperationalWorkflowService`) lista para ese siguiente paso.
- `wwwroot/index.html` y los datos quemados de `PedidoForm.razor`/`ManufacturaForm.razor` se retiran como superficies activas; su valor visual se reutiliza, no se mantiene duplicado.
- No se requiere persistencia en base de datos para que la POC se considere "operable" en el sentido de esta spec — operable significa navegable e interactivo en una sesión de navegador real, no durable entre reinicios del servidor.
