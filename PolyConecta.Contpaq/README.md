# PolyConecta.Contpaq (Capa de Integración ERP & Bridge Win32)

**Propósito**: Servicio ejecutable Win32 (.NET 8 x86) dedicado a la integración bidireccional segura con **CONTPAQi Comercial Premium v10+**. Aísla las llamadas SDK nativas (`MGW_SDK.dll`) y ejecuta lecturas SQL directas en modo `NOLOCK`.

---

## 🎯 Responsabilidades y Reglas de la Capa

1. **Aislamiento de Proceso x86**:
   * Corre como proceso independiente de 32 bits (`win-x86`) en la **Sesión 2 interactiva** de Windows para interactuar con la sesión gráfica abierta de CONTPAQi.
   * Evita fallos de memoria CLR/Borland (`0xc0000005`) aislando `fInicializaSDK()` y `fTerminaSDK()`.

2. **Procesador Outbox & Resiliencia**:
   * Consume mensajes de la cola Outbox (`bridge_outbox.db`) y ejecuta transacciones de alta de documentos (`fAltaDocumento`) y afectación de capas de inventario (`fAltaMovimientoSeriesCapas`).
   * Manejo automático de reintentos y cola Dead Letter Queue (DLQ).

3. **Lecturas SQL Directas**:
   * Consulta catálogos y existencias de la base de datos de Polyempaques (`admProductos`, `admAlmacenes`, `admCapasProducto`) mediante SQL directo en modo `NOLOCK`.

---

## 🔗 Dependencias Permitidas

* **Dependencias de Salida**: 
  * `PolyConecta.Domain` (Value Objects y contratos de eventos).
  * `PolyConecta.Infrastructure` (Esquema de Outbox).
