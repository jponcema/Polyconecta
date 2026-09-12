# Informe de Diagnóstico Operativo del Estado Actual ("As-Is") - Polyempaques

**Empresa:** Polyempaques (Poly-conecta)  
**Fecha de Elaboración:** Septiembre 2026  
**Base del Diagnóstico:** Sesiones de Assessment de Compras, Ventas, Planeación, Producción, Calidad y Logística  

---

## 1. Compras y Gestión de Insumos

### 1.1 Negociación de Resinas (Materia Prima Principal)
* **Gestión Centralizada:** La negociación de resinas (Alta Densidad, Baja Densidad, Fraccional) es realizada directamente por la Dirección General (Ing. Alfredo Leal). Se negocian volúmenes y precios fijos por periodos aproximados de dos meses con proveedores nacionales.
* **Sin Descuentos Formales en Sistema:** Aunque se consiguen tarifas preferenciales por volumen, los costos se registran directamente como un precio neto fijo por kilo en las requisiciones u órdenes de compra, sin aplicar un concepto de descuento en el ERP.
* **Variabilidad de Códigos del Proveedor:** Al surtir camiones dentro del periodo negociado, el proveedor frecuentemente entrega resinas con códigos técnicos distintos a los pactados originalmente, lo que genera discrepancias manuales entre la Orden de Compra (OC) y la factura recibida.

### 1.2 Insumos Generales y Refacciones
* **Mínimos y Máximos en Excel:** Los insumos de proceso (bobinas, aditivos deslizantes/antibloqueo, pigmentos) son gestionados por el Almacén General (Francisco Hernández / Ismael). El control de reorden se calcula en un archivo Excel ajustado periódicamente según estimaciones de clientes y tiempos de entrega de proveedores (1 a 4 semanas).
* **Flujo de Requisición e Insumos Nuevos:** Para insumos no comunes o refacciones, el usuario llena una solicitud informal. Compras (Karen Ortega) cotiza con tres proveedores y comparte las opciones con el solicitante para definir la compra.
* **Proceso Obligatorio de 3 Firmas:** Toda requisición requiere de manera estricta **3 firmas físicas** (Solicitante, Autorizador y Elaborador) sin importar el monto económico involucrado. En ausencias temporales, las aprobaciones se gestionan por WhatsApp y se firman físicamente a posteriori.

### 1.3 Registro y Entradas en CONTPAQi Premium
* **Omisión de la Entrada Contable:** Actualmente, Compras genera la requisición y la convierte en Orden de Compra (OC) en CONTPAQi Premium. Sin embargo, al recibir el material del proveedor, **no se registra la entrada formal de inventario en el ERP**.
* **Manejo de Pasivos y Facturas:** Al llegar la factura, el pasivo se crea directamente contra la cuenta de gastos/inventario general sin afectar existencias físicas por unidades o kilogramos en el sistema.
* **Control Físico en WhatsApp:** Ismael (auxiliar de almacén) realiza un conteo diario manual y comparte fotografías del inventario en un grupo de WhatsApp para informar lo consumido y disponible.

---

## 2. Ventas, Atención a Clientes (AC) y Cotizaciones

### 2.1 Cotizaciones y Nuevos Desarrollos
* **Proceso Comercial Manual:** Cassandra Castillo recibe por correo electrónico las solicitudes de clientes (medidas, calibre, color, cantidad y destino).
* **Cálculo de Precios en Word/Excel:** Las cotizaciones se elaboran manualmente mediante fórmulas internas en Word/Excel, expresando precios por kilo, millar, pieza o rollo, asignando un folio mensual en número romano (ej. `IV118-26`).
* **Canalización en PDF:** Al ser aprobada por el cliente, la propuesta comercial se convierte a PDF y se envía por correo al equipo de Atención a Clientes (Customer Service).

### 2.2 Órdenes de Trabajo (OT) y Aprobaciones
* **Generación de la OT Interna:** Celia/Mayra (Atención a Clientes) traducen la cotización aprobada a una Orden de Trabajo (OT) en Excel. Si la orden implica extrusión y conversión (bolseo/impresión), la OT se divide en secciones para cada etapa.
* **Verificación de Inventario Existente:** Mayra Gallegos revisa los inventarios de Producto Terminado (PT) y Almacén Semi antes de liberar la OT a producción, con la finalidad de apartar stock existente y evitar sobre-fabricaciones.
* **Proceso de Aprobaciones y Sellos:** La OT impresa requiere las firmas físicas de Atención a Clientes, Dirección General, y la revisión/sello de Crédito y Cobranza (para verificar adeudos atrasados). Una vez autorizada, se sube a Google Drive.

