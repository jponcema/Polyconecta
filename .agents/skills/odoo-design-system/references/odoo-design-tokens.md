# Tokens de marca y diseño de Odoo (oficiales)

Fuente: Brand Assets oficiales de Odoo (odoo.com/page/brand-assets) + patrones visibles de forma consistente en Odoo.com y en el backend/website builder de Odoo. Si el usuario provee capturas o un tema propio, esas referencias tienen prioridad sobre esta tabla.

## Colores de marca

| Token (nombre a usar en el código) | Referencia interna | HEX | Uso típico |
|---|---|---|---|
| `--brand-primary` | (equivalente al morado de Odoo) | `#714B67` | Color de marca principal: CTAs primarios, acentos, iconografía de marca, elementos activos/seleccionados |
| `--brand-secondary` | (equivalente al teal de Odoo) | `#017E84` | Acento secundario, enlaces, badges, gráficos, estados "info" |
| `--brand-gray` | (equivalente al gris de Odoo) | `#8F8F8F` | Texto secundario, iconos inactivos, bordes, elementos deshabilitados |

**Importante**: los nombres de variable/clase/token en cualquier código o mockup entregado deben ser genéricos (`--brand-primary`, `.btn-primary`, `.smart-button`, etc.), nunca `--odoo-*`, `.odoo-*`, ni la palabra "Odoo" en ningún atributo, comentario, texto visible o identificador del entregable. Ver la regla completa en el `SKILL.md`.

Colores de partners (usar solo si el proyecto tiene relación con el programa de partners de Odoo, no como paleta genérica):
`#E46E78` (Learning), `#21B799` (Ready), `#5B899E` (Silver), `#E4A900` (Gold).

### Paleta funcional recomendada para UI (derivada, no oficial pero consistente con el estilo Odoo)
Odoo evita paletas saturadas: usa el morado con moderación (nunca como color de fondo dominante en pantallas de trabajo) sobre una base neutra muy limpia.

- Fondo de app / superficie base: blanco `#FFFFFF` o gris muy claro `#F5F5F4` / `#FAFAFA`
- Superficie elevada (cards, modales): blanco con sombra sutil, radio de borde grande
- Texto primario: `#1F1F1F` / `#212121` (casi negro, no negro puro)
- Texto secundario: el gris de marca `#8F8F8F`
- Bordes / divisores: `#E0E0E0` aprox.
- Éxito: verde estándar (`#28A745`-ish); Advertencia: ámbar; Error: rojo — Odoo no fuerza tonos exactos aquí, usar semántica estándar con suficiente contraste
- Modo oscuro (cuando aplique): fondo `#1E1E1E`/`#2B2B2B`, el morado `#714B67` se aclara ligeramente para mantener contraste (no usar el hex plano sobre fondo oscuro sin ajustar luminosidad)

## Tipografía

- **Encabezados y títulos**: Inter, peso 500–700, interlineado 1
- **Cuerpo de texto / párrafos**: Inter, peso 400, interlineado 1.5
- Fallback stack: `'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif`
- Jerarquía típica de Odoo: títulos grandes y audaces (bold, tracking ajustado), cuerpo de texto generoso y muy legible, mucho uso de peso en vez de tamaño para crear jerarquía
- Odoo evita fuentes decorativas o serif en producto — todo es sans-serif limpio

## Ortografía de la marca
- Escribir «Odoo» con mayúscula inicial únicamente (no ODOO, no Odoos en plural)
- Si el proyecto menciona la marca Odoo textualmente, respetar esta regla

## Iconografía
- Iconos de línea, geométricos, monocromáticos o duotono (morado + gris), nunca ilustraciones fotorrealistas para iconos de UI
- Esquinas suavemente redondeadas, trazo consistente, estilo "flat" sin gradientes duros
- Cada app/módulo de Odoo tiene un icono cuadrado con esquinas redondeadas y un pictograma simple centrado

## Componentes y patrones visuales característicos de Odoo

- **Botones**: radio de borde moderado-alto (pill o muy redondeado en marketing, rectángulo con esquinas suaves ~6-8px en backend), botón primario sólido en morado `#714B67`, botón secundario en outline o gris neutro, texto en semibold
- **Cards**: fondo blanco, sombra muy sutil (no dura), radio de borde generoso (8-16px), padding amplio
- **Espaciado**: sistema generoso, mucho aire en blanco, agrupación clara por espaciado antes que por líneas divisorias
- **Navegación**: top bar simple con logo a la izquierda, navegación horizontal minimalista en marketing; en backend, sidebar/topbar compacta, iconografía + texto, breadcrumbs claros
- **Formularios**: labels arriba del campo, inputs con borde sutil y radio suave, foco en morado o teal, validación con colores semánticos estándar
- **Ilustraciones** (marketing/onboarding): estilo flat, paleta duotono morado-teal-gris, personajes/objetos geométricos simples, nunca fotografía saturada de stock genérico
- **Movimiento/microinteracciones**: transiciones suaves y rápidas (150-250ms), sin efectos exagerados — la marca prioriza "fácil de usar" sobre "llamativo"

## Principio de marca a mantener siempre
Odoo se posiciona como "muy fácil de usar y totalmente integrado". Toda decisión de UI debe priorizarse hacia: claridad, poca carga cognitiva, consistencia entre pantallas/módulos, y una sensación profesional-pero-accesible (no corporativo frío, no "startup juguetón").
