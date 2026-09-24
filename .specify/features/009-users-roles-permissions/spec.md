# Feature Specification: SPEC-009: Usuarios, Roles y Permisos

**Feature Branch**: `009-users-roles-permissions`

**Created**: 2026-09-23

**Status**: Draft — en construcción, requiere validación con la operación

**Input**: "Vamos a ir creando la especificación que defina los usuarios, roles y permisos por rol."

---

## Executive Summary

Hoy el sistema no tiene noción de quién está operando. El prototipo muestra un nombre fijo en la barra superior y las acciones sensibles —autorizar un pedido, validar una recolección, aprobar calidad, cerrar una orden— están disponibles para cualquiera que abra la pantalla. Eso rompe tres cosas que la operación ya declaró obligatorias:

1. **La autorización de dos firmas** (SPEC-007) no significa nada si la misma persona puede pulsar el botón dos veces.
2. **La recolección** (SPEC-008) se apoya en que *Almacén* declara lo que sale y *Producción* solo lo solicita; hoy ambos son el mismo usuario anónimo.
3. **El hard-stop de calidad** (Principio IV de la Constitución) exige que la liberación la firme Calidad, no quien tenga la pantalla abierta.

Esta especificación define el modelo de **usuarios, roles y permisos** que cierra esos tres huecos, extendiendo la seguridad en dos capas que SPEC-002 ya dejó planteada (FR-024 y FR-025) hasta el nivel de acción concreta.

> **Estado del documento.** Las secciones de roles y de matriz reflejan lo que se puede derivar del código, de los mockups y de los documentos de arquitectura ya validados. Todo lo que **no** puede derivarse está marcado como pregunta abierta al final; ninguna de esas se ha resuelto por supuesto.

---

## Modelo

### Las tres piezas

| Pieza | Qué es | Ejemplo |
| :--- | :--- | :--- |
| **Usuario** | Una persona con credenciales | Celia Villarreal |
| **Rol** | Un conjunto de permisos, no una persona | Atención a Clientes |
| **Asignación** | Usuario × Rol × Planta | Celia · AC · PIM |

Un usuario puede tener **varios roles**, y la asignación es **por planta**: el Planner de PIM y el de Santa Cruz son el mismo rol con distinto alcance. Esto evita duplicar roles por planta (`Planner-PIM`, `Planner-STC`) y hace que el alcance sea dato, no catálogo.

### Seguridad en dos capas (extiende SPEC-002 FR-024/FR-025)

- **Capa 1 — permisos por acción**: qué puede *hacer* un rol sobre un tipo de documento. Es una matriz declarativa.
- **Capa 2 — reglas de fila**: sobre *qué registros* puede hacerlo. Se expresan como filtros reutilizables por rol, nunca como condicionales dispersos.

La distinción importa: "el Planner puede confirmar órdenes de fabricación" es capa 1; "solo las de su planta" es capa 2. Mezclarlas es lo que produce lógica de permisos regada por toda la aplicación.

---

## Roles

Derivados de la operación observada en el repositorio, los mockups y `ARQUITECTURA_ALMACENES_RUTAS_Y_ABASTECIMIENTO.md`:

| Rol | Responsabilidad | Titulares observados |
| :--- | :--- | :--- |
| **Atención a Clientes** | Captura y confirma el pedido; orquesta qué procesos requiere la venta | Celia Villarreal (Agente) |
| **Comercial** | Firma la autorización comercial del pedido | — |
| **Crédito y Cobranza** | Firma la autorización de crédito | — |
| **Planner** | Configura componentes, confirma la OF, programa centros de trabajo, cierra producción | Roosvelt (PIM), Diana (SC) |
| **Almacenista** | Declara y valida lo que sale del almacén: recolecciones, traslados, recepciones | — |
| **Calidad** | Aprueba o rechaza lotes; único que puede levantar el hard-stop | — |
| **Logística / Tráfico** | Valida entregas a cliente y traspasos interplanta | — |
| **Operador de piso** | Captura pesaje e incidencias a pie de máquina | Alejandro Varela, Alfonso Ortega |
| **Administrador** | Configura catálogos, ubicaciones, tipos de operación y asignaciones de rol | — |