### 2.3 Registro de Productos en CONTPAQi
* **Alta por Facturación:** Brenda Leos (Facturación) da de alta el código de producto en CONTPAQi Premium al recibir la OT digitalizada.
* **Duplicidad de Códigos por Cliente:** Si dos clientes solicitan exactamente la misma bolsa (ej. 20x30 calibre 300), se registran **dos códigos independientes en CONTPAQi** debido a que los clientes exigen su propio número de parte e identificador en la factura.

---

## 3. Planeación y Programación de Producción

### 3.1 Recepción y Organización por el Planner
* **Extracción de Órdenes:** Roosvelt Lara (Planner) descarga las OTs de Google Drive notificadas por WhatsApp y las transcribe a un Excel interno de control mensual.
* **Matriz Maestra de Programación:** Registra los pedidos en una Matriz Maestra en Excel con pestañas por máquina. Clasifica las órdenes por densidad (Alta/Baja) y color para minimizar el scrap en los cambios de cañón.
* **Foliador Mecánico:** Para garantizar la trazabilidad en Apodaca, Roosvelt asigna un folio único consecutivo mediante un foliador mecánico físico de escritorio (ej. `42747`).

### 3.2 Generación de la Receta (BOM) de Extrusión
* **Orden de Producción ISO 9000:** Imprime un formato estandarizado que detalla la máquina asignada, kilogramos solicitados, metros por rollo y especificaciones de soplado.
* **Fórmula por Tolva (Coextrusora de 3 Capas):** La receta establece la dosificación por cañón (Capa Central B al 50%, Capas Laterales A y C al 25% cada una). Especifica los kilos exactos de resinas vírgenes, aditivos (deslizantes/antibloqueo) y pigmentos.
* **Surtido Consolidado:** Almacén General entrega un consolidado diario de materia prima al sub-almacén de mezclas a pie de máquina, sin un descuento transaccional por orden específica.

---

## 4. Producción y Manufactura (Plantas Operativas)

### 4.1 Extrusión (Planta PIM / Apodaca)
* **Turnos de Operación:** La planta opera en 2 turnos continuos de 12 horas (día y noche).
* **Bitácoras Físicas a Pie de Máquina:** Los operadores (bajo la supervisión de Don Chuy) registran a mano en libros/libretas físicas la fecha, turno, número de operador, folios trabajados, kilos producidos, peso de cada rollo y scrap generado.
* **Vaciado Digital y Control de Paros:** Roosvelt o el supervisor transcriben los datos del libro a la Matriz Maestra diariamente. Utilizan códigos de color para identificar pausas por mantenimiento, falta de resina, ausencia de operador o fin de semana.
* **Identificación de Rollo:** Cada rollo extruido se etiqueta manualmente con la medida, calibre, peso, número de máquina, turno y folio consecutivo.

### 4.2 Conversión: Impresión y Bolseo (Planta Santa Cruz / Guadalupe)
* **Recepción en Almacén Semi:** Brian Palomino recibe los rollos maestros de Apodaca/Montemorelos, los pesa en báscula local y actualiza matutinamente un Excel de inventario.
* **Programación en Conversión:** Diana Ayala (Planner Santa Cruz) recibe las OTs de AC, calcula los millares esperados y asigna la orden a las máquinas impresoras (5 máquinas) y bolseadoras (23-24 máquinas).
* **Captura de Producción y Mermas:** Al finalizar la OT, el operador llena un formato físico reportando millares producidos, kilos de rollos consumidos y mermas por proceso.

### 4.3 Planta Montemorelos
* **Operación Multiproceso:** Ejecuta procesos de extrusión, bolseo y reciclado a menor escala. La planeación de extrusión la gestiona Roosvelt Lara.
* **Razón Social Independiente:** Opera bajo una figura jurídica/RFC diferente a Apodaca y Santa Cruz, requiriendo un tratamiento contable específico para movimientos de material.

### 4.4 Planta Huinalá y Proceso de Peletizado
* **Prensado y Clasificación de Scrap:** El desperdicio generado por reventones o ajustes se separa rigurosamente por tipo de resina, densidad y color, prensándose en pacas o cajas.
* **Proceso de Peletizado (Reciclado):** El scrap se envía a Huinalá o Guadalupe, donde se muele, fusiona y procesa para reconvertirse en **pellets de resina reciclada**. Esta resina reingresa como insumo para formulaciones que admiten material reciclado.

---

