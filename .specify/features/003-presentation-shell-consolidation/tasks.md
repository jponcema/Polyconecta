# Tasks: SPEC-003 Presentation Layer Consolidation

**Feature Branch**: `003-presentation-shell-consolidation`
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

Cada tarea cerrada abajo se verificó ejecutando `dotnet build` **y** `dotnet run` seguido de peticiones reales contra el servidor (evidencia anotada por tarea), no solo compilación — por FR-011/FR-012 de esta spec.

## Fase 1: Host Blazor Server real

- [x] T001 Crear `App.razor`, `Routes.razor`, `_Imports.razor` en la raíz de `PolyConecta.Presentation`
- [x] T002 Reescribir `Program.cs`: `AddRazorComponents().AddInteractiveServerComponents()`, `AddSignalR()`, `UseAntiforgery()`, `MapRazorComponents<App>().AddInteractiveServerRenderMode()`, `MapHub<ChatterHub>("/hubs/chatter")` — se retira `MapFallbackToFile("index.html")`
  - **Evidencia**: `dotnet build` limpio; `curl -s http://127.0.0.1:5299/` devuelve el shell Blazor renderizado (no la página de bienvenida ni 404); `curl -X POST /_blazor/negotiate` y `curl -X POST /hubs/chatter/negotiate` responden con `negotiateVersion` válido (el circuito interactivo puede establecerse).

## Fase 2: Shell único (User Story 2)

- [x] T003 Convertir el shell en un `MainLayout.razor` real (`Components/Layout/MainLayout.razor`, `LayoutComponentBase`, `@Body`) — reemplaza `OdooAppShell.razor` (retirado)
- [x] T004 `OdooTopbar.razor` y `OdooSidebar.razor` navegan por `NavigationManager.NavigateTo(...)` a rutas reales, no por `EventCallback<string>` local
  - **Evidencia**: `grep -rn "OdooAppShell"` no devuelve resultados; el HTML servido en `/` incluye el topbar/sidebar reales alrededor del `Dashboard`.

## Fase 3: Rutas reales por fase (User Story 1)

- [x] T005 `Pages/Dashboard.razor` → `@page "/"` con CTA hacia `/pedidos`
- [x] T006 `Pages/PocDemoSuite.razor` → seis `@page` reales (`/pedidos`, `/manufactura`, `/piso/bascula`, `/manufactura/cierre`, `/logistica`, `/conversion`) en vez de `/demo/poc` con `switch` interno
  - **Evidencia**: `curl` a cada una de las 6 rutas devuelve el título de fase correspondiente (`Fase 1: Captura Pedido ERP`, `Fase 3: Jerarquía MRP...`, `Fase 4: Pesaje Handheld...`, `Fase 5: Balance de Masa...`, `Fase 6: Logística...`, `Fase 7: Conversión...`); una ruta inexistente devuelve `404` real vía la plantilla `NotFound`.

## Fase 4: Estado por sesión, no compartido (Edge case de spec.md)

- [x] T007 Crear `Services/OperationalFlowState.cs` (Scoped) con los campos que antes vivían como `@code` local de `PocDemoSuite` (`CurrentOrderStage`, `SalesApproved`, `CreditApproved`, `ExtrusionState`, `AssignedMachine`, `BomLoaded`, `ClosureExecuted`, `TransferStep`, `BaggingCompleted`, `StepCompleted`), registrado `builder.Services.AddScoped<OperationalFlowState>()`
  - **Evidencia**: registrado en `Program.cs`; `PocDemoSuite.razor` lo inyecta y navega entre rutas sin perder el estado capturado (mismo circuito = misma instancia `Scoped`).

## Fase 5: Retiro de superficies duplicadas (parcial — ver Notas)