> Los titulares vacíos son roles que la operación ejerce hoy pero cuyo responsable nominal no consta en el repositorio. Hay que completarlos con la operación.

---

## Capa 1 — Matriz de permisos por acción

Leyenda: **C** crear · **L** leer · **E** editar · **T** transicionar (la acción que mueve el estado) · **—** sin acceso

### Pedido de venta

| Rol | Leer | Confirmar | Firmar Comercial | Firmar Cobranza | Revocar autorización | Cancelar |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: |
| Atención a Clientes | L | T | — | — | — | T |
| Comercial | L | — | **T** | — | ◐ | — |
| Crédito y Cobranza | L | — | — | **T** | ◐ | — |
| Planner | L | — | — | — | — | — |
| Logística / Tráfico | L | — | — | — | — | — |
| Administrador | L | T | — | — | T | T |

> El pedido **no se crea** en PolyConecta: nace de la sincronización con CONTPAQi (SPEC-001). Por eso no hay columna "Crear".
> ◐ = pendiente de decidir (pregunta ③).

### Orden de fabricación

| Rol | Leer | Editar componentes | Confirmar | Programar WO | Cerrar producción | Cancelar |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: |
| Atención a Clientes | L | — | — | — | — | — |
| Planner | L | E | **T** | E | **T** | T |
| Almacenista | L | — | — | — | — | — |
| Calidad | L | — | — | — | — | — |
| Operador de piso | L | — | — | — | — | — |
| Administrador | L | E | T | E | T | T |

### Recolección y operaciones de inventario

| Rol | Leer | Declarar lotes | Comprobar disponibilidad | Validar | Emitir devolución | Cancelar |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: |
| Planner | L | — | L | — | T | — |
| **Almacenista** | L | **E** | L | **T** | T | T |
| Logística / Tráfico | L | E | L | T | — | T |
| Calidad | L | — | — | — | — | — |
| Administrador | L | E | L | T | T | T |

> **El Planner no puede validar su propia solicitud.** Es la separación que da sentido a la recolección: Producción pide, Almacén da salida. Es también la regla que hoy no existe en el código (SPEC-008 FR-003 la declara, pero sin roles no puede aplicarse).

### Calidad

| Rol | Leer | Registrar inspección | Aprobar lote | Rechazar a cuarentena |
| :--- | :---: | :---: | :---: | :---: |
| **Calidad** | L | C | **T** | **T** |
| Planner | L | — | — | — |
| Operador de piso | L | C | — | — |
| Administrador | L | C | T | T |

> El hard-stop del Principio IV depende de esta fila: **ningún otro rol puede aprobar un lote**, ni siquiera el Administrador para saltarse un rechazo vigente.

### Lotes e inventario

| Rol | Ver disponibilidad | Capturar pesaje | Re-lotificar / fraccionar | Sustituir producto |
| :--- | :---: | :---: | :---: | :---: |
| Atención a Clientes | L | — | — | ◐ |
| Planner | L | E | E | — |
| Almacenista | L | — | E | — |
| Operador de piso | — | E | — | — |
| Calidad | L | — | — | — |
| Administrador | L | E | E | E |

### Configuración

| Rol | Catálogos | Ubicaciones y almacenes | Tipos de operación | Usuarios y roles |
| :--- | :---: | :---: | :---: | :---: |
| Administrador | C/E | C/E | C/E | C/E |
| Todos los demás | L | L | L | — |

---

## Capa 2 — Reglas de fila

