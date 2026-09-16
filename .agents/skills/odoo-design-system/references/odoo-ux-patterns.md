# Patrones estructurales de UX/IA de Odoo (arquitectura de información)

Esta referencia documenta los patrones de **estructura e interacción** que hacen que cualquier app se sienta "como Odoo" a nivel funcional, más allá del estilo visual (para estilo visual ver la skill `odoo-uxui`). Son patrones de navegación y organización de información, aplicables tanto si se construye dentro de Odoo (vistas XML reales) como si se diseña un producto independiente que solo se *inspira* en esta arquitectura.

## 1. Smart buttons (botones inteligentes)

**Qué son**: botones ubicados en la cabecera de una ficha (form), agrupados en una fila horizontal justo debajo/al lado del breadcrumb, que muestran un icono + un número (contador) + una etiqueta corta, y al hacer clic navegan a la vista lista/kanban de los registros relacionados, filtrada por el registro actual.

**Cuándo usarlos**: siempre que un documento tenga registros relacionados que el usuario querría consultar sin salir del contexto (pedidos de un cliente, facturas de un pedido, tareas de un proyecto, reuniones de un contacto, etc.). Es el mecanismo estándar para "documentos relacionados entre sí" — nunca escondas esa relación solo en una pestaña interna si el usuario necesita saltar entre documentos.

**Reglas de diseño**:
- Viven en una caja horizontal en la cabecera del formulario (`oe_button_box` en Odoo real), NUNCA dentro del cuerpo/tabs del formulario.
- Formato: `[icono]  N⁣ \n Etiqueta` (número grande arriba, texto descriptivo debajo, en un botón con borde sutil tipo "outline", no un botón sólido de color — no deben competir visualmente con el botón de acción primaria del documento).
- Si el contador es 0, el botón puede ocultarse o mostrarse atenuado según si "crear el primero" es una acción útil ahí.
- Máximo recomendado: 4-6 smart buttons visibles a la vez; si hay más, se colapsan/scrollean horizontalmente en vez de romper el layout.
- Al hacer clic, la navegación siempre es: ir a la vista lista (o form directo si es un solo registro) de esos documentos relacionados, con breadcrumb que permite volver atrás al documento de origen.
- Ejemplos típicos: en un Cliente → "Ventas (5)", "Facturas (12)", "Reuniones (2)"; en un Pedido → "Factura", "Envíos (2)"; en un Proyecto → "Tareas (24)", "Partes de horas".

## 2. Chatter (hilo de comunicación y seguimiento)

**Qué es**: el panel de historial/comunicación presente en la parte inferior (o lateral, en vistas más anchas) de casi cualquier ficha de documento en Odoo. Es un patrón transversal: el mismo componente se repite igual en todos los modelos.

**Componentes fijos del chatter** (siempre los tres juntos, en este orden funcional):
1. **Barra de acciones**: "Enviar mensaje" (comunicación visible para seguidores/clientes) y "Registrar nota" (nota interna, no notifica a clientes externos) — son dos modos claramente diferenciados, casi nunca se debe fusionar la nota interna con la comunicación externa.
2. **Actividades programadas** (`activity_ids`): recordatorios/tareas de seguimiento con fecha límite, tipo de actividad (llamada, email, reunión...) y responsable asignado. Se muestran como una lista compacta encima del hilo de mensajes, con indicador visual de vencidas (rojo), hoy (naranja/amarillo) y futuras (gris).
3. **Hilo de mensajes** (`message_ids`): historial cronológico inverso (más reciente arriba) de mensajes, notas, cambios de estado registrados automáticamente (log de auditoría) y emails relacionados.
4. **Seguidores** (`message_follower_ids`): lista de personas suscritas a las notificaciones de ese documento, con opción de añadir/quitar seguidores.

**Reglas de diseño**:
- El chatter es el mismo componente reutilizado en todos los documentos — no rediseñes uno distinto por modelo.
- Nunca reemplaces el log automático de cambios (p. ej. "Estado cambiado de Borrador a Confirmado") — es auditoría, debe persistir visualmente en el hilo.
- Si el layout es de una sola columna, el chatter va debajo del contenido del formulario. Si hay espacio (pantallas anchas), puede ir como panel lateral derecho fijo mientras se hace scroll del contenido principal.
- El chatter no es un chat en tiempo real tipo mensajería instantánea — es un historial persistente por documento.

## 3. Vistas estándar (tipos de vista universales)

