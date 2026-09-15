# PolyConecta.Infrastructure (Capa de Infraestructura & Persistencia)

**Propósito**: Encapsula el acceso a datos relacionales (EF Core `PolyDbContext`), el mapeo inmutable de tablas, la ejecución de migraciones y la infraestructura del patrón Outbox para la integración con CONTPAQi.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Persistencia & EF Core**:
   * `PolyDbContext`: Mapeo de entidades del dominio a esquemas relacionales relacionales.
   * Configuraciones de entidades y datos semilla (`PolyLocationConfiguration`, `RawMaterialCatalogConfiguration`).
   * Aplicación del **Quality Gate Hard-Stop**: Regla de persisencia que bloquea activamente los movimientos de inventario de lotes almacenados en Cuarentena (`PIM/Cuarentena`).

2. **Publicador de Outbox (`OutboxPublisher`)**:
   * Encolamiento durable de eventos de negocio (`RollCreated`, `StockTransferred`) para procesamiento asíncrono hacia CONTPAQi.

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: 
  * `PolyConecta.Domain` (Implementa interfaces y persiste entidades del dominio).