| # | Regla | Racional |
| :--- | :--- | :--- |
| RF-1 | Un **Planner** solo ve y edita órdenes de fabricación de **su planta**. | Ya declarada en SPEC-002. |
| RF-2 | Un **Almacenista** solo valida operaciones cuyo **almacén origen** pertenece a su planta. | El almacenista de PIM no puede dar salida de `SC/Stock/MP`. |
| RF-3 | **Comercial** solo escribe su propia firma; **Cobranza** solo la suya. | Ninguno puede firmar por el otro. |
| RF-4 | **Ningún usuario puede aportar las dos firmas del mismo pedido**, aunque tenga ambos roles. | Segregación de funciones: es lo que hace que "dos firmas" signifique algo. |
| RF-5 | El **Operador de piso** no tiene acceso directo al sistema; captura por terminal mediada. | Principio VI de la Constitución. |
| RF-6 | Las acciones sobre documentos en estado **Hecho** quedan cerradas para todos los roles. | Un documento validado se corrige con un documento inverso, nunca editándolo. |

**RF-4 es la regla central de esta spec.** Sin ella, el botón único de autorización es decorativo: la misma persona lo pulsa dos veces y el pedido avanza. Hoy el prototipo hace exactamente eso, porque no hay usuario.

---

## User Story 1 — Identidad y sesión (Priority: P1)

Como **usuario del sistema**, necesito autenticarme para que cada acción quede atribuida a mí y el sistema sepa qué puedo hacer.

**Acceptance Scenarios**:

1. **Given** un usuario autenticado, **When** ejecuta cualquier transición de estado, **Then** queda registrado quién y cuándo, de forma consultable.
2. **Given** un usuario sin el rol requerido, **When** abre una pantalla con una acción que no le corresponde, **Then** la acción aparece **deshabilitada**, no oculta, con la razón visible.
3. **Given** un usuario con roles en dos plantas, **When** opera, **Then** ve solo los registros de las plantas donde tiene asignación.

## User Story 2 — Segregación de las dos firmas (Priority: P1)

Como **responsable de control interno**, necesito que las dos firmas de autorización provengan de dos personas distintas.

**Acceptance Scenarios**:

1. **Given** un pedido Confirmado, **When** firma Comercial, **Then** el botón queda deshabilitado **para esa persona** y el pedido sigue en Confirmado.
2. **Given** un usuario con ambos roles, **When** intenta aportar la segunda firma del mismo pedido, **Then** el sistema **lo impide** y explica por qué.
3. **Given** las dos firmas de personas distintas, **When** se completa la segunda, **Then** el pedido pasa a Autorizado y ambas firmas constan con nombre y fecha.

## User Story 3 — Producción pide, Almacén entrega (Priority: P1)

Como **Almacenista**, necesito ser el único que pueda validar la salida de material de mi almacén.

**Acceptance Scenarios**:

1. **Given** una recolección liberada, **When** el Planner la abre, **Then** puede consultarla y comprobar disponibilidad, pero **no** declarar lotes ni validarla.
2. **Given** la misma recolección, **When** la abre el Almacenista de esa planta, **Then** puede declarar lotes y validar.
3. **Given** un Almacenista de otra planta, **When** la abre, **Then** solo puede leerla (RF-2).

## User Story 4 — Hard-stop de calidad (Priority: P1)

Como **responsable de Calidad**, necesito ser el único que pueda liberar un lote.

**Acceptance Scenarios**:

1. **Given** un lote sin liberación, **When** cualquier rol distinto de Calidad intenta aprobarlo, **Then** la acción no está disponible.
2. **Given** un lote rechazado, **When** el Administrador intenta despacharlo, **Then** el hard-stop **se mantiene**: ningún rol lo levanta salvo una nueva liberación de Calidad.

---

## Functional Requirements

