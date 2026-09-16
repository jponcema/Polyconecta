# Filosofía de modelado de datos de Odoo

Esta referencia traduce a principios agnósticos de stack la forma en que Odoo diseña sus
entidades de negocio (el "ORM" y sus convenciones), para replicarla en cualquier solución
propia (C#/EF Core, Java/JPA, Django, Rails, etc.) sin depender de Odoo como plataforma.

## 1. Convención de nombres de entidades y campos

- **Entidades**: nombre singular, con un prefijo de dominio/módulo para evitar colisiones y
  agrupar conceptualmente (en Odoo: `sale.order`, `res.partner`, `project.task` — el patrón es
  `dominio.entidad`). En tu solución esto se traduce a namespaces/carpetas por módulo de negocio
  (`Sales.Order`, `Contacts.Partner`, `Projects.Task`) en vez de nombres planos sin agrupar.
- **Campos**: snake_case o camelCase según convención del stack, pero siempre con sufijos que
  delatan el tipo de relación: `_id` para relación uno-a-muchos (FK simple), `_ids` para
  colecciones (uno-a-muchos inverso o muchos-a-muchos). Campos booleanos siempre como pregunta:
  `is_active`, `has_discount`, `is_done` — nunca ambiguos como `status_flag`.
- Toda entidad principal tiene un campo identificador legible para humanos (`name` o
  equivalente), distinto de la clave primaria técnica — la clave técnica nunca se muestra al
  usuario final.

## 2. Campos base que toda entidad de negocio debería tener

Odoo añade estos campos automáticamente a cada modelo; replica el mismo set como una clase
base o mixin de auditoría en tu solución:

| Campo | Propósito |
|---|---|
| Identificador técnico | Clave primaria interna, nunca editable, nunca mostrada como referencia de negocio |
| `name` / identificador legible | Lo que el usuario reconoce (número de pedido, nombre de contacto...) |
| `active` (booleano, default true) | **Archivar en vez de borrar**: nunca hagas DELETE físico de un registro de negocio consultado por otros; márcalo inactivo y fíltralo por defecto en las consultas. Reservar el borrado físico solo para datos verdaderamente descartables sin ninguna referencia |
| `created_at` / `created_by` | Auditoría de creación, poblada automáticamente por la capa de persistencia, nunca editable manualmente |
| `updated_at` / `updated_by` | Auditoría de última modificación, igual de automática |
| `company_id` / `tenant_id` (si la solución es multiempresa o multi-tenant) | Ver sección 5 |
| `sequence` (entero) | Orden manual arrastrable definido por el usuario en listas, independiente del orden de creación |

## 3. Relaciones: filosofía, no solo sintaxis

- **Uno a muchos**: se modela con una FK en el lado "muchos" apuntando al lado "uno" (p. ej.
  `Order.customer_id → Customer`). El lado "uno" expone una colección de solo lectura/navegación
  hacia sus "muchos" (equivalente al `one2many` de Odoo) — nunca se persiste esa colección
  directamente, es siempre el reverso computado de la FK.
- **Muchos a muchos**: tabla puente explícita, incluso si el ORM la abstrae. Nombra la tabla
  puente de forma descriptiva del vínculo (`order_tag_rel`), no genérica.
- **Uno a uno / especialización**: dos patrones válidos, igual que en Odoo:
  - **Herencia clásica (mixin/extensión de campos)**: la entidad hija comparte la misma tabla
    conceptual que la base, añadiendo campos — útil cuando la especialización es solo "más
    campos", sin identidad propia. Equivale a herencia por tabla única o clases parciales.
  - **Herencia por delegación (composición con "es-un" vía referencia)**: la entidad hija tiene
    su propia tabla con una FK a la entidad base y delega en ella los campos comunes — útil
    cuando la especialización necesita su propio ciclo de vida o identidad. Equivale a
    tabla-por-tipo (table-per-type) o "has-a con delegación".
  - Elige el patrón según si la especialización necesita existir independientemente de la base
    (usa delegación) o no (usa extensión simple).
- **Mixins reutilizables**: comportamientos transversales (auditoría, archivado, seguimiento de
  actividad/comentarios, traducción) se definen una sola vez como mixin/trait/clase base
  abstracta y se aplican por composición a cualquier entidad que los necesite — nunca se
  copian y pegan campo por campo en cada entidad nueva.

## 4. Campos calculados y derivados

- Distingue siempre entre **campo calculado en memoria** (se recalcula en cada lectura, nunca
  se persiste — usar cuando el cálculo es barato y depende de datos que cambian seguido) y
  **campo calculado y almacenado** (se persiste y se recalcula solo cuando cambian sus
  dependencias declaradas — usar cuando el cálculo es costoso o se necesita para filtrar/
  ordenar/agregar en consultas).
- Un **campo relacionado** (atajo de solo lectura hacia un campo de una entidad vinculada, p.
  ej. mostrar el email del cliente directamente en el pedido sin duplicar el dato) debe
  declararse explícitamente como tal, nunca duplicarse manualmente por FK y luego sincronizarse
  a mano.

## 5. Multiempresa / multi-tenant

Si la solución sirve a más de una empresa/organización sobre la misma base de datos:
- Cada entidad relevante lleva un campo de "propietario organizacional" (`company_id` /
  `tenant_id`), poblado automáticamente según el contexto del usuario autenticado, nunca
  editable libremente por el usuario final.
- La regla de visibilidad por defecto es: *un registro es visible si no tiene organización
  asignada (dato compartido/global) O si su organización coincide con alguna de las
  organizaciones permitidas del usuario actual* — nunca ocultar por completo los datos
  globales/compartidos.
- Esta regla se aplica como una capa transversal de filtrado (interceptor/query filter global),
  no repitiendo el `WHERE company_id = ...` a mano en cada consulta.

## 6. Seguridad: dos capas separadas, nunca mezcladas

Odoo separa siempre dos conceptos distintos; replica la misma separación:
1. **Permisos por tipo de entidad (a nivel de modelo)**: qué rol puede Crear/Leer/Actualizar/
   Eliminar en cada tipo de entidad, sin importar el registro concreto. Se define como una
   matriz rol × entidad × operación (CRUD), centralizada y auditable, no dispersa en cada
   controlador/endpoint.
2. **Reglas de fila (a nivel de registro)**: dado que un rol ya tiene permiso sobre el tipo de
   entidad, ¿qué registros concretos puede ver/tocar? (p. ej. "solo sus propios documentos",
   "solo los de su organización", "todos si es manager"). Se expresan como filtros/condiciones
   reutilizables por rol y entidad, no como `if` dispersos en el código de negocio.
- Nunca mezcles ambas capas: el permiso de tipo responde "¿puede hacer esta operación en
  general?"; la regla de fila responde "¿puede hacerla sobre *este* registro en particular?".

## 7. Flujos de estado (workflows tipo borrador → confirmado → hecho)

- Todo documento de negocio con ciclo de vida (pedidos, facturas, tareas, aprobaciones) tiene
  un campo de estado explícito, de tipo enumerado cerrado (no texto libre): normalmente
  `borrador → confirmado → en_proceso → hecho`, con variantes como `cancelado` accesible desde
  varios estados intermedios.
- Las transiciones de estado **nunca se hacen por edición directa del campo** desde fuera; se
  exponen como operaciones/métodos explícitos con nombre de acción (`Confirmar()`,
  `Cancelar()`, `Completar()`) que validan precondiciones antes de cambiar el estado (p. ej. no
  se puede confirmar un pedido sin líneas).
- Cada transición debe quedar auditada (quién, cuándo, de qué estado a cuál) — esto es lo que
  en Odoo alimenta el chatter/historial de un documento; en tu solución puede ser una tabla de
  auditoría de cambios de estado por entidad, o un log de eventos de dominio.
- Los campos editables de un documento suelen depender del estado (p. ej. las líneas de un
  pedido dejan de ser editables una vez confirmado) — esta regla vive en la capa de negocio,
  no solo se oculta en el frontend.

## 8. Numeración automática de referencias de negocio

Cuando un documento necesita un código legible único (número de pedido, folio de factura), usa
un generador de secuencias centralizado y configurable (prefijo + relleno de ceros + reinicio
periódico opcional), nunca un `MAX(id)+1` manual ni un GUID como referencia visible al usuario.

## 9. Traducibilidad (si aplica)

Si la solución debe soportar múltiplos idiomas, los campos de texto orientados al usuario final
se marcan explícitamente como traducibles y se almacenan en una tabla de traducciones aparte
(clave: entidad + campo + idioma), nunca como columnas duplicadas por idioma (`name_en`,
`name_es`, `name_fr`) en la tabla principal.

## Principio general
Cada uno de estos patrones existe en Odoo porque resuelve un problema recurrente de integridad,
auditoría o consistencia — no son arbitrarios. Al replicarlos, prioriza siempre el *problema
que resuelven* sobre la *sintaxis específica* de Odoo: el objetivo es una base de datos honesta,
auditable, consistente entre módulos y fácil de razonar, no un clon técnico del ORM de Odoo.