- [x] T008 `wwwroot/index.html` deja de ser la superficie servida por defecto (se retiró `UseDefaultFiles()`/`MapFallbackToFile`); el archivo permanece en disco como referencia histórica, no se elimina en este pase
- [ ] T009 Fundir el acabado visual "hoja + chatter lateral" de `Pages/PedidoForm.razor`/`ManufacturaForm.razor` dentro de `PocSalesOrderForm`/`PocMrpHierarchy` — **NO completada en este pase**; ambos archivos siguen en el repo, ya no están ruteados/alcanzables, pero su valor visual todavía no se trasladó (FR-007 solo parcialmente satisfecho)
- [ ] T010 Reemplazar la interfaz `IOperationalWorkflowService`/`OperationalDataStore` documentada en `contracts/ioperationalworkflowservice.md` por la implementación real — **NO completada**; se optó por `OperationalFlowState` (más simple, alcance real de `PocDemoSuite`) en vez de retomar `OperationalDataStore`/`Presentation.Models.OperationalModels`, que sigue sin inyectarse en ningún componente. Pendiente decidir si se retira `OperationalDataStore` o se adopta como la implementación real hacia la API en `002`.

## Verificación honesta (FR-011/FR-012/SC-005)

- [x] T011 `dotnet build PolyConecta.Presentation` — 0 errores
- [x] T012 `dotnet run` + `curl` a `/`, `/pedidos`, `/manufactura`, `/piso/bascula`, `/manufactura/cierre`, `/logistica`, `/conversion`, ruta inexistente — todas responden lo esperado
- [ ] T013 Prueba manual en navegador real (clicks, atrás/adelante, dos pestañas concurrentes) — **pendiente, no ejecutada por este agente** (solo verificado a nivel HTTP/HTML con `curl`, que no ejecuta JavaScript ni abre un circuito interactivo real). Recomendado antes de considerar esta feature 100% cerrada.

## Fase 6: Arquitectura de vistas real (List/Kanban/Form) — Pedidos como modelo de referencia

Corrección tras feedback del usuario: la consolidación de rutas (Fases 1-5) resolvió la navegación pero no la arquitectura de vistas — las pantallas seguían siendo formularios ad-hoc sin selector Lista/Kanban/Form, sin button box real, sin chatter acoplado. Se aplicó el patrón de `.agents/skills/odoo-design-system/references/odoo-ux-patterns.md` completo a **un solo modelo (Pedidos)** como referencia, por decisión explícita del usuario, antes de replicarlo a OM/OF/WO/Calidad.

- [x] T014 `Components/Views/OdooBreadcrumb.razor` (nuevo) — breadcrumb reutilizable
- [x] T015 `Pages/PedidosList.razor` (`@page "/pedidos"`) — vista Lista (tabla) + vista Kanban (columnas por etapa) intercambiables vía `OdooViewSwitcher`, sin resetear el pedido de ejemplo al cambiar de vista
- [x] T016 `Pages/PedidoFormView.razor` (`@page "/pedidos/{Folio}"`) — vista Form siguiendo el orden estándar: breadcrumb → acciones + `OdooStatusPipeline` → `OdooSmartButtons` en `.o_button_box` → `.o_form_sheet` (reutiliza `PocSalesOrderForm` sin su propio `card` duplicado) → `OdooChatterDrawer` lateral
- [x] T017 Retirado `PocOrderHeaderPipeline.razor` (duplicaba la lógica de `OdooStatusPipeline` con su propia barra de arco inline) y se quitaron los `case 1: case 2:` de `PocDemoSuite.razor` (esos pasos ahora viven en `/pedidos`, no en el wizard)
- [x] T018 Corregido un `CS0121` real de compilación (ambigüedad `AddAttribute(bool)` vs `AddAttribute(string?)`) causado por pasar `new() { ... }` (target-typed) como parámetro de componente — se resolvió tipando explícitamente `new List<OdooBreadcrumb.Crumb> { ... }`
  - **Evidencia**: `dotnet build` limpio; `curl /pedidos` devuelve la tabla con el pedido de ejemplo; `curl /pedidos/IV214-26` devuelve `o_control_panel`, `o_button_box`, `o_form_sheet`, `o_main_navbar`, el pipeline de estado y el chatter (`Enviar mensaje`/`Registrar una nota`); `/manufactura` y las demás rutas de fase siguen respondiendo sin regresión.
