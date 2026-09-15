# PolyConecta.Api (Capa API Gateway & Backend Host)

**Propósito**: Capa de entrada HTTP y API Gateway ASP.NET Core que expone los endpoints RESTful para la interfaz de usuario, procesa peticiones HTTP, gestiona el middleware de excepciones y sirve la documentación Swagger/OpenAPI en el puerto `9020`.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Puerto por Defecto**:
   * **Swagger API (PolyConecta.Api)**: `http://localhost:9020/swagger`
   * **REST API Endpoints Base**: `http://localhost:9020/api/v1`

2. **Controladores RESTful (`/api/v1/...`)**:
   * `OrdersController`: Creación de Órdenes Maestras (`OM`) y actualización de estados en Sub-Órdenes (`OF`).
   * `RollsController`: Registro y pesaje de rollos de extrusión a pie de máquina.
   * `LocationsController`: Consulta de almacenes y ejecución de traspasos de stock entre ubicaciones.
   * `RawMaterialsController`: Gestión del Catálogo Maestro de Insumos y mapeo de equivalencias de proveedores.

3. **Middleware & Configuración**:
   * Habilitación de CORS para permitir peticiones desde la SPA (puerto 9000).
   * Manejo de referencias circulares en JSON (`ReferenceHandler.IgnoreCycles`).
   * Especificación y UI de Swagger/OpenAPI (`/swagger`).
   * Filtros de excepción global y validación de DTOs.

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: 
  * `PolyConecta.Domain` (Entidades, Value Objects, Servicios de Dominio).
  * `PolyConecta.Infrastructure` (DbContext, Outbox Publisher).