Odoo expone el mismo conjunto de datos a través de distintos "tipos de vista" intercambiables mediante un selector de iconos en la esquina superior derecha de la vista de lista/búsqueda. La regla de oro: **cualquier colección de documentos debería poder mostrarse en más de un tipo de vista sin cambiar el modelo de datos**, y el usuario cambia de una a otra sin perder los filtros/búsqueda activos.

| Vista | Cuándo usarla | Patrón visual |
|---|---|---|
| **Lista (tree/list)** | Vista por defecto para revisar/editar muchos registros a la vez, comparar campos entre filas | Tabla densa, columnas configurables, edición inline opcional, checkboxes para selección múltiple + acciones en lote |
| **Kanban** | Flujos con estado/etapa (pipeline de ventas, tareas, candidatos) | Columnas = etapas, tarjetas arrastrables (drag & drop) entre columnas, cada tarjeta resume lo esencial (título, responsable/avatar, indicadores clave, etiquetas de color) |
| **Formulario (form)** | Ver/editar el detalle completo de un registro | Cabecera con breadcrumb + botones de acción + smart buttons + barra de estado (statusbar) si el documento tiene flujo de estados; cuerpo organizado en pestañas si hay mucha información; chatter al final |
| **Calendario** | Registros con fecha/hora relevante (citas, eventos, vencimientos) | Vista mes/semana/día intercambiable, eventos como bloques de color, clic para crear/editar rápido |
| **Gantt** | Planificación con dependencias temporales y/o recursos (proyectos, fabricación, turnos) | Barras horizontales por tarea/recurso a lo largo del eje temporal, agrupables por responsable/proyecto, con posibilidad de arrastrar para reprogramar |
| **Pivot** | Análisis cruzado de datos numéricos por varias dimensiones | Tabla dinámica con filas/columnas configurables, totales y subtotales |
| **Gráfico (graph)** | Comparar magnitudes o tendencias | Barras, líneas o tarta, cambia junto con los mismos filtros que el pivot/lista |
| **Actividad (activity)** | Seguimiento de tareas pendientes cruzando varios registros | Matriz registros × tipo de actividad, con celdas indicando estado/vencimiento |
| **Mapa (map)** | Registros con ubicación geográfica | Pines sobre un mapa, agrupables por proximidad |

**Reglas de diseño transversales a todas las vistas**:
- El selector de tipo de vista es siempre el mismo control (iconos), en la misma posición (arriba a la derecha, junto a la barra de búsqueda), en toda la aplicación.
- La barra de búsqueda + filtros + agrupar por es compartida entre todas las vistas de una misma colección — cambiar de vista no debe resetear los filtros activos.
- El breadcrumb en la parte superior siempre refleja la ruta de navegación (Menú › Submenú › Registro abierto), permitiendo volver a cualquier nivel anterior con un clic.
- Las acciones masivas (archivar, exportar, eliminar, asignar) se exponen igual en todas las vistas que soportan selección múltiple (normalmente lista y kanban).

## 4. Estructura estándar de un formulario (form view)

De arriba a abajo, el orden esperado es:

1. **Breadcrumb** de navegación.
2. **Barra de acciones/estado**: botones de acción primaria (p. ej. "Confirmar", "Guardar") + barra de estado tipo statusbar (Borrador → Confirmado → Hecho) si el documento tiene flujo.
3. **Smart button box** (ver sección 1), alineada a la derecha de la cabecera.
4. **Cuerpo del formulario ("sheet")**: campos principales arriba (los más identificativos: nombre, referencia, contraparte), seguido de pestañas (notebook) para agrupar información secundaria (líneas de detalle, información adicional, notas internas, otra información).
5. **Chatter** al final (o lateral en pantallas anchas).

Este orden no cambia entre modelos: mantener esta misma estructura en cualquier documento nuevo del proyecto es lo que da la sensación de coherencia "tipo Odoo", incluso si el contenido de cada ficha es distinto.

## 5. Principio general de consistencia

La usabilidad de Odoo no viene de un componente aislado sino de la **repetición exacta del mismo patrón en todos los módulos**: el usuario que aprende a usar el chatter en Ventas ya sabe usarlo en Proyectos; el que aprende a cambiar de vista lista→kanban en CRM ya sabe hacerlo en Reclutamiento. Al diseñar un proyecto nuevo, la prioridad es la **repetibilidad del patrón entre pantallas**, no la originalidad puntual de una sola pantalla.