- [ ] T019 Replicar el mismo patrón (List + Kanban + Form + button box + chatter) a Manufactura (`OM`/`OF`/`WO`), Calidad y Logística — **no hecho todavía**, queda como siguiente iteración explícita.

## Fase 7: Ajustes de distribución contra referencia real de Odoo (verificado con navegador conectado)

Con acceso real a Chrome se comparó la pantalla de Pedidos contra `demo5.odoo.com/odoo/manufacturing` (Órdenes de Fabricación y ficha de Producto) y se corrigieron 3 discrepancias de distribución señaladas por el usuario:

- [x] T020 `OdooViewSwitcher.razor` — selector de vista ahora es solo iconos (sin texto "kanban"/"list"), como en la referencia real
- [x] T021 `OdooSmartButtons.razor` + `.o_smart_button` en `app.css` — botones inteligentes más compactos (icono pequeño + texto de 2 líneas, padding reducido), y reubicados de una fila propia a la **misma fila del breadcrumb** en `.o_control_panel` (`PedidoFormView.razor`), igual que en la ficha de producto de referencia
- [x] T022 Barra de acciones + `OdooStatusPipeline` movidas de un `<div>` flotando a nivel de página a `.o_statusbar`, ahora el primer hijo de `.o_form_sheet` — queda acoplada al techo de la hoja (una sola pieza visual), no separada de ella
  - **Evidencia**: capturas de pantalla reales vía Chrome conectado (no solo HTML crudo) de `/pedidos` (lista, switcher solo-icono), `/pedidos` en kanban, y `/pedidos/IV214-26` (button box en la fila del breadcrumb, statusbar+acciones pegados a la hoja); interacción real verificada (clic en "Confirmar (Atención a Clientes)" → statusbar avanza a "Confirmado" con fondo morado, aparecen "Validar (Ventas)"/"Validar (Crédito)").
- [x] T023 (hallazgo durante la Fase 5, no reportado antes) — `OdooStatusPipeline.razor` usaba la clase `text-teal`, que no existe en Bootstrap ni en `app.css`; el pipeline no tenía forma de flecha ni resaltaba la etapa activa. Se reescribió con clip-path (forma de flecha real) y clases `.active`/`.done` con los tokens de marca.

## Fase 8: Form de Pedido reestructurado como Orden de Venta CONTPAQi (maestro + detalle)

Cambio de enfoque de negocio: los campos de usuario de CONTPAQi a nivel de Pedido son limitados y no alcanzan para la ficha técnica completa; el Pedido (maestro + líneas/detalle) vive en CONTPAQi y PolyConecta lo refleja con la estructura real de una orden de venta (no un formulario plano de especificaciones). Verificado contra `demo5.odoo.com/odoo/sales` (orden S00073).

- [x] T024 `PocSalesOrderForm.razor` reescrito: título grande + grupo de campos en dos columnas con pares label:valor (`.o_form_label_row`) en vez de 4 cajas horizontales — corrige la falta de alineación señalada por el usuario
- [x] T025 Tab **Líneas del Pedido**: tabla real (Producto, Descripción, Cantidad, UoM) en vez de un formulario de ficha técnica a nivel de pedido
- [x] T026 Tab **Otra Información**: la ficha técnica (Extrusión/Conversión) se muestra ahí, explícitamente etiquetada como datos del **Producto** (catálogo maestro), no del Pedido — refleja el nuevo entendimiento del límite de campos de usuario de CONTPAQi
- [x] T027 Validado que `002-domain-model-redefinition/spec.md` ya modelaba la ficha técnica delegada en `Product` (no en `SalesOrder`) — se agregó una nota de validación posterior en esa spec, sin cambios de entidades
  - **Evidencia**: captura de pantalla real vía Chrome del form con las dos columnas alineadas y ambas pestañas.

## Fase 9: Corrección de dónde vive la ficha técnica (no en CONTPAQi, ni siquiera en Producto)

