# Feature Specification: SPEC-007: Rutas y Reglas de Abastecimiento, Visor de Disponibilidad y Autorización de Dos Firmas

**Feature Branch**: `007-procurement-rules-and-authorization`

**Created**: 2026-09-21 · **Revisada**: 2026-09-22

**Status**: Draft

**Input**: "Desde el pedido se debe poder decidir si lo que estoy vendiendo requiere un proceso. Como lo hace Odoo es mediante la configuración de reglas de abastecimiento que te permite configurar que si no hay existencia de un producto te dispara las órdenes de fabricación, si hay entonces no fabrica nada y genera una entrega para lo que exista — pero debe saber Atención a Clientes qué es lo que está ingresando en el detalle."

---

## ⚠️ Revisión del 22-sep-2026 — cambio de enfoque

La primera versión de esta spec modelaba la decisión como un **cálculo en la pantalla del pedido**: un visor de existencias y una resta de disponible contra solicitado. El usuario la corrigió: ese no es el mecanismo de Odoo y además ignoraba el motor Push/Pull que `ARQUITECTURA_ALMACENES_RUTAS_Y_ABASTECIMIENTO.md` ya define en su sección 4.

**La decisión no vive en la pantalla: vive en el catálogo, como regla configurada.** El visor de disponibilidad se conserva, pero degradado a herramienta de consulta — el equivalente del informe de previsión de Odoo — no como el mecanismo que decide.

---

## Executive Summary

Hoy `PocSalesOrderForm.razor` renderiza "Procesos Requeridos" como una tabla fija de checkboxes deshabilitados: vender una bolsa implica siempre extrusión + impresión + bolseo, exista o no material en piso.

Esta especificación sustituye esa lógica por un **motor de reglas de abastecimiento** con tres piezas:

1. **Rutas configurables en tres niveles** (clasificación → producto → línea de pedido), donde cada ruta es una secuencia de reglas encadenadas.
2. **Un motor que las ejecuta al autorizar el pedido**, generando documentos reales — entrega, orden de fabricación, traslado — y encadenando hacia atrás por la receta de cada producto.
3. **Autorización de dos firmas con botón único**, compartido por Comercial y Cobranza, que es lo que dispara el motor.

El comportamiento central es el que Odoo llama **MTSO** (*Make To Stock or Order*, "reabastecer bajo pedido con excepción de stock disponible"): la regla consume primero la existencia y solo dispara la regla siguiente por el faltante. Es distinto del MTO puro, que fabrica siempre.

---

## Decisiones validadas con el usuario

| # | Pregunta | Decisión |
| :--- | :--- | :--- |
| ① | ¿Criterio de sustitución de producto? | **No existe criterio cerrado.** AC es quien primero sabe si un rollo sirve. El sistema no decide: asiste y registra. |
| ② | ¿Restricción al reasignar material entre clientes? | Se resuelve mediante **re-lotificación** explícita. |
| ③ | ¿Cuándo se compromete la reserva? | **Al Autorizar**, y la autorización es de **dos firmas con un único botón** visible para Comercial y Cobranza. |
| ④ | ¿Alcance del visor? | **Por clasificación de producto.** Herramienta de consulta, no mecanismo de decisión. |
| ⑤ | ¿Sobrante por lote entero? | **Reasignación de cantidades (kg) por lote**, soportada en CONTPAQi. |
| ⑥ | ¿Dónde se configura la ruta? | **En varios niveles con herencia**: clasificación → producto → línea. Flexible en el momento de la orden de venta. |
| ⑦ | ¿AC puede forzar fabricación habiendo existencia? | **Sí. AC orquesta la fabricación.** La ruta elegida en la línea manda sobre toda la configuración. |
| ⑧ | ¿Política de entrega parcial? | **La entrega se genera por el total.** Al entregar en parcialidades el sistema pregunta si crear backorder por el restante. |
| ⑨ | ¿Quién decide el traspaso interplanta? | **Atención a Clientes**, cuando detecta que hay rollo en PIM que puede irse a SC a producir. |

---

## Modelo: rutas, reglas y encadenamiento

Una **ruta** es una secuencia ordenada de **reglas**. Cada regla declara una acción, una ubicación origen, una destino y el tipo de operación que genera. Lo que una regla no cubre se convierte en la necesidad que resuelve la siguiente.

