# PolyConecta.Presentation (Capa de Presentación UI/UX)

**Propósito**: Capa de Interfaz de Usuario (UI/UX) y assets estáticos web que entregan la experiencia operativa de **Odoo 19 Enterprise Edition** para piso de planta y consola administrativa, corriendo de forma independiente en el puerto `9000`.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Puerto por Defecto**:
   * **Odoo 19 Web SPA (Presentation)**: `http://localhost:9000`

2. **Grid de Aplicaciones de Odoo 19 (App Launcher)**:
   * Menú principal interactivo que permite navegar entre los 6 módulos núcleo:
     * 🏭 **Manufactura (MRP / MES)**: Órdenes de Extrusión, Recetas 3 Capas, Balance de Masa.
     * 📦 **Almacenes (WMS)**: Rutas Multi-Planta (`PIM`, `STC`, `MTM`), Ubicaciones y Traspasos 2 Pasos.
     * 📱 **Handheld Báscula**: Terminal móvil táctil (Odoo Barcode UX) para pesaje pie de máquina.
     * 🛡️ **Control de Calidad (QC)**: Quality checks, firmas digitales y aislamiento Red Tag.
     * 🛒 **Compras / Insumos MP (P2P)**: Catálogo Maestro de Resinas y Equivalencias de Proveedores.
     * 💼 **Ventas / Pedidos (O2C)**: Sincronización CONTPAQi, Workflow 3 Firmas.

3. **Vistas Estándar de Odoo 19**:
   * **Vista Kanban (🎴)**: Tarjetas visuales organizadas en columnas por etapa del pipeline (`Borrador` → `Programado` → `En Proceso` → `Calidad` → `Finalizado`).
   * **Vista Lista (Tree View ☰)**: Tablas interactivas con filtros, búsqueda y badges de estado.
   * **Vista Formulario (Form View 📄)**: Botones de acción principal, **Smart Buttons Box** con contadores en tiempo real (*Rollos*, *Calidad*, *Movimientos*) y **Statusbar Pipeline**.

4. **Independencia de Frameworks Complex**:
   * Construida con HTML5, Bootstrap 5 y JavaScript vanilla modular.
   * Consume de forma desacoplada la API Gateway a través de contratos REST/JSON (`http://localhost:9020/api/v1/...`).

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: Consume los contratos REST JSON expuestos por `PolyConecta.Api` (puerto 9020). No posee referencias directas a bases de datos ni bibliotecas de infraestructura backend.
