# PolyConecta.Presentation (Capa de Presentación UI/UX)

**Propósito**: Capa de Interfaz de Usuario (UI/UX) que entrega la experiencia operativa de PolyConecta para piso de planta y consola administrativa, corriendo de forma independiente en el puerto `9000`.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Puerto por Defecto**:
   * **PolyConecta Web Presentation**: `http://localhost:9000`

2. **Hub de Módulos (App Launcher)**:
   * Pantalla de inicio con acceso directo a los módulos núcleo:
     * 💼 **Ventas**: Pedidos, sincronización con CONTPAQi Premium, workflow de firmas Ventas/Crédito.
     * ⚙️ **Fabricación**: Órdenes de Fabricación (Extrusión, Impresión, Bolseo), componentes, subproductos, producción y planeación.
     * 🛡️ **Calidad**: Control de calidad por lote, ligado a la producción de cada Orden de Fabricación.
     * 🚚 **Logística**: Traslados interplanta y Entregas a cliente.
     * 📱 **Planta**: Terminal Handheld de báscula, captura de incidencias y captura masiva de producción.

3. **Vistas Estándar**:
   * **Vista Kanban**: Tarjetas visuales organizadas en columnas por etapa del pipeline.
   * **Vista Lista**: Tablas interactivas con badges de estado.
   * **Vista Formulario**: Botones de acción, caja de Smart Buttons con contadores y statusbar de pipeline.

4. **Stack**:
   * Blazor Server (.NET 8), Bootstrap 5.
   * Consume de forma desacoplada la API Gateway a través de contratos REST/JSON (`http://localhost:9020/api/v1/...`).

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: Consume los contratos REST JSON expuestos por `PolyConecta.Api` (puerto 9020). No posee referencias directas a bases de datos ni bibliotecas de infraestructura backend.