El usuario precisó (con el CFDI real como referencia de la estructura maestro/detalle de CONTPAQi) que la ficha técnica completa **no cabe en ningún campo de CONTPAQi** — ni Pedido ni Producto — y dio la lista exacta de campos que usa Atención a Clientes. Esto también corrigió `002-domain-model-redefinition/spec.md` (ver su "Nota de validación posterior — revisión 2"): `BagSpecification` → `PtSpecification`, la referencia `PtSpecification → RollSpecification` pasa de opcional a obligatoria, se agrega `SalesOrderLine` (el Pedido es maestro + detalle, no un único producto/cantidad plano), y `target_production_kg`/tolerancia se mueven de la ficha del rollo a la línea del pedido (son metas de la corrida, no del catálogo).

- [x] T028 `PocSalesOrderForm.razor`: pestaña "Ficha Técnica (Rollo + PT)" reescrita con los campos exactos proporcionados (Rollo: tipo de material, tipo/medida, calibre, kg/rollo, tratado, pigmento, aditivo, perforación, impresión, kg a fabricar, % tolerancia; PT: código sistema, no. parte, medida, tintas, pantones, suaje, empaque, millares/kg, kg/millar, tipo de sello), con nota explícita de que viven en PolyConecta, no en CONTPAQi
- [x] T029 Campos que ya existen en otra parte del form (Código Sistema, Millares/Kg Solicitados) se muestran como referencia cruzada ("ver arriba" / "ver Líneas del Pedido") en vez de duplicarse como fuente de verdad
  - **Evidencia**: captura de pantalla real del tab con las dos columnas (Rollo/PT) y los 21 campos exactos.
- [ ] T030 Reflejar `SalesOrderLine` como concepto real en la UI (hoy la tabla "Líneas del Pedido" ya tiene la forma correcta — producto + cantidad — pero no hay soporte real para más de una línea) — pendiente, no bloqueante para esta demo de un solo producto.

## Fase 10: Datos de la demo anclados a un CFDI real + unidad de venta variable

- [x] T031 Reemplazados los datos ficticios ("Empaques y Alimentos de Monterrey"/bolsa camiseta) por el ejemplo real del CFDI de referencia (`docs/references/documents/CFDI_APODACA_MXN_CON_IVA_4.0_S_26234.pdf`): cliente Owens America (RFC VAM140630FL1), producto "BOLSA TERMOENCOGIBLE 60X48", 1470 KG, $52.50/kg — en `PocSalesOrderForm.razor` y `PedidosList.razor`
- [x] T032 Columna "Descripción" de Líneas del Pedido ahora replica el string plano real de CONTPAQi (sin ficha técnica embebida); se agregaron columnas Valor Unit./Importe de solo lectura, e Importe se recalcula en vivo al cambiar Cantidad
- [x] T033 Selector de Unidad (KG/MIL/PZA) en la línea del pedido — demuestra que la unidad de venta es configurable por producto, no fija, per hallazgo del CFDI real (esa línea vende en KG, no en Millares)
- [x] T034 `002-domain-model-redefinition/spec.md`: agregada entidad `PackagingUnit` (conversión a `Product.base_uom = KG` siempre); `SalesOrderLine.requested_qty_kg` ahora es explícitamente un campo calculado, nunca capturado a mano (FR-010c/FR-010d, Nota de validación — revisión 3)
  - **Evidencia**: captura de pantalla real del form con los datos del CFDI; prueba interactiva en navegador (cambié Cantidad de 1470 a 1000, Importe recalculó a $52,500.00 correctamente).

## Notas

- T009 y T010 quedan abiertas deliberadamente — cerrarlas sin haberlas hecho repetiría el problema que esta spec documenta sobre `001-poc-end-to-end-operational-flow`.
- La interfaz `IOperationalWorkflowService` de `contracts/ioperationalworkflowservice.md` no se implementó tal cual — se simplificó a `OperationalFlowState` al descubrir, durante la implementación, que ningún componente `Poc/*` leía `OperationalDataStore` (los componentes ya eran auto-contenidos vía `[Parameter]`). El contrato documentado queda como diseño de referencia para cuando se conecte la API real (`002-domain-model-redefinition`), no como lo que se implementó hoy.