| Ruta | Reglas en orden |
| :--- | :--- |
| `MTSO-BOL` | 1. Entregar de `SC/Stock/PT` → `Customers` · 2. Fabricar (`STC-BOL-MO`) |
| `MTSO-IMP` | 1. Entregar de `SC/Stock/MP` → `SC/Produccion` · 2. Fabricar (`STC-IMP-MO`) |
| `MTSO-EXT` | 1. Entregar de `SC/Stock/MP` · 2. **Traspasar de `PIM/Stock/Rollos`** · 3. Fabricar (`PIM-MO`) |
| `MTO-BOL` | 1. Fabricar siempre — ignora la existencia |
| `MP-STOCK` | 1. Consumir de `PIM/Stock/MP` → `PIM/Produccion` |
| `STOCK` | 1. Solo entregar de stock; no fabrica |

Una regla de fabricación **vuelve a lanzar necesidades** por los componentes del producto, que a su vez se resuelven con la ruta de cada componente. De ahí sale la cascada completa:

```
Bolsa 20,000 MIL (hay 3,000)
├─ ENTREGAR   3,000 MIL   SC/Stock/PT → Customers
└─ FABRICAR  17,000 MIL   OF-BOL
   ├─ ENTREGAR   180 kg   rollo impreso existente
   └─ FABRICAR   245 kg   OF-IMP
      ├─ TRASPASAR 200 kg  PIM/Stock/Rollos → SC/Stock/MP   ← no se extruye: ya existe
      └─ FABRICAR   45 kg  OF-EXT
         └─ PACT 30.6 · GA502022 13.5 · MP0032 0.9 desde PIM/Stock/MP
```

La regla de traspaso es lo que evita fabricar de más: si el rollo maestro existe en otra planta, el motor lo mueve en vez de extruirlo.

### Resolución en tres niveles

La ruta aplicable se resuelve en cascada, **de lo más específico a lo más general**:

`línea del pedido` → `producto` → `clasificación de producto` → `predeterminada`

El nivel de línea es donde AC orquesta: lo que elija ahí gana sobre toda la configuración del catálogo, y la pantalla muestra en qué nivel quedó resuelta.

---

## User Story 1 — El proceso es el residuo, no un atributo del producto (Priority: P1)

Como **Atención a Clientes**, necesito que al autorizar un pedido el sistema decida por regla si hay que fabricar, entregar o traspasar, para que un producto con existencia no dispare órdenes de fabricación innecesarias.

**Independent Test**: Capturar una línea de rollo maestro con existencia suficiente, autorizar, y verificar que no se generó ninguna OF.

**Acceptance Scenarios**:

1. **Given** 200 kg disponibles de rollo maestro y una línea de 150 kg, **When** se autoriza, **Then** se genera **un solo documento** de movimiento y **ninguna** orden de fabricación.
2. **Given** 3,000 MIL de bolsa en stock y una línea de 20,000, **When** se autoriza, **Then** se genera una entrega por 3,000 y una OF-BOL por 17,000 — no por 20,000.
3. **Given** la OF-BOL generada, **When** se consulta el plan, **Then** el rollo impreso existente se consume antes de lanzar la OF-IMP, y esta se lanza solo por el faltante.
4. **Given** rollo maestro disponible en PIM y necesidad en SC, **When** se resuelve la necesidad, **Then** la regla aplicable es el **traspaso**, no la extrusión.
5. **Given** un producto sin ruta que cubra la cantidad, **When** se planifica, **Then** la necesidad **queda expuesta** en el plan como sin cobertura, en lugar de desaparecer en silencio.

---

## User Story 2 — AC ve qué está ingresando (Priority: P1)

Como **Atención a Clientes**, necesito ver mientras capturo el detalle qué disponibilidad hay y qué documentos generará el pedido, sin que eso comprometa inventario.

**Why this priority**: Es la mitad del requerimiento del usuario. El motor puede ser correcto y aun así el pedido se captura a ciegas si AC no ve el efecto.

**Independent Test**: Cambiar la cantidad de la línea y verificar que la vista previa recalcula sin reservar nada.

**Acceptance Scenarios**:

1. **Given** una línea en captura, **When** AC la consulta, **Then** ve disponible, entrante y solicitado, y la **vista previa** de los documentos que se generarían.
2. **Given** el pedido en Borrador o Confirmado, **When** se recalcula la vista previa, **Then** **ninguna existencia queda reservada** — está marcada explícitamente como simulación.
3. **Given** el pedido autorizado, **When** AC lo consulta, **Then** la misma tabla muestra los documentos reales con su folio, y los smart buttons reflejan lo que el motor generó, no una lista fija.
4. **Given** una línea, **When** AC consulta la ruta, **Then** ve cuál se está aplicando y **en qué nivel quedó definida**.

