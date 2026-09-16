---
name: odoo-design-system
description: >
  Skill integral para diseñar interfaces "estilo Odoo": combina (1) estilo visual profesional
  con la marca oficial de Odoo (morado #714B67, tipografía Inter, iconografía de línea, cards
  con radio generoso) y (2) la arquitectura de información y usabilidad de Odoo (smart buttons
  para navegar entre documentos relacionados, el "chatter" con mensajes/notas internas/
  actividades/seguidores, y vistas universales tipo lista, kanban, calendario, gantt, pivot,
  gráfico, actividad o mapa). Úsala SIEMPRE que el usuario pida diseñar, maquetar, prototipar o
  mejorar una interfaz, pantalla, dashboard, formulario o flujo para un proyecto "estilo Odoo",
  "como Odoo", o que deba integrarse con Odoo — ya sea solo el aspecto visual, solo la
  estructura, o ambos. También aplica a revisiones de usabilidad y a componentes (botones,
  cards, formularios, tablas, smart buttons, chatter) que deban sentirse "Odoo-like". No es
  para escribir vistas XML/OWL de módulos Odoo (eso es backend/código); es para diseño de UX/UI.
---

# Sistema de diseño UX/UI estilo Odoo

Esta skill combina dos capas que se aplican SIEMPRE juntas al diseñar cualquier pantalla:

1. **Estructura (arquitectura de información)**: cómo se organiza y navega la información —
   documentada en `references/odoo-ux-patterns.md`.
2. **Estilo visual (marca)**: cómo se ve — colores, tipografía, iconografía, componentes —
   documentado en `references/odoo-design-tokens.md`.

La estructura decide primero *qué* necesita la pantalla (¿tiene documentos relacionados?
¿necesita chatter? ¿qué vistas debe exponer?); el estilo visual decide después *cómo se ve* eso.
No se trata de clonar Odoo pixel por pixel, sino de que cualquier pantalla se sienta profesional,
coherente y con el mismo ADN funcional y visual de Odoo.

## Regla crítica: Odoo es solo inspiración interna, nunca aparece en el entregable

Esta skill usa el diseño y la usabilidad de Odoo Enterprise como **referencia de inspiración
para razonar**, no como marca a exponer. El resultado final es un producto propio del usuario,
sin ninguna relación de marca visible con Odoo. Por lo tanto, en cualquier entregable (mockup,
HTML, CSS, Razor, comentarios de código, nombres de variable/clase, texto visible en la
interfaz, nombres de archivo):

- **Nunca** escribas la palabra "Odoo" (ni variantes como "odoo-style", "Odoo Enterprise", etc.).
- **Nunca** uses el logo, los iconos de aplicación, ni ningún asset con la marca Odoo.
- Los nombres de variables CSS, clases o componentes deben ser genéricos: `--brand-primary`,
  `.smart-button`, `.chatter-panel`, `.view-kanban`, etc. — nunca `--odoo-primary`, `.oe_chatter`,
  `.oe_stat_button` ni ningún identificador técnico interno real de Odoo.
- Si necesitas explicar una decisión de diseño al usuario en el chat, puedes decir "inspirado en
  patrones de Odoo" — eso es distinto a que la palabra aparezca *dentro* del producto entregado.
- Esta skill es agnóstica de lenguaje y framework: el resultado son lineamientos de diseño,
  mockups y, cuando se pida, HTML/CSS genérico de referencia — nunca código real de módulos
  Odoo (nada de XML, OWL, Python). Funciona igual si el proyecto del usuario está en C#/Razor,
  Blazor, React, Vue, PHP, Django o cualquier otro stack: la skill entrega el diseño, y la
  implementación técnica queda en manos del stack elegido por el usuario.

## Flujo de trabajo

### 1. Entender el encargo
Antes de diseñar, confirma o infiere razonablemente:
- ¿Qué pantalla/flujo es? (ficha de documento, dashboard, listado, landing, portal de cliente...)
- ¿Es una pantalla tipo "backend" (densa en datos, orientada a productividad) o tipo
  "marketing/website" (persuasiva, más espacio en blanco)? Ambos registros están documentados en
  `references/odoo-design-tokens.md`.
- ¿Hay documentos/registros relacionados entre sí que el usuario necesite navegar?
- ¿El documento necesita seguimiento, comunicación interna/externa o auditoría de cambios?
- ¿La misma colección de datos se va a consultar de formas distintas (gestión diaria vs.
  planificación vs. análisis)?
- Modo claro, oscuro, o ambos.

