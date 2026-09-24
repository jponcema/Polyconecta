# Feature Specification: SPEC-010: Vista de Búsqueda — Facetas y Filtros Dinámicos por Modelo

**Feature Branch**: `010-search-view-dynamic-filters`

**Created**: 2026-09-23

**Status**: Draft

**Input**: "El filtro que aparece en el breadcrumb debe responder a lo que sea que se pueda configurar como filtro en la barra de búsqueda, tal como lo hace Odoo. Pero esta configuración de filtro debe ser dinámica por el modelo que estamos mostrando, como lo hace Odoo."

---

## Executive Summary

Hoy cada lista de PolyConecta resuelve su búsqueda a mano: `OdooSearchBox` es un `<input>` y cada pantalla escribe su propio `Where(...)` contra los campos que su autor consideró relevantes. El resultado es que **la búsqueda es distinta en cada pantalla** — Traslados busca por folio, operación y destino; Fabricación por folio, producto y proceso; Recolecciones por folio, operación, OF y destino — sin que el usuario pueda saber por qué, ni cambiarlo.

Cuando apareció el primer filtro real —"ver solo las órdenes de fabricación de este pedido", que llega por el smart button— se resolvió como un chip junto al breadcrumb. Eso confunde dos cosas distintas: **el breadcrumb dice dónde estoy; la barra de búsqueda dice qué estoy viendo**. Un filtro es lo segundo.

Esta especificación define una **vista de búsqueda declarativa por modelo**: cada tipo de documento declara sobre qué campos se busca, qué filtros predefinidos ofrece y por qué campos se puede agrupar. La barra de búsqueda los lee y se configura sola, y todo filtro aplicado —venga del usuario o del contexto de navegación— se muestra como **faceta** removible en un solo lugar.

> **Ya implementado como anticipo**: la faceta de contexto. El filtro por pedido pasó del breadcrumb a la barra de búsqueda (`OdooSearchBox.Facet`). El resto de esta spec está pendiente.

---

## El modelo de Odoo, y por qué aplica aquí

La pieza que falta es la **vista de búsqueda** (`search view`): una definición por modelo, separada de la pantalla, con tres partes.

| Parte | Qué declara | Ejemplo en Órdenes de Fabricación |
| :--- | :--- | :--- |
| **Campos de búsqueda** | Sobre qué se busca al teclear, y con qué etiqueta | Folio · Producto · Proceso · Pedido |
| **Filtros** | Condiciones predefinidas con nombre | En progreso · Mi planta · Con recolección pendiente |
| **Agrupaciones** | Por qué campos se puede agrupar | Proceso · Estado · Planta · Pedido |

Y una regla de combinación que no es arbitraria y conviene respetar desde el principio:

- Dos facetas de **campos distintos** se combinan con **Y** (planta = PIM **y** estado = En progreso).
- Dos valores del **mismo campo** se combinan con **O** (estado = Planeado **o** En progreso).

Eso es lo que hace que agregar filtros amplíe el resultado dentro de una dimensión y lo acote entre dimensiones, que es como la gente espera que funcione.

---

## User Story 1 — Un filtro aplicado se ve y se quita en un solo lugar (Priority: P1)

Como **usuario**, necesito que todo filtro activo aparezca como faceta en la barra de búsqueda, sin importar si lo elegí yo o vino de la navegación.

**Independent Test**: Llegar a la lista de fabricación desde el smart button de un pedido y verificar que el filtro se muestra como faceta removible en la barra de búsqueda, no junto al breadcrumb.

**Acceptance Scenarios**:

1. **Given** una lista abierta con filtro de contexto, **When** el usuario la observa, **Then** ve una faceta con el **campo** y el **valor** filtrado, y el breadcrumb **no** lleva ningún chip de filtro.
2. **Given** una faceta activa, **When** el usuario pulsa su ✕, **Then** el filtro se retira y la lista muestra todos los registros.
3. **Given** un filtro de contexto retirado, **When** el usuario vuelve atrás, **Then** el filtro **no** reaparece solo.
4. **Given** varias facetas activas, **When** se observan, **Then** se distinguen entre sí por su etiqueta de campo.

## User Story 2 — La búsqueda se configura por modelo, no por pantalla (Priority: P1)

Como **responsable del producto**, necesito que cada modelo declare sus campos de búsqueda, filtros y agrupaciones, para que la barra se comporte igual en toda la aplicación y cambiarla no obligue a tocar cada lista.

**Independent Test**: Agregar un campo de búsqueda a la definición de un modelo y verificar que la lista lo usa sin modificar su código.

**Acceptance Scenarios**:

1. **Given** un modelo con su vista de búsqueda declarada, **When** se abre su lista, **Then** la barra ofrece sus filtros y agrupaciones, sin que la pantalla los codifique.
2. **Given** dos modelos distintos, **When** se comparan sus barras, **Then** cada una ofrece lo suyo, con la misma mecánica.
3. **Given** un campo de búsqueda añadido a la declaración, **When** se busca por él, **Then** funciona sin cambios en la lista.
4. **Given** un modelo sin declaración, **When** se abre, **Then** cae a una búsqueda por su campo de referencia, nunca a una pantalla rota.

## User Story 3 — Filtros y agrupaciones predefinidos (Priority: P2)