---

## User Story 3 — AC orquesta la fabricación (Priority: P1)

Como **Atención a Clientes**, necesito poder forzar la ruta de una línea concreta — fabricar aunque haya existencia, o entregar sin fabricar — porque hay razones comerciales que el catálogo no conoce.

**Independent Test**: Asignar `MTO-BOL` a una línea con existencia y verificar que se fabrica el total.

**Acceptance Scenarios**:

1. **Given** 3,000 MIL en stock y una línea de 20,000 con ruta `MTO-BOL`, **When** se planifica, **Then** se fabrican **20,000** y no se genera entrega desde stock de PT.
2. **Given** una ruta elegida en la línea, **When** existe también una excepción por producto, **Then** **gana la de la línea**.
3. **Given** una ruta de línea que se retira, **When** se vuelve a resolver, **Then** se hereda de nuevo del producto o de la clasificación.
4. **Given** una ruta forzada, **When** se consulta el pedido, **Then** queda visible que fue una decisión de línea y no la configuración por defecto.

---

## User Story 4 — Autorización de dos firmas con botón único (Priority: P1)

Como **responsable de Comercial o de Cobranza**, necesito un único botón "Autorizar" que registre mi firma, y que el pedido solo avance cuando ambas áreas hayan firmado.

**Why this priority**: Es el disparador del motor. Supersede los botones separados de Ventas y Crédito descritos en SPEC-001.

**Independent Test**: Firmar con un rol, verificar que el pedido sigue en Confirmado, firmar con el otro y verificar que pasa a Autorizado y se generan los documentos.

**Acceptance Scenarios**:

1. **Given** un pedido Confirmado, **When** Comercial presiona Autorizar, **Then** queda registrada su firma, el contador muestra 1/2 y el pedido **sigue en Confirmado**.
2. **Given** una firma registrada, **When** el mismo rol vuelve a presionar, **Then** **no se registra una segunda firma** y el botón queda deshabilitado para él.
3. **Given** un usuario con rol distinto de Comercial o Cobranza, **When** abre el pedido, **Then** el botón está deshabilitado.
4. **Given** la primera firma puesta, **When** Cobranza firma, **Then** el pedido pasa a **Autorizado** y el motor ejecuta las rutas generando los documentos con folio real.
5. **Given** un pedido autorizado, **When** se revoca la autorización, **Then** se limpian las firmas, **se liberan las reservas** y se descartan los documentos generados.
6. **Given** el pedido en cualquier estado, **When** se consulta, **Then** se ve qué áreas firmaron, quién firmó y cuál falta.

---

## User Story 5 — Visor de disponibilidad (Priority: P2)

Como **Atención a Clientes**, necesito consultar el inventario agrupado por clasificación de producto cuando quiero investigar qué hay, sin depender de la línea del pedido.

**Why this priority**: Deja de ser el mecanismo de decisión y pasa a ser herramienta de consulta, equivalente al informe de previsión de Odoo. Sigue siendo útil, pero no bloquea nada.

**Acceptance Scenarios**:

1. **Given** el visor abierto, **When** AC filtra por clasificación, **Then** ve físico, reservado, en WIP, disponible y entrante con fecha, por SKU y ubicación.
2. **Given** un SKU, **When** AC lo expande, **Then** ve sus lotes con cantidad, estado y el documento que los tiene comprometidos.
3. **Given** material en cuarentena o scrap, **When** se calcula el disponible, **Then** queda **excluido** del material vendible.
4. **Given** un producto usado antes como sustituto, **When** AC lo consulta, **Then** ve el conteo histórico como **referencia informativa**, nunca como recomendación del sistema.

---

## Functional Requirements