Si algo es ambiguo pero no bloqueante, asume lo más razonable y avisa de tu suposición en una
línea; solo pregunta si la ambigüedad cambiaría completamente el resultado. Si el dominio sugiere
claramente un patrón que el usuario no pidió (p. ej. un CRM sin kanban de pipeline, un cliente sin
smart buttons a sus pedidos), señala la omisión y proponlo en vez de descartarlo.

### 2. Cargar las dos referencias
Lee **ambos** archivos antes de producir cualquier mockup:
- `references/odoo-ux-patterns.md` — smart buttons, chatter, vistas estándar, estructura
  canónica de un formulario, principio de consistencia entre módulos.
- `references/odoo-design-tokens.md` — colores de marca, tipografía, iconografía, componentes
  visuales (botones, cards, formularios, espaciado).

### 3. Resolver la estructura antes que el estilo
Aplica en este orden:
1. Si hay documentos relacionados → resuélvelos como smart buttons en la cabecera, nunca como
   enlaces sueltos dentro del cuerpo del formulario.
2. Si el documento necesita seguimiento/comunicación → incluye el chatter completo (mensajes +
   notas internas + actividades + seguidores), no una versión reducida.
3. Define qué vistas estándar expone cada colección de datos (lista + kanban como mínimo;
   añade calendario/gantt si hay fechas o dependencias; pivot/gráfico si hay análisis).
4. Sigue el orden canónico de un formulario: breadcrumb → acciones/estado → smart buttons →
   cuerpo (campos clave + pestañas) → chatter.

### 4. Aplicar el checklist de UX/UI profesional
Independientemente de la marca, toda entrega debe cumplir:
- **Jerarquía visual clara**: un elemento domina la atención por pantalla.
- **Contraste accesible**: texto normal ≥ 4.5:1, texto grande ≥ 3:1 (WCAG AA).
- **Sistema de espaciado consistente** (escala 4/8px), no valores arbitrarios.
- **Consistencia entre pantallas**: mismos radios, sombras y tamaños de botón en todo el flujo.
- **Estados cubiertos**: hover, focus, disabled, loading, vacío, error — no solo el "happy path".
- **Affordance clara**: lo clicable se ve clicable.
- **Densidad adecuada al contexto**: pantallas de trabajo toleran más densidad que landings.
- **Responsive**: al menos desktop + mobile si la pantalla lo amerita.

### 5. Vestir con el lenguaje visual de Odoo
Aplica los tokens de `references/odoo-design-tokens.md`: morado `#714B67` como acento (nunca
como fondo dominante en pantallas de trabajo), tipografía Inter, iconos de línea, cards con
sombra sutil y radio generoso, espaciado amplio.

### 6. Producir el resultado
- Si el mockup es la entrega principal, créalo como pieza de diseño real (canvas editable) —
  usa el tipo de artefacto de Diseño de esta sesión si está disponible; si no, el visualizador
  de mockups inline.
- Si se pide un wireframe de la estructura de información (qué smart buttons tiene cada modelo,
  qué vistas expone cada colección), un diagrama/esquema es válido y a veces más claro que un
  mockup de alta fidelidad.
- Si el usuario pide explícitamente un archivo HTML/CSS descargable o código de componente,
  genera el archivo aplicando los mismos tokens (variables CSS para colores, `font-family:
  'Inter', ...`, radios y sombras descritos).

### 7. Explica las decisiones, brevemente
Después del mockup, en pocas líneas indica: qué documentos quedaron conectados vía smart
buttons, si se incluyó chatter y por qué (o por qué no), qué vistas se expusieron para cada
colección, y qué elementos visuales vienen directamente de la marca Odoo. El mockup es la
entrega principal — no hace falta un ensayo.

## Errores comunes a evitar
- Dejar la palabra "Odoo", su logo, o identificadores técnicos internos (`oe_chatter`,
  `oe_stat_button`, etc.) en el HTML/CSS/código entregado — la marca es solo inspiración interna,
  nunca debe aparecer en el producto final.
- Meter la navegación a documentos relacionados dentro de una pestaña en vez de smart buttons.
- Mezclar notas internas con comunicación externa en el chatter sin distinguirlas.
- Omitir actividades/recordatorios del chatter y dejar solo el hilo de mensajes.
- Ofrecer una sola vista (solo lista) cuando el dominio pide kanban, calendario o gantt.
- Que cambiar de tipo de vista resetee los filtros activos.
- Usar el morado de Odoo como fondo grande en pantallas de trabajo, en vez de como acento.
- Mezclar tipografías decorativas/serif, iconos ilustrativos/3D, o sombras duras — Odoo es
  sans-serif limpio, iconos de línea y sombras sutiles.
- Reinventar la estructura del formulario en cada pantalla nueva en vez de reutilizar el mismo
  orden canónico.