Como **usuario**, necesito filtros con nombre y poder agrupar, para no tener que recordar qué escribir.

**Acceptance Scenarios**:

1. **Given** la barra abierta, **When** se despliega **Filtros**, **Then** se ven los del modelo y al elegir uno se agrega como faceta.
2. **Given** dos filtros del mismo campo, **When** se aplican, **Then** se combinan con **O**.
3. **Given** dos filtros de campos distintos, **When** se aplican, **Then** se combinan con **Y**.
4. **Given** una agrupación elegida, **When** se aplica, **Then** la lista se agrupa con su encabezado y el conteo por grupo.

## User Story 4 — Búsquedas guardadas (Priority: P3)

Como **usuario frecuente**, necesito guardar una combinación de filtros con un nombre para reutilizarla.

**Acceptance Scenarios**:

1. **Given** varias facetas activas, **When** se guardan con un nombre, **Then** aparecen en **Favoritos** del modelo.
2. **Given** un favorito marcado como predeterminado, **When** se abre la lista, **Then** se aplica solo.
3. **Given** un favorito, **When** se elimina, **Then** desaparece sin afectar la lista actual.

---

## Functional Requirements

- **FR-001**: Cada modelo listable MUST declarar su **vista de búsqueda**: campos buscables con etiqueta, filtros predefinidos y agrupaciones disponibles.
- **FR-002**: La barra de búsqueda MUST configurarse a partir de esa declaración; ninguna lista MUST codificar sus propios campos de búsqueda.
- **FR-003**: Todo filtro activo MUST representarse como **faceta** en la barra de búsqueda, con su campo, su valor y su acción de quitar.
- **FR-004**: El breadcrumb MUST NOT mostrar filtros: expresa ubicación, no selección.
- **FR-005**: Un filtro que llega por el **contexto de navegación** MUST comportarse como cualquier otro: visible y removible.
- **FR-006**: Facetas del mismo campo MUST combinarse con **O**; de campos distintos, con **Y**.
- **FR-007**: El estado de filtros MUST reflejarse en la URL, de modo que una lista filtrada se pueda compartir y sobreviva a una recarga.
- **FR-008**: Al teclear, la búsqueda MUST aplicarse sobre los campos declarados por el modelo, indicando sobre cuáles busca.
- **FR-009**: Un modelo sin declaración MUST caer a búsqueda por su campo de referencia.
- **FR-010**: Las agrupaciones MUST mostrar encabezado por grupo y conteo de registros.
- **FR-011**: Los favoritos MUST guardarse por usuario y modelo, con opción de predeterminado.
- **FR-012**: Los filtros disponibles MUST respetar los permisos del usuario: no ofrecer una dimensión cuyos registros no puede ver (SPEC-009).

## Key Entities

| Entidad | Propósito |
| :--- | :--- |
| `SearchView` | Declaración por modelo: campos, filtros, agrupaciones |
| `SearchField` | Campo buscable con etiqueta y tipo |
| `SearchFilter` | Filtro con nombre y condición |
| `SearchGroupBy` | Campo por el que se puede agrupar |
| `Facet` | Filtro aplicado: campo, valor, origen (usuario o contexto) |
| `SavedSearch` | Combinación guardada por usuario y modelo |

## Success Criteria

- **SC-001**: Un filtro activo siempre se ve en el mismo lugar, venga de donde venga.
- **SC-002**: El breadcrumb no muestra filtros en ninguna pantalla.
- **SC-003**: Agregar un campo de búsqueda a un modelo no requiere tocar su lista.
- **SC-004**: Una lista filtrada puede compartirse por URL y se reconstruye igual.
- **SC-005**: Todas las listas se buscan y filtran con la misma mecánica.

## Fuera de Alcance

- Editor visual de vistas de búsqueda para el usuario final: las declara el desarrollo.
- Filtros por rango de fechas con calendario propio.
- Búsqueda de texto completo sobre documentos adjuntos.

## Dependencias

- **SPEC-009** — FR-012: los filtros ofrecidos dependen de lo que el rol puede ver.
- **SPEC-003 (presentation shell)** — `OdooSearchBox` y las listas son parte de la capa que esta spec generaliza.

## Decisiones validadas con el usuario (2026-09-23)

| # | Pregunta | Decisión |
| :--- | :--- | :--- |
| ① | ¿Alcance inicial? | **Todos los modelos de una vez.** Cada modelo listable declara su vista de búsqueda desde la primera entrega; no hay pantallas con búsqueda a mano conviviendo con las declarativas. |
| ② | ¿El Planner abre su planta ya filtrada? | **No: es regla de fila de SPEC-009.** El alcance por planta es una restricción de visibilidad, no un filtro. La diferencia importa — un filtro se puede quitar, una regla de fila no. |

> La ② delimita la frontera entre ambas specs: **SPEC-009 decide qué registros existen para el usuario; SPEC-010 decide cuáles de esos está mirando.** Un filtro nunca debe poder ampliar lo que una regla de fila restringe.

## Preguntas abiertas

1. **Agrupaciones**: declaradas en el modelo, pendiente el render agrupado en las listas.
2. **Favoritos por usuario**: dependen del modelo de usuarios de SPEC-009.