- **FR-001**: El sistema MUST exponer por SKU y ubicación: **físico**, **reservado**, **en WIP**, **disponible** y **entrante** con fecha.
- **FR-002**: El sistema MUST permitir definir **rutas** compuestas de reglas ordenadas, cada una con acción, origen, destino y tipo de operación.
- **FR-003**: El sistema MUST resolver la ruta aplicable en cascada **línea → producto → clasificación → predeterminada**, y MUST mostrar en qué nivel se resolvió.
- **FR-004**: Una ruta MTSO MUST consumir la existencia disponible antes de disparar la regla siguiente, y disparar esta **solo por el faltante**.
- **FR-005**: Una ruta MTO MUST ignorar la existencia y disparar la fabricación por la cantidad completa.
- **FR-006**: Una regla de fabricación MUST relanzar necesidades por los componentes del producto, resolviéndolas con la ruta de cada componente.
- **FR-007**: El motor MUST ejecutarse en la transición a **Autorizado** y MUST generar documentos con folio real: entrega, orden de fabricación o traslado.
- **FR-008**: Antes de autorizar, el sistema MUST ofrecer una **simulación** que no reserve existencia, identificada visiblemente como tal.
- **FR-009**: AC MUST poder asignar una ruta a la línea del pedido, y esa elección MUST prevalecer sobre producto y clasificación.
- **FR-010**: La autorización MUST requerir **dos firmas** (Comercial y Cobranza) recogidas mediante **un único botón**; el pedido MUST permanecer en Confirmado hasta tener ambas.
- **FR-011**: Un mismo rol MUST NOT poder firmar dos veces; un rol no autorizador MUST NOT poder firmar.
- **FR-012**: Revocar la autorización MUST limpiar las firmas, liberar las reservas y descartar los documentos generados.
- **FR-013**: La reserva MUST hacerse lote por lote, fraccionando el último por **reasignación de cantidad** en vez de tomarlo completo.
- **FR-014**: La entrega MUST generarse por el **total** de la línea. Al entregar parcialmente, el sistema MUST preguntar si crear **backorder** por el restante.
- **FR-015**: Una necesidad sin cobertura MUST quedar **visible en el plan**, nunca descartarse en silencio.
- **FR-016**: El disponible MUST excluir cuarentena y scrap.
- **FR-017**: El sistema MUST NOT proponer ni aplicar sustituciones de SKU automáticamente; toda sustitución MUST ser explícita, con motivo e identidad registrados.

## Key Entities

| Entidad | Propósito |
| :--- | :--- |
| `ProcurementRoute` | Secuencia de reglas; marca si consume existencia primero (MTSO) o no (MTO) |
| `ProcurementRule` | Acción, origen, destino, tipo de operación, orden de aplicación |
| `RouteAssignment` | Asignación por clasificación, por producto o por línea |
| `ProcurementNeed` | Necesidad a resolver: producto, cantidad, ubicación, nivel de encadenamiento |
| `ProcurementDocument` | Documento generado por una regla, con su nivel en la cascada |
| `ProcurementPlan` | Resultado completo: simulado o ejecutado |
| `StockAvailability` | Proyección de las cinco cifras |
| `StockReservation` | Compromiso firme lote ↔ documento |
| `SubstitutionDecision` | Evidencia de sustitución: SKU vendido, lote usado, motivo, autor |
| `LotReassignment` | Fraccionamiento de cantidad entre lotes |

## Success Criteria

- **SC-001**: Un pedido de producto con existencia suficiente se autoriza **sin generar ninguna orden de fabricación**.
- **SC-002**: Un pedido con existencia parcial genera entrega por lo que hay y fabricación **solo por el faltante**.
- **SC-003**: Cambiar la ruta de un producto cambia el comportamiento del pedido **sin tocar código**.
- **SC-004**: AC puede prever los documentos de un pedido antes de comprometer inventario.
- **SC-005**: Ningún pedido avanza a Autorizado con una sola firma.
- **SC-006**: Dos pedidos distintos no pueden comprometer el mismo lote.
- **SC-007**: Existiendo rollo en otra planta, el sistema traslada en vez de fabricar.

## Fuera de Alcance

- **Compra de materia prima**: `MP-STOCK` consume del almacén; si no alcanza, la necesidad queda expuesta. El abastecimiento por compra está diferido en el roadmap.
- **Catálogo formal de sustitución por atributos** (ancho/calibre/material con tolerancias): depende de acumular evidencia vía `SubstitutionDecision`.
- **Reglas por cliente o por almacén de despacho**: hoy la ruta depende del producto y de la línea, no del destinatario.

## Dependencias

- **SPEC-001** — la autorización de dos firmas con botón único **supersede** los botones separados de Ventas y Crédito descritos ahí.
- **`ARQUITECTURA_ALMACENES_RUTAS_Y_ABASTECIMIENTO.md`** — este motor es la implementación concreta de las reglas Push/Pull de su sección 4; los tipos de operación de las reglas deben coincidir con su sección 5.
- **SPEC-008** — las OF generadas por el motor son las que luego emiten su surtido a WIP.
- **Matriz de pruebas CONTPAQi** — bloque F (lectura de existencias) sostiene FR-001; bloque C (lotes) sostiene FR-013.
