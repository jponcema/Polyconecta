# PolyConecta.Domain (Capa de Dominio & Reglas de Negocio Puras)

**Propósito**: Corazón del sistema. Define las entidades de negocio canónicas, objetos de valor (Value Objects), servicios de dominio y contratos de repositorios sin ninguna dependencia de frameworks externos, bases de datos o HTTP.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Entidades Puramente Inmutables**:
   * `MasterOrder` & `SubOrder`: Jerarquía de 3 niveles de órdenes de producción.
   * `RolloMaestro`: Identidad física del rollo, cálculo de peso neto ($\text{Neto} = \text{Bruto} - \text{Tara}$), calibre y métricas de calidad.
   * `PolyLocation`: Representación de plantas (`PIM`, `STC`, `MTM`) y almacenes virtuales (`PIM/Stock/MP`, `PIM/Produccion`, `PIM/Stock/PT`, `PIM/Cuarentena`).
   * `RawMaterialCatalog` & `SupplierProductMapping`: Catálogo estandarizado de materias primas y mapeos a SKUs de proveedores.
   * `MassBalanceAudit`: Regla de auditoría de balance de masa ($\text{Varianza} \le 2.0\%$).

2. **Value Objects**:
   * `Folio`: Generación e inmutabilidad de folios de extrusión (`EX-01-YYMMDD-HHMMSS`).

3. **Regla de Arquitectura Limpia**:
   * **CERO dependencias externas**: No contiene referencias a Entity Framework, ASP.NET Core, SQL Server, PostgreSQL ni bibliotecas de UI.

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: **Ninguna**. Es el núcleo independiente del sistema.
