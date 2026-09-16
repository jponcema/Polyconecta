---
name: odoo-model-philosophy
description: >
  Aplica la filosofía de modelado de datos de Odoo (diseño de entidades, herencia y mixins,
  campos base de auditoría/archivado, relaciones, campos calculados, seguridad en dos capas
  -permisos por tipo de entidad y reglas de fila-, multiempresa, numeración automática de
  referencias, y flujos de estado tipo borrador→confirmado→hecho) a cualquier solución propia,
  sin importar el lenguaje o framework (C#/EF Core, Java, Python, Node, etc.). Úsala SIEMPRE que
  el usuario pida diseñar o revisar el modelo de datos, las entidades, el esquema de base de
  datos, las reglas de seguridad/permisos, el soporte multiempresa, o los workflows de estado de
  su proyecto "estilo Odoo". También aplica para un diagrama entidad-relación con ese enfoque.
  Es la skill correcta para decisiones de backend/datos — para interfaz visual usa odoo-uxui, y
  para navegación/arquitectura de pantallas usa odoo-ux-structure o odoo-design-system.
---

# Filosofía de modelado de datos estilo Odoo

Esta skill traduce los principios con los que Odoo diseña sus entidades de negocio a un
conjunto de reglas agnósticas de stack, para que cualquier solución propia (sin importar el
lenguaje) tenga la misma solidez conceptual: auditoría consistente, archivado en vez de
borrado, relaciones bien tipadas, seguridad en dos capas separadas, soporte multiempresa
opcional, y workflows de estado explícitos y auditables.

## Regla crítica: Odoo es solo inspiración conceptual, nunca aparece en el entregable

Igual que con las skills de diseño visual y de estructura de UX, esta skill usa la filosofía de
modelado de Odoo como **referencia de razonamiento interno**, nunca como algo a exponer en el
resultado. En cualquier entregable (nombres de entidad, nombres de campo, nombres de tabla,
comentarios de código, migraciones, documentación técnica del proyecto):

- **Nunca** escribas la palabra "Odoo" ni nombres de modelos técnicos reales de Odoo
  (`res.partner`, `sale.order`, `mail.thread`, etc.) — usa siempre nombres propios del dominio
  del usuario.
- Los nombres de mixins/clases base deben ser genéricos y descriptivos de lo que hacen
  (`AuditableEntity`, `ArchivableEntity`, `TrackedEntity`, `StatefulDocument`), nunca calcos de
  nombres internos de Odoo.
- Es agnóstica de lenguaje y motor de base de datos: el resultado son principios de diseño,
  esquemas conceptuales y, cuando se pida, clases/entidades concretas en el stack del usuario —
  nunca modelos Python/ORM reales de Odoo.

## Flujo de trabajo

### 1. Cargar la referencia
Lee `references/odoo-model-philosophy.md` antes de proponer cualquier modelo de datos.
Contiene las 9 áreas: convención de nombres, campos base de auditoría/archivado, filosofía de
relaciones (incluye los dos patrones de herencia/especialización y los mixins reutilizables),
campos calculados/derivados, multiempresa, seguridad en dos capas, flujos de estado, numeración
automática, y traducibilidad.

### 2. Entender el alcance del encargo
Antes de diseñar, identifica qué partes aplican:
- ¿Qué entidades de negocio necesita el dominio y cómo se relacionan entre sí?
- ¿Hay especializaciones/variantes de una misma entidad? → decide entre extensión de campos o
  delegación (sección 3 de la referencia), y justifica la elección en una línea.
- ¿La solución es multiempresa o multi-tenant? Si no se ha dicho explícitamente pero el dominio
  lo sugiere (SaaS con varios clientes, por ejemplo), pregúntalo — cambia el modelo de forma
  importante.
- ¿Hay documentos con ciclo de vida (algo que pasa de borrador a confirmado a hecho)? Si sí,
  diseña el campo de estado y sus transiciones explícitas, no un campo de texto libre.
- ¿Se necesita seguridad por rol? Diseña primero la matriz de permisos por tipo de entidad, y
  después, solo si aplica, las reglas de fila.

Si algo es ambiguo pero no bloqueante, asume lo más razonable siguiendo los principios de la
referencia y dilo en una línea; pregunta solo si la ambigüedad (p. ej. multiempresa sí/no)
cambiaría sustancialmente el diseño.

### 3. Diseñar el modelo
- Aplica los campos base de auditoría/archivado a toda entidad de negocio relevante (no a
  tablas puramente técnicas/de configuración estática si no lo necesitan).
- Define las relaciones con la sintaxis/convenciones propias del stack del usuario, pero
  siguiendo la filosofía de la sección 3 de la referencia.
- Si hay comportamientos transversales repetidos (auditoría, archivado, seguimiento de
  actividad, traducción), factorízalos como mixins/clases base reutilizables — no los repitas
  campo por campo en cada entidad.
- Para workflows, define el enumerado de estados cerrado y las operaciones de transición como
  métodos con nombre de acción y validación de precondiciones, no como edición libre del campo.

### 4. Producir el resultado
- Documentación/lineamientos de diseño de datos: siempre en texto claro, con las entidades y
  sus relaciones explicadas.
- Si se pide, genera clases/entidades concretas en el stack del usuario (por ejemplo, clases
  C# con anotaciones de EF Core, o el ORM que corresponda), aplicando los mismos principios y
  respetando la regla crítica de no mencionar Odoo.
- Si se pide un diagrama entidad-relación, constrúyelo en formato Mermaid (`erDiagram`) y
  muéstralo como diagrama renderizado (usa el visualizador de diagramas de esta sesión, o un
  bloque ```mermaid``` si el entregable es un documento Markdown/artefacto publicado) — nunca
  como texto plano sin renderizar cuando hay forma de mostrarlo visualmente.

### 5. Explica las decisiones, brevemente
Después del modelo/diagrama, resume en pocas líneas: qué patrón de herencia se usó y por qué,
si se aplicó multiempresa y cómo, y qué entidades llevan flujo de estado explícito. No hace
falta un ensayo — el modelo/diagrama es la entrega principal.

## Errores comunes a evitar
- Mencionar "Odoo" o nombres de modelos técnicos reales de Odoo en el entregable.
- Hacer DELETE físico de registros de negocio consultados por otras entidades en vez de
  archivarlos (campo `active`/equivalente).
- Duplicar manualmente un campo relacionado en vez de declararlo como derivado/de solo lectura.
- Permitir que el estado de un documento se edite libremente en vez de por operaciones
  explícitas con validación.
- Mezclar la capa de permisos por tipo de entidad con las reglas de fila como si fueran lo
  mismo.
- Repetir el filtro de multiempresa a mano en cada consulta en vez de aplicarlo como capa
  transversal.
- Copiar y pegar los mismos campos de auditoría/archivado en cada entidad en vez de factorizar
  un mixin/clase base reutilizable.