- **FR-001**: El sistema MUST autenticar a cada usuario y atribuirle toda acción que ejecute.
- **FR-002**: Los permisos MUST expresarse como matriz declarativa **rol × documento × acción**, separada del código de negocio.
- **FR-003**: Las reglas de visibilidad por registro MUST expresarse como filtros reutilizables por rol, nunca como condicionales dispersos (SPEC-002 FR-025).
- **FR-004**: Un usuario MUST poder tener varios roles, y toda asignación MUST llevar **alcance por planta**.
- **FR-005**: El sistema MUST impedir que un mismo usuario aporte las dos firmas de autorización del mismo pedido, aunque posea ambos roles.
- **FR-006**: El rol Planner MUST NOT poder validar una recolección; el rol Almacenista MUST ser el único que declare lotes y valide la salida de su almacén.
- **FR-007**: Solo el rol Calidad MUST poder aprobar o rechazar un lote; ningún rol, incluido Administrador, MUST poder levantar un rechazo vigente sin una nueva liberación.
- **FR-008**: Las acciones no permitidas MUST mostrarse **deshabilitadas con su razón**, no ocultas — el usuario debe entender que existe y por qué no puede.
- **FR-009**: Toda transición de estado MUST registrar usuario, rol ejercido, fecha y documento afectado, de forma consultable.
- **FR-010**: Los documentos en estado terminal MUST quedar cerrados a edición para todos los roles; la corrección MUST hacerse con un documento inverso.

## Key Entities

| Entidad | Propósito |
| :--- | :--- |
| `User` | Persona con credenciales, activa o archivada |
| `Role` | Conjunto nombrado de permisos |
| `RoleAssignment` | Usuario × Rol × Planta (alcance) |
| `Permission` | Rol × tipo de documento × acción |
| `RecordRule` | Filtro de fila reutilizable, asociado a un rol |
| `AuditEntry` | Quién, qué, cuándo, sobre qué documento |

## Success Criteria

- **SC-001**: Ninguna acción sensible puede ejecutarse sin usuario autenticado.
- **SC-002**: Un pedido no puede autorizarse con las dos firmas de la misma persona.
- **SC-003**: Un Planner no puede dar salida a material del almacén.
- **SC-004**: Un lote rechazado por Calidad no puede despacharse por ningún rol.
- **SC-005**: Toda transición es atribuible a una persona concreta.
- **SC-006**: Cambiar los permisos de un rol no requiere tocar código de negocio.

## Fuera de Alcance

- Flujo de requisición de tres firmas (Solicitante / Autorizador / Elaborador) — diferido a Fase 2 por la Constitución.
- Federación de identidad con CONTPAQi: los usuarios de PolyConecta son propios (ver pregunta ①).
- Permisos a nivel de campo individual.

## Dependencias

- **SPEC-002** — extiende FR-024 y FR-025 (seguridad en dos capas) hasta el nivel de acción.
- **SPEC-007** — la segregación de firmas (RF-4) es lo que hace efectivo su botón único de autorización.
- **SPEC-008** — FR-003 (solo Almacén valida) depende enteramente de esta spec.
- **Constitución, Principio IV** — hard-stop de calidad; **Principio VI** — captura mediada del operador.

---

## Preguntas abiertas

1. **Autenticación**: ¿usuarios propios de PolyConecta, directorio corporativo (AD/LDAP/SSO), o reutilizar los de CONTPAQi? Determina si hay que construir gestión de contraseñas.
2. **Titulares de rol**: ¿quiénes son hoy Comercial, Crédito y Cobranza, Almacenista, Calidad y Tráfico? En el repositorio solo constan AC, los Planners y dos operadores.
3. **Revocar autorización**: ¿quién puede hacerlo — cualquiera de los dos firmantes, solo Administrador, o nadie una vez que hay documentos generados?
4. **Segregación de firmas**: ¿existe hoy alguna persona que ejerza Comercial y Cobranza a la vez? Si la hay, RF-4 la bloquea y hay que preverlo antes de implementarla.
5. **Alcance del Planner**: ¿un Planner puede ver las órdenes de la otra planta en modo lectura, o no debe verlas en absoluto?
6. **Almacenista por almacén**: ¿el alcance es por planta o más fino, por almacén (MP distinto de PT)?
7. **Sustitución de producto** (SPEC-007): ¿la puede decidir cualquier AC, o requiere una segunda autorización?
8. **Suplencias**: ¿qué ocurre cuando el titular de un rol falta — hay suplente designado, o el Administrador reasigna temporalmente?
9. **Operador de piso**: ¿se confirma que no tendrá acceso propio, o la terminal de báscula tendrá login propio para atribuir el pesaje a quien lo captura?