## 5. Control de Calidad, Inspecciones, Cuarentena y RMA

### 5.1 Los Tres Filtros de Calidad
1. **Inspección de Recibo de Materia Prima:** Calidad audita visualmente las resinas, bobinas y tarimas que llegan del proveedor. Verifica el certificado de calidad; si detecta humedad, empaque roto o color amarillento, se rechaza sin descargarse.
2. **Inspección en Proceso (Línea de Fabricación):**
   * *Extrusión:* Muestreo de calibre en 10 a 20 puntos, peso de rollo y prueba de tratamiento corona. Se llena el formato físico de inspección y se coloca la etiqueta verde de liberación.
   * *Impresión:* Verificación hora por hora (*time check*) de repetición, tono de tinta y calidad gráfica.
   * *Bolseo:* Revisión de sellos, suaje, dimensiones y ausencia de bloqueo.
3. **Auditoría de Embarque:** Verificación final de embalaje, tarimas limpias, flejado y etiquetas antes de la carga al transporte.

### 5.2 Gestión de Cuarentena
* **Etiquetado Rojo:** El material que no cumple especificaciones es etiquetado como No Conforme y transferido al Almacén de Cuarentena.
* **Disposición Técnica:** En juntas diarias, Dirección (Ing. Alfredo Leal) o Roosvelt evalúan si el lote se retrabaja (reembobinado/corte), se reasigna a otro cliente con re-etiquetado autorizado, o se envía a scrap/peletizado.

### 5.3 Certificados de Calidad y Retorno de Material (RMA)
* **Certificado Impreso:** Para clientes directos, Calidad emite un Certificado de Calidad firmado que acompaña a la factura y a la Orden de Compra durante el viaje.
* **Atención a Quejas (RMA):** Si el cliente reporta un rechazo en sus instalaciones, se exige evidencia física. Al aceptarse la reclamación, se emite la autorización de retorno (RMA), la cual ampara el cambio físico, la nota de crédito o la cancelación de factura.

---

## 6. Logística, Embarques y Traspasos Interplantas

### 6.1 Traspasos Internos (Apodaca ➔ Santa Cruz)
* **Hoja de Salida Interna:** El traslado de rollos maestros entre Apodaca y Santa Cruz se realiza utilizando una Hoja de Salida en Excel (sin factura ni certificado de calidad).
* **Priorización de Carga:** Carlos Eloí (Logística Apodaca) coordina las salidas basándose en materiales liberados y notificaciones de urgencia en WhatsApp. Consolida la carga en papeletas informativas por tarima.
* **Recepción y Discrepancias en Santa Cruz:** Brian Palomino descarga el camión y contrasta los bultos contra la Hoja de Salida. Pesa los rollos en la báscula de Santa Cruz; si hay diferencias de peso, ajusta la entrada en su Excel de Almacén Semi y notifica la corrección a la planta de origen.

### 6.2 Embarque a Cliente Directo
* **Programa Diario de Embarques:** Elaborado conjuntamente en Excel por Atención a Clientes (Mayra) y Logística.
* **Pesaje y Evidencias Fotográficas:** En báscula, Logística toma **4 fotografías obligatorias** (material en báscula, peso de tarima, papeleta, monitor) y las envía al chat de facturación.
* **Facturación y Despacho:** Facturación emite la Factura o Remisión en CONTPAQi Premium. El transportista recibe la Factura, Orden de Compra del cliente, Certificado de Calidad, Checklist de la unidad y Certificado de Fumigación (además de etiquetas QR específicas para clientes como Owens).

---

## 7. Infraestructura Tecnológica (CONTPAQi vs. Excel)

### 7.1 Uso Limitado de CONTPAQi Premium
* **Función Actual:** El ERP se utiliza exclusivamente para la emisión de facturas/remisiones, la generación de pasivos de compra y la impresión de Órdenes de Compra.
* **Inexistencia de Inventario Operativo:** El sistema no registra entradas de almacén, traspasos interplantas, saldos de materia prima, ni movimientos de producción en proceso o producto terminado.

### 7.2 Proliferación de Archivos Excel y WhatsApp
* **Operación Fragmentada:** La totalidad de la planeación, recetas de soplado, matrices de máquinas, seguimiento de OTs, inventarios de almacén semi y PT, reportes de scrap y programas de embarque se gestionan en hojas de cálculo independientes.
* **Captura Repetitiva y Comunicación Informal:** La información se re-captura múltiples veces entre departamentos, apoyándose fuertemente en grupos de WhatsApp para coordinar prioridades, avisos de embarque y conteos de almacén.
