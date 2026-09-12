# Estructura de la Base de Datos — CONTPAQi® Comercial

> **Documento Técnico de Referencia**  
> Especificación completa de la estructura de base de datos de CONTPAQi® Comercial: esquemas de tablas, catálogo de campos, tipos de datos, llaves primarias, índices y relaciones entre módulos.

## 1. Introducción y Convenciones de Tipos de Datos

La base de datos de CONTPAQi® Comercial se divide en dos categorías principales:
- **Tablas de la Empresa**: Tablas contenidas de forma independiente en la base de datos de cada empresa.
- **Tablas Generales**: Tablas globales compartidas por todo el sistema y gestión de accesos/licencias.

### Convención de Tipos de Datos

| Tipo | Definición | Rango / Formato de Valores |
| :---: | :--- | :--- |
| **V (Varchar)** | Caracteres alfanuméricos (texto). | De 10 a 254 caracteres. En la descripción se indica la longitud máxima. |
| **B (Bit)** | Tipo de datos lógico (Booleano). | Verdadero (True / 1) o Falso (False / 0). |
| **I (Integer)** | Números Enteros. | Desde -2,147,483,647 hasta 2,147,483,648. |
| **F (Float)** | Números flotantes con decimales. | Desde -1.79E+308 hasta 1.79E+308. |
| **D (DateTime)** | Fecha y Hora. | 01/01/1753 al 31/12/9999. Formato: dd/mm/aaaa hh:mm:ss a.m./p.m. |
| **T (Texto)** | Texto libre / extenso. | Cadenas de texto libre sin restricción fija de longitud. |

---

## 2. Diagramas y Estructura Relacional por Módulos

El modelo relacional de la base de datos se interconecta mediante los siguientes módulos clave:

1. **Catálogos Principales**: Clientes y Proveedores (`admClientes`), Agentes (`admAgentes`), Almacenes (`admAlmacenes`), Monedas (`admMonedas`), Clasificaciones y Características.
2. **Productos y Servicios**: Productos (`admProductos`), Unidades de Medida (`admUnidadesMedidaPeso`), Listas de Precios de Compra (`admPreciosCompra`), Paquetes (`admComponentesPaquete`) y Promociones (`admPromociones`).
3. **Encabezado y Detalle de Documentos**: Documentos (`admDocumentos`), Movimientos de Documento (`admMovimientos`), Conceptos (`admConceptos`), Domicilios (`admDomicilios`) y Folios Digitales (`admFoliosDigitales`).
4. **Inventarios, Existencias y Costeos**: Capas de Producto (`admCapasProducto`), Existencias y Costos (`admExistenciaCosto`), Números de Serie (`admNumerosSerie`), Movimientos de Inventario Físico (`admMovtosInvFisico`).
5. **Contabilidad y Afectaciones**: Asientos Contables (`admAsientosContables`), Movimientos Contables (`admMovimientosContables`), Prepólizas (`admPrepolizas`), Acumulados (`admAcumulados`) y Afectación de Cargos/Abonos (`admAsocCargosAbonos`).
6. **Integración Nube y Pagos Electronic**: Ligas de Pago (`admLigasPago`), CEPs (`admMovtosCEPs`), Configuración Nube (`admConfigProveedoresNube`, `NubeCuentas`, `NubeDiarios`).

---

## 3. Tablas de la Empresa

### `admAcumulados` — Tabla de Acumulados

> Tablas de la empresa

> Dueños del acumulado

> La mayoría de los acumulados tienen dos dueños.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDACUMULADO` | `I` | Identificador del acumulado. |
| 2 | `CIDTIPOACUMULADO` | `I` | Identificador del tipo de un acumulado. |
| 3 | `CIDOWNER1` | `I` | Identificador del primer dueño del acumulado.<br>Importante: Los acumulados tienen dos dueños.<br>Ejemplo: En el acumulado de las ventas del producto por cliente<br>los dueños son cliente y producto. En el acumulado de entradas<br>del producto al almacén los dueños son almacén y producto. |
| 4 | `CIDOWNER2` | `I` | Identificador del segundo dueño del acumulado.<br>Importante: Algunos acumulados sólo tienen un dueño, en estos<br>acumulados, el campo vIdCatalogo2 esta vacío.<br>Ejemplo: El acumulado de ventas por cliente sólo tiene un<br>dueño, el cliente, no hay segundo dueño. |
| 5 | `CIMPORTEMODELO` | `I` | Número de importe del documento o de movimiento a relacionar<br>con el tipo de acumulado. |
| 6 | `CIDEJERCICIO` | `I` | Identificador del ejercicio del acumulado. |
| 7 | `CIMPORTEINICIAL` | `F` | Importe Inicial del acumulado. |
| 8 | `CIDMONEDA` | `I` | Identificador de moneda que utiliza el acumulado. |
| 9 | `CIMPORTEPERIODO1` | `F` | Importe del periodo 1. |
| 10 | `CIMPORTEPERIODO2` | `F` | Importe del periodo 2. |
| 11 | `CIMPORTEPERIODO3` | `F` | Importe del periodo 3. |
| 12 | `CIMPORTEPERIODO4` | `F` | Importe del periodo 4. |
| 13 | `CIMPORTEPERIODO5` | `F` | Importe del periodo 5. |
| 14 | `CIMPORTEPERIODO6` | `F` | Importe del periodo 6. |
| 15 | `CIMPORTEPERIODO7` | `F` | Importe del periodo 7. |
| 16 | `CIMPORTEPERIODO8` | `F` | Importe del periodo 8. |
| 17 | `CIMPORTEPERIODO9` | `F` | Importe del periodo 9. |
| 18 | `CIMPORTEPERIODO10` | `F` | Importe del periodo 10. |
| 19 | `CIMPORTEPERIODO11` | `F` | Importe del periodo 11. |
| 20 | `CIMPORTEPERIODO12` | `F` | Importe del periodo 12. |
| 21 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 30 caracteres. |

---

### `admAcumuladosTipos` — Tabla de Tipos de acumulados

> Esta tabla contiene los campos de los Tipos de Acumulados, con la información estadística de los  documentos (Ventas por mes, Compras a un Proveedor, etcétera).

> Los acumulados tienen dos dueños.  Ejemplo: En el acumulado de las ventas del producto por cliente  los dueños son cliente y producto, en el acumulado de entradas  del producto al almacén los dueños son almacén y producto.

> Nota: Algunos acumulados sólo tienen un dueño, por lo que  presentan el campo vacío vIdCatalogo2.  Ejemplo: El acumulado de ventas por cliente sólo tiene un  dueño: el cliente, no hay segundo dueño

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDTIPOACUMULADO` | `I` | Identificador del tipo de acumulado. |
| 2 | `CNOMBRE` | `V` | Nombre del tipo de acumulado.<br>Varchar: 60 caracteres. |
| 3 | `CTIPOOWNER1` | `I` | Indica el tipo del primer dueño de un acumulado.<br>Los acumulados tienen dos dueños.<br>Ejemplo: En el acumulado de las ventas del producto por cliente<br>los dueños son cliente y producto, en el acumulado de entradas<br>del producto al almacén los dueños son almacén y producto. |
| 4 | `CTIPOOWNER2` | `I` | Indica el tipo del segundo dueño de un acumulado.<br>Nota: Algunos acumulados sólo tienen un dueño, por lo que<br>presentan el campo vacío vIdCatalogo2.<br>Ejemplo: El acumulado de ventas por cliente sólo tiene un<br>dueño: el cliente, no hay segundo dueño |
| 5 | `CTIPOACTUALIZACION` | `I` | Es el tipo de actualización:<br>0 = Actualizacion por saldos.<br>1 = Actualización por estadísticas. |
| 6 | `CTIPOMONEDA` | `I` | Moneda del acumulado:<br>1 = El acumulado se encuentra en la moneda base.<br>2 = El acumulado se encuentra en la moneda del cliente o<br>proveedor.<br>3 = El acumulado se encuentra en la moneda de los documentos<br>que lo afectaron. |

---

### `admAgentes` — Tabla de Agentes

> Esta tabla contiene los campos del catálogo Agentes

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAGENTE` | `I` | Identificador del agente. |
| 2 | `CCODIGOAGENTE` | `V` | Código del agente.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBREAGENTE` | `V` | Nombre del agente.<br>Varchar: 60 caracteres. |
| 4 | `CFECHAALTAAGENTE` | `D` | Fecha de alta del agente. |
| 5 | `CTIPOAGENTE` | `I` | Tipo de agente:<br>1 = Agente de Ventas.<br>2 = Agente Venta / Cobro.<br>3 = Agente de Cobro. |
| 6 | `CCOMISIONVENTAAGENTE` | `F` | Comisión de venta del agente. |
| 7 | `CCOMISIONCOBROAGENTE` | `F` | Comisión de cobro del agente. |
| 8 | `CIDCLIENTE` | `I` | Permite considerar un cliente como agente. |
| 9 | `CIDPROVEEDOR` | `I` | Permite considerar un agente como proveedor. |
| 10 | `CIDVALORCLASIFICACION1` | `I` | Identificador de la clasificación 1 del agente. |
| 11 | `CIDVALORCLASIFICACION2` | `I` | Identificador de la clasificación 2 del agente. |
| 12 | `CIDVALORCLASIFICACION3` | `I` | Identificador de la clasificación 3 del agente. |
| 13 | `CIDVALORCLASIFICACION4` | `I` | Identificador de la clasificación 4 del agente. |
| 14 | `CIDVALORCLASIFICACION5` | `I` | Identificador de la clasificación 5 del agente. |
| 15 | `CIDVALORCLASIFICACION6` | `I` | Identificador de la clasificación 6 del agente. |
| 16 | `CSEGCONTAGENTE` | `V` | Segmento 1 de la cuenta contable del agente.<br>Varchar: 50 caracteres. |
| 17 | `CTEXTOEXTRA1` | `V` | Texto extra 1.<br>Varchar: 50 caracteres. |
| 18 | `CTEXTOEXTRA2` | `V` | Texto extra 2.<br>Varchar: 50 caracteres. |
| 19 | `CTEXTOEXTRA3` | `V` | Texto extra 3.<br>Varchar: 50 caracteres. |
| 20 | `CFECHAEXTRA` | `D` | Fecha extra.<br>Varchar: 50 caracteres. |
| 21 | `CIMPORTEEXTRA1` | `F` | Importe extra 1. |
| 22 | `CIMPORTEEXTRA2` | `F` | Importe extra 2. |
| 23 | `CIMPORTEEXTRA3` | `F` | Importe extra 3. |
| 24 | `CIMPORTEEXTRA4` | `F` | Importe extra 4. |
| 25 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 26 | `CSCAGENTE2` | `V` | Segmento 2 de la cuenta contable del agente.<br>Varchar: 50 caracteres. |
| 27 | `CSCAGENTE3` | `V` | Segmento 3 de la cuenta contable del agente.<br>Varchar: 50 caracteres. |

---

### `admAlmacenes` — Tabla de Almacenes

> Esta tabla contiene los campos del catálogo Almacenes.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDALMACEN` | `I` | Identificador del almacén. |
| 2 | `CCODIGOALMACEN` | `V` | Código del almacén.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBREALMACEN` | `V` | Nombre del almacén.<br>Varchar: 60 caracteres. |
| 4 | `CFECHAALTAALMACEN` | `D` | Fecha de alta del almacén. |
| 5 | `CIDVALORCLASIFICACION1` | `I` | Identificador de la clasificación 1 del almacén. |
| 6 | `CIDVALORCLASIFICACION2` | `I` | Identificador de la clasificación 2 del almacén. |
| 7 | `CIDVALORCLASIFICACION3` | `I` | Identificador de la clasificación 3 del almacén. |
| 8 | `CIDVALORCLASIFICACION4` | `I` | Identificador de la clasificación 4 del almacén. |
| 9 | `CIDVALORCLASIFICACION5` | `I` | Identificador de la clasificación 5 del almacén. |
| 10 | `CIDVALORCLASIFICACION6` | `I` | Identificador de la clasificación 6 del almacén. |
| 11 | `CSEGCONTALMACEN` | `V` | Segmento 1 de la cuenta contable del almacén.<br>Varchar: 50 caracteres. |
| 12 | `CTEXTOEXTRA1` | `V` | Texto extra 1.<br>Varchar: 50 caracteres. |
| 13 | `CTEXTOEXTRA2` | `V` | Texto extra 2.<br>Varchar: 50 caracteres. |
| 14 | `CTEXTOEXTRA3` | `V` | Texto extra 3.<br>Varchar: 50 caracteres. |
| 15 | `CFECHAEXTRA` | `D` | Fecha extra. |
| 16 | `CIMPORTEEXTRA1` | `F` | Importe extra 1. |
| 17 | `CIMPORTEEXTRA2` | `F` | Importe extra 2. |
| 18 | `CIMPORTEEXTRA3` | `F` | Importe extra 3. |
| 19 | `CIMPORTEEXTRA4` | `F` | Importe extra 4. |
| 20 | `CBANDOMICILIO` | `I` | Bandera de domicilio. Indica si el almacén lleva domicilio o no. |
| 21 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 22 | `CSCALMAC2` | `V` | Segmento 2 de la cuenta contable del almacén.<br>Varchar: 50 caracteres. |
| 23 | `CSCALMAC3` | `V` | Segmento 3 de la cuenta contable del almacén.<br>Varchar: 50 caracteres. |
| 24 | `CSISTORIG` | `I` | Sistema origen. |

---

### `admAsientosContables` — Tabla de Asientos contables

> Esta tabla contiene los asientos contables que indican la estructura de las pólizas.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDASIENTOCONTABLE` | `I` | Identificador del asiento contable. |
| 2 | `CNUMEROASIENTOCONTABLE` | `V` | Número del asiento contable.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBREASIENTOCONTABLE` | `V` | Nombre del asiento contable.<br>Varchar: 60 caracteres. |
| 4 | `CFRECUENCIA` | `I` | Frecuencia con las que se generan las pólizas del asiento.<br>1 = Una póliza por documento.<br>2 = Una póliza por día.<br>3 = Una póliza por periodo. |
| 5 | `CORIGENFECHA` | `I` | Origen de la fecha de las pólizas generadas por el asiento.<br>1 = Fecha del documento.<br>2 = Fecha de contabilización. |
| 6 | `CTIPOPOLIZA` | `I` | Tipo de las pólizas generadas por el asiento contable.<br>1 = Ingresos<br>2 = Egresos<br>3 = Diario<br>4 = Orden |
| 7 | `CORIGENNUMERO` | `I` | Origen del número de las pólizas generadas por este asiento:<br>1 = Consecutivo de CONTPAQi® Contabilidad.<br>2 = Consecutivo de CONTPAQi® Comercial.<br>3 = Folio del documento. |
| 8 | `CORIGENCONCEPTO` | `I` | Origen del concepto de las pólizas generadas por el asiento<br>contable:<br>1 = Ninguno.<br>2 = Referencia del documento<br>3 = Nombre del concepto<br>4 = Texto capturado<br>5 = Texto capturado+Folio del documento<br>6 = Texto Extra 1 del Documento<br>7 = Texto Extra 2 del Documento<br>8 = Texto Extra 3 del Documento |
| 9 | `CCONCEPTO` | `V` | Concepto capturado de las pólizas generadas por el asiento<br>contable.<br>Varchar: 50 caracteres.<br>Nota: Sólo se usa si el campo cTipoConcepto es igual a 4 o 5. |
| 10 | `CDIARIO` | `V` | Diario por omisión de las pólizas generadas por el asiento<br>contable.<br>Varchar: 10 caracteres. |
| 11 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admAsocAcumConceptos` — Tabla de Conceptos y tipos de

> Esta tabla contiene una relación entre los importes de un Concepto de Documento y los Tipo de Acumulado  que debe afectar.

> 1  CIDCONCEPTOTIPO  ACUMULADO

> I  Identificador de la tabla. Se usa en para tener un campo que  sirva de llave única.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCONCEPTOTIPO
ACUMULADO` | `I` | Identificador de la tabla. Se usa en para tener un campo que<br>sirva de llave única. |
| 2 | `CIDCONCEPTODOCUMENTO` | `I` | Identificador del concepto de documento. |
| 3 | `CIDTIPOACUMULADO` | `I` | Identificador del tipo de acumulado. |
| 4 | `CIMPORTEMODELO` | `I` | Número de importe del documento o de movimiento a relacionar<br>con el tipo de acumulado. |
| 5 | `CSUMARESTA` | `I` | Indica si el importe suma o resta a un acumulado.<br>0 = Resta<br>1 = Suma |
| 6 | `CESTATUS` | `I` | Indica si el concepto está activo o inactivo para su despliegue.<br>0 = Inactivo<br>1 = Activo |

---

### `admAsocCargosAbonos` — Tabla de Abonos y Cargos

> Esta tabla contiene las relaciones entre los documentos de abonos y cargos que se saldan entre ellos, ya sea  parcial o completamente.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDDOCUMENTOABONO` | `I` | Identificador del documento de abono. |
| 3 | `CIDDOCUMENTOCARGO` | `I` | Identificador del documento de cargo. |
| 4 | `CIMPORTEABONO` | `F` | Importe de la asociación en la moneda del abono. |
| 5 | `CIMPORTECARGO` | `F` | Importe de la asociación en la moneda del cargo. |
| 6 | `CFECHAABONOCARGO` | `D` | Fecha de la asociación. |
| 7 | `CIDDESCUENTOPRONTOPAGO` | `I` | Identificador del documento de descuento por pronto pago<br>generado por esta asociación. |
| 8 | `CIDUTILIDADPERDIDACAMB` | `I` | Identificador del documento de utilidad o pérdida cambiaria<br>generado por esta asociación. |
| 9 | `CIDAJUSIVA` | `I` | Identificador del documento de ajuste de IVA generado por esta<br>asociación. |

---

### `admBanderas` — Tabla de Imágenes de banderas de monedas

> Esta tabla almacena la información de las imágenes de banderas de monedas

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDBANDERA` | `I` | Identificador de la imagen. |
| 2 | `CNOMBREBANDERA` | `V` | Nombre de la imagen.<br>Varchar: 40 caracteres. |
| 3 | `CBANDERA` | `T` | Imagen. |
| 4 | `CCLAVEISO` | `V` | Clave ISO.<br>Varchar: 3 caracteres. |

---

### `admCapasProducto` — Tabla de Capas de producto

> Esta tabla contiene los datos de capas UEPS, PEPS, pedimentos y lotes pertenecientes a los productos.

> Importante: Siempre será el mismo del movimiento. Se coloca  aquí solo para acelerar las búsquedas.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCAPA` | `I` | Identificador único de la capa. |
| 2 | `CIDALMACEN` | `I` | Identificador del almacén asignado a la capa.<br>Importante: Siempre será el mismo del movimiento. Se coloca<br>aquí solo para acelerar las búsquedas. |
| 3 | `CIDPRODUCTO` | `I` | Producto que cuenta con número de serie y/o lote y/o pedimento<br>o que su costo se calcula por UEPS o PEPS. |
| 4 | `CFECHA` | `D` | Fecha del movimiento que generó la capa. |
| 5 | `CTIPOCAPA` | `I` | Contenido de la capa:<br>1 = Número de serie<br>2 = Pedimento<br>3 = Lote<br>4 = Pedimento y Lote<br>5 = Capa de costo (para costeo UEPS y PEPS) |
| 6 | `CNUMEROLOTE` | `V` | Número de lote.<br>Varchar: 30 caracteres. |
| 7 | `CFECHACADUCIDAD` | `D` | Fecha de caducidad del lote. |
| 8 | `CFECHAFABRICACION` | `D` | Fecha de fabricación del lote. |
| 9 | `CPEDIMENTO` | `V` | Número del pedimento.<br>Varchar: 30 caracteres. |
| 10 | `CADUANA` | `V` | Agencia aduanal.<br>Varchar: 60 caracteres. |
| 11 | `CFECHAPEDIMENTO` | `D` | Fecha del pedimento. |
| 12 | `CTIPOCAMBIO` | `F` | Tipo de cambio arbitrario del pedimento. |
| 13 | `CEXISTENCIA` | `F` | Existencia todavía disponible de la capa. |
| 14 | `CCOSTO` | `F` | Costo unitario del producto en la capa. |
| 15 | `CIDCAPAORIGEN` | `I` | Identificador de la capa origen. Se usa en consignaciones del<br>cliente y traspasos de capa para indicarle a la capa en el<br>almacén destino, qué capa la generó en el almacén origen. |
| 16 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 17 | `CNUMADUANA` | `I` | Número de la agencia aduanal. |
| 18 | `CCLAVESAT` | `V` | Clave SAT utilizada para los pedimentos<br>Varchar: 30 caracteres. |

---

### `admCaracteristicas` — Tabla de Características

> Esta tabla almacena características cuando existen productos que las tienen.

> Importante: La tabla padre, en particular, tiene los nombres de todas las características que podrá tener un  producto en particular.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPADRECARACTERISTICA` | `I` | Identificador de una característica. |
| 2 | `CNOMBRECARACTERISTICA` | `V` | Nombre de la característica.<br>Varchar: 60 caracteres. |

---

### `admCaracteristicasValores` — Tabla de Valores de

> Esta tabla contiene los valores posibles de las características.

> Nota: Cada valor corresponde a una sola característica.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDVALORCARACTERISTICA` | `I` | Identificador de la característica a la que pertenece el valor. |
| 2 | `CIDPADRECARACTERISTICA` | `I` | Identificador del valor de la característica. |
| 3 | `CVALORCARACTERISTICA` | `V` | Valor de la característica.<br>Varchar: 20 caracteres. |
| 4 | `CNEMOCARACTERISTICA` | `V` | Nemotécnico de la característica, usado en la captura de<br>movimientos.<br>Varchar: 3 caracteres. |

---

### `admClasificaciones` — Tabla de Clasificaciones

> Esta tabla almacena las distintas clasificaciones de cada catálogo.

> Esta es la tabla padre y solo almacena información general de cada clasificación.

> 1 – 6 = Clasificaciones de Agentes  7 –12 = Clasificaciones de Clientes  13 – 18 = Clasificaciones del Proveedor  19 – 24 = Clasificaciones del Almacén  25 – 30 = Clasificaciones del Producto  31 = Clasificación de Movimientos

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCLASIFICACION` | `I` | Identificador de la clasificación del catálogo.<br>1 – 6 = Clasificaciones de Agentes<br>7 –12 = Clasificaciones de Clientes<br>13 – 18 = Clasificaciones del Proveedor<br>19 – 24 = Clasificaciones del Almacén<br>25 – 30 = Clasificaciones del Producto<br>31 = Clasificación de Movimientos |
| 2 | `CNOMBRECLASIFICACION` | `V` | Nombre de la clasificación.<br>Varchar: 60 caracteres. |

---

### `admClasificacionesValores` — Tabla de Valores de

> Esta tabla almacena clasificaciones de cada catálogo.

> Esta tabla es la que almacena los valores que tiene cada clasificación y los descuentos por excepción de  cada clasificación.

> 4  CCODIGOVALOR  CLASIFICACION

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDVALORCLASIFICACION` | `I` | Identificador del valor de las clasificaciones. |
| 2 | `CVALORCLASIFICACION` | `V` | Valores de las clasificaciones.<br>Varchar: 60 caracteres. |
| 3 | `CIDCLASIFICACION` | `I` | Clasificación a la que pertenece el valor. |
| 4 | `CCODIGOVALOR
CLASIFICACION` | `V` | Carácter utilizado para la fácil captura de los valores de<br>clasificación.<br>Varchar: 3 caracteres. |

---

### `admClientes` — Tabla de Clientes y Proveedores

> Esta tabla contiene los campos de los Clientes y Proveedores.

> 62  CBANPRODUCTO  CONSIGNACION

> I  Indica si se permite producto en consignación:  0 = No   1 = Sí

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCLIENTEPROVEEDOR` | `I` | Identificador del cliente o proveedor. |
| 2 | `CCODIGOCLIENTE` | `V` | Código del cliente o proveedor.<br>Varchar: 30 caracteres. |
| 3 | `CRAZONSOCIAL` | `V` | Razón Social del cliente o proveedor.<br>Varchar: 60 caracteres. |
| 4 | `CFECHAALTA` | `D` | Fecha de alta del cliente/proveedor. |
| 5 | `CRFC` | `V` | Registro Federal de Contribuyentes del cliente.<br>Varchar: 20 caracteres. |
| 6 | `CCURP` | `V` | Código Único de Registro de Población o si el cliente es extranjero<br>se puede utilizar para guardar el número de identidad tributaria<br>Varchar: 20 caracteres. |
| 7 | `CDENComercial` | `V` | Denominación comercial del cliente/ proveedor.<br>Varchar: 50 caracteres. |
| 8 | `CREPLEGAL` | `V` | Representante o contacto del cliente.<br>Varchar: 50 caracteres. |
| 9 | `CIDMONEDA` | `I` | Moneda del cliente/ proveedor. |
| 10 | `CLISTAPRECIOCLIENTE` | `I` | Significado de la lista de precios asignada al cliente. |
| 11 | `CDESCUENTODOCTO` | `F` | Porcentaje de descuento por documento asignado al cliente. |
| 12 | `CDESCUENTOMOVTO` | `F` | Porcentaje de descuento por movimiento asignado al cliente. |
| 13 | `CBANVENTACREDITO` | `I` | Indica si se activa o no la venta a crédito.<br>0 = No<br>1 = Sí |
| 14 | `CIDVALORCLASIFCLIENTE1` | `I` | Identificador de la clasificación 1 del cliente. |
| 15 | `CIDVALORCLASIFCLIENTE2` | `I` | Identificador de la clasificación 2 del cliente. |
| 16 | `CIDVALORCLASIFCLIENTE3` | `I` | Identificador de la clasificación 3 del cliente. |
| 17 | `CIDVALORCLASIFCLIENTE4` | `I` | Identificador de la clasificación 4 del cliente. |
| 18 | `CIDVALORCLASIFCLIENTE5` | `I` | Identificador de la clasificación 5 del cliente. |
| 19 | `CIDVALORCLASIFCLIENTE6` | `I` | Identificador de la clasificación 6 del cliente. |
| 20 | `CTIPOCLIENTE` | `I` | Tipo de cliente o proveedor:<br>1 = Cliente<br>2 = Cliente/Proveedor<br>3 = Proveedor |
| 21 | `CESTATUS` | `I` | Estatus actual del cliente/proveedor.<br>0 = Inactivo<br>1 = Activo |
| 22 | `CFECHABAJA` | `D` | Fecha en que el cliente y/o proveedor quedó inactivo. |
| 23 | `CFECHAULTIMAREVISION` | `D` | Fecha de la última revisión. |
| 24 | `CLIMITECREDITOCLIENTE` | `F` | Límite de crédito del cliente. |
| 25 | `CDIASCREDITOCLIENTE` | `I` | Días de crédito del cliente. |
| 26 | `CBANEXCEDERCREDITO` | `I` | Indica si se permite exceder el crédito o no.<br>0 = No<br>1 = Sí |
| 27 | `CDESCUENTOPRONTOPAGO` | `F` | Descuento por pronto pago. |
| 28 | `CDIASPRONTOPAGO` | `I` | Días de descuento por pronto pago del cliente. |
| 29 | `CINTERESMORATORIO` | `F` | Porcentaje del Interés moratorio. |
| 30 | `CDIAPAGO` | `I` | Días en que el cliente pago. |
| 31 | `CDIASREVISION` | `I` | Días de revisión de crédito. |
| 32 | `CMENSAJERIA` | `V` | Nombre del servicio de mensajería.<br>Varchar: 20 caracteres. |
| 33 | `CCUENTAMENSAJERIA` | `V` | Número de cuenta de la mensajería.<br>Varchar: 60 caracteres. |
| 34 | `CDIASEMBARQUECLIENTE` | `I` | Día en que se embarcará mercancía al cliente. |
| 35 | `CIDALMACEN` | `I` | Permite considerar un cliente como un almacén. |
| 36 | `CIDAGENTEVENTA` | `I` | Agente de venta del cliente/proveedor. |
| 37 | `CIDAGENTECOBRO` | `I` | Agente de cobro del cliente/proveedor. |
| 38 | `CRESTRICCIONAGENTE` | `I` | Restricciones para el uso de agentes. |
| 39 | `CIMPUESTO1` | `F` | Porcentaje de impuesto 1 del cliente. |
| 40 | `CIMPUESTO2` | `F` | Porcentaje de impuesto 2 del cliente. |
| 41 | `CIMPUESTO3` | `F` | Porcentaje de impuesto 3 del cliente. |
| 42 | `CRETENCIONCLIENTE1` | `F` | Porcentaje de la retención 1 para cliente. |
| 43 | `CRETENCIONCLIENTE2` | `F` | Porcentaje de la retención 2 para cliente. |
| 44 | `CIDVALORCLASIFPROVEEDOR1` | `I` | Identificador de la clasificación 1 del proveedor. |
| 45 | `CIDVALORCLASIFPROVEEDOR2` | `I` | Identificador de la clasificación 2 del proveedor. |
| 46 | `CIDVALORCLASIFPROVEEDOR3` | `I` | Identificador de la clasificación 3 del proveedor. |
| 47 | `CIDVALORCLASIFPROVEEDOR4` | `I` | Identificador de la clasificación 4 del proveedor. |
| 48 | `CIDVALORCLASIFPROVEEDOR5` | `I` | Identificador de la clasificación 5 del proveedor. |
| 49 | `CIDVALORCLASIFPROVEEDOR6` | `I` | Identificador de la clasificación 6 del proveedor. |
| 50 | `CLIMITECREDITOPROVEEDOR` | `F` | Límite de crédito que otorga el proveedor. |
| 51 | `CDIASCREDITOPROVEEDOR` | `I` | Días de crédito que otorga el proveedor. |
| 52 | `CTIEMPOENTREGA` | `I` | Tiempo de entrega del proveedor en días. |
| 53 | `CDIASEMBARQUEPROVEEDOR` | `I` | Días en que el proveedor embarca. |
| 54 | `CIMPUESTOPROVEEDOR1` | `F` | Porcentaje de impuesto 1 del proveedor. |
| 55 | `CIMPUESTOPROVEEDOR2` | `F` | Porcentaje de impuesto 2 del proveedor. |
| 56 | `CIMPUESTOPROVEEDOR3` | `F` | Porcentaje de impuesto 3 del proveedor. |
| 57 | `CRETENCIONPROVEEDOR1` | `F` | Porcentaje de la retención 1 para proveedor. |
| 58 | `CRETENCIONPROVEEDOR2` | `F` | Porcentaje de la retención 2 para proveedor. |
| 59 | `CBANINTERESMORATORIO` | `I` | Indica si a un cliente se le calculan intereses moratorios.<br>0 = No<br>1 = Sí |
| 60 | `CCOMVENTAEXCEPCLIENTE` | `F` | Comisión de venta por excepción del cliente. |
| 61 | `CCOMCOBROEXCEPCLIENTE` | `F` | Comisión de cobro por excepción del cliente. |
| 62 | `CBANPRODUCTO
CONSIGNACION` | `I` | Indica si se permite producto en consignación:<br>0 = No<br>1 = Sí |
| 63 | `CSEGCONTCLIENTE1` | `V` | 1o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 64 | `CSEGCONTCLIENTE2` | `V` | 2o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 65 | `CSEGCONTCLIENTE3` | `V` | 3o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 66 | `CSEGCONTCLIENTE4` | `V` | 4o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 67 | `CSEGCONTCLIENTE5` | `V` | 5o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 68 | `CSEGCONTCLIENTE6` | `V` | 6o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 69 | `CSEGCONTCLIENTE7` | `V` | 7o. Segmento contable del cliente.<br>Varchar: 50 caracteres. |
| 70 | `CSEGCONTPROVEEDOR1` | `V` | 1o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 71 | `CSEGCONTPROVEEDOR2` | `V` | 2o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 72 | `CSEGCONTPROVEEDOR3` | `V` | 3o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 73 | `CSEGCONTPROVEEDOR4` | `V` | 4o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 74 | `CSEGCONTPROVEEDOR5` | `V` | 5o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 75 | `CSEGCONTPROVEEDOR6` | `V` | 6o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 76 | `CSEGCONTPROVEEDOR7` | `V` | 7o. Segmento contable del proveedor.<br>Varchar: 50 caracteres. |
| 77 | `CTEXTOEXTRA1` | `V` | Texto extra 1 para interfaz configurable.<br>Varchar: 50 caracteres. |
| 78 | `CTEXTOEXTRA2` | `V` | Texto extra 2 para interfaz configurable.<br>Varchar: 50 caracteres. |
| 79 | `CTEXTOEXTRA3` | `V` | Texto extra 3 para interfaz configurable.<br>Varchar: 50 caracteres. |
| 80 | `CFECHAEXTRA` | `D` | Fecha extra para interfaz configurable. |
| 81 | `CIMPORTEEXTRA1` | `F` | Importe extra 1 para interfaz configurable. |
| 82 | `CIMPORTEEXTRA2` | `F` | Importe extra 2 para interfaz configurable. |
| 83 | `CIMPORTEEXTRA3` | `F` | Importe extra 3 para interfaz configurable. |
| 84 | `CIMPORTEEXTRA4` | `F` | Importe extra 4 para interfaz configurable. |
| 85 | `CBANDOMICILIO` | `I` | Bandera de domicilio. Indica si el cliente lleva domicilio o no. |
| 86 | `CBANCREDITOYCOBRANZA` | `I` | Bandera de crédito y cobranza. |
| 87 | `CBANENVIO` | `I` | Bandera de envío. |
| 88 | `CBANAGENTE` | `I` | Bandera de agente. |
| 89 | `CBANIMPUESTO` | `I` | Bandera de impuesto. |
| 90 | `CBANPRECIO` | `I` | Bandera de precio. |
| 91 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 92 | `CFACTERC01` | `I` | Indica si el cliente puede facturar a terceros.<br>0 = No<br>1 = Sí |
| 93 | `CCOMVENTA` | `F` | Indica la comisión de venta del agente para todos los<br>documentos de ese cliente. |
| 94 | `CCOMCOBRO` | `F` | Indica la comisión de cobro del agente para todos los<br>documentos de ese cliente. |
| 95 | `CIDMONEDA2` | `I` | Identifica la moneda asumida en documentos para el cliente y/o<br>proveedor. Esta moneda se define en el catálogo Clientes y<br>Proveedores, respectivamente. |
| 96 | `CEMAIL1` | `V` | Dirección de correo electrónico 1 para la emisión y entrega de<br>CFD’s.<br>Varchar: 60 caracteres. |
| 97 | `CEMAIL2` | `V` | Dirección de correo electrónico 2 para la emisión y entrega de<br>CFD’s.<br>Varchar: 60 caracteres. |
| 98 | `CEMAIL3` | `V` | Dirección de correo electrónico 3 para la emisión y entrega de<br>CFD’s.<br>Varchar: 60 caracteres. |
| 99 | `CTIPOENTRE` | `I` | Tipo de entrega:<br>0 = Correo electrónico<br>1 = Impresión<br>2 = Archivo en disco |
| 100 | `CCONCTEEMA` | `I` | Abrir el correo electrónico al enviar:<br>0 = No<br>1 = Sí |
| 101 | `CFTOADDEND` | `I` | Reservado |
| 102 | `CIDCERTCTE` | `I` | Identificador del certificado del cliente |
| 103 | `CENCRIPENT` | `I` | Entregar encriptado:<br>0 = No<br>1 = Sí |
| 104 | `CBANCFD` | `I` | Indica si se manejará CFD.<br>0 = No<br>1 = Sí |
| 105 | `CTEXTOEXTRA4` | `V` | Texto extra 4 para Interfaz configurable.<br>Varchar: 50 caracteres. |
| 106 | `CTEXTOEXTRA5` | `V` | Texto extra 5 para Interfaz configurable.<br>Varchar: 50 caracteres. |
| 107 | `CIMPORTEEXTRA5` | `F` | Importe extra 5 para Interfaz configurable. |
| 108 | `CIDADDENDA` | `I` | Identificador de la addenda. |
| 109 | `CCODPROVCO` | `I` | Código de proveedor en CONTPAQi® Contabilidad. Este campo<br>se muestra en la ventana Segmentos Contables por Proveedor. |
| 110 | `CENVACUSE` | `I` | Reservado |
| 111 | `CCON1NOM` | `V` | Reservado |
| 112 | `CCON1TEL` | `V` | Reservado |
| 113 | `CQUITABLAN` | `I` | Eliminar espacios en blanco al emitir CFD.<br>0 = No<br>1 = Sí |
| 114 | `CFMTOENTRE` | `I` | Formato de entrega de CFD por omisión.<br>0 = PDF<br>1 = XML |
| 115 | `CIDCOMPLEM` | `I` | Identificador del complemento |
| 116 | `CDESGLOSAI` | `I` | Desglosar IEPS en CFD.<br>0 = No<br>1 = Sí |
| 117 | `CLIMDOCTOS` | `I` | Número máximo de documentos vencidos para limitar la captura<br>de nuevos documentos. |
| 118 | `CSITIOFTP` | `V` | Dirección ftp para entrega de CFD.<br>Varchar: 60 caracteres. |
| 119 | `CUSRFTP` | `V` | Usuario ftp para entrega de CFD.<br>Varchar: 60 caracteres. |
| 120 | `CMETODOPAG` | `V` | Método de pago (efectivo, transferencia, etc.).<br>Varchar: 100 caracteres. |
| 121 | `CNUMCTAPAG` | `V` | Número de cuenta con la que se realizó el pago.<br>Varchar: 100 caracteres. |
| 122 | `CUSOCFDI` | `V` | Uso que el cliente le dará a los CFDIs por omisión.<br>Varchar: 30 caracteres |
| 123 | `CIDCUENTA` | `I` | Identificador de la cuenta bancaria asumida por el cliente |
| 125 | `CWHATSAPP` | `V` | Indica el número de contacto de WhatsApp<br>Varchar: 15 caracteres |

---

### `admComponentesPaquete` — Tabla de Componentes del

> Esta tabla almacena productos tipo Paquete. Un paquete está compuesto por varios productos.

> Nota: Se necesita para hacer único cada registro, ya que la llave  cIdPaquete+cIdProducto no es única.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCOMPONENTE` | `I` | Identificador del componente.<br>Nota: Se necesita para hacer único cada registro, ya que la llave<br>cIdPaquete+cIdProducto no es única. |
| 2 | `CIDPAQUETE` | `I` | Identificador del producto tipo paquete. |
| 3 | `CIDPRODUCTO` | `I` | Identificador del producto componente o del detalle componente. |
| 4 | `CCANTIDADPRODUCTO` | `F` | Cantidad del producto componente que incluye el paquete. |
| 5 | `CIDVALORCARACTERISTICA1` | `I` | Identificador del valor de la característica 1. |
| 6 | `CIDVALORCARACTERISTICA2` | `I` | Identificador del valor de la característica 2. |
| 7 | `CIDVALORCARACTERISTICA3` | `I` | Identificador del valor de la característica 3. |
| 8 | `CTIPOPRODUCTO` | `I` | Tipo del Producto:<br>1 = Producto<br>2 = Paquete<br>3 = Servicio |
| 9 | `CIDUNIVEN` | `I` | Indica la unidad que será asumida en los documentos de venta<br>para los productos con control de existencia por unidades. |

---

### `admConceptos` — Tabla de Conceptos de documento

> Esta tabla contiene los campos de los conceptos.

> modificable.

> 16  CUSAFECHAENTREGARECEP CION

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCONCEPTODOCUMENTO` | `I` | Identificador del concepto de documento. |
| 2 | `CCODIGOCONCEPTO` | `V` | Código del concepto.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBRECONCEPTO` | `V` | Nombre del concepto.<br>Varchar: 60 caracteres. |
| 4 | `CIDDOCUMENTODE` | `I` | Tipo de documento del concepto de documento. Referencia de la<br>tabla TblDocumentoDe. |
| 5 | `CNATURALEZA` | `I` | Naturaleza de los documentos generados por el concepto:<br>0 = Cargo<br>1 = Abono<br>2 = Sin naturaleza |
| 6 | `CDOCTOACREDITO` | `I` | Bandera que indica si el documento es a crédito o al contado:<br>0 = Documento al Contado<br>1 = Documento a Crédito |
| 7 | `CTIPOFOLIO` | `I` | Tipo de Folio:<br>1 = Folio automático calculado al crear el documento y<br>modificable.<br>2 = Folio automático calculado antes de imprimir el documento y<br>modificable. |
| 8 | `CMAXIMOMOVTOS` | `I` | Número máximo de movimientos que puede tener un<br>documento. |
| 9 | `CCREACLIENTE` | `I` | Indica si se puede crear un cliente en la captura del documento<br>asociado a este concepto:<br>1 = Sí<br>0 = No |
| 10 | `CSUMARPROMOCIONES` | `I` | Indica si un movimiento en el que se apliquen dos promociones<br>estas se deben sumar o no.<br>0 = No<br>1 = Sí |
| 11 | `CFORMAPREIMPRESA` | `V` | Ruta y nombre de la forma preimpresa para imprimir los<br>documentos del concepto.<br>Varchar: 253 caracteres. |
| 12 | `CORDENCALCULO` | `I` | Atributo que define el orden en que se calculan los impuestos,<br>descuentos e impuestos.<br>Las diferentes combinaciones que toma este orden son:<br>1 = Descuentos, Impuestos y Retenciones<br>2 = Descuentos, Retenciones e Impuestos<br>3 = Impuestos, Descuentos y Retenciones<br>4 = Impuestos, Retenciones y Descuentos<br>5 = Retenciones, Impuestos y Descuentos<br>6 = Retenciones, Descuentos e Impuestos |
| 13 | `CUSANOMBRECTEPROV` | `I` | Atributo que indica el comportamiento del control de nombre de<br>cliente o proveedor en los documentos:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 14 | `CUSARFC` | `I` | Atributo que indica el comportamiento de control del RFC del<br>cliente en el documento:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 15 | `CUSAFECHAVENCIMIENTO` | `I` | Atributo que indica el comportamiento de control de fecha de<br>vencimiento en el documento:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 16 | `CUSAFECHAENTREGARECEP
CION` | `I` | Atributo que indica el comportamiento de control de fecha de<br>entrega o recepción en el documento.<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 17 | `CUSAMONEDA` | `I` | Atributo que indica el comportamiento de control de moneda en<br>el documento:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 18 | `CUSATIPOCAMBIO` | `I` | Atributo que indica el comportamiento de control de tipo de<br>cambio en el documento.<br>1 = No es visible (por lo cual no se captura).<br>2 = Es visible y no es capturable (sólo lectura).<br>3 = Es visible y se captura. |
| 19 | `CUSACODIGOAGENTE` | `I` | Atributo que indica el comportamiento de control de código de<br>agente en el documento:<br>1 = No es visible (por lo cual no se captura).<br>2 = Es visible y no es capturable (sólo lectura).<br>3 = Es visible y se captura. |
| 20 | `CUSANOMBREAGENTE` | `I` | Atributo que indica el comportamiento de control del nombre de<br>agente en el documento:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura) |
| 21 | `CUSADIRECCION` | `I` | Atributo que indica el comportamiento de control de la dirección<br>del documento (stxDireccionDocumento) y del botón de acceso a<br>las direcciones:<br>1 = No es visible ni el stx ni el botón<br>2 = Es visible y no es capturable: el stx es visible y el botón de<br>direcciones aparece deshabilitado.<br>3 = Es visible y se captura: el stx es visible y el botón de<br>direcciones<br>aparece habilitado |
| 22 | `CUSAREFERENCIA` | `I` | Atributo que indica el comportamiento de control de la referencia<br>del documento:<br>1 = No es visible (por lo cual no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 23 | `CSERIEPOROMISION` | `V` | Serie por omisión de los documentos creados con el concepto.<br>Varchar: 11 caracteres. |
| 24 | `CANCHOCODIGOPRODUCTO` | `I` | Ancho en pixeles de la columna del código del producto. |
| 25 | `CUSANOMBREPRODUCTO` | `I` | Atributo utilizado para hacer visible la columna del nombre del<br>producto en los movimientos del documento para este concepto.<br>Si este campo se encuentra en cero, de todas formas el grid de<br>movimientos debe tener una columna cIdalmacen llena con un<br>asumido.<br>1 = No se usa<br>2 = No es Visible (por lo cual no se captura)<br>3 = Es visible y no es capturable (solo lectura)<br>4 = Es visible y se captura |
| 26 | `CANCHONOMBREPRODUCTO` | `I` | Ancho en pixeles de la columna del nombre del producto. |
| 27 | `CUSAALMACEN` | `I` | Atributo utilizado para hacer visible la columna de código de<br>Almacén en los movimientos del documento para este concepto.<br>Si este campo se encuentra en cero, de todas formas el grid de<br>movimientos debe tener una columna cIdalmacen llena con un<br>asumido.<br>1 = No se usa<br>2 = No es visible (por lo cual no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 28 | `CANCHOCODIGOALMACEN` | `I` | Ancho en pixeles de la columna del código del almacén. |
| 29 | `CANCHOIMPORTES` | `I` | Ancho en pixeles de las columnas de importes del movimiento<br>como precio, unidades, neto, impuestos, etcétera. |
| 30 | `CANCHOPORCENTAJES` | `I` | Ancho en pixeles de las columnas de porcentajes de impuestos,<br>retenciones y descuentos del movimiento. |
| 31 | `CANCHOUNIDADPESOMEDIDA` | `I` | Ancho en pixeles de la columna de unidad de peso y medida. |
| 32 | `CUSAPRECIO` | `I` | Bandera utilizada para hacer visible y utilizar la columna Precio<br>en los movimientos del documento para este concepto.<br>1 = No se usa<br>2 = No es visible (por lo cual no se captura)<br>3 = Es visible y no es capturable (solo lectura)<br>4 = Es visible y se captura |
| 33 | `CIDFORMULAPRECIO` | `I` | Numero de fórmula que se asocia a la columna Precio para<br>calcularse. |
| 34 | `CUSACOSTOCAPTURADO` | `I` | Atributo utilizado para hacer visible y utilizar la columna Costo<br>Unitario capturado por el usuario en los movimientos del<br>documento para este concepto:<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 35 | `CIDFORMULACOSTO
CAPTURADO` | `I` | Numero de fórmula que se asocia a la columna Costo Unitario<br>capturado por el usuario. |
| 36 | `CUSAEXISTENCIA` | `I` | Atributo utilizado para hacer visible y utilizar la columna de la<br>existencia del producto en el almacén, en los movimientos del<br>documento para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (solo lectura) |
| 37 | `CUSANETO` | `I` | Atributo utilizado para hacer visible y utilizar la columna Neto en<br>los movimientos del documento para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 38 | `CIDFORMULANETO` | `I` | Número de fórmula que se asocia a la columna Neto para<br>calcularse. |
| 39 | `CUSAPORCENTAJEIMPUESTO1` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de impuesto en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 40 | `CIDFORMULAPORCIMPUESTO1` | `I` | Fórmula del porcentaje de impuesto 1. |
| 41 | `CUSAIMPUESTO1` | `I` | Atributo utilizada para hacer visible y utilizar la columna del<br>impuesto 1 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 42 | `CIDFORMULAIMPUESTO1` | `I` | Número de fórmula que se asocia a la columna Impuesto 1 para<br>calcularse. |
| 43 | `CUSAPORCENTAJEIMPUESTO2` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje del Impuesto 2 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 44 | `CIDFORMULAPORCIMPUESTO2` | `I` | Fórmula del porcentaje de impuesto 2. |
| 45 | `CUSAIMPUESTO2` | `I` | Atributo utilizado para hacer visible y utilizar la columna del<br>Impuesto 2 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es Visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 46 | `CIDFORMULAIMPUESTO2` | `I` | Número de fórmula que se asocia a la columna Impuesto 2 para<br>calcularse. |
| 47 | `CUSAPORCENTAJEIMPUESTO3` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de Impuesto 3 en los movimientos del documento<br>para este Concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 48 | `CIDFORMULAPORCIMPUESTO3` | `I` | Fórmula del porcentaje de impuesto 3. |
| 49 | `CUSAIMPUESTO3` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Impuesto 3 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 50 | `CIDFORMULAIMPUESTO3` | `I` | Número de Fórmula que se asocia a la columna Impuesto 3<br>para calcularse. |
| 51 | `CUSAPORCENTAJERETENCION1` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>porcentaje de retención 1 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (por lo cual no se captura)<br>3 = Es visible y no es capturable (solo lectura)<br>4 = Es visible y se captura |
| 52 | `CIDFORMULAPORCRETENCION1` | `I` | Número de fórmula que se asocia a la columna porcentaje de<br>retención para calcularse. |
| 53 | `CUSARETENCION1` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Retención 1 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 54 | `CIDFORMULARETENCION1` | `I` | Número de fórmula que se asocia a la columna Retención 1<br>para calcularse. |
| 55 | `CUSAPORCENTAJERETENCION2` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de retención 2 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 56 | `CIDFORMULAPORCRETENCION2` | `I` | Número de fórmula que se asocia a la columna Porcentaje de<br>retención 2 para calcularse. |
| 57 | `CUSARETENCION2` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Retención 2 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 58 | `CIDFORMULARETENCION2` | `I` | Número de fórmula que se asocia a la columna Retención 2<br>para calcularse. |
| 59 | `CUSAPORCENTAJE
DESCUENTO1` | `I` | Atributo utilizado para hacer visible y utilizar la columna del<br>porcentaje de descuento 1 en los movimientos del documento<br>para este concepto:<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 60 | `CIDFORMULAPORC
DESCUENTO1` | `I` | Número de fórmula que se asocia a la columna Porcentaje de<br>descuento 1 para calcularse. |
| 61 | `CUSADESCUENTO1` | `I` | Atributo utilizada para hacer visible y utilizar la columna del<br>descuento 1 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 62 | `CIDFORMULADESCUENTO1` | `I` | Número de fórmula que se asocia a la columna Descuento 1<br>para calcularse. |
| 63 | `CUSAPORCENTAJE
DESCUENTO2` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de descuento 2 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 64 | `CIDFORMULAPORC
DESCUENTO2` | `I` | Número de fórmula que se asocia a la columna Porcentaje de<br>descuento 2 para calcularse. |
| 65 | `CUSADESCUENTO2` | `I` | Atributo utilizado para hacer visible y utilizar la columna del<br>descuento 2 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 66 | `CIDFORMULADESCUENTO2` | `I` | Número de fórmula que se asocia a la columna Descuento 2<br>para calcularse. |
| 67 | `CUSAPORCENTAJE
DESCUENTO3` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de descuento 3 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 68 | `CIDFORMULAPORC
DESCUENTO3` | `I` | Numero de frmula que se asocia a la columna de porcentaje de<br>descuento 3 para calcularse. |
| 69 | `CUSADESCUENTO3` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Descuento 3 en los movimientos del documento para este<br>concepto.<br>1= No se usa<br>2= No es visible (no se captura)<br>3= Es visible y no es capturable (sólo lectura)<br>4= Es visible y se captura |
| 70 | `CIDFORMULADESCUENTO3` | `I` | Número de fórmula que se asocia a la columna Descuento 3<br>para calcularse. |
| 71 | `CUSAPORCENTAJE
DESCUENTO4` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de descuento 4 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 72 | `CIDFORMULAPORC
DESCUENTO4` | `I` | Numero de formula que se asocia a la columna de porcentaje de<br>descuento 4 para calcularse. |
| 73 | `CUSADESCUENTO4` | `I` | Atributo utilizado para hacer visible y utilizar la columna del<br>descuento 4 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 74 | `CIDFORMULADESCUENTO4` | `I` | Número de fórmula que se asocia a la columna del importe de<br>descuento 4 para calcularse. |
| 75 | `CUSAPORCENTAJE
DESCUENTO5` | `I` | Atributo utilizado para hacer visible y utilizar la columna<br>Porcentaje de descuento 5 en los movimientos del documento<br>para este concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 76 | `CIDFORMULAPORC
DESCUENTO5` | `I` | Numero de formula que se asocia a la columna de porcentaje de<br>descuento 5 para calcularse. |
| 77 | `CUSADESCUENTO5` | `I` | Atributo utilizado para hacer visible y utilizar la columna del<br>descuento 5 en los movimientos del documento para este<br>concepto.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 78 | `CIDFORMULADESCUENTO5` | `I` | Número de fórmula que se asocia a la columna del importe de<br>descuento 5 para calcularse. |
| 79 | `CUSATOTAL` | `I` | Atributo que indica el comportamiento de control del total del<br>documento.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Valor sin uso. Utilizado por compatibilidad |
| 80 | `CANCHOREFERENCIA` | `I` | Ancho en pixeles de la columna de la referencia del movimiento. |
| 81 | `CUSACLASIFICACIONMOVTO` | `I` | Bandera para hacer visible y utilizar la clasificación del<br>movimiento.<br>1 = No se usa<br>2 = No es visible (no se captura)<br>3 = Es visible y no es capturable (sólo lectura)<br>4 = Es visible y se captura |
| 82 | `CANCHOVALORCLASIFICACION` | `I` | Ancho en pixeles de la columna del valor de clasificación del<br>movimiento. |
| 83 | `CIDFORMULATOTAL` | `I` | Número de fórmula que se asocia a la columna Total para<br>calcularse. |
| 84 | `CUSADESCUENTODOC1` | `I` | Atributo que indica el comportamiento de control de Descuento 1<br>en el documento.<br>1 = No es visible (no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 85 | `CIDFORMULADESDOC1` | `I` | Número de fórmula que se asocia al Importe de descuento 1 del<br>documento para calcularse. |
| 86 | `CUSADESCUENTODOC2` | `I` | Atributo que indica el comportamiento de control de descuento 2<br>en el documento.<br>1 = No es visible (no se captura)<br>2 = Es visible y no es capturable (sólo lectura)<br>3 = Es visible y se captura |
| 87 | `CIDFORMULADESDOC2` | `I` | Número de fórmula que se asocia al Importe de descuento 2 del<br>documento para calcularse. |
| 88 | `CUSAGASTO1` | `I` | Bandera para hacer visible y utilizar el importe del documento<br>gasto sobre compra 1 para este concepto.<br>0 = No se utiliza<br>1 = Se utiliza |
| 89 | `CIDFORMULAGASTO1` | `I` | Fórmula del gasto sobre compra 1 del documento. |
| 90 | `CUSAGASTO2` | `I` | Bandera para hacer visible y utilizar el importe del documento<br>gasto sobre compra 2 para este concepto.<br>0 = No se utiliza<br>1 = Se utiliza |
| 91 | `CIDFORMULAGASTO2` | `I` | Fórmula del gasto sobre compra 2 del documento. |
| 92 | `CUSAGASTO3` | `I` | Bandera para hacer visible y utilizar el importe del documento<br>gasto sobre compra 3 para este concepto.<br>0 = No se utiliza<br>1 = Se utiliza |
| 93 | `CIDFORMULAGASTO3` | `I` | Fórmula del gasto sobre compra 3 del documento. |
| 94 | `CUSATEXTOEXTRA1` | `I` | Bandera para hacer visible y utilizar la columna Texto Extra 1 en<br>los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 95 | `CUSATEXTOEXTRA2` | `I` | Bandera para hacer visible y utilizar la columna Texto Extra 2 en<br>los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 96 | `CUSATEXTOEXTRA3` | `I` | Bandera para hacer visible y utilizar la columna Texto Extra 3 en<br>los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es Visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 97 | `CANCHOTEXTOEXTRA` | `I` | Ancho en pixeles de las columnas de campos de texto extra del<br>movimiento. |
| 98 | `CUSAFECHAEXTRA` | `I` | Bandera para hacer visible y utilizar la columna Fecha Extra en<br>los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 99 | `CANCHOFECHAEXTRA` | `I` | Ancho en pixeles de la columna de la fecha extra del<br>movimiento. |
| 100 | `CUSAIMPORTEEXTRA1` | `I` | Bandera para hacer visible y utilizar la columna Importe Extra 1<br>en los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 101 | `CIDFORMULAEXTRA1` | `I` | Número de fórmula que se asocia a la columna del importe extra<br>1 para calcularse. |
| 102 | `CUSAIMPORTEEXTRA2` | `I` | Bandera para hacer visible y utilizar la columna Importe Extra 2<br>en los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 103 | `CIDFORMULAEXTRA2` | `I` | Número de fórmula que se asocia a la columna del importe extra<br>2 para calcularse. |
| 104 | `CUSAIMPORTEEXTRA3` | `I` | Bandera para hacer visible y utilizar la columna de importe extra<br>3 en los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 105 | `CIDFORMULAEXTRA3` | `I` | Número de fórmula que se asocia a la columna del importe extra<br>3 para calcularse. |
| 106 | `CUSAIMPORTEEXTRA4` | `I` | Bandera para hacer visible y utilizar la columna de importe extra<br>4 en los movimientos del documento para este concepto.<br>1 = Valor sin uso. Utilizado por compatibilidad<br>2 = No es visible (no se captura)<br>3 = Valor sin uso. Utilizado por compatibilidad<br>4 = Es visible y se captura |
| 107 | `CIDFORMULAEXTRA4` | `I` | Número de fórmula que se asocia a la columna del importe extra<br>4 para calcularse. |
| 108 | `CUSATEXTOEXTRA1DOC` | `I` | Bandera para hacer visible y utilizar el campo Texto Extra 1 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 109 | `CUSATEXTOEXTRA2DOC` | `I` | Bandera para hacer visible y utilizar el campo Texto Extra 2 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 110 | `CUSATEXTOEXTRA3DOC` | `I` | Bandera para hacer visible y utilizar el campo Texto Extra 3 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 111 | `CUSAFECHAEXTRADOC` | `I` | Bandera para hacer visible y utilizar el campo Fecha Extra del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 112 | `CUSAIMPORTEEXTRA1DOC` | `I` | Bandera para hacer visible y utilizar el Importe Extra 1 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 113 | `CUSAIMPORTEEXTRA2DOC` | `I` | Bandera para hacer visible y utilizar el Importe Extra 2 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 114 | `CUSAIMPORTEEXTRA3DOC` | `I` | Bandera para hacer visible y utilizar el Importe Extra 3 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 115 | `CUSAIMPORTEEXTRA4DOC` | `I` | Bandera para hacer visible y utilizar el Importe Extra 4 del<br>documento para este concepto.<br>0 = No es visible (no se captura)<br>1 = Es visible y se captura |
| 116 | `CUSAEXTRACOMOGASTO` | `I` | Indica si se usa uno de los importes extra del movimiento como<br>gasto sobre compra.<br>0 = No se usa ningún Importe Extra como gasto<br>1 = Usar el Importe Extra 1 como gasto<br>2 = Usar el Importe Extra 2 como gasto<br>3 = Usar el Importe Extra 3 como gasto<br>4 = Usar el Importe Extra 4 como gasto |
| 117 | `CUSAOBSERVACIONES` | `I` | Usa Observaciones.<br>0 = Sí<br>1 = No |
| 118 | `CPRESENTAFISCAL` | `I` | Bandera para que se presente la pantalla Domicilio Fiscal<br>después de capturar el encabezado del documento. |
| 119 | `CPRESENTAREFERENCIA` | `I` | Bandera para que se presente la pantalla Referencia después de<br>capturar el encabezado del documento. |
| 120 | `CPRESENTACONDICIONES` | `I` | Bandera para que se presente la pantalla Condiciones después<br>de capturar el encabezado del documento. |
| 121 | `CPRESENTAENVIO` | `I` | Bandera para que se presente la pantalla Datos de Envío<br>después de capturar el encabezado del documento. |
| 122 | `CPRESENTADETALLE` | `I` | Bandera para que se presente la pantalla Detalle después de<br>capturar el encabezado del documento. |
| 123 | `CPRESENTAIMPRIMIR` | `I` | Bandera para que se presente la pantalla Imprimir después de<br>afectar el documento. |
| 124 | `CPRESENTAPAGAR` | `I` | Bandera para que se presente la pantalla Pagar después de<br>afectar el documento. |
| 125 | `CPRESENTASALDAR` | `I` | Bandera para que se presente la pantalla Saldar después de<br>afectar el documento. |
| 126 | `CPRESENTADOCUMENTAR` | `I` | Bandera para que se presente la pantalla Documentar Deuda<br>después de afectar el documento. |
| 127 | `CPRESENTAGASTOSCOMPRA` | `I` | Bandera para que se presente la pantalla Gastos de Compra<br>después de Capturar el encabezado del documento. |
| 128 | `CSEGCONTCONCEPTO` | `V` | Segmento de la cuenta contable del concepto.<br>Varchar: 50 caracteres. |
| 129 | `CBANENCABEZADO` | `I` | Bandera que indica si ya se capturó la configuración del<br>encabezado. |
| 130 | `CBANMOVIMIENTO` | `I` | Bandera que indica si ya se capturó la configuración de los<br>movimientos. |
| 131 | `CBANDESCUENTO` | `I` | Bandera que indica si ya se capturó la configuración de los<br>descuentos. |
| 132 | `CBANIMPUESTO` | `I` | Bandera que indica si ya se capturó la configuración de los<br>impuestos. |
| 133 | `CBANACCIONAUTOMATICA` | `I` | Bandera que indica si ya se capturó la configuración de las<br>acciones automáticas. |
| 134 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 135 | `CNOFOLIO` | `F` | Folio asumido del concepto. |
| 136 | `CIDPROCESOSEGURIDAD` | `I` | Proceso de seguridad de uso interno. |
| 137 | `CUSAGTOMOV` | `I` | Bandera que indica al concepto que la columna Gasto sobre<br>Compra debe ser visible en los movimientos del documento. |
| 138 | `CUSASCMOV` | `I` | Bandera que indica al concepto que la columna Segmento<br>Contable debe ser visible en los movimientos del documento. |
| 139 | `CIDASTOCON` | `I` | Identificador del asiento contable del concepto. |
| 140 | `CSCCPTO2` | `V` | Segmento contable 2 del concepto.<br>Varchar: 50 caracteres. |
| 141 | `CSCCPTO3` | `V` | Segmento contable 3 del concepto.<br>Varchar: 50 caracteres. |
| 142 | `CSCMOVTO` | `V` | Segmento de la cuenta contable de movimientos.<br>Varchar: 50 caracteres. |
| 143 | `CIDCONAUTO` | `I` | Identificador del concepto de documento que se generará<br>automáticamente. |
| 144 | `CIDALMASUM` | `I` | Identificador del almacén asumido por concepto. |
| 145 | `CUSACOMVTA` | `I` | Indica si ese concepto de documento utiliza la comisión de venta. |
| 146 | `CIDPRSEG02` | `I` | Identificador del proceso que corresponde al permiso de<br>seguridad para modificación del concepto. |
| 147 | `CIDPRSEG03` | `I` | Identificador del proceso que corresponde al permiso de<br>seguridad para eliminación del concepto. |
| 148 | `CIDPRSEG04` | `I` | Contiene el identificador del proceso que corresponde al permiso<br>de seguridad para cancelación. |
| 149 | `CIDPRSEG05` | `I` | Contiene el identificador del proceso que corresponde al permiso<br>de seguridad para cambiar el estado del documento impreso. |
| 150 | `CFORMAAJ01` | `I` | Contiene el identificador del concepto Ajuste al costo.<br>0 = Costo Total del Almacén<br>1 = Costo Unitario del Almacén |
| 151 | `CIDPRSEG06` | `I` | Identificador del proceso que corresponde al permiso de<br>seguridad para creación del Concepto. |
| 152 | `CAPFORMULA` | `I` | Indica si el sistema debe recalcular o no las fórmulas, durante la<br>transformación de documentos.<br>0 = No<br>1 = Sí |
| 153 | `CESCFD` | `I` | Indica si el concepto es Comprobante Fiscal Digital (CFD). |
| 154 | `CIDFIRMARL` | `I` | Identificador de la firma electrónica. |
| 155 | `CGDAPASSW` | `I` | Guarda la contraseña del certificado digital para la emisión de<br>CFD’s. |
| 156 | `CEMITEYENT` | `I` | Indica si emite y entrega a la vez:<br>0 = No<br>1 = Sí |
| 157 | `CBANCFD` | `I` | Indica si la ventana Configuración del Comprobante Fiscal Digital<br>tiene datos:<br>0 = No<br>1 = Sí |
| 158 | `CREPIMPCFD` | `V` | Ruta y nombre de archivo del reporte en formato de Impresión.<br>Varchar: 253 caracteres. |
| 159 | `CIDDIRSUCU` | `I` | Asocia un concepto con una dirección de sucursal. |
| 160 | `CBANDIRSUC` | `I` | Indica si un concepto está asociado a una dirección de sucursal. |
| 161 | `CVERFACELE` | `I` | Verifica con que versión del anexo 20 del SAT se va a emitir el<br>CFD.<br>0 = Versión 1.0<br>1 = Versión 2.0<br>2 = Versión 4.0<br>Nota: Actualmente solo se puede emitir versión 2.0. |
| 162 | `CCALFECHAS` | `I` | Campo para el recálculo de las fechas en transformaciones en el<br>documento destino.<br>0 = No Recalcular Fechas<br>1 = No Recalcular FV, Recalcular FE en Base al Docto. Origen<br>2 = Recalcular FV en Base a Docto Origen, No Recalcular FE<br>3 = Recalcular Fechas en Base a Docto Origen<br>4 = Recalcular FV en Base a Días de Crédito del Cte, No<br>Recalcular FE<br>5 = Recalcular FV en Base a Días de Crédito del Cte, Recalcular<br>FE en Base a Docto Origen<br>6 = Recalcular Fechas en Base a Días de Crédito del Cte |
| 163 | `CTIPCAMTR1` | `I` | Tipo de cambio en transformaciones 1.<br>En transformación de un documento en moneda extrajera (ME) a<br>moneda base (MB) tomar el tipo de cambio (TC) del destino.<br>0 = No Activo<br>1 = Activo |
| 164 | `CTIPCAMTR2` | `I` | Tipo de cambio en transformaciones 2.<br>En transformación de un documento en moneda base (MB) a<br>moneda extranjera (ME) tomar el tipo de cambio (TC) del origen.<br>0 = No Activo<br>1 = Activo |
| 165 | `CCONSOLIDA` | `I` | Activa si el documento destino podrá ser consolidado en una<br>transformación. Para configurar si el concepto podrá ser<br>consolidado. |
| 166 | `CENVIODIG` | `I` | Activa la envío de documentos fiscales por correo electrónico.<br>Solo aplica para los documentos modelo:<br>Cotización y Pedido (Ventas)<br>Cotización del Proveedor y Orden de Compra (Compras) |
| 167 | `CBANTRANS` | `I` | Bandera que indica si ya se capturó la configuración de las<br>transformaciones de documentos. |
| 168 | `CCONFNOAPR` | `I` | Indica si se va a preguntar si el número de aprobación está<br>correctamente capturado.<br>0 = No<br>1 = Sí |
| 169 | `CNOAPROB` | `I` | Número de aprobación para documentos tradicionales. |
| 170 | `CAUTOIMPR` | `I` | Es autoimpreso<br>0 = No<br>1 = Sí |
| 171 | `CRECIBECFD` | `I` | Sin uso. |
| 172 | `CSISTORIG` | `I` | Sistema origen |
| 173 | `CIDCPTODE1` | `I` | Identificador del concepto destino de transformación 1. |
| 174 | `CIDCPTODE2` | `I` | Identificador del concepto destino de transformación 2. |
| 175 | `CIDCPTODE3` | `I` | Identificador del concepto destino de transformación 3. |
| 176 | `CPLAMIGCFD` | `V` | Ruta de la plantilla de formato amigable para entrega de<br>CFD/CFDI/CBB. Exclusivo. |
| 177 | `CIDPRSEG07` | `I` | Identificador del proceso que corresponde al permiso de<br>seguridad para impresión del concepto. |
| 178 | `CRESERVADO` | `I` | Concepto reservado.<br>0 = No<br>1 = Sí |
| 179 | `CVERREFER` | `I` | Configurar concepto para permitir ver la referencia en la<br>aplicación de pagos. |
| 180 | `CVERDOCORI` | `I` | Configurar concepto para permitir ver la serie y folio origen en la<br>aplicación de pagos. |
| 181 | `CCBB` | `I` | Código de barras bidimensional. |
| 182 | `CCARTAPOR` | `I` | Carta Porte. |
| 183 | `CCOMPDONAT` | `I` | Especificar si los documentos de un cierto concepto debe incluir<br>o no el complemento de Donatarias. |
| 184 | `COBSXML` | `I` | Este campo indica si se activó la impresión de las observaciones<br>en el XML. |
| 185 | `CRUTAENTREGA` | `V` | Ruta de entrega por omisión para el concepto.<br>Varchar: 253 caracteres. |
| 186 | `CPREFICON` | `V` | Prefijo para el nombre en la entrega de documentos.<br>Varchar: 30 caracteres. |
| 187 | `CREGIMFISC` | `V` | Régimen por omisión en el que tributa el contribuyente emisor a<br>nivel conceptos.<br>Varchar: 100 caracteres. |
| 188 | `CCOMPEDUCA` | `I` | Reservado |
| 189 | `CMETODOPAG` | `V` | Método de pago por omisión a nivel conceptos.<br>Varchar: 50 caracteres. |
| 190 | `CVERESQUE` | `V` | Versión del esquema del SAT<br>Varchar: 50 caracteres. |
| 191 | `CIDFIRMADSL` | `V` | Identificador de la Firma Electronica en el ADD.<br>Varchar: 40 caracteres. |
| 192 | `CORDENCAPTURA` | `C` |  |
| 193 | `CESTATUS` | `I` | Indica si un concepto está activo o no<br>0=No está activo<br>1= Está activo |
| 194 | `CIDCUENTA` | `I` | Identificador de la cuenta bancaria de la empresa asumida<br>por el concepto |
| 195 | `CUSAOBJIMP` | `I` | Indica si el concepto actual permitirá mostrar la opción para<br>seleccionar el objeto de impuesto.<br>0 = No permite<br>1 = Sí permite |
| 196 | `CCONFIEPS` | `I` | Indica si el concepto actual permitirá mostrar la opción para<br>seleccionar si el IEPS se tomará como tasa/cuota 0 o exenta.<br>0 = No permite<br>1 = Sí permite |

---

### `admConversionesUnidad` — Tabla de Conversión de unidades

> Esta tabla contiene los campos de conversiones entre unidades de los los servicios, productos y/o paquetes.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDUNIDAD1` | `I` | Identificador de la unidad 1. |
| 3 | `CIDUNIDAD2` | `I` | Identificador de la unidad 2. |
| 4 | `CFACTORCONVERSION` | `F` | Factor de conversión. Unidad2 -> Unidad1. |

---

### `admCostosHistoricos` — Tabla de Costos históricos

> Esta tabla contiene los costos históricos. Guarda dos registros de cada día en el que cambia el costo un  producto.

> Uno de ellos contiene el nuevo Costo Promedio y último de la empresa y el otro contiene el último Costo  Promedio y el último del almacén.    Nota: El Costo Promedio se basa en entradas, así que sólo los documentos de entrada crean o modifican  registros de esta tabla.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCOSTOH` | `I` | Identificador del costo histórico. |
| 2 | `CIDPRODUCTO` | `I` | Identificador del producto o detalle. |
| 3 | `CIDALMACEN` | `I` | Identificador del almacén, |
| 4 | `CFECHACOSTOH` | `D` | Fecha de inicio del valor del costo histórico. |
| 5 | `CCOSTOH` | `F` | Valor del costo histórico. |
| 6 | `CULTIMOCOSTOH` | `F` | Ultimo costo a la fecha del registro de costos históricos.<br>Nota: Usualmente contiene el costo de la entrada que creó el<br>registro de costos históricos, a menos que haya dos entradas el<br>mismo día. |
| 7 | `CIDMOVIMIENTO` | `I` | Identificador del ultimo movimiento que modificó el registro. A<br>este movimiento le pertenece el Ultimo Costo.<br>Nota: Si este movimiento se borra, entonces el campo se pone<br>en cero, para indicar que el registro no tiene referencia.<br>Para corregir esto, debe ejecutar un recosteo. |
| 8 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admDatosAddenda` — Tabla de Datos adicionales de

> Esta tabla contiene los campos de la tabla Datos adicionales de Addendas.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `IDADDENDA` | `I` | Identificador de la addenda. |
| 3 | `TIPOCAT` | `I` | Tipo de catálogo. |
| 4 | `IDCAT` | `I` | Identificador del catálogo. |
| 5 | `NUMCAMPO` | `I` | Número de campo. |
| 6 | `VALOR` | `V` | Valor.<br>Varchar: 254 caracteres. |

---

### `admDocumentos` — Tabla de Documentos

> Esta tabla contiene los campos con los que se crean los diferentes documentos.

> Nota: Algunas veces este campo esta vacío porque los  documentos de almacén no llevan cliente o proveedor y los  productos en consignación de clientes pueden salir a nombre de  un agente.

> Nota: Algunas veces este campo esta vacío porque los  documentos de proveedor y almacén no llevan agente.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDDOCUMENTO` | `I` | Identificador del documento. |
| 2 | `CIDDOCUMENTODE` | `I` | Tipo de documento del concepto de documento. Referencia de la<br>tabla TblDocumentoDe. |
| 3 | `CIDCONCEPTODOCUMENTO` | `I` | Identificador del concepto del documento. |
| 4 | `CSERIEDOCUMENTO` | `V` | Serie del documento. |
| 5 | `CFOLIO` | `F` | Folio del documento. |
| 6 | `CFECHA` | `D` | Fecha del documento. |
| 7 | `CIDCLIENTEPROVEEDOR` | `I` | Identificador del cliente o proveedor del documento.<br>Nota: Algunas veces este campo esta vacío porque los<br>documentos de almacén no llevan cliente o proveedor y los<br>productos en consignación de clientes pueden salir a nombre de<br>un agente. |
| 8 | `CRAZONSOCIAL` | `V` | Es la razón social del cliente. |
| 9 | `CRFC` | `V` | Es el RFC del cliente. |
| 10 | `CIDAGENTE` | `I` | Identificador del agente del documento.<br>Nota: Algunas veces este campo esta vacío porque los<br>documentos de proveedor y almacén no llevan agente. |
| 11 | `CFECHAVENCIMIENTO` | `D` | Fecha de vencimiento del documento. |
| 12 | `CFECHAPRONTOPAGO` | `D` | Fecha de pronto pago del documento. |
| 13 | `CFECHAENTREGARECEPCION` | `D` | Fecha de la entrega en documentos de venta o fecha de<br>recepción en documentos de compras. |
| 14 | `CFECHAULTIMOINTERES` | `D` | Fecha del último cálculo de intereses moratorios. |
| 15 | `CIDMONEDA` | `I` | Moneda asociada al documento. |
| 16 | `CTIPOCAMBIO` | `F` | Tipo de cambio del documento.<br>En caso de que el documento tenga una moneda diferente a la<br>base, este parámetro contiene el tipo de cambio con que se creó<br>el documento. En caso de que el documento tenga la moneda<br>base entonces, el tipo de cambio es igual a 1.<br>Nota: Se necesitan 10 enteros y de 0 a 9 decimales. |
| 17 | `CREFERENCIA` | `V` | Referencia del documento.<br>Varchar: 20 caracteres. |
| 18 | `COBSERVACIONES` | `T` | Observaciones del documento. |
| 19 | `CNATURALEZA` | `I` | Naturaleza del documento:<br>C = Cargo<br>A = Abono<br>N = Sin naturaleza |
| 20 | `CIDDOCUMENTOORIGEN` | `I` | Identificador del documento origen.<br>Ejemplo: En un descuento por pronto pago el documento origen<br>es un pago. En un documento de intereses moratorios el<br>documento origen es una factura. |
| 21 | `CPLANTILLA` | `I` | Define a un documento como una plantilla la cual se tomara<br>como modelo en el alta masiva de documentos.<br>0 = No<br>1 = Sí |
| 22 | `CUSACLIENTE` | `I` | Indica si se usa cliente en el documento.<br>1 = Sí<br>0 = No |
| 23 | `CUSAPROVEEDOR` | `I` | Indica si se usa proveedor en el documento.<br>1 = Sí<br>0 = No |
| 24 | `CAFECTADO` | `I` | Indica si el documento ya está afectado.<br>0 = No afectado<br>1 = Afectado |
| 25 | `CIMPRESO` | `I` | Indica si el documento ya está impreso.<br>0 = No impreso<br>1 = Impreso |
| 26 | `CCANCELADO` | `I` | Indica si el documento está cancelado.<br>0 = No cancelado.<br>1 = Cancelado. |
| 27 | `CDEVUELTO` | `I` | Indica si el documento ya fue devuelto.<br>0 = No devuelto.<br>1 = Devuelto. |
| 28 | `CIDPREPOLIZA` | `I` | Cuando el documento está precontabilizado este campo contiene<br>el identificador de la prepóliza a la que pertenece. |
| 29 | `CIDPREPOLIZACANCELACION` | `I` | En caso de documentos cancelados este campo contiene el<br>Identificador de la prepóliza usada para cancelar la prepóliza<br>original. |
| 30 | `CESTADOCONTABLE` | `I` | Estado del documento en el proceso de interfaz contable:<br>1 = No Contabilizado.<br>2 = Pertenece a una Prepóliza de documento.<br>3 = Pertenece a una Prepóliza Diaria.<br>4 = Pertenece a una Prepóliza por Periodo.<br>5 = Pertenece a una Póliza de documento.<br>6 = Pertenece a una Póliza Diaria.<br>7 = Pertenece a una Póliza por Periodo.<br>8 = Pertenece a una Póliza modificada (contabilizada<br>libremente). |
| 31 | `CNETO` | `F` | Importe del total del neto para el documento. |
| 32 | `CIMPUESTO1` | `F` | Importe del total del impuesto 1 para el documento. |
| 33 | `CIMPUESTO2` | `F` | Importe del total del impuesto 2 para el documento. |
| 34 | `CIMPUESTO3` | `F` | Importe del total del impuesto 3 para el documento. |
| 35 | `CRETENCION1` | `F` | Importe del total de la retención 1 para el documento. |
| 36 | `CRETENCION2` | `F` | Importe del total de la retención 2 para el documento. |
| 37 | `CDESCUENTOMOV` | `F` | Importe del total de los descuentos de los movimientos del<br>documento. |
| 38 | `CDESCUENTODOC1` | `F` | Importe del descuento 1 para el documento. |
| 39 | `CDESCUENTODOC2` | `F` | Importe del descuento 2 para el documento. |
| 40 | `CGASTO1` | `F` | Importe del gasto sobre compra 1 del documento. |
| 41 | `CGASTO2` | `F` | Importe del gasto sobre compra 2 del documento. |
| 42 | `CGASTO3` | `F` | Importe del gasto sobre compra 3 del documento. |
| 43 | `CTOTAL` | `F` | Importe del total de los totales de los movimientos para el<br>documento. |
| 44 | `CPENDIENTE` | `F` | Saldo pendiente del documento. |
| 45 | `CTOTALUNIDADES` | `F` | Unidades totales del documento. |
| 46 | `CDESCUENTOPRONTOPAGO` | `F` | Porcentaje de descuento por pronto pago. |
| 47 | `CPORCENTAJEIMPUESTO1` | `F` | Porcentaje del impuesto 1 para el documento. |
| 48 | `CPORCENTAJEIMPUESTO2` | `F` | Porcentaje del impuesto 2 para el documento. |
| 49 | `CPORCENTAJEIMPUESTO3` | `F` | Porcentaje del impuesto 3 para el documento. |
| 50 | `CPORCENTAJERETENCION1` | `F` | Porcentaje de la retención 1 para el documento. |
| 51 | `CPORCENTAJERETENCION2` | `F` | Porcentaje de la retención 2 para el documento. |
| 52 | `CPORCENTAJEINTERES` | `F` | Porcentaje de interés moratorio del documento. |
| 53 | `CTEXTOEXTRA1` | `V` | Texto extra 1.<br>Varchar: 50 caracteres. |
| 54 | `CTEXTOEXTRA2` | `V` | Texto extra 2.<br>Varchar: 50 caracteres. |
| 55 | `CTEXTOEXTRA3` | `V` | Texto extra 3.<br>Varchar: 50 caracteres. |
| 56 | `CFECHAEXTRA` | `D` | Fecha extra. |
| 57 | `CIMPORTEEXTRA1` | `F` | Importe extra 1. |
| 58 | `CIMPORTEEXTRA2` | `F` | Importe extra 2. |
| 59 | `CIMPORTEEXTRA3` | `F` | Importe extra 3. |
| 60 | `CIMPORTEEXTRA4` | `F` | Importe extra 4. |
| 61 | `CDESTINATARIO` | `V` | Nombre del destinatario.<br>Varchar: 60 caracteres. |
| 62 | `CNUMEROGUIA` | `V` | Número de guía.<br>Varchar: 60 caracteres. |
| 63 | `CMENSAJERIA` | `V` | Nombre de la mensajería.<br>Varchar: 20 caracteres. |
| 64 | `CCUENTAMENSAJERIA` | `V` | Número de cuenta de la mensajería.<br>Varchar: 60 caracteres. |
| 65 | `CNUMEROCAJAS` | `F` | Número de cajas. |
| 66 | `CPESO` | `F` | Peso de las cajas. |
| 67 | `CBANOBSERVACIONES` | `I` | Bandera que indica si ya se capturaron las observaciones del<br>documento.<br>0 = No se han capturado.<br>1 = Ya se capturaron. |
| 68 | `CBANDATOSENVIO` | `I` | Bandera que indica si ya se capturaron los datos de envío del<br>documento.<br>0 = No se han capturado.<br>1 = Ya se capturaron. |
| 69 | `CBANCONDICIONESCREDITO` | `I` | Bandera que indica si ya se capturaron las condiciones de<br>crédito del documento.<br>0 = No se han capturado.<br>1 = Ya se capturaron. |
| 70 | `CBANGASTOS` | `I` | Bandera que indica si ya se capturaron los gastos del<br>documento.<br>0 = No se han capturado.<br>1 = Ya se capturaron. |
| 71 | `CUNIDADESPENDIENTES` | `F` | Unidades por surtir del documento.<br>Es la suma de las unidades pendientes de los movimientos. |
| 72 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 73 | `CIMPCHEQPAQ` | `F` | Campo usado por CONTPAQi® Bancos para despliegues. |
| 74 | `CSISTORIG` | `I` | Sistema en donde se originó el documento:<br>5 = AdminPAQ®<br>205 = CONTPAQi® Comercial.<br>101 = CONTPAQi® Punto de venta. |
| 75 | `CIDMONEDCA` | `I` | Identificador de la cuenta bancaria del cliente |
| 76 | `CTIPOCAMCA` | `F` | Tipo de cambio que se utilizó para pagar un documento en<br>moneda extranjera. Sólo aplica en asociaciones hechas desde<br>CONTPAQi® Bancos. Este importe se guarda en el<br>cargo/abono cuya moneda es peso y será utilizado para<br>desplegar el documento en moneda extranjera en el reporte de<br>Estado de Cuenta. |
| 77 | `CESCFD` | `I` | Indica si el documento es Comprobante Fiscal Digital (CFD). |
| 78 | `CTIENECFD` | `I` | Sin uso. |
| 79 | `CLUGAREXPE` | `V` | Lugar de expedición del comprobante. |
| 80 | `CMETODOPAG` | `V` | Método de pago.<br>Varchar: 100 caracteres. |
| 81 | `CNUMPARCIA` | `I` | Número de parcialidades. |
| 82 | `CCANTPARCI` | `I` | Cantidad de parcialidades. |
| 83 | `CCONDIPAGO` | `V` | Condiciones de pago.<br>Varchar: 253 caracteres. |
| 84 | `CNUMCTAPAG` | `V` | Número de cuenta con la que se realizó el pago.<br>Varchar: 100 caracteres. |
| 85 | `CGUIDDOCUMENTO` | `V` | Identificador único del documento (en SQL).<br>Varchar: 40 caracteres. |
| 86 | `CIDCUENTA` | `I` | Identificador de la cuenta bancaria de la empresa |
| 87 | `CIDCOPIADE` | `I` | Identificador del documento de donde se copió el documento actual |
| 88 | `CVERESQUE` | `V` | Versión del esquema Anexo 20 del SAT<br>Varchar: 6 caracteres |
| 89 | `CDATOSADICIONALES` | `V` | Campos para almacenar datos como el archivo INI del CCP.<br>Varchar: MAX |

---

### `admDocumentosModelo` — Tabla de Documentos soportados

> Esta tabla contiene los documentos modelo. Cada concepto de documento creado en alguno de estos  sistemas pertenece a un documento modelo.

> 1 = Cotización  2 = Pedido  3 = Remisión   4 = Factura   5 = Devolución sobre Venta   6 = Devolución de Remisión   7 = Nota de Crédito   8 = Cambio del cliente   9 = Pago del cliente   10 = Cheque recibido  11 = Honorarios del cliente   12 = Abono del Cliente   13 = Nota de Cargo al Cliente  14 = Descuento por pronto pago   15 = Pagaré   16 = Interés Moratorio   17 = Orden de Compra   18 = Consignación del Proveedor   19 = Compra   20 = Devolución sobre Compra   21 = Devolución de Consignación   22 = Nota de Crédito del Proveedor   23 = Pago al proveedor   24 = Cheque emitido   25 = Honorarios del Proveedor   26 = Abono al Proveedor   27 = Cargo del Proveedor   28 = Utilidad Cambiaria Cliente   29 = Pérdida Cambiaria Cliente   30 = Utilidad Cambiaria Proveedor   31 = Pérdida Cambiaria Proveedor   32 = Entrada al Almacén   33 = Salida del Almacén   34 = Traspasos   35 = Nota de Venta   36 = Devolución sobre Nota de Venta  37 = Ajuste al Costo

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDDOCUMENTODE` | `I` | Tipo de documento. Se usa para elegir identificar el documento<br>modelo a usar. Puede tener los siguientes valores:<br>1 = Cotización<br>2 = Pedido<br>3 = Remisión<br>4 = Factura<br>5 = Devolución sobre Venta<br>6 = Devolución de Remisión<br>7 = Nota de Crédito<br>8 = Cambio del cliente<br>9 = Pago del cliente<br>10 = Cheque recibido<br>11 = Honorarios del cliente<br>12 = Abono del Cliente<br>13 = Nota de Cargo al Cliente<br>14 = Descuento por pronto pago<br>15 = Pagaré<br>16 = Interés Moratorio<br>17 = Orden de Compra<br>18 = Consignación del Proveedor<br>19 = Compra<br>20 = Devolución sobre Compra<br>21 = Devolución de Consignación<br>22 = Nota de Crédito del Proveedor<br>23 = Pago al proveedor<br>24 = Cheque emitido<br>25 = Honorarios del Proveedor<br>26 = Abono al Proveedor<br>27 = Cargo del Proveedor<br>28 = Utilidad Cambiaria Cliente<br>29 = Pérdida Cambiaria Cliente<br>30 = Utilidad Cambiaria Proveedor<br>31 = Pérdida Cambiaria Proveedor<br>32 = Entrada al Almacén<br>33 = Salida del Almacén<br>34 = Traspasos<br>35 = Nota de Venta<br>36 = Devolución sobre Nota de Venta<br>37 = Ajuste al Costo |
| 2 | `CDESCRIPCION` | `V` | Descripción del documento modelo.<br>Varchar: 50 caracteres.<br>Nota: Las descripciones correspondientes se incluyen en el<br>campo 1 de esta tabla. |
| 3 | `CNATURALEZA` | `I` | Naturaleza de los documentos generados por el concepto.<br>0 = Cargo.<br>1 = Abono.<br>2 = Sin naturaleza. |
| 4 | `CAFECTAEXISTENCIA` | `I` | Manera en que se afecta las existencias por el concepto.<br>1 = Entradas.<br>2 = Salidas.<br>3 = Ninguno. |
| 5 | `CMODULO` | `I` | Módulo de donde se capturan los documentos de este tipo:<br>1 = Ventas<br>2 = Compras<br>3 = Clientes<br>4 = Proveedores<br>5 = Inventarios |
| 6 | `CNOFOLIO` | `F` | Folio del documento. |
| 7 | `CIDCONCEPTODOCTOASUMIDO` | `I` | Identificador del concepto de documento que se mostrará en la<br>pantalla en caso de estar en modo inserción. |
| 8 | `CUSACLIENTE` | `I` | Indica si se usa cliente en el documento:<br>1 = Sí<br>0 = No |
| 9 | `CUSAPROVEEDOR` | `I` | Indica si se usa proveedor en el documento:<br>0 = No<br>1 = Sí |
| 10 | `CIDASIENTOCONTABLE` | `I` | Identificador del asiento contable del documento modelo. |

---

### `admDomicilios` — Tabla de Domicilios

> Esta tabla contiene los campos de las direcciones de las empresas, documentos, clientes y proveedores.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDDIRECCION` | `I` | Identificador de la dirección. |
| 2 | `CIDCATALOGO` | `I` | Identificador del catálogo. |
| 3 | `CTIPOCATALOGO` | `I` | Define el tipo del catalogo asociado:<br>1 = Clientes<br>2 = Proveedores<br>3 = Documentos<br>4 = Empresas |
| 4 | `CTIPODIRECCION` | `I` | Tipo de dirección:<br>0 = Fiscal<br>1 = Envío |
| 5 | `CNOMBRECALLE` | `V` | Nombre de la calle.<br>Varchar: 60 caracteres. |
| 6 | `CNUMEROEXTERIOR` | `V` | Numero exterior de la calle.<br>Varchar: 30 caracteres. |
| 7 | `CNUMEROINTERIOR` | `V` | Número interior del edificio o local.<br>Varchar: 30 caracteres. |
| 8 | `CCOLONIA` | `V` | Colonia o fraccionamiento.<br>Varchar: 60 caracteres. |
| 9 | `CCODIGOPOSTAL` | `V` | Código postal.<br>Varchar: 6 caracteres. |
| 10 | `CTELEFONO1` | `V` | Número telefónico.<br>Varchar: 15 caracteres. |
| 11 | `CTELEFONO2` | `V` | Número telefónico.<br>Varchar: 15 caracteres. |
| 12 | `CTELEFONO3` | `V` | Número telefónico.<br>Varchar: 15 caracteres. |
| 13 | `CTELEFONO4` | `V` | Número telefónico.<br>Varchar: 15 caracteres. |
| 14 | `CEMAIL` | `V` | Dirección de correo electrónico.<br>Varchar: 50 caracteres. |
| 15 | `CDIRECCIONWEB` | `V` | Dirección de página WEB o URL.<br>Varchar: 50 caracteres. |
| 16 | `CPAIS` | `V` | Nombre del país.<br>Varchar: 60 caracteres. |
| 17 | `CESTADO` | `V` | Nombre del estado.<br>Varchar: 60 caracteres. |
| 18 | `CCIUDAD` | `V` | Nombre de la ciudad.<br>Varchar: 60 caracteres. |
| 19 | `CTEXTOEXTRA` | `V` | Texto extra.<br>Varchar: 60 caracteres. |
| 20 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 21 | `CMUNICIPIO` | `V` | Municipio de la dirección fiscal.<br>Varchar: 60 caracteres. |
| 22 | `CSUCURSAL` | `V` | Nombre de la sucursal.<br>Varchar: 60 caracteres. |

---

### `admEjercicios` — Tabla de Ejercicios y Periodos

> Esta tabla contiene los campos de periodos y ejercicios para la definición de Ejercicios montados.

> Nota: Un Ejercicio montado empieza en una fecha diferente al mes de enero, es decir, el primer periodo del  ejercicio puede empezar en abril y terminar en marzo, a diferencia de los ejercicios tradicionales que  empiezan en el mes enero y terminan en diciembre.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDEJERCICIO` | `I` | Identificador del ejercicio. |
| 2 | `CNUMEROEJERCICIO` | `I` | Número consecutivo del ejercicio. |
| 3 | `CFECINIPERIODO1` | `D` | Fecha de inicio del periodo. |
| 4 | `CFECINIPERIODO2` | `D` | Fecha de inicio del periodo. |
| 5 | `CFECINIPERIODO3` | `D` | Fecha de inicio del periodo. |
| 6 | `CFECINIPERIODO4` | `D` | Fecha de inicio del periodo. |
| 7 | `CFECINIPERIODO5` | `D` | Fecha de inicio del periodo. |
| 8 | `CFECINIPERIODO6` | `D` | Fecha de inicio del periodo. |
| 9 | `CFECINIPERIODO7` | `D` | Fecha de inicio del periodo. |
| 10 | `CFECINIPERIODO8` | `D` | Fecha de inicio del periodo. |
| 11 | `CFECINIPERIODO9` | `D` | Fecha de inicio del periodo. |
| 12 | `CFECINIPERIODO10` | `D` | Fecha de inicio del periodo. |
| 13 | `CFECINIPERIODO11` | `D` | Fecha de inicio del periodo. |
| 14 | `CFECINIPERIODO12` | `D` | Fecha de inicio del periodo. |
| 15 | `CFECHAFINAL` | `D` | Fecha de fin del ejercicio. |
| 16 | `CEJERCICIO` | `I` | Para el manejo del ejercicio en la Vistas versión 2.0. |

---

### `admExistenciaCosto` — Tabla de Existencias y Costos

> Esta tabla contiene los campos de las existencias y costos acumulados.

> Nota: Los Acumulados se llevan a una fecha de corte. En un  acumulado anual el 31 de diciembre, en uno mensual la fecha de  corte es la del fin de mes, en uno diario la fecha del día y en uno  histórico no hay fecha.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDEXISTENCIA` | `I` | Identificador de la existencia. |
| 2 | `CIDALMACEN` | `I` | Identificador del almacén de la existencia. |
| 3 | `CIDPRODUCTO` | `I` | Identificador del producto de la existencia. |
| 4 | `CIDEJERCICIO` | `I` | Fecha de corte del acumulado.<br>Nota: Los Acumulados se llevan a una fecha de corte. En un<br>acumulado anual el 31 de diciembre, en uno mensual la fecha de<br>corte es la del fin de mes, en uno diario la fecha del día y en uno<br>histórico no hay fecha. |
| 5 | `CTIPOEXISTENCIA` | `I` | Indica si el registro contiene la existencia del producto:<br>1 = Existencia<br>2 = Existencia en la unidad no convertible. |
| 6 | `CENTRADASINICIALES` | `F` | Entradas acumuladas de ejercicios anteriores. |
| 7 | `CSALIDASINICIALES` | `F` | Salidas acumuladas de ejercicios anteriores. |
| 8 | `CCOSTOINICIALENTRADAS` | `F` | Costo acumulado de las entradas de ejercicios anteriores. |
| 9 | `CCOSTOINICIALSALIDAS` | `F` | Costo acumulado de las salidas de ejercicios anteriores. |
| 10 | `CENTRADASPERIODO1` | `F` | Acumulado de entradas en unidades en el periodo 1. |
| 11 | `CENTRADASPERIODO2` | `F` | Acumulado de entradas en unidades en el periodo 2. |
| 12 | `CENTRADASPERIODO3` | `F` | Acumulado de entradas en unidades en el periodo 3. |
| 13 | `CENTRADASPERIODO4` | `F` | Acumulado de entradas en unidades en el periodo 4. |
| 14 | `CENTRADASPERIODO5` | `F` | Acumulado de entradas en unidades en el periodo 5. |
| 15 | `CENTRADASPERIODO6` | `F` | Acumulado de entradas en unidades en el periodo 6. |
| 16 | `CENTRADASPERIODO7` | `F` | Acumulado de entradas en unidades en el periodo 7. |
| 17 | `CENTRADASPERIODO8` | `F` | Acumulado de entradas en unidades en el periodo 8. |
| 18 | `CENTRADASPERIODO9` | `F` | Acumulado de entradas en unidades en el periodo 9. |
| 19 | `CENTRADASPERIODO10` | `F` | Acumulado de entradas en unidades en el periodo 10. |
| 20 | `CENTRADASPERIODO11` | `F` | Acumulado de entradas en unidades en el periodo 11. |
| 21 | `CENTRADASPERIODO12` | `F` | Acumulado de entradas en unidades en el periodo 12. |
| 22 | `CSALIDASPERIODO1` | `F` | Acumulado de salidas en unidades en el periodo 1. |
| 23 | `CSALIDASPERIODO2` | `F` | Acumulado de salidas en unidades en el periodo 2. |
| 24 | `CSALIDASPERIODO3` | `F` | Acumulado de salidas en unidades en el periodo 3. |
| 25 | `CSALIDASPERIODO4` | `F` | Acumulado de salidas en unidades en el periodo 4. |
| 26 | `CSALIDASPERIODO5` | `F` | Acumulado de salidas en unidades en el periodo 5. |
| 27 | `CSALIDASPERIODO6` | `F` | Acumulado de salidas en unidades en el periodo 6. |
| 28 | `CSALIDASPERIODO7` | `F` | Acumulado de salidas en unidades en el periodo 7. |
| 29 | `CSALIDASPERIODO8` | `F` | Acumulado de salidas en unidades en el periodo 8. |
| 30 | `CSALIDASPERIODO9` | `F` | Acumulado de salidas en unidades en el periodo 9. |
| 31 | `CSALIDASPERIODO10` | `F` | Acumulado de salidas en unidades en el periodo 10. |
| 32 | `CSALIDASPERIODO11` | `F` | Acumulado de salidas en unidades en el periodo 11. |
| 33 | `CSALIDASPERIODO12` | `F` | Acumulado de salidas en unidades en el periodo 12. |
| 34 | `CCOSTOENTRADASPERIODO1` | `F` | Costo acumulado de las entradas en el periodo 1. |
| 35 | `CCOSTOENTRADASPERIODO2` | `F` | Costo acumulado de las entradas en el periodo 2. |
| 36 | `CCOSTOENTRADASPERIODO3` | `F` | Costo acumulado de las entradas en el periodo 3. |
| 37 | `CCOSTOENTRADASPERIODO4` | `F` | Costo acumulado de las entradas en el periodo 4. |
| 38 | `CCOSTOENTRADASPERIODO5` | `F` | Costo acumulado de las entradas en el periodo 5. |
| 39 | `CCOSTOENTRADASPERIODO6` | `F` | Costo acumulado de las entradas en el periodo 6. |
| 40 | `CCOSTOENTRADASPERIODO7` | `F` | Costo acumulado de las entradas en el periodo 7. |
| 41 | `CCOSTOENTRADASPERIODO8` | `F` | Costo acumulado de las entradas en el periodo 8. |
| 42 | `CCOSTOENTRADASPERIODO9` | `F` | Costo acumulado de las entradas en el periodo 9. |
| 43 | `CCOSTOENTRADASPERIODO10` | `F` | Costo acumulado de las entradas en el periodo 10. |
| 44 | `CCOSTOENTRADASPERIODO11` | `F` | Costo acumulado de las entradas en el periodo 11. |
| 45 | `CCOSTOENTRADASPERIODO12` | `F` | Costo acumulado de las entradas en el periodo 12. |
| 46 | `CCOSTOSALIDASPERIODO1` | `F` | Costo acumulado de las salidas en el periodo 1. |
| 47 | `CCOSTOSALIDASPERIODO2` | `F` | Costo acumulado de las salidas en el periodo 2. |
| 48 | `CCOSTOSALIDASPERIODO3` | `F` | Costo acumulado de las salidas en el periodo 3. |
| 49 | `CCOSTOSALIDASPERIODO4` | `F` | Costo acumulado de las salidas en el periodo 4. |
| 50 | `CCOSTOSALIDASPERIODO5` | `F` | Costo acumulado de las salidas en el periodo 5. |
| 51 | `CCOSTOSALIDASPERIODO6` | `F` | Costo acumulado de las salidas en el periodo 6. |
| 52 | `CCOSTOSALIDASPERIODO7` | `F` | Costo acumulado de las salidas en el periodo 7. |
| 53 | `CCOSTOSALIDASPERIODO8` | `F` | Costo acumulado de las salidas en el periodo 8. |
| 54 | `CCOSTOSALIDASPERIODO9` | `F` | Costo acumulado de las salidas en el periodo 9. |
| 55 | `CCOSTOSALIDASPERIODO10` | `F` | Costo acumulado de las salidas en el periodo 10. |
| 56 | `CCOSTOSALIDASPERIODO11` | `F` | Costo acumulado de las salidas en el periodo 11. |
| 57 | `CCOSTOSALIDASPERIODO12` | `F` | Costo acumulado de las salidas en el periodo 12. |
| 58 | `CBANCONGELADO` | `I` | Revisa si un producto-almacen están congelados:<br>0 = No congelado<br>1 = Congelado |
| 59 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admFoliosDigitales` — Tabla de Folios digitales

> Esta tabla contiene los campos de la tabla Folios digitales.

> CENTREGADO  I  Entregado:  0 = No  1 = Sí

> 22  CCADPEDI  M  Contiene la lista de UUIDs de los CFDIs relacionados al  documento

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDFOLDIG` | `I` | Identificador del folio digital. |
| 2 | `CIDDOCTODE` | `I` | Identificador del documento modelo. |
| 3 | `CIDCPTODOC` | `I` | Identificador del concepto del documento. |
| 4 | `CIDDOCTO` | `I` | Identificador del documento. |
| 5 | `CIDDOCALDI` | `I` | Relaciona el documento del almacén con el folio. Todos son<br>digitales. |
| 6 | `CIDFIRMARL` | `I` | Identificador del certificado del sello digital. |
| 7 | `CNOORDEN` | `I` | Número de orden de la generación del sello digital. |
| 8 | `CSERIE` | `V` | Serie digital.<br>Varchar: 10 caracteres. |
| 9 | `CFOLIO` | `F` | Folio digital. |
| 10 | `CNOAPROB` | `I` | Número de aprobación de la serie y folio digital. |
| 11 | `CFECAPROB` | `D` | Fecha de aprobación de la serie y folio digital. |
| 12 | `CESTADO` | `I` | Estado:<br>0 = Disponible<br>1 = Ocupado y no emitido<br>2 = Ocupado y emitido<br>3 = Cancelado<br>4 = Por Confirmar timbrado (Requiere clave de confirmación)<br>5 = Por Autorizar cancelación (CFDI emitido en espera de<br>confirmación del receptor)<br>6 = Por confirmar cancelación (Confirmar o rechazar<br>cancelación de CFDI recibido)<br>10 = Disponible no asociado<br>11 = Recibido no asociado<br>12 = Recibido cancelado<br>13 = Recibido asociado Adw<br>14 = Recibido asociado Ctw |
| 19 | `CFECHACANC` | `D` | Fecha de cancelación del documento. |
| 20 | `CHORACANC` | `V` | Hora de cancelación del documento.<br>Varchar: 8 caracteres. |
| 23 | `CARCHCBB` | `V` | Archivo del código de barras.<br>Varchar: 60 caracteres. |
| 24 | `CINIVIG` | `D` | Inicio de vigencia del folio. |
| 25 | `CFINVIG` | `D` | Fin devigencia del folio. |
| 26 | `CTIPO` | `V` | Tipo de CFD recibido:<br>I = Ingreso<br>E = Egreso<br>Varchar: 1 caracter. |
| 27 | `CSERIEREC` | `V` | Serie digital del documento recibido.<br>Varchar: 25 caracteres. |
| 28 | `CFOLIOREC` | `F` | Folio digital del documento recibido. |
| 29 | `CRFC` | `V` | RFC del emisor del documento digital recibido.<br>Varchar: 20 caracteres. |
| 30 | `CRAZON` | `V` | Razón social del emisor del documento digital recibido.<br>Varchar: 253 caracteres. |
| 31 | `CSISORIGEN` | `I` | Sistema origen con el que se asoció el documento digital<br>recibido. |
| 32 | `CEJERPOL` | `I` | Ejercicio de la póliza asociada al documento digital recibido. |
| 33 | `CPERPOL` | `I` | Periodo de la póliza asociada al documento digital recibido. |
| 34 | `CTIPOPOL` | `I` | Tipo de la póliza asociada al documento digital recibido. |
| 35 | `CNUMPOL` | `I` | Número de la póliza asociada al documento digital recibido. |
| 36 | `CTIPOLDESC` | `V` | Descripción del tipo de póliza asociada al documento digital<br>recibido.<br>Varchar: 100 caracteres. |
| 37 | `CTOTAL` | `F` | Importe del total del documento digital recibido. |
| 38 | `CALIASBDCT` | `V` | Alias de la base de datos de CONTPAQi® Contabilidad con el<br>que se asociació el documento digital recibido.<br>Varchar: 50 caracteres. |
| 39 | `CCFDPRUEBA` | `I` | Indica si el documento digital recibido se generó con un<br>certificado de prueba o no. |
| 40 | `CDESESTADO` | `V` | Folio relacionado de cancelación<br>Varchar: 20 caracteres. |
| 41 | `CPAGADOBAN` | `I` | Facturación global / Año. |
| 42 | `CDESPAGBAN` | `V` | Facturación global / Periodicidad.<br>Varchar: 20 caracteres. |
| 43 | `CREFEREN01` | `` |  |
| 44 | `COBSERVA01` | `` |  |
| 45 | `CCODCONCBA` | `` |  |
| 46 | `CDESCONCBA` | `V` | Utilizado para almacenar el número de operación del<br>complemento de recepción de pagos. Nodo: NumOperación<br>Varchar: 100 caracteres. |
| 47 | `CNUMCTABAN` | `V` | Número de la cuenta bancaria asociada al XML.<br>Varchar: 30 caracteres. |
| 48 | `CFOLIOBAN` | `V` | Facturación global / Meses<br>Varchar: 20 caracteres. |
| 49 | `CIDDOCDEBA` | `I` | Identificador del documento de del documento bancario<br>asociado. |
| 50 | `CUSUAUTBAN` | `V` | Utilizado para almacenar el tipo de pago del complemento de<br>recepción de pagos. Nodo: TipoCadPago<br>Varchar: 20 caracteres. |
| 51 | `CUUID` | `V` | Guarda el UUID del documento timbrado.<br>Varchar: 60 caracteres. |
| 52 | `CUSUBAN01` | `V` | Contiene la clave de confirmación utilizada por el CFDI<br>Varchar: 20 caracteres. |
| 53 | `CAUTUSBA01` | `I` | Estado de la cancelación:<br>0 = Sin intentos<br>1 = Cancelado Rechazado<br>2 = Cancelado sin Aceptación<br>3 = Cancelado Plazo Vencido<br>4 = Cancelado con Aceptación<br>5 = Cancelación en proceso |
| 54 | `CUSUBAN02` | `V` | Tipo de realción que el documento tiene con otro(s) CFDIs<br>Varchar: 20 caracteres. |
| 55 | `CAUTUSBA02` | `I` | Bandera para indicar opciones de timbrado del XML Anexo 20 4.0:<br>0 = No desglosa el IEPS<br>1 = Con desglose de IEPS |
| 56 | `CUSUBAN03` | `V` | Hora de Pago.<br>Varchar: 20 caracteres. |
| 57 | `CAUTUSBA03` | `I` | Campo para almacenar el tipo de exportación del documento (Anexo 20 4.0):<br>1 - No aplica<br>2 - Definitiva con clave A1<br>3 - Temporal<br>- Definitiva con clave distinta a A1 o cuando no existe enajenación en términos del CFF |
| 58 | `CDESCAUT01` | `V` | 4<br>Moneda del XML<br>Varchar: 20 caracteres. |
| 59 | `CDESCAUT02` | `V` | Tipo de cambio.<br>Varchar: 20 caracteres. |
| 60 | `CDESCAUT03` | `V` | Régimen fiscal del receptor para el documento actual.<br>Varchar: 20 caracteres. |
| 61 | `CERRORVAL` | `I` | Maneja los errores de validaciones en el almacén digital. |
| 62 | `CACUSECAN` | `V` | Nombre del archivo del acuse de cancelación que envía el SAT.<br>Varchar: 30 caracteres. |
| 63 | `CIDDOCTODSL` | `V` | Identificador del documento en el Administrador de<br>Documentos Digitales (ADD).<br>Varchar: 40 caracteres. |

---

### `admMaximosMinimos` — Tabla de Existencias máximas y

> Esta tabla almacena la existencia mínima y máxima de cada producto por almacén.

> Nota: En caso de un producto sin características, este campo  está en ceros.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDALMACEN` | `I` | Identificador del almacén. |
| 3 | `CIDPRODUCTO` | `I` | Identificador del producto o del detalle. |
| 4 | `CIDPRODUCTOPADRE` | `I` | Identificador del producto padre de un detalle de características<br>con máximo y mínimo.<br>Nota: En caso de un producto sin características, este campo<br>está en ceros. |
| 5 | `CEXISTENCIAMINBASE` | `F` | Existencia mínima del producto en el almacén correspondiente a<br>la unidad base. |
| 6 | `CEXISTENCIAMAXBASE` | `F` | Existencia máxima del producto en el almacén correspondiente a<br>la unidad base. |
| 7 | `CEXISTMINNOCONVERTIBLE` | `F` | Existencia mínima del producto en el almacén correspondiente a<br>la unidad No Convertible. |
| 8 | `CEXISTMAXNOCONVERTIBLE` | `F` | Existencia máxima del producto en el almacén correspondiente a<br>la unidad no convertible. |
| 9 | `CZONA` | `V` | Zona donde se encuentra el producto en el almacén.<br>Varchar: 60 caracteres. |
| 10 | `CPASILLO` | `V` | Pasillo donde se encuentra el producto en el almacén.<br>Varchar: 60 caracteres. |
| 11 | `CANAQUEL` | `V` | Anaquel donde se encuentra el producto en el almacén.<br>Varchar: 60 caracteres. |
| 12 | `CREPISA` | `V` | Repisa donde se encuentra el producto en el almacén.<br>Varchar: 60 caracteres. |

---

### `admMonedas` — Tabla de Monedas

> Esta tabla contiene los campos del catálogo Monedas.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMONEDA` | `I` | Identificador de la moneda del documento. |
| 2 | `CNOMBREMONEDA` | `V` | Nombre de la moneda.<br>Varchar: 60 caracteres. |
| 3 | `CSIMBOLOMONEDA` | `V` | Símbolo de la moneda.<br>Varchar: 1 caracter. |
| 4 | `CPOSICIONSIMBOLO` | `I` | Posición del símbolo de la moneda:<br>0 = Antes de la cifra<br>1 = Después de la cifra |
| 5 | `CPLURAL` | `V` | Plural de la moneda.<br>Varchar: 60 caracteres. |
| 6 | `CSINGULAR` | `V` | Singular de la moneda.<br>Varchar: 60 caracteres. |
| 7 | `CDESCRIPCIONPROTEGIDA` | `V` | Descripción de la cantidad protegida.<br>Varchar: 60 caracteres. |
| 8 | `CIDBANDERA` | `I` | Identificador de la bandera asociado a la moneda. |
| 9 | `CDECIMALESMONEDA` | `I` | Número de decimales de la moneda. |
| 10 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admMovimientos` — Tabla de Movimientos

> Esta tabla contiene los movimientos realizados en los diferentes documentos.

> Importante: En los documentos hay dos clases de movimientos: Reales y Ocultos; ambos movimientos se  guardan en la misma tabla. En esta tabla se explica en qué consisten.

> Clase  Descripción  Valor del campo

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMOVIMIENTO` | `I` | Identificador del movimiento. |
| 2 | `CIDDOCUMENTO` | `I` | Identificador del documento dueño del movimiento.<br>Nota: Cuando el movimiento es oculto este campo debe estar<br>vacío. |
| 3 | `CNUMEROMOVIMIENTO` | `F` | Número de movimiento. |
| 4 | `CIDDOCUMENTODE` | `I` | Tipo de documento del concepto de documento. Referencia de la<br>tabla TblDocumentoDe. |
| 5 | `CIDPRODUCTO` | `I` | Identificador del producto del movimiento (algunas veces este<br>atributo esta vacío porque los documentos de CxC y CxP no<br>llevan producto) o del detalle. |
| 6 | `CIDALMACEN` | `I` | Identificador del almacén del movimiento.<br>Nota: Algunas veces este campo está vacío porque algunos<br>movimientos no llevan almacén o porque su documento lo lleva. |
| 7 | `CUNIDADES` | `F` | Cantidad de unidad base del movimiento. Se diferencia del<br>campo cUnidadesCapturadas porque cUnidades siempre está en<br>la unidad base y cUnidadesCapturadas puede estar en una<br>unidad con equivalencia con la base. |
| 8 | `CUNIDADESNC` | `F` | Cantidad de unidades no convertibles del movimiento. |
| 9 | `CUNIDADESCAPTURADAS` | `F` | Cantidad de unidades capturadas por el usuario. Se diferencia<br>del campo cUnidades porque cUnidades siempre está en la<br>unidad base y cUnidadesCapturadas puede estar en una unidad<br>con equivalencia con la base. |
| 10 | `CIDUNIDAD` | `I` | Identificador de unidad de peso y medida que representa en qué<br>unidad se captura el movimiento.<br>Nota: Puede ser solamente la base o una convertible. |
| 11 | `CIDUNIDADNC` | `I` | Identificador de la unidad de peso y medida no convertible (si la<br>tiene). |
| 12 | `CPRECIO` | `F` | Precio del producto. |
| 13 | `CPRECIOCAPTURADO` | `F` | Precio capturado por el usuario. Se diferencia de cPrecio porque<br>cPrecio es el precio de la unidad base y cPrecioCapturado es el<br>precio de la unidad capturada por el usuario. |
| 14 | `CCOSTOCAPTURADO` | `F` | Costo unitario del movimiento capturado por el usuario. Se usa<br>para devoluciones sobre ventas. |
| 15 | `CCOSTOESPECIFICO` | `F` | Es el costo calculado del movimiento.<br>Nota: En caso de que tenga un costeo promedio por almacén o<br>último costo, el valor de este campo no necesariamente<br>aparecerá en los reportes de Kárdex, ya que tomará el valor de<br>la tabla Costos históricos. |
| 16 | `CNETO` | `F` | Importe del neto para el movimiento. |
| 17 | `CIMPUESTO1` | `F` | Importe del Impuesto 1 para el movimiento. |
| 18 | `CPORCENTAJEIMPUESTO1` | `F` | Porcentaje del impuesto 1. |
| 19 | `CIMPUESTO2` | `F` | Importe del impuesto 2 para el movimiento. |
| 20 | `CPORCENTAJEIMPUESTO2` | `F` | Porcentaje del impuesto 2. |
| 21 | `CIMPUESTO3` | `F` | Importe del impuesto 3 para el movimiento. |
| 22 | `CPORCENTAJEIMPUESTO3` | `F` | Porcentaje del impuesto 3. |
| 23 | `CRETENCION1` | `F` | Importe de la retención 1 para el movimiento. |
| 24 | `CPORCENTAJERETENCION1` | `F` | Porcentaje de la retención 1. |
| 25 | `CRETENCION2` | `F` | Importe de la retención 2 para el movimiento. |
| 26 | `CPORCENTAJERETENCION2` | `F` | Porcentaje de la retención 2. |
| 27 | `CDESCUENTO1` | `F` | Importe del descuento 1 para el movimiento. |
| 28 | `CPORCENTAJEDESCUENTO1` | `F` | Porcentaje del descuento 1. |
| 29 | `CDESCUENTO2` | `F` | Importe del descuento 2 para el movimiento. |
| 30 | `CPORCENTAJEDESCUENTO2` | `F` | Porcentaje del descuento 2. |
| 31 | `CDESCUENTO3` | `F` | Importe del descuento 3 para el movimiento. |
| 32 | `CPORCENTAJEDESCUENTO3` | `F` | Porcentaje del descuento 3. Importe del descuento 3 para el<br>movimiento. |
| 33 | `CDESCUENTO4` | `F` | Importe del descuento 4 para el movimiento. |
| 34 | `CPORCENTAJEDESCUENTO4` | `F` | Porcentaje del descuento 4. |
| 35 | `CDESCUENTO5` | `F` | Importe del descuento 5 para el movimiento. |
| 36 | `CPORCENTAJEDESCUENTO5` | `F` | Porcentaje del descuento 5. |
| 37 | `CTOTAL` | `F` | Importe del total del movimiento. |
| 38 | `CPORCENTAJECOMISION` | `F` | Porcentaje de comisión del movimiento. |
| 39 | `CREFERENCIA` | `V` | Referencia del movimiento.<br>Varchar: 20 caracteres. |
| 40 | `COBSERVAMOV` | `T` | Observaciones del movimiento. |
| 41 | `CAFECTAEXISTENCIA` | `I` | Manera en que se afecta las existencias por el concepto.<br>1 = Entradas<br>2 = Salidas<br>3 = Ninguno |
| 42 | `CAFECTADOSALDOS` | `I` | Indica si el movimiento ya afectó saldos y estadísticas.<br>0 = No afectado<br>1 = Afectado |
| 43 | `CAFECTADOINVENTARIO` | `I` | Indica si el movimiento ya afectó existencias y costos.<br>0 = No afectado<br>1 = Afectado |
| 44 | `CFECHA` | `D` | Fecha del documento. |
| 45 | `CMOVTOOCULTO` | `I` | Idica si un movimiento fue capturado por el usuario (movimiento<br>real) o fue generado por el sistema (movimiento oculto).<br>0 = Movimiento real<br>1 = Movimiento oculto<br>2 = Movimiento oculto<br>Importante: Un movimiento oculto se genera para el movimiento<br>del almacén destino de un traspaso, los movimientos de los<br>detalles de características y los movimientos de los componentes<br>de un paquete. |
| 46 | `CIDMOVTOOWNER` | `I` | Identificador del documento dueño de un movimiento oculto.<br>Importante: Cuando el movimiento es oculto este campo<br>contiene el Identificador del movimiento que lo originó. Cuando el<br>movimiento es real este campo debe estar vacío.<br>Nota: Un movimiento oculto se genera para el movimiento del<br>almacén destino de un traspaso, los movimientos de los detalles<br>de características y los movimientos de los componentes de un<br>paquete. |
| 47 | `CIDMOVTOORIGEN` | `I` | Cuando el movimiento proviene de una conversión este campo<br>contiene el Identificador del movimiento origen.<br>Ejemplo: Si el movimiento es de una factura que surtió a un<br>pedido, este campo contendrá el identificador del movimiento del<br>pedido. |
| 48 | `CUNIDADESPENDIENTES` | `F` | Son las unidades que faltan por convertir en la conversión. |
| 49 | `CUNIDADESNCPENDIENTES` | `F` | Son las unidades no convertibles que faltan por convertir en la<br>conversión. |
| 50 | `CUNIDADESORIGEN` | `F` | Son las unidades que el movimiento destino (factura) toma del<br>movimiento origen (pedido), en la conversión. |
| 51 | `CUNIDADESNCORIGEN` | `F` | Son las unidades no convertibles que el movimiento destino<br>(factura) toma del movimiento origen (pedido), en la conversión. |
| 52 | `CTIPOTRASPASO` | `I` | Tipo de traspaso en el movimiento:<br>1 = Sin traspaso<br>2 = Origen traspaso<br>3 = Destino traspaso<br>4 = Detalle de características de un traspaso<br>5 = Facturación de consignación al cliente<br>6 = Compra de consignación del proveedor<br>7 = Movimiento de un componente |
| 53 | `CIDVALORCLASIFICACION` | `I` | Identificador del valor de clasificación del movimiento. Debe ser<br>un valor de la clasificación 31. |
| 54 | `CTEXTOEXTRA1` | `V` | Texto extra 1.<br>Varchar: 50 caracteres. |
| 55 | `CTEXTOEXTRA2` | `V` | Texto extra 2.<br>Varchar: 50 caracteres. |
| 56 | `CTEXTOEXTRA3` | `V` | Texto extra 3.<br>Varchar: 50 caracteres. |
| 57 | `CFECHAEXTRA` | `D` | Fecha extra. |
| 58 | `CIMPORTEEXTRA1` | `F` | Importe extra 1. |
| 59 | `CIMPORTEEXTRA2` | `F` | Importe extra 2. |
| 60 | `CIMPORTEEXTRA3` | `F` | Importe extra 3. |
| 61 | `CIMPORTEEXTRA4` | `F` | Importe extra 4. |
| 62 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 63 | `CGTOMOVTO` | `F` | Gasto sobre compra del movimiento. |
| 64 | `CSCMOVTO` | `V` | Segmento contable del movimiento.<br>Varchar: 50 caracteres. |
| 65 | `CCOMVENTA` | `F` | Indica la comisión de venta por ese movimiento. |
| 66 | `CIDMOVTODESTINO` | `I` | Identificador del movimiento destino para el manejo de<br>consolidacion. |
| 67 | `CNUMEROCONSOLIDACIONES` | `I` | Número de consolidaciones realizadas en un movimiento. |
| 68 | `COBJIMPU01` | `V` | Indica el objeto de impuesto de cada movimiento. |
| 69 | `CCONFIMP1` | `I` | Indica la configuración del impuesto 1 |
| 70 | `CCONFIMP2` | `I` | Indica la configuración del impuesto 2<br>0 = IEPS tomado de la configuración del concepto<br>1 = IEPS Tasa 0<br>2 = IEPS Tasa exenta<br>3 = IEPS Cuota 0 |
| 71 | `CCONFIMP3` | `I` | Indica la configuración del impuesto 3<br>0 = IEPS tomado de la configuración del concepto<br>1 = IEPS Tasa 0<br>2 = IEPS Tasa exenta<br>3 = IEPS Cuota 0 |
| 72 | `CCONFIMP4` | `I` | Indica la configuración del impuesto 4<br>0 = IEPS tomado de la configuración del concepto<br>1 = IEPS Tasa 0<br>2 = IEPS Tasa exenta<br>3 = IEPS Cuota 0 |

---

### `admMovimientosCapas` — Tabla de Movimientos de capas

> Esta tabla contiene los campos que relacionan los movimientos con sus pedimentos, números de lote o capas  de UEPS y PEPS de los productos.

> Esta tabla está relacionada con la tabla admCapasProducto ya que contiene los movimientos de esta.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDMOVIMIENTO` | `I` | Identificador del movimiento asociado a la capa. |
| 3 | `CIDCAPA` | `I` | Identificador de la capa asociada al movimiento, la capa puede<br>contener los pedimentos, lotes o capas UEPS y PEPS del<br>movimiento. |
| 4 | `CFECHA` | `D` | Fecha del movimiento, se usa para reportes. |
| 5 | `CUNIDADES` | `F` | Unidades de la capa cIdCapa que le pertenecen al movimiento<br>cIdMovimiento. |
| 6 | `CTIPOCAPA` | `I` | Contenido de la capa a la que hace referencia el movimiento de<br>capas:<br>1 = Número de serie<br>2 = Pedimento<br>3 = Lote<br>4 = Pedimento y Lote<br>5 = Capa de costo (para costeo UEPS y PEPS) |
| 7 | `CIDUNIDAD` | `I` | Identifica la unidad de captura de los movimientos de capas. |

---

### `admMovimientosContables` — Tabla de Movimientos

> Esta tabla contiene las líneas de detalle de los asientos contables, es decir, los campos de los movimientos  contables que forman los asientos.

> Nota: Puede contener una cuenta contable, una combinación de  mnemónicos de cuentas o una combinación de mnemónicos con  segmentos fijos.

> 1 = Ninguno  2 = Referencia del Movimiento  3 = Texto Capturado  4 = Texto Capturado + Serie + Folio del documento  5 = Texto Extra 1 del Movimiento  6 = Texto Extra 2 del Movimiento  7 = Texto Extra 3 del Movimiento  8 = Referencia Documento  9 = Nombre del Concepto  10 = Texto Extra 1 de Documento  11 = Texto Extra 2 de Documento  12 = Texto Extra 3 de Documento

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMOVIMIENTOCONTABLE` | `I` | Identificador del movimiento contable. |
| 2 | `CIDASIENTOCONTABLE` | `I` | Identificador del asiento contable dueño del movimiento. |
| 3 | `CCUENTA` | `V` | Cuenta del movimiento contable.<br>Varchar: 250 caracteres.<br>Nota: Puede contener una cuenta contable, una combinación de<br>mnemónicos de cuentas o una combinación de mnemónicos con<br>segmentos fijos. |
| 4 | `CTIPOMOVIMIENTO` | `I` | Tipo del movimiento contable:<br>1 = Cargo<br>2 = Cargo en Rojo<br>3 = Abono<br>4 = Abono en Rojo |
| 5 | `CIMPORTEBASE` | `F` | Importe del movimiento contable.<br>1 = Neto<br>2 = Total<br>3 = Impuesto 1<br>4 = Impuesto 2<br>5 = Impuesto 3<br>6 = Retención 1<br>7 = Retención 2<br>8 = Descuento del Documento 1<br>9 = Descuento del Documento 2<br>10 = Descuento del Movimiento 1<br>11 = Descuento del Movimiento 2<br>12 = Descuento del Movimiento 3<br>13 = Descuento del Movimiento 4<br>14 = Descuento del Movimiento 5<br>15 = Total Descuentos<br>16 = Neto - Total Descuentos<br>17 = Gasto 1<br>18 = Gasto 2<br>19 = Gasto 3<br>20 = Diferencia Cargos – Abonos<br>21 = Costo del Movimiento<br>22 = Complementaria<br>23 = Total en la moneda del Documento<br>24 = Importe Extra 1 del Documento<br>25 = Importe Extra 2 del Documento<br>26 = Importe Extra 3 del Documento<br>27 = Importe Extra 4 del Documento<br>28 = Importe Extra 1 del Movimiento<br>29 = Importe Extra 2 del Movimiento<br>30 = Importe Extra 3 del Movimiento<br>31 = Importe Extra 4 del Movimiento |
| 6 | `CPORCENTAJE` | `F` | Porcentaje que se toma del Importe Base. |
| 7 | `CORIGENREFERENCIA` | `I` | Origen de la referencia del movimiento contable:<br>1 = Ninguno<br>2 = Referencia del Movimiento<br>3 = Texto Capturado<br>4 = Texto Capturado + Serie + Folio del documento<br>5 = Texto Extra 1 del Movimiento<br>6 = Texto Extra 2 del Movimiento<br>7 = Texto Extra 3 del Movimiento<br>8 = Referencia Documento<br>9 = Nombre del Concepto<br>10 = Texto Extra 1 de Documento<br>11 = Texto Extra 2 de Documento<br>12 = Texto Extra 3 de Documento |
| 8 | `CREFERENCIA` | `V` | Referencia de los movimientos contables. Sólo se usa si el<br>campo cOrigenReferencia es igual a 3 o 4.<br>Varchar: 10 caracteres. |
| 9 | `CORIGENDIARIO` | `I` | Origen del diario del movimiento contable.<br>1 = Ninguno<br>2 = Diario Capturado |
| 10 | `CDIARIO` | `V` | Diario por omisión del movimiento contable.<br>Varchar: 10 caracteres.<br>Nota: Sólo se usa si el campo cOrigenDiario es igual a 2. |
| 11 | `CORIGENCONCEPTO` | `I` | Origen del concepto del movimiento contable:<br>1 = Ninguno.<br>2 = Referencia del Movimiento.<br>3 = Texto Capturado.<br>4 = Texto Capturado + Serie + Folio del documento.<br>5 = Texto Extra 1 del Movimiento.<br>6 = Texto Extra 2 del Movimiento.<br>7 = Texto Extra 3 del Movimiento.<br>8 = Referencia Documento.<br>9 = Nombre del Concepto.<br>10 = Texto Extra 1 de Documento.<br>11 = Texto Extra 2 de Documento.<br>12 = Texto Extra 3 de Documento |
| 12 | `CCONCEPTO` | `V` | Concepto del Movimiento de la póliza.<br>Varchar: 50 caracteres.<br>Nota: Sólo se usa si el campo CORIGENC01<br>(CORIGENCONCEPTO) es igual a 3. |
| 13 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 14 | `CSUMARIZ` | `I` | Indica si los movimientos de la póliza van sumarizados o<br>detallados. |
| 15 | `CSUPMOVS0` | `I` | Indica si en la póliza se deben o no suprimir movimientos en<br>cero. |
| 16 | `CORISEGNEG` | `I` | Indica de dónde se tomará el valor del Segmento de Negocio<br>seleccionado:<br>1 = Ninguno<br>2 = Número Capturado<br>3 = Código del almacén<br>4-10 = Segmento CL01 al CL07 (Clientes)<br>11-17 = Segmento PV01 al PV07 (Proveedores)<br>18-20 = Segmento PR01 al PR03 (Productos)<br>21-23 = Segmento AG01 al AG03 (Agentes / Vendedores)<br>24-26 = Segmento AL01 al AL03 (Almacenes)<br>27 = Segmento MO01 |
| 17 | `CSEGNEG` | `V` | Indica el valor capturado en el campo.<br>0-999 = Representa el número de Segmento de Negocio que<br>debe existir en CONTPAQi® Contabilidad.<br>Varchar: 4 caracteres.<br>Nota: Sólo se utiliza cuando en el campo CORISEGNEG<br>aparece el valor 2 = Número Capturado. |
| 18 | `CIMPMONEXT` | `I` | Significa que el importe base va a usarse para el campo de<br>moneda extranjera del movimiento de la póliza. Se usa en la<br>definicición de movimientos de asientos contables. |
| 19 | `CIMPMONDOC` | `I` | Configurar importes base como importes en la moneda del<br>documento. |
| 20 | `CCOMPLEMEN` | `I` | Configurar importes base como complementaria |

---

### `admMovimientosPrepoliza` — Tabla de Movimientos de

> Esta tabla almacena los movimientos de las prepólizas creadas en CONTPAQi® Comercial.

> Ejemplo: 1999, 2000, 2001.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMOVIMIENTOPREPOLIZA` | `I` | Identificador del movimiento de prepóliza. |
| 2 | `CIDPREPOLIZA` | `I` | Identificador de la prepóliza dueña de los movimientos. |
| 3 | `EJE` | `I` | Número de ejercicio de la póliza.<br>Ejemplo: 1999, 2000, 2001. |
| 4 | `PERIODO` | `I` | Número de periodo de la póliza. |
| 5 | `TIPOPOL` | `I` | Tipo de la póliza:<br>1 = Ingresos<br>2 = Egresos<br>3 = Diario<br>4 = Orden<br>5 = Estadística |
| 6 | `NUMPOL` | `I` | Número de la póliza. |
| 7 | `MOVTO` | `I` | Número de movimiento de la póliza. |
| 8 | `CUENTA` | `V` | Cuenta del movimiento de la póliza.<br>Varchar: 50 caracteres. |
| 9 | `TIPOMOV` | `I` | Tipo de movimiento. Toma 4 valores para conservar equivalencia<br>con el campo cTipoMovimiento de la tabla<br>admMovimientosContables y para distinguir los cargos y<br>abonos en rojo:<br>1 = Cargo<br>2 = Cargo en Rojo<br>3 = Abono<br>4 = Abono en Rojo |
| 10 | `REFERENCIA` | `V` | Referencia del movimiento de la póliza.<br>Varchar: 10 caracteres. |
| 11 | `IMPORTE` | `F` | Importe del movimiento de la póliza. |
| 12 | `DIARIO` | `V` | Diario del movimiento de la póliza.<br>Varchar: 10 caracteres. |
| 13 | `MONEDA` | `F` | Importe en moneda extranjera del movimiento de la póliza. |
| 14 | `CONCEPTO` | `V` | Concepto del movimiento de la póliza.<br>Varchar: 50 caracteres. |
| 15 | `FECHA` | `D` | Fecha de la póliza. |
| 16 | `SEGNEG` | `V` | Segmento de negocio configurado al movimiento de póliza.<br>Varchar: 10 caracteres. |

---

### `admMovimientosSerie` — Tabla de Movimientos de series

> Esta tabla almacena la relación de los movimientos con los números de serie incluidos en ellos.

> Esta tabla está relacionada con la tabla admNumerosSerie.

> Nota: Se usa para reportes.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDMOVIMIENTO` | `I` | Identificador del movimiento asociado a la serie. |
| 3 | `CIDSERIE` | `I` | Identificador del número de serie asociado al movimiento. |
| 4 | `CFECHA` | `D` | Fecha del movimiento.<br>Nota: Se usa para reportes. |

---

### `admMovtosInvFisico` — Tabla de Movimientos de inventario

> Esta tabla almacena los movimientos del inventario físico.

> Nota: Útil en caso de productos con características.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMOVIMIENTO` | `I` | Identificador del movimiento del inventario físico. |
| 2 | `CIDPRODUCTO` | `I` | Identificador del producto perteneciente al movimiento del<br>inventario físico. |
| 3 | `CIDALMACEN` | `I` | Identificador del almacén perteneciente al movimiento del<br>inventario físico. |
| 4 | `CIDUNIDAD` | `I` | Identificador de la unidad perteneciente al movimiento del<br>inventario físico. |
| 5 | `CUNIDADES` | `F` | Numero de unidades en la unidad base o convertibles del<br>movimiento. |
| 6 | `CUNIDADESNC` | `F` | Numero de unidades en la unidad no convertible del movimiento. |
| 7 | `CUNIDADESCAPTURADAS` | `F` | Numero de unidades capturadas en la unidad seleccionada por<br>el usuario. |
| 8 | `CMOVTOOCULTO` | `I` | Bandera que indica si este movimiento es generado por otro.<br>Nota: Útil en caso de productos con características. |
| 9 | `CIDMOVTOOWNER` | `I` | Identificador del movimiento dueño de éste.<br>Nota: Útil en caso de productos con características. |

---

### `admMovtosInvFisicoSerieCa` — Tabla de Movimientos de

> Esta tabla almacena los movimientos de números de serie y capas capturadas en el inventario físico.

> Nota: Siempre será el mismo del movimiento. Se coloca aquí  solo para acelerar las búsquedas.

> Nota: Para una serie siempre es 1.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDSERIECAPA` | `I` | Identificador de la serie capa asignada al movimiento de<br>inventario físico. |
| 2 | `CIDMOVTOINVENTARIOFISICO` | `I` | Identificador del movimiento del inventario físico relacionado con<br>la serie o lote. |
| 3 | `CIDPRODUCTO` | `I` | Producto que cuenta con nÚmero de serie, lote y/o pedimento o<br>que su costo se calcula por UEPS. |
| 4 | `CNUMEROSERIE` | `V` | Numero de serie.<br>Varchar: 30 caracteres. |
| 5 | `CIDALMACEN` | `I` | Identificador del Almacén asignado a la capa.<br>Nota: Siempre será el mismo del movimiento. Se coloca aquí<br>solo para acelerar las búsquedas. |
| 6 | `CTIPO` | `I` | Tipo de la serie capa:<br>0 = Serie<br>1 = Capa |
| 7 | `CNUMEROLOTE` | `V` | Numero de Lote.<br>Varchar: 30 caracteres. |
| 8 | `CFECHACADUCIDAD` | `D` | Fecha de caducidad del lote. |
| 9 | `CFECHAFABRICACION` | `D` | Fecha de fabricación del lote. |
| 10 | `CPEDIMENTO` | `V` | Número del pedimento.<br>Varchar: 30 caracteres. |
| 11 | `CADUANA` | `V` | Nombre de la agencia aduanal.<br>Varchar: 60 caracteres. |
| 12 | `CFECHAPEDIMENTO` | `D` | Fecha del pedimento. |
| 13 | `CTIPOCAMBIO` | `F` | Tipo de cambio arbitrario del pedimento. |
| 14 | `CCANTIDAD` | `F` | Cantidad de unidades en el lote/pedimento.<br>Nota: Para una serie siempre es 1. |
| 15 | `CIDCAPA` | `I` | Id de de la capa, se utiliza para identificar la capa a la cual se<br>afectará en el ajuste de inventario. |

---

### `admNumerosSerie` — Tabla de Números de Serie

> Esta tabla contiene los campos para los números de serie de los productos.

> Nota: Siempre será el mismo del movimiento. Se coloca aquí  sólo para acelerar las búsquedas.

> inventario).  5 = Serie huérfana consignación (se consignó pero nunca

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDSERIE` | `I` | Identificador del número de serie. |
| 2 | `CIDPRODUCTO` | `I` | Producto que cuenta con número de serie y/o lote y/o pedimento<br>o que su costo se calcula por UEPS O PEPS. |
| 3 | `CNUMEROSERIE` | `V` | Numero de serie.<br>Varchar: 30 caracteres. |
| 4 | `CIDALMACEN` | `I` | Identificador del Almacén asignado a la capa.<br>Nota: Siempre será el mismo del movimiento. Se coloca aquí<br>sólo para acelerar las búsquedas. |
| 5 | `CESTADO` | `I` | Estado del número de serie:<br>1 = Serie disponible.<br>2 = Serie consignada del proveedor.<br>3 = Serie no disponible (ya fue facturada o se le dio salida)<br>4 = Serie huérfana (se vendió pero nunca existió en<br>inventario).<br>5 = Serie huérfana consignación (se consignó pero nunca<br>existió en inventario).<br>6 = Serie registrada para inventario físico.<br>7 = Serie vendidas.<br>8 = Serie consignada al cliente.<br>9 = Serie huérfana devuelta (se vendió, luego se devolvió pero<br>nunca existió en inventario).<br>10 = Serie huérfana de consignación devuelta (se consignó,<br>luego se devolvió pero nunca existió en inventario).<br>11 = Serie consignada del proveedor y luego vendida.<br>12 = Serie consignada del proveedor y luego consignada a un<br>cliente. |
| 6 | `CESTADOANTERIOR` | `I` | Estado del número de serie antes del último movimiento. Sirve<br>para verificar de manera rápida la historia de la serie.<br>0 = Serie nueva.<br>1 = Serie disponible.<br>2 = Serie consignada del proveedor.<br>3 = Serie no disponible (ya fue facturada o se le dio salida).<br>4 = Serie huérfana (se vendió pero nunca existió en inventario).<br>5 = Serie huérfana consignación (se consignó pero nunca<br>existió en inventario).<br>6 = Serie registrada para inventario físico.<br>7 = Serie vendidas.<br>8 = Serie consignada al cliente.<br>9 = Serie huérfana devuelta (se vendió, luego se devolvió pero<br>nunca existió en inventario).<br>10 = Serie huérfana de consignación devuelta (se consignó,<br>luego se devolvió pero nunca existió en inventario).<br>11 = Serie consignada del proveedor y luego vendida.<br>12 = Serie consignada del proveedor y luego consignada a un<br>cliente. |
| 7 | `CNUMEROLOTE` | `V` | Numero de lote.<br>Varchar: 30 caracteres. |
| 8 | `CFECHACADUCIDAD` | `D` | Fecha de caducidad del lote. |
| 9 | `CFECHAFABRICACION` | `D` | Fecha de fabricación del lote. |
| 10 | `CPEDIMENTO` | `V` | Número del pedimento.<br>Varchar: 30 caracteres. |
| 11 | `CADUANA` | `V` | Agencia aduanal.<br>Varchar: 60 caracteres. |
| 12 | `CFECHAPEDIMENTO` | `D` | Fecha del pedimento. |
| 13 | `CTIPOCAMBIO` | `F` | Tipo de cambio arbitrario del pedimento. |
| 14 | `CCOSTO` | `F` | Costo del producto. |
| 15 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 16 | `CNUMADUANA` | `I` | Número de la agencia aduanal. |
| 17 | `CCLAVESAT` | `V` | Clave SAT utilizada para las series con pedimentos.<br>Varchar: 30 caracteres. |

---

### `admParametros` — Tabla de Parámetros

> Esta tabla contiene los campos de parámetros en la empresa.

> 2 = Afectación de saldos  4 = Operación de documentos  8 = Operación de movimientos  16 = Operación de conversiones  32 = Relación cargos-abonos

> No.  Campo  T  descripción

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDEMPRESA` | `I` | Identificador de la empresa. |
| 2 | `CNOMBREEMPRESA` | `V` | Nombre de la empresa.<br>Varchar: 60 caracteres. |
| 3 | `CEXISTENCIANEGATIVA` | `I` | No está en uso. |
| 4 | `CIDEJERCICIOACTUAL` | `I` | Identificador del ejercicio Aatual. |
| 5 | `CPERIODOACTUAL` | `I` | Periodo. Los ejercicios siempre tienen 12 periodos, por lo tanto<br>este número siempre es entre 1 y 12. |
| 6 | `CRFCEMPRESA` | `V` | RFC de la empresa.<br>Varchar: 20 caracteres. |
| 7 | `CCURPEMPRESA` | `V` | Código Único de Registro de Población de la Empresa.<br>Varchar: 20 caracteres. |
| 8 | `CREGISTROCAMARA` | `V` | Registro en la Cámara.<br>Varchar: 50 caracteres. |
| 9 | `CCUENTAESTATAL` | `V` | Cuenta Estatal de la Empresa.<br>Varchar: 50 caracteres. |
| 10 | `CREPRESENTANTELEGAL` | `V` | Representante legal de la empresa.<br>Varchar: 50 caracteres. |
| 11 | `CNOMBRECORTO` | `V` | Descripción corta de la empresa.<br>Varchar: 20 caracteres. |
| 12 | `CIDALMACENASUMIDO` | `I` | Identificador del almacén asumido de la empresa. Se usa como<br>asumido en la columna de almacén de los movimientos. |
| 13 | `CFECHACIERRE` | `D` | Fecha a partir de la cual se pueden realizar movimientos. Es<br>actualizada por el proceso de cierre. |
| 14 | `CDECIMALESUNIDADES` | `I` | Número de decimales para Unidades. |
| 15 | `CDECIMALESPRECIOVENTA` | `I` | Número de decimales para el precio de venta. |
| 16 | `CDECIMALESCOSTOS` | `I` | Número de decimales para costos y precios de compra. |
| 17 | `CDECIMALESTIPOSCAMBIO` | `I` | Número de decimales para tipos de cambio. |
| 18 | `CBANMARGENUTILIDAD` | `I` | Indica si los precios del producto deben mantenerse arriba de un<br>margen de utilidad.<br>0 = No se deben mantener arriba del margen de utilidad.<br>1 = Si se deben mantener arriba del margen de utilidad. |
| 19 | `CIMPUESTO1` | `F` | Porcentaje de impuesto 1 de la empresa. |
| 20 | `CIMPUESTO2` | `F` | Porcentaje de impuesto 2 de la empresa. |
| 21 | `CIMPUESTO3` | `F` | Porcentaje de impuesto 3 de la empresa. |
| 22 | `CUSOCUOTAIESPS` | `I` | Indica si el impuesto 2 del producto se usa como cuota de<br>IESPS, esto sirve para cumplir con la Ley del IESPS del 2000:<br>0 = No usar el impuesto 2 del producto como cuota de IESPS.<br>1 = Usar el impuesto 2 del producto como cuota de IESPS |
| 23 | `CRETENCIONCLIENTE1` | `F` | Porcentaje de retención 1 para clientes de la empresa. |
| 24 | `CRETENCIONCLIENTE2` | `F` | Porcentaje de retención 2 para clientes de la empresa. |
| 25 | `CRETENCIONPROVEEDOR1` | `F` | Porcentaje de retención 1 para proveedores de la empresa. |
| 26 | `CRETENCIONPROVEEDOR2` | `F` | Porcentaje de retención 2 para proveedores de la empresa. |
| 27 | `CDESCUENTODOCTO` | `F` | Porcentaje de descuento por documento general de la<br>empresa. |
| 28 | `CDESCUENTOMOVTO` | `F` | Porcentaje de descuento por movimiento general de la<br>empresa. |
| 29 | `CCOMISIONVENTA` | `F` | Comisión de venta general de la empresa. |
| 30 | `CCOMISIONCOBRO` | `F` | Comisión de cobro general de la empresa. |
| 31 | `CLISTAPRECIOGENERAL` | `I` | Número de lista de precios por omisión. |
| 32 | `CIDALMACENCONSIGNACION` | `I` | Almacén utilizado por el documento de consignación de<br>proveedor. |
| 33 | `CMANEJOFECHA` | `I` | Campo para el manejo de documentos fuera de fecha.<br>0 = No permite manejar documentos fuera de fecha.<br>1 = Permite manejar documentos fuera de fecha. |
| 34 | `CIDMONEDABASE` | `I` | Identificador de la moneda base del sistema. |
| 35 | `CIDCLIENTEMOSTRADOR` | `I` | Identificador del cliente de mostrador. |
| 36 | `CRUTACONTPAQ` | `V` | Ruta de los archivos de CONTPAQi® Contabilidad. |
| 37 | `CUSACARACTERISTICAS` | `I` | Indica si la empresa usa o no productos con características.<br>0 = No se usan productos con características.<br>1 = Se usan productos con características. |
| 38 | `CUSAUNIDADNC` | `I` | Campo que especifica si en las pantallas de los documentos,<br>los movimientos mostrarán o no las unidades no convertibles.<br>0 = No se muestran.<br>1 = Si se muestran. |
| 39 | `CMASCARILLACLIENTES` | `V` | Mascarilla de Clientes y Proveedores.<br>Varchar: 30 caracteres. |
| 40 | `CMASCARILLAPRODUCTO` | `V` | Mascarilla de Productos.<br>Varchar: 30 caracteres. |
| 41 | `CMASCARILLAALMACEN` | `V` | Mascarilla de Almacenes.<br>Varchar: 30 caracteres. |
| 42 | `CMASCARILLAAGENTE` | `V` | Mascarilla de Agentes.<br>Varchar: 30 caracteres. |
| 43 | `CMASCARILLARFC` | `V` | Mascarilla del RFC.<br>Varchar: 30 caracteres. |
| 44 | `CMASCARILLACURP` | `V` | Mascarilla del CURP.<br>Varchar: 30 caracteres. |
| 45 | `CBANDIRECCION` | `I` | Bandera que indica si se activa o no la dirección. |
| 46 | `CNOMBRELISTA1` | `V` | Nombre de la lista de precios de venta 1.<br>Varchar: 20 caracteres. |
| 47 | `CIDMONEDALISTA1` | `I` | Identificador de la moneda de la lista de precios de venta 1. |
| 48 | `CNOMBRELISTA2` | `V` | Nombre de la lista de precios de venta 2.<br>Varchar: 20 caracteres. |
| 49 | `CIDMONEDALISTA2` | `I` | Identificador de la moneda de la lista de precios de venta 2. |
| 50 | `CNOMBRELISTA3` | `V` | Nombre de la lista de precios de venta 3.<br>Varchar: 20 caracteres. |
| 51 | `CIDMONEDALISTA3` | `I` | Identificador de la moneda de la lista de precios de venta 3. |
| 52 | `CNOMBRELISTA4` | `V` | Nombre de la lista de precios de venta 4.<br>Varchar: 20 caracteres. |
| 53 | `CIDMONEDALISTA4` | `I` | Identificador de la moneda de la lista de precios de venta 4. |
| 54 | `CNOMBRELISTA5` | `V` | Nombre de la lista de precios de venta 5.<br>Varchar: 20 caracteres. |
| 55 | `CIDMONEDALISTA5` | `I` | Identificador de la moneda de la lista de precios de venta 5. |
| 56 | `CNOMBRELISTA6` | `V` | Nombre de la lista de precios de venta 6.<br>Varchar: 20 caracteres. |
| 57 | `CIDMONEDALISTA6` | `I` | Identificador de la moneda de la lista de precios de venta 6. |
| 58 | `CNOMBRELISTA7` | `V` | Nombre de la lista de precios de venta 7.<br>Varchar: 20 caracteres. |
| 59 | `CIDMONEDALISTA7` | `I` | Identificador de la moneda de la lista de precios de venta 7. |
| 60 | `CNOMBRELISTA8` | `V` | Nombre de la lista de precios de venta Numero 8.<br>Varchar: 20 caracteres. |
| 61 | `CIDMONEDALISTA8` | `I` | Identificador de la moneda de la lista de precios de venta 8. |
| 62 | `CNOMBRELISTA9` | `V` | Nombre de la lista de precios de venta 9.<br>Varchar: 20 caracteres. |
| 63 | `CIDMONEDALISTA9` | `I` | Identificador de la moneda de la lista de precios de venta 9. |
| 64 | `CNOMBRELISTA10` | `V` | Nombre de la lista de precios de venta 10.<br>Varchar: 20 caracteres. |
| 65 | `CIDMONEDALISTA10` | `I` | Identificador de la moneda de la lista de precios de venta 10. |
| 66 | `CNOMBREIMPUESTO1` | `V` | Nombre del impuesto 1 de la empresa.<br>Varchar: 20 caracteres. |
| 67 | `CNOMBREIMPUESTO2` | `V` | Nombre del impuesto 2 de la empresa.<br>Varchar: 20 caracteres. |
| 68 | `CNOMBREIMPUESTO3` | `V` | Nombre del impuesto 3 de la empresa.<br>Varchar: 20 caracteres. |
| 69 | `CNOMBRERETENCION1` | `V` | Nombre de la retención 1 de la empresa.<br>Varchar: 20 caracteres. |
| 70 | `CNOMBRERETENCION2` | `V` | Nombre de la retención 2 de la empresa.<br>Varchar: 20 caracteres. |
| 71 | `CNOMBREGASTO1` | `V` | Nombre del gasto sobre compras 1 de la empresa.<br>Varchar: 20 caracteres. |
| 72 | `CNOMBREGASTO2` | `V` | Nombre del gasto sobre compras 2 de la empresa.<br>Varchar: 20 caracteres. |
| 73 | `CNOMBREGASTO3` | `V` | Nombre del gasto sobre compras 3 de la empresa.<br>Varchar: 20 caracteres. |
| 74 | `CNOMBREDESCUENTOMOV1` | `V` | Nombre del descuento 1 de movimientos.<br>Varchar: 20 caracteres. |
| 75 | `CNOMBREDESCUENTOMOV2` | `V` | Nombre del descuento 2 de movimientos.<br>Varchar: 20 caracteres. |
| 76 | `CNOMBREDESCUENTOMOV3` | `V` | Nombre del descuento 3 de movimientos.<br>Varchar: 20 caracteres. |
| 77 | `CNOMBREDESCUENTOMOV4` | `V` | Nombre del descuento 4 de movimientos.<br>Varchar: 20 caracteres. |
| 78 | `CNOMBREDESCUENTOMOV5` | `V` | Nombre del descuento 5 de movimientos.<br>Varchar: 20 caracteres. |
| 79 | `CNOMBREDESCUENTODOC1` | `V` | Nombre del descuento 1 de documentos.<br>Varchar: 20 caracteres. |
| 80 | `CNOMBREDESCUENTODOC2` | `V` | Nombre del descuento 2 de documentos.<br>Varchar: 50 caracteres. |
| 81 | `CSEGCONTGENERAL1` | `V` | Segmento contable general 1 de la empresa.<br>Varchar: 50 caracteres. |
| 82 | `CSEGCONTGENERAL2` | `V` | Segmento contable general 2 de la empresa.<br>Varchar: 50 caracteres. |
| 83 | `CSEGCONTGENERAL3` | `V` | Segmento contable general 3 de la empresa.<br>Varchar: 50 caracteres. |
| 84 | `CSEGCONTGENERAL4` | `V` | Segmento contable general 4 de la empresa.<br>Varchar: 50 caracteres. |
| 85 | `CSEGCONTGENERAL5` | `V` | Segmento contable general 5 de la empresa.<br>Varchar: 50 caracteres. |
| 86 | `CSEGCONTGENERAL6` | `V` | Segmento contable general 6 de la empresa.<br>Varchar: 50 caracteres. |
| 87 | `CSEGCONTGENERAL7` | `V` | Segmento contable general 7 de la empresa.<br>Varchar: 50 caracteres. |
| 88 | `CSEGCONTGENERAL8` | `V` | Segmento contable general 8 de la empresa.<br>Varchar: 50 caracteres. |
| 89 | `CSEGCONTGENERAL9` | `V` | Segmento contable general 9 de la empresa.<br>Varchar: 50 caracteres. |
| 90 | `CSEGCONTGENERAL10` | `V` | Segmento contable general 10 de la empresa.<br>Varchar: 50 caracteres. |
| 91 | `CSEGCONTGENERAL11` | `V` | Segmento contable general 11 de la empresa.<br>Varchar: 50 caracteres. |
| 92 | `CCONSECUTIVODIARIO` | `F` | Consecutivo para pólizas de Ingresos. |
| 93 | `CCONSECUTIVOINGRESOS` | `F` | Consecutivo para pólizas de Egresos. |
| 94 | `CCONSECUTIVOEGRESOS` | `F` | Consecutivo para pólizas de Diario. |
| 95 | `CCONSECUTIVOORDEN` | `F` | Consecutivo para pólizas de Orden. |
| 96 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 97 | `CFECHACONGELAMIENTO` | `D` | Fecha de congelación de existencias sólo entra en efecto si<br>cBanCongelamiento = 1. |
| 98 | `CBANCONGELAMIENTO` | `I` | Bandera que indica si se activa o no el congelamiento de<br>existencias. |
| 99 | `CRUTAEMPRESAPRED` | `V` | Ruta de los archivos de la empresa predeterminada.<br>Varchar: 253 caracteres. |
| 100 | `CBANVISTASVENTAS` | `I` | Indica si despliega las Vistas de documentos de venta.<br>0 = No<br>1 = Sí |
| 101 | `CBANVISTASCOMPRAS` | `I` | Indica si despliega las Vistas de documentos de compra.<br>0 = No<br>1 = Sí |
| 102 | `CBANVISTASCTEPROVINVEN` | `I` | Indica si despliega las Vistas de documentos de clientes,<br>proveedores e inventarios.<br>0 = No<br>1 = Sí |
| 103 | `CBANVISTASCATALOGOS` | `I` | Indica si despliega las Vistas de catálogos.<br>0 = No<br>1 = Sí |
| 104 | `CAFECTARINVAUTOMATICO` | `I` | Afecta en línea las existencias y costos de los movimientos.<br>0 = No afectar<br>1 = Afectar |
| 105 | `CMETODOCOSTEO` | `I` | Método de costeo por omisión.<br>1 = Costo Promedio en Base a Entradas.<br>2 = Costo Promedio en Base a Entradas Almacén.<br>3 = Último Costo.<br>4 = UEPS<br>5 = PEPS<br>6 = Costo Específico<br>7 = Costo Estándar. |
| 106 | `CBANOBLIGATORIOEXISTENCIA` | `I` | No permite crear movimientos de salida si no hay existencias.<br>0 = Permite movimientos sin existencia.<br>1 = No permite movimientos sin existencia. |
| 107 | `CNUMIMPUESTOIVA` | `I` | Indica cuál de los tres impuestos es el IVA:<br>1 = Impuesto 1<br>2 = Impuesto 2<br>3 = Impuesto 3 |
| 108 | `CVERSIONACTUAL` | `V` | Versión de CONTPAQi® Comercial.<br>Varchar: 20 caracteres. |
| 109 | `CPRECIOSCONIVA` | `I` | Precios con IVA incluido.<br>0 = No<br>1 = Si |
| 110 | `CIDPRODU01` | `I` | Identificador único del catálogo Servicios para facturar ventas<br>con impuesto 15%. |
| 111 | `CIDPRODU02` | `I` | Identificador único del catálogo Servicios para facturar ventas<br>con impuesto 10%. |
| 112 | `CIDPRODU03` | `I` | Identificador único del catálogo Servicios para facturar ventas<br>con impuesto 0%. |
| 113 | `CIDPRODU04` | `I` | Identificador único del catálogo Servicios para facturar ventas<br>con impuesto Exento. |
| 114 | `CIDPRODU05` | `I` | Identificador único del catálogo Servicios para facturar ventas<br>con impuesto Otros %. |
| 115 | `CIDCONCE01` | `I` | Identificador único de conceptos de documentos para crear<br>documento de factura de las notas de venta. |
| 116 | `CIDCONCE02` | `I` | Identificador único de conceptos de documentos para crear<br>documento de devolución s/venta. |
| 117 | `CIDCLIEN02` | `I` | Identificador único del cliente para crear los documentos de<br>facturas y nota de crédito. |
| 118 | `CIDCONCE03` | `I` | Identificador único de conceptos de documentos para crear<br>movimiento de abono para saldar la factura. |
| 119 | `CIDCONCE04` | `I` | Identificador único de conceptos de documentos para crear el<br>movimiento de cargo por devolución s/venta. |
| 120 | `CPERANTFUT` | `I` | Indica si se podrá modificar periodos anteriores ó futuros.<br>0 = Periodos cerrados (solo el actual)<br>1 = Solo periodos anteriores y el actual<br>2 = Solo periodos futuros y el actual<br>3 = Periodos abiertos (todos) |
| 121 | `CVMOSTPEND` | `I` | Activa o desactiva la vista de existencias y costos. |
| 122 | `CMOVTEXEX1` | `V` | Indica el título del campo texto extra 1 en los movimientos. |
| 123 | `CMOVTEXEX2` | `V` | Indica el título del campo texto extra 2 en los movimientos. |
| 124 | `CMOVTEXEX3` | `V` | Indica el título del campo texto extra 3 en los movimientos. |
| 125 | `CMOVIMPEX1` | `V` | Indica el título del importe extra 1 en los movimientos. |
| 126 | `CMOVIMPEX2` | `V` | Indica el título del importe extra 2 en los movimientos. |
| 127 | `CMOVIMPEX3` | `V` | Indica el título del importe extra 3 en los movimientos. |
| 128 | `CMOVIMPEX4` | `V` | Indica el título del importe extra 4 en los movimientos. |
| 129 | `CMOVFECEX1` | `V` | Indica el título de la fecha extra en los movimientos. |
| 130 | `CVISTAAJ01` | `I` | Indica si muestra la vista ajuste al costo, al capturar documentos<br>de este tipo. Se configura en la pestaña Vistas de la<br>Configuración general.<br>0 = No<br>1 = Sí |
| 131 | `CESCFD` | `I` | Indica si la empresa manejará la emisión de Comprobantes<br>Fiscales Digitales (CFD).<br>0 = No<br>1 = Sí |
| 132 | `CTIEMPOCFD` | `I` | Indica el tiempo en minutos para volver a pedir la contraseña del<br>certificado CFD. |
| 133 | `CINTENTOS` | `I` | Indica el número de intentos para introducir la contraseña. |
| 134 | `CINTERFAZ` | `I` | Indica si manejará Interfaz con el Banco del Bajío.<br>0 = No<br>1 = Sí |
| 135 | `CCONTSIMUL` | `I` | Indica si habrá contabilización simultánea.<br>0 = No<br>1 = Sí |
| 136 | `CBANACTPLP` | `I` | Bandera para actualizar el precio de la lista de precios de<br>acuerdo a la unidad de captura del movimiento. |
| 137 | `CPOSFOLIO` | `I` | Este campo se inicializa con valor cero; el cual indica que el<br>control de serie / folio de los documentos de Punto de Venta se<br>lleva por almacén concepto. |
| 138 | `CPOSMODOIM` | `I` | Este campo se inicializa con valor cero; el cual indica que la<br>impresión de los documentos de Punto de Venta se realiza hasta<br>que son pagados. |
| 139 | `CCALCOSTO1` | `I` | Opción para calcular el costo del movimiento cuando en una<br>salida la existencia es cero o negativa.<br>0 = No se ha configurado opción<br>1 = Cero<br>2 = Promedio Unitario<br>3 = Último Costo |
| 140 | `CGENBITACS` | `I` | Opción para generar bitácoras, funciona del mismo modo que el<br>campo control de existencias.<br>1 = Afectación de inventarios<br>2 = Afectación de saldos<br>4 = Operación de documentos<br>8 = Operación de movimientos<br>16 = Operación de conversiones<br>32 = Relación cargos-abonos |
| 141 | `CSUGERIRRE` | `I` | Sugerir recosteo.<br>0 = Por omisión deshabilitado.<br>1 = Habilitado. |
| 142 | `CIDKEYEMP` | `I` | Sin uso. |
| 143 | `CALMACENAC` | `I` | Indica si hay un almacén global para concentrar las existencias<br>de la empresa.<br>0 = Deshabilitado<br>1 = Habilitado |
| 144 | `CVERPOSI` | `V` | Versión de CONTPAQi® PUNTO DE VENTA. |
| 145 | `CIDSUCURSA` | `I` | Identificador de la Sucursal actual. |
| 146 | `CPERFIL` | `I` | Sin uso. |
| 147 | `CAUTORIZAR` | `I` | Sin uso. |
| 148 | `CMOSTRARDOCTOS` | `I` | Configuración para mostrar los documentos a partir de cierta<br>fecha. |
| 149 | `CBITACORA0` | `I` | Uso exclusivo para bitácoras |
| 150 | `CBITACORA1` | `I` | Uso exclusivo para bitácoras |
| 151 | `CBITACORA2` | `I` | Uso exclusivo para bitácoras |
| 152 | `CBITACORA3` | `I` | Uso exclusivo para bitácoras |
| 153 | `CBITACORA4` | `I` | Uso exclusivo para bitácoras |
| 154 | `CBITACORA5` | `I` | Uso exclusivo para bitácoras |
| 155 | `CBITACORA6` | `I` | Uso exclusivo para bitácoras |
| 156 | `CBITACORA7` | `I` | Uso exclusivo para bitácoras |
| 157 | `CCOSTOMEN` | `I` | No disponible. |
| 158 | `CSEGCIVA15` | `V` | Segmento contable de la cuenta de IVA a tasa 15 para clientes.<br>Varchar: 50 caracteres. |
| 159 | `CSEGCIVA10` | `V` | Segmento contable de la cuenta de IVA a tasa 10 para clientes.<br>Varchar: 50 caracteres. |
| 160 | `CSEGCIVAOT` | `V` | Segmento contable de la cuenta de otras tasas de IVA para<br>clientes.<br>Varchar: 50 caracteres. |
| 161 | `CSEGCIVA16` | `V` | Segmento contable de la cuenta de IVA a tasa 16 para clientes.<br>Varchar: 50 caracteres. |
| 162 | `CSEGCIVA11` | `V` | Segmento contable de la cuenta de IVA a tasa 11 para clientes.<br>Varchar: 50 caracteres. |
| 163 | `CSEGPIVA15` | `V` | Segmento contable de la cuenta de IVA a tasa 15 para<br>proveedores.<br>Varchar: 50 caracteres. |
| 164 | `CSEGPIVA10` | `V` | Segmento contable de la cuenta de IVA a tasa 05 para<br>proveedores.<br>Varchar: 50 caracteres. |
| 165 | `CSEGPIVAOT` | `V` | Segmento contable de la cuenta de otras tasas de IVA para<br>proveedores.<br>Varchar: 50 caracteres. |
| 166 | `CSEGPIVA16` | `V` | Segmento contable de la cuenta de IVA a tasa 16 para<br>proveedores.<br>Varchar: 50 caracteres. |
| 167 | `CSEGPIVA11` | `V` | Segmento contable de la cuenta de IVA a tasa 11 para<br>proveedores.<br>Varchar: 50 caracteres. |
| 168 | `CGENAJ2010` | `I` | Para el manejo de Ajuste del IVA por Reforma Fiscal 2010. |
| 169 | `CFECAJ2010` | `D` | Fecha a partir de la cual se aplica ajuste de IVA por Reforma<br>Fiscal 2010. |
| 170 | `CAJ2010ORI` | `I` | Generar ajuste de IVA con el mismo concepto del cargo. |
| 171 | `CHOST` | `V` | Servidor de correo POP3.<br>Varchar: 60 caracteres. |
| 172 | `VTIPESTCAL` | `I` | Dato para el manejo del tipo de estructura del calendario del<br>almacén digital. |
| 173 | `CCFDIMPU01` | `I` | Impuesto 1 para el Comprobante fiscal digital. |
| 174 | `CCFDIMPU02` | `I` | Impuesto 2 para el Comprobante fiscal digital. |
| 175 | `CCFDIMPU03` | `I` | Impuesto 3 para el Comprobante fiscal digital. |
| 176 | `CCFDIMPU04` | `I` | Impuesto 4 para el Comprobante fiscal digital. |
| 177 | `CCFDIMPU05` | `I` | Impuesto 5 para el Comprobante fiscal digital. |
| 178 | `CRUTAPLA01` | `V` | Ruta de la plantilla que será utilizada para visualizar CFD.<br>Varchar: 253 caracteres. |
| 179 | `CRUTAPLA02` | `V` | Ruta de la plantilla que será utilizada para visualizar CFDi.<br>Varchar: 253 caracteres. |
| 180 | `CFECDONAT` | `D` | Fecha del oficio en el que se le autoriza a una empresa<br>(asociación civil o fideicomiso) para recibir donativos. |
| 181 | `CNUMDONAT` | `V` | Número del oficio en el que se le autoriza a una empresa<br>(asociación civil o fideicomiso) para recibir donativos.<br>Varchar: 30 caracteres. |
| 182 | `CHOSTPROXY` | `V` | Dirección del host Proxy.<br>Varchar: 253 caracteres. |
| 183 | `CPTOPROXY` | `I` | Puerto Proxy. |
| 184 | `CUSRPROXY` | `V` | Usuario Proxy.<br>Varchar: 60 caracteres. |
| 185 | `CHOSTSMTP` | `V` | Dirección del correo saliente (SMTP).<br>Varchar: 60 caracteres. |
| 186 | `CPTOPOP` | `I` | Puerto para el correo de entrada (POP3). |
| 187 | `CPTOSMTP` | `I` | Puerto para el correo saliente (SMTP). |
| 188 | `CCNXSEGPOP` | `I` | Si se maneja o no conexión segura (SSL: protocolo de capa de<br>conexión segura). |
| 189 | `CRUTAENTREGA` | `V` | Ruta de entrega por omisión para la empresa.<br>Varchar: 253 caracteres. |
| 190 | `CPREFIRFC` | `I` | Uso de RFC del cliente como prefijo para el nombre en la<br>entrega de documentos.<br>0 = No se usa<br>1 = Sí se usa |
| 191 | `CVALIDACFD` | `V` | Para configurar si se validará CFD en el almacén digital.<br>Varchar: 20 caracteres. |
| 192 | `CREGIMFISC` | `V` | Régimen por omisión en el que tributa el contribuyente emisora<br>nivel congiguración general.<br>Varchar: 253 caracteres. |
| 193 | `CAUTRVOE` | `V` | Autorización o reconocimiento de validez oficial de estudios en<br>los términos de la Ley General de Educación.<br>Varchar: 30 caracteres. |
| 194 | `CLEYENDON1` | `V` | Leyenda para donatarias 1.<br>Varchar: 253 caracteres. |
| 195 | `CLEYENDON2` | `V` | Leyenda para donatarias 2.<br>Varchar: 253 caracteres. |
| 196 | `CASUNTO` | `V` | Asunto para la personalización del correo electrónico.<br>Varchar: 253 caracteres. |
| 197 | `CCUERPO` | `T` | Cuerpo para la personalización del correo electrónico. |
| 198 | `CFIRMA` | `V` | Firma para la personalización del correo electrónico.<br>Varchar: 253 caracteres. |
| 199 | `CADJUNTO1` | `V` | Archivo adjunto 1 para la personalización del correo electrónico.<br>Varchar: 253 caracteres. |
| 200 | `CADJUNTO2` | `V` | Archivo adjunto 2 para la personalización del correo electrónico.<br>Varchar: 253 caracteres. |
| 201 | `CCORREOPRU` | `V` | Uso interno. |
| 202 | `CGUIDDSL` | `V` | Identificador global único de la empresa en el Administrador de<br>Documentos Digitales (ADD).<br>Varchar: 40 caracteres. |
| 203 | `CGUIDEMPRESA` | `V` | Identificador global único de la empresa en CONTPAQi®<br>Comercial.<br>Varchar: 40 caracteres. |
| 204 | `CTOKENCN` | `T` | Información encriptada del token de acceso a la sesión<br>con CONTPAQi® Contabiliza. |
| 205 | `CREFRESHTOKENCN` | `T` | Información encriptada del token para renovar el token de<br>acceso. |
| 206 | `CLEYENDON` | `T` | Leyenda para donatarias |

---

### `admPreciosCompra` — Tabla de Lista de precios de compra

> Esta tabla contiene las listas de precio de compra por proveedor.

> 6  CCODIGOPRODUCTO  PROVEEDOR

> V  Código del producto según el proveedor.  Varchar:  20 caracteres.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `CIDPRODUCTO` | `I` | Identificador del producto o del detalle de la característica. |
| 3 | `CIDPROVEEDOR` | `I` | Identificador del proveedor. |
| 4 | `CPRECIOCOMPRA` | `F` | Precio de compra del producto o del detalle de la característica. |
| 5 | `CIDMONEDA` | `I` | Identificador de la moneda utilizada en el precio. |
| 6 | `CCODIGOPRODUCTO
PROVEEDOR` | `V` | Código del producto según el proveedor.<br>Varchar: 20 caracteres.<br>Nota: Es el mostrado en las cotizaciones y listas de venta del<br>proveedor. |
| 7 | `CIDUNIDAD` | `I` | Identificador de la unidad de medida y peso que corresponde al<br>precio de compra. |
| 8 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admPrepolizas` — Tabla de Prepólizas

> Esta tabla almacena las prepólizas (pólizas creadas al ejecutar la Elaboración de pólizas).

> Ejemplo: 1999, 2000, 2001.

> 1 = ContPAQ® Windows  2 = MegaPAQ  5 = CheqPAQ® Windows  6 = NomiPAQ®  Windows / CONTPAQi®NÓMINAS      7 =

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPREPOLIZA` | `I` | Identificador de la prepóliza. |
| 2 | `CESTADOCONTABLE` | `I` | Estado de la prepóliza:<br>1 = No contabilizado.<br>2 = Prepóliza por documento.<br>3 = Prepóliza Diaria.<br>4 = Prepóliza Periodo.<br>5 = Póliza en CONTPAQi® Contabilidad.<br>6 = Póliza Diaria en CONTPAQi® Contabilidad.<br>7 = Póliza Periodo en CONTPAQi® Contabilidad.<br>8 = Póliza modificada en CONTPAQi® Contabilidad. |
| 3 | `EJE` | `I` | Número de ejercicio de la póliza.<br>Ejemplo: 1999, 2000, 2001. |
| 4 | `PERIODO` | `I` | Número de periodo de la póliza. |
| 5 | `TIPOPOL` | `I` | Tipo de póliza:<br>1 = Ingresos<br>2 = Egresos<br>3 = Diario<br>4 = Orden<br>5 = Estadística |
| 6 | `NUMPOL` | `I` | Número de la póliza. |
| 7 | `CLASE` | `I` | Clase de la póliza.<br>0 = No afectable<br>1 = Afectable |
| 8 | `IMPRESA` | `I` | Póliza impresa:<br>1 = Impresa<br>0 = No impresa |
| 9 | `CONCEPTO` | `V` | Concepto de la póliza.<br>Varchar: 50 caracteres. |
| 10 | `FECHA` | `D` | Fecha de la póliza. |
| 11 | `CARGOS` | `F` | Total de cargos de la póliza. |
| 12 | `ABONOS` | `F` | Total de abonos de la póliza. |
| 13 | `DIARIO` | `V` | Número de diario especial.<br>Varchar: 10 caracteres. |
| 14 | `SISTORIG` | `I` | Sistema donde se originó el documento.<br>1 = ContPAQ® Windows<br>2 = MegaPAQ<br>5 = CheqPAQ® Windows<br>6 = NomiPAQ® Windows / CONTPAQi®NÓMINAS<br>7 =<br>8 = Exión<br>9 = Póliza Costo de lo Vendido<br>10 = ContPAQ Windows (Reexpresión)<br>11 = CONTPAQi® Contabilidad<br>101 = CONTPAQi® Punto de venta<br>201 = CONTPAQi® Bancos<br>202 = CONTPAQi® Factura electrónica |
| 15 | `CHORA` | `V` | Hora de la contabilización simultánea.<br>Varchar: 8 caracteres.<br>Nota: Este campo debe ser igual a la hora de emisión del CFD. |
| 16 | `CGUIDPOLIZA` | `V` | Identificador único de la póliza.<br>Varchar: 40 caracteres. |

**Llaves e Índices:**
- `Indice: CIDTRANSACCION`
- `Llave:   cIdTransaccion`

---

### `admProductos` — Tabla de Productos

> Esta tabla contiene los campos para los productos, servicios y paquetes.

> 11  CCOMVENTAEXCEP  PRODUCTO

> F  Comisión de venta por excepción del producto.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPRODUCTO` | `I` | Identificador del producto. |
| 2 | `CCODIGOPRODUCTO` | `V` | Código del producto.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBREPRODUCTO` | `V` | Nombre del producto.<br>Varchar: 60 caracteres. |
| 4 | `CTIPOPRODUCTO` | `I` | Tipo de producto:<br>1 = Producto<br>2 = Paquete<br>3 = Servicio |
| 5 | `CFECHAALTAPRODUCTO` | `D` | Fecha de alta del producto. |
| 6 | `CCONTROLEXISTENCIA` | `I` | Control de existencias mediante:<br>00000001 = Unidades<br>00000010 = Características<br>00000100 = Series<br>00001000 = Pedimentos<br>00010000 = Lotes |
| 7 | `CIDFOTOPRODUCTO` | `I` | Foto del producto. |
| 8 | `CDESCRIPCIONPRODUCTO` | `T` | Descripción detallada del producto. |
| 9 | `CMETODOCOSTEO` | `I` | Método de costeo del producto:<br>1 = Costo Promedio en Base a Entradas<br>2 = Costo Promedio en Base a Entradas Almacén<br>3 = Último Costo<br>4 = UEPS<br>5 = PEPS<br>6 = Costo Específico<br>7 = Costo Estándar |
| 10 | `CPESOPRODUCTO` | `F` | Peso de producto. |
| 11 | `CCOMVENTAEXCEP
PRODUCTO` | `F` | Comisión de venta por excepción del producto. |
| 12 | `CCOMCOBROEXCEP
PRODUCTO` | `F` | Comisión de cobro por excepción del producto. |
| 13 | `CCOSTOESTANDAR` | `F` | Valor del costo estándar del producto. |
| 14 | `CMARGENUTILIDAD` | `F` | Margen de utilidad. |
| 15 | `CSTATUSPRODUCTO` | `I` | Estatus actual del producto:<br>0 = Baja lógica<br>1 = Alta |
| 16 | `CIDUNIDADBASE` | `I` | Identificador de la unidad base del producto. |
| 17 | `CIDUNIDADNOCONVERTIBLE` | `I` | Identificador de la unidad no convertible del producto. |
| 18 | `CFECHABAJA` | `D` | Fecha en que el producto quedó inactivo. |
| 19 | `CIMPUESTO1` | `F` | Porcentaje de impuesto 1 del producto. |
| 20 | `CIMPUESTO2` | `F` | Porcentaje de impuesto 2 del producto. |
| 21 | `CIMPUESTO3` | `F` | Porcentaje de impuesto 3 del producto. |
| 22 | `CRETENCION1` | `F` | Porcentaje de la retención 1 del producto. |
| 23 | `CRETENCION2` | `F` | Porcentaje de la retención 2 del producto. |
| 24 | `CIDPADRECARACTERISTICA1` | `I` | Identificador del padre 1 de características que usa el producto. |
| 25 | `CIDPADRECARACTERISTICA2` | `I` | Identificador del padre 2 de características que usa el producto. |
| 26 | `CIDPADRECARACTERISTICA3` | `I` | Identificador del padre 3 de características que usa el producto. |
| 27 | `CIDVALORCLASIFICACION1` | `I` | Identificador de la clasificación 1 del producto. |
| 28 | `CIDVALORCLASIFICACION2` | `I` | Identificador de la clasificación 2 del producto. |
| 29 | `CIDVALORCLASIFICACION3` | `I` | Identificador de la clasificación 3 del producto. |
| 30 | `CIDVALORCLASIFICACION4` | `I` | Identificador de la clasificación 4 del producto. |
| 31 | `CIDVALORCLASIFICACION5` | `I` | Identificador de la clasificación 5 del producto. |
| 32 | `CIDVALORCLASIFICACION6` | `I` | Identificador de la clasificación 6 del producto. |
| 33 | `CSEGCONTPRODUCTO1` | `V` | 1er. segmento contable del producto.<br>Varchar: 50 caracteres. |
| 34 | `CSEGCONTPRODUCTO2` | `V` | 2do. segmento contable del producto.<br>Varchar: 50 caracteres. |
| 35 | `CSEGCONTPRODUCTO3` | `V` | 3er. segmento contable del producto.<br>Varchar: 50 caracteres. |
| 36 | `CTEXTOEXTRA1` | `V` | Texto extra 1.<br>Varchar: 50 caracteres. |
| 37 | `CTEXTOEXTRA2` | `V` | Texto extra 2.<br>Varchar: 50 caracteres. |
| 38 | `CTEXTOEXTRA3` | `V` | Texto extra 3.<br>Varchar: 50 caracteres. |
| 39 | `CFECHAEXTRA` | `D` | Fecha extra. |
| 40 | `CIMPORTEEXTRA1` | `F` | Importe extra 1. |
| 41 | `CIMPORTEEXTRA2` | `F` | Importe extra 2. |
| 42 | `CIMPORTEEXTRA3` | `F` | Importe extra 3. |
| 43 | `CIMPORTEEXTRA4` | `F` | Importe extra 4. |
| 44 | `CPRECIO1` | `F` | Precio 1 del producto. |
| 45 | `CPRECIO2` | `F` | Precio 2 del producto. |
| 46 | `CPRECIO3` | `F` | Precio 3 del producto. |
| 47 | `CPRECIO4` | `F` | Precio 4 del producto. |
| 48 | `CPRECIO5` | `F` | Precio 5 del producto. |
| 49 | `CPRECIO6` | `F` | Precio 6 del producto. |
| 50 | `CPRECIO7` | `F` | Precio 7 del producto. |
| 51 | `CPRECIO8` | `F` | Precio 8 del producto. |
| 52 | `CPRECIO9` | `F` | Precio 9 del producto. |
| 53 | `CPRECIO10` | `F` | Precio 10 del producto. |
| 54 | `CBANUNIDADES` | `I` | Bandera de unidades. |
| 55 | `CBANCARACTERISTICAS` | `I` | Bandera de características. |
| 56 | `CBANMETODOCOSTEO` | `I` | Bandera de método de costeo. |
| 57 | `CBANMAXMIN` | `I` | Bandera de máximos y mínimos. |
| 58 | `CBANPRECIO` | `I` | Bandera de precio. |
| 59 | `CBANIMPUESTO` | `I` | Bandera de impuestos. |
| 60 | `CBANCODIGOBARRA` | `I` | Bandera de código de barras. |
| 61 | `CBANCOMPONENTE` | `I` | Bandera de componentes. |
| 62 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 63 | `CERRORCOSTO` | `I` | Indica si hay un error de costeo en el producto.<br>0 = Sin error<br>1 = Captura en desorden de entradas<br>2 = Captura en desorden de salidas<br>3 = Existencias negativas<br>4 = Series huérfanas<br>5 = Series sin costo |
| 64 | `CFECHAERRORCOSTO` | `D` | Indica la fecha de error de costeo. |
| 65 | `CPRECIOCALCULADO` | `F` | Sólo se utiliza en la Actualización de listas de precios. |
| 66 | `CESTADOPRECIO` | `I` | Sólo se utiliza en la Actualización de listas de precios. |
| 67 | `CBANUBICACION` | `I` | Indica si fueron capturadas las ubicaciones del producto. |
| 68 | `CESEXENTO` | `I` | Indica si el producto es exento de IVA.<br>0 = No exento<br>1 = Exento |
| 69 | `CEXISTENCIANEGATIVA` | `I` | Indica si el producto ha tenido existencia negativa.<br>0 = No<br>1 = Sí |
| 70 | `CCOSTOEXT1` | `F` | Costo capturado extra 1.<br>Usado por el Módulo de Producción. |
| 71 | `CCOSTOEXT2` | `F` | Costo capturado extra 2.<br>Usado por el Módulo de Producción. |
| 72 | `CCOSTOEXT3` | `F` | Costo capturado extra 3.<br>Usado por el Módulo de Producción. |
| 73 | `CCOSTOEXT4` | `F` | Costo capturado extra 4.<br>Usado por el Módulo de Producción. |
| 74 | `CCOSTOEXT5` | `F` | Costo capturado extra 5.<br>Usado por el Módulo de Producción. |
| 75 | `CFECCOSEX1` | `D` | Fecha del costo capturado extra 1.<br>Usado por el Módulo de Producción. |
| 76 | `CFECCOSEX2` | `D` | Fecha del costo capturado extra 2.<br>Usado por el Módulo de Producción. |
| 77 | `CFECCOSEX3` | `D` | Fecha del costo capturado extra 3.<br>Usado por el Módulo de Producción. |
| 78 | `CFECCOSEX4` | `D` | Fecha del costo capturado extra 4.<br>Usado por el Módulo de Producción. |
| 79 | `CFECCOSEX5` | `D` | Fecha del costo capturado extra 5.<br>Usado por el Módulo de Producción. |
| 80 | `CMONCOSEX1` | `I` | Moneda del costo capturado extra 1.<br>Usado por el Módulo de Producción. |
| 81 | `CMONCOSEX2` | `I` | Moneda del costo capturado extra 2.<br>Usado por el Módulo de Producción. |
| 82 | `CMONCOSEX3` | `I` | Moneda del costo capturado extra 3.<br>Usado por el Módulo de Producción. |
| 83 | `CMONCOSEX4` | `I` | Moneda del costo capturado extra 4.<br>Usado por el Módulo de Producción. |
| 84 | `CMONCOSEX5` | `I` | Moneda del costo capturado extra 5.<br>Usado por el Módulo de Producción. |
| 85 | `CBancosEX` | `I` | Indica si fueron capturados costos extra del producto. |
| 86 | `CESCUOTAI2` | `I` | Indica si el valor del impuesto 2 se considera como importe o<br>como porcentaje.<br>0 = Se considera como porcentaje.<br>1 = Se considera como cuota fija. |
| 87 | `CESCUOTAI3` | `I` | Indica si el valor del impuesto 3 se considera como importe o<br>como porcentaje.<br>0 = Se considera como porcentaje.<br>1 = Se considera como cuota fija. |
| 88 | `CIDUNICOMPRA` | `I` | Indica la unidad que será asumida en los documentos de compra<br>para los productos con Control de existencia por unidades.. |
| 89 | `CIDUNIVENTA` | `I` | Indica la unidad que será asumida en los documentos de venta<br>para los productos con Control de existencia por unidades.. |
| 90 | `CSUBTIPO` | `I` | Sub clasificación para los productos:<br>1 = Pago de servicio<br>2 = Paquete inventariable (Nuevo)<br>9 = Servicio de facturación de CONTPAQi® PUNTO DE VENTA. |
| 91 | `CCODALTERN` | `V` | Código alterno del producto.<br>Varchar: 30 caracteres. |
| 92 | `CNOMALTERN` | `V` | Nombre alterno del producto.<br>Varchar: 60 caracteres. |
| 93 | `CDESCCORTA` | `V` | Descripción corta del producto.<br>Varchar: 30 caracteres. |
| 94 | `CIDMONEDA` | `I` | Moneda asociada al producto. |
| 95 | `CUSABASCU` | `I` | Indica si el producto requiere ser pesado.<br>0 = No<br>1 = Sí |
| 96 | `CTIPOPAQUE` | `I` | No disponible. |
| 97 | `CPRECSELEC` | `I` | No disponible. |
| 98 | `CDESGLOSAI2` | `I` | Desglosar IEPS en CFD.<br>0 = No<br>1 = Sí |
| 99 | `CSEGCONTPRODUCTO4` | `V` | 4o. Segmento contable del producto.<br>Varchar: 20 caracteres. |
| 100 | `CSEGCONTPRODUCTO5` | `V` | 5o. Segmento contable del producto.<br>Varchar: 20 caracteres. |
| 101 | `CSEGCONTPRODUCTO6` | `V` | 6o. Segmento contable del producto.<br>Varchar: 20 caracteres. |
| 102 | `CSEGCONTPRODUCTO7` | `V` | 7o. Segmento contable del producto.<br>Varchar: 20 caracteres. |
| 103 | `CCTAPRED` | `V` | Número de cuenta predial del inmueble utilizado en recibos de<br>arrendamiento.<br>Varchar: 150 caracteres. |
| 104 | `CNODESCOMP` | `I` | Si el producto es un paquete desglosa sus componentes en el<br>XML:<br>0 = Sí los desglosa<br>1 = No los desglosa |
| 105 | `CIDUNIXML` | `I` | Identificador de la unidad dentro del XML. |
| 106 | `CCLAVESAT` | `V` | Clave SAT para identificar al producto o servicio<br>Varchar: 3 caracteres. |
| 107 | `CCANTIDADFISCAL` | `F` | Proporción de la base para el cálculo de impuesto al utilizar cuota<br>fija de IEPS |

---

### `admProductosDetalles` — Tabla de Identificador de productos

> Esta tabla contiene los campos identificadores para productos y detalles.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPRODUCTO` | `I` | Identificador del producto. |
| 2 | `CTIPOPRODUCTO` | `I` | Tipo de producto:<br>0 = Producto.<br>1 = Detalle (Producto con características). |
| 3 | `CIDPRODUCTOPADRE` | `I` | Si es un detalle, este campo indica el producto del que surgen los<br>detalles. |
| 4 | `CIDVALORCARACTERISTICA1` | `I` | Valor de la característica 1. |
| 5 | `CIDVALORCARACTERISTICA2` | `I` | Valor de la característica 2. |
| 6 | `CIDVALORCARACTERISTICA3` | `I` | Valor de la característica 3. |
| 7 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admProductosFotos` — Tabla de Imágenes de productos

> Esta tabla almacena la información de las imágenes de productos.

> V  Nombre de la imagen.  Varchar:  40 caracteres.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDFOTOPRODUCTO` | `I` | Identificador de la imagen. |
| 2 | `CNOMBREFOTOPRODUCTO` | `V` | Nombre de la imagen.<br>Varchar: 40 caracteres. |
| 3 | `CFOTOPRODUCTO` | `T` | Imagen. |

---

### `admPromociones` — Tabla de Promociones

> Esta tabla contiene los campos del catálogo Promociones.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPROMOCION` | `I` | Identificador de la promoción. |
| 2 | `CCODIGOPROMOCION` | `V` | Código de la promoción.<br>Varchar: 30 caracteres. |
| 3 | `CNOMBREPROMOCION` | `V` | Nombre de la promoción.<br>Varchar: 60 caracteres. |
| 4 | `CFECHAINICIO` | `D` | Fecha de inicio de vigencia. |
| 5 | `CFECHAFIN` | `D` | Fecha de fin de vigencia. |
| 6 | `CVOLUMENMINIMO` | `F` | Límite mínimo de unidades a los que se puede aplicar la<br>promoción. |
| 7 | `CVOLUMENMAXIMO` | `F` | Límite máximo de unidades a los que se puede aplicar la<br>promoción. |
| 8 | `CPORCENTAJEDESCUENTO` | `F` | Porcentaje de descuento. |
| 9 | `CIDVALORCLASIFCLIENTE1` | `I` | Identificador de la clasificación 1 del cliente. |
| 10 | `CIDVALORCLASIFCLIENTE2` | `I` | Identificador de la clasificación 2 del cliente. |
| 11 | `CIDVALORCLASIFCLIENTE3` | `I` | Identificador de la clasificación 3 del cliente. |
| 12 | `CIDVALORCLASIFCLIENTE4` | `I` | Identificador de la clasificación 4 del cliente. |
| 13 | `CIDVALORCLASIFCLIENTE5` | `I` | Identificador de la clasificación 5 del cliente. |
| 14 | `CIDVALORCLASIFCLIENTE6` | `I` | Identificador de la clasificación 6 del cliente. |
| 15 | `CIDVALORCLASIFPRODUCTO1` | `I` | Identificador de la clasificación 1 del producto. |
| 16 | `CIDVALORCLASIFPRODUCTO2` | `I` | Identificador de la clasificación 2 del producto. |
| 17 | `CIDVALORCLASIFPRODUCTO3` | `I` | Identificador de la clasificación 3 del producto. |
| 18 | `CIDVALORCLASIFPRODUCTO4` | `I` | Identificador de la clasificación 4 del producto. |
| 19 | `CIDVALORCLASIFPRODUCTO5` | `I` | Identificador de la clasificación 5 del producto. |
| 20 | `CIDVALORCLASIFPRODUCTO6` | `I` | Identificador de la clasificación 6 del producto. |
| 21 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |
| 22 | `CTIPOPROMO` | `I` | Tipo de promoción. |
| 23 | `CIDCPTODOC` | `I` | Identificador del concepto de documento. |
| 24 | `CSUBTIPO` | `I` | Subtipo de promoción. |
| 25 | `CHORAINI` | `V` | Hora de inicio de la promoción.<br>Varchar: 4 caracteres. |
| 26 | `CHORAFIN` | `V` | Hora fin de la promoción.<br>Varchar: 4 caracteres. |
| 27 | `CTIPOPRO` | `I` | Reservado. |
| 28 | `CVALA` | `I` | Reservado. |
| 29 | `CVALB` | `I` | Reservado. |
| 30 | `CDIAS` | `I` | Días que aplican la promoción. |
| 31 | `CFECHAALTA` | `D` | Fecha en la cual se creo la promoción. |
| 32 | `CSTATUS` | `I` | Estatus de la promoción.<br>1 = Activa<br>0 = Inactiva |

---

### `admTiposCambio` — Tabla de tipos de cambio

> Esta tabla almacena los tipos de cambio diarios en los documentos registrados.

> Nota: Este Importe siempre se expresa en la moneda base, que  siempre tiene tipo de cambio 1.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDTIPOCAMBIO` | `I` | Identificador del tipo de cambio. |
| 2 | `CIDMONEDA` | `I` | Identificador de la moneda dueña del tipo de cambio. |
| 3 | `CFECHA` | `D` | Fecha del tipo de cambio. |
| 4 | `CIMPORTE` | `F` | Tipo de cambio de la moneda a la fecha.<br>Nota: Este Importe siempre se expresa en la moneda base, que<br>siempre tiene tipo de cambio 1. |
| 5 | `CTIMESTAMP` | `V` | Concurrencia.<br>Varchar: 23 caracteres. |

---

### `admUnidadesMedidaPeso` — Tabla de Unidades de medida y

> Esta tabla contiene los campos de las unidades de peso y medida de los servicios, productos y/o  paquetes.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDUNIDAD` | `I` | Identificador de la unidad. |
| 2 | `CNOMBREUNIDAD` | `V` | Nombre de la unidad.<br>Varchar: 60 caracteres. |
| 3 | `CABREVIATURA` | `V` | Abreviatura de la unidad.<br>Varchar: 3 caracteres. |
| 4 | `CDESPLIEGUE` | `V` | Dato de despliegue en reportes.<br>Varchar: 3 caracteres. |
| 5 | `CCLAVEINT` | `V` | Clave SAT de acuerdo al Anexo 20 3.3<br>Varchar: 3 caracteres. |
| 6 | `CCLAVESAT` | `V` | Clave SAT utilizada por el Complemento de Comercio Exterior<br>Varchar: 3 caracteres |

---

### `admCuentasBancarias` — Tabla de Cuentas bancarias

> Esta tabla contiene los campos de las cuentas bancarias

> Estatus actual de la cuenta bancaria.  0=Inactivo  1=Activo

> Define el tipo del catálogo asociado:  1 = Clientes  4 = Empresa

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDCUENTA` | `I` | Identificador de la cuenta bancaria |
| 2 | `CACCOUNTID` | `V` | Reservado<br>Varchar: 26 caracteres |
| 3 | `CNUMEROCUENTA` | `V` | Número de la cuenta bancaria<br>Varchar: 30 caracteres |
| 4 | `CNOMBRECUENTA` | `V` | Nombre de la cuenta bancaria<br>Varchar: 60 caracteres |
| 5 | `CFECHAALTA` | `D` | Fecha de alta de la cuenta bancaria. |
| 6 | `CFECHABAJA` | `D` | Fecha de baja de la cuenta bancaria. |
| 7 | `CESTATUS` | `I` | Estatus actual de la cuenta bancaria.<br>0=Inactivo<br>1=Activo |
| 8 | `CCLABE` | `V` | CLABE interbancaria<br>Varchar: 20 caracteres |
| 9 | `CCLAVE` | `V` | Clave del banco<br>Varchar: 3 caracteres |
| 10 | `CSEGCONT01` | `V` | Segmento 1 de la cuenta contable de la cuenta bancaria<br>Varchar:50 caracteres |
| 11 | `CSEGCONT02` | `V` | Segmento 2 de la cuenta contable de la cuenta bancaria<br>Varchar:50 caracteres |
| 12 | `CSEGCONT03` | `V` | Segmento 3 de la cuenta contable de la cuenta bancaria<br>Varchar:50 caracteres |
| 13 | `CTEXTOEXTRA1` | `V` | Texto extra 1 para interfaz configurable<br>Varchar:50 caracteres |
| 14 | `CTEXTOEXTRA2` | `V` | Texto extra 2 para interfaz configurable<br>Varchar:50 caracteres |
| 15 | `CTEXTOEXTRA3` | `V` | Texto extra 3 para interfaz configurable<br>Varchar:50 caracteres |
| 16 | `CFECHAEXTRA` | `D` | Fecha extra para interfaz configurable |
| 17 | `CIMPORTEEXTRA1` | `F` | Importe extra 1 para interfaz configurable |
| 18 | `CIMPORTEEXTRA2` | `F` | Importe extra 2 para interfaz configurable |
| 19 | `CIMPORTEEXTRA3` | `F` | Importe extra 3 para interfaz configurable |
| 20 | `CIMPORTEEXTRA4` | `F` | Importe extra 4 para interfaz configurable |
| 21 | `CTIMESTAMP` | `V` | Concurrencia<br>Varchar:23 caracteres |
| 22 | `CIDMONEDA` | `I` | Identificador de la moneda utilizada por la cuenta bancaria |
| 23 | `CIDCATALOGO` | `I` | Identificador del catálogo. |
| 24 | `CTIPOCATALOGO` | `I` | Define el tipo del catálogo asociado:<br>1 = Clientes<br>4 = Empresa |
| 25 | `CNOMBANEXT` | `V` | Nombre del banco cuando se trata de un banco extranjero<br>Varchar:254 caracteres |
| 26 | `CRFCBANCO` | `V` | RFC del banco<br>Varchar:20 caracteres |

---

### `admMovtosCEPs` — Movimientos de CEPs

> admMovtosCEPs – Movimientos de CEPs

> Define el estado del CEP:   0=Recibido pendiente  1=Emitido pendiente  5=Recibido aplicado  6=Emitido aplicado

> Indica el nodo TipoCuenta del Ordenante del CEP  Varchar: 2 caracteres

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDMOVTOCEP` | `I` | Identificador del movimiento de CEP |
| 2 | `CIDDOCUMENTO` | `I` | Identificador del documento al que está relacionado |
| 3 | `CFECHA` | `D` | Indica el nodo FechaOperacion del CEP |
| 4 | `CHORA` | `V` | Indica el nodo Hora del CEP<br>Varchar: 8 caracteres |
| 5 | `CCLAVE` | `V` | Indica el nodo ClaveSPEI del CEP<br>Varchar:12 caracteres |
| 6 | `CSELLO` | `V` | Indica el nodo sello del CEP<br>Varchar: 254 caracteres |
| 7 | `CCERTIFICADO` | `V` | Indica el nodo numeroCertificado del CEP<br>Varchar: 20 caracteres |
| 8 | `CCADENA` | `M` | Indica el nodo SPEI_Tercero cadenaCDA del CEP |
| 9 | `CESTADO` | `I` | Define el estado del CEP:<br>0=Recibido pendiente<br>1=Emitido pendiente<br>5=Recibido aplicado<br>6=Emitido aplicado |
| 10 | `CCONCEPTO` | `V` | Indica el nodo Concepto del CEP<br>Varchar: 60 caracteres |
| 11 | `CIVA` | `F` | Indica el nodo IVA del CEP |
| 12 | `CIMPORTE` | `F` | Indica el nodo MontoPago del CEP |
| 13 | `CRBANCO` | `V` | Indica el nodo BancoReceptor del CEP<br>Varchar: 60 caracteres |
| 14 | `CRNOMBRE` | `V` | Indica el nodo Nombre del Beneficiario del CEP<br>Varchar: 60 caracteres |
| 15 | `CRRFC` | `V` | Indica el nodo RFC del Beneficiario del CEP<br>Varchar: 20 caracteres |
| 16 | `CRCUENTA` | `V` | Indica el nodo Cuenta del Beneficiario del CEP<br>Varchar: 20 caracteres |
| 17 | `CRTIPOCTA` | `V` | Indica el nodo TipoCuenta del Beneficiario del CEP<br>Varchar: 2 caracteres |
| 18 | `CEBANCO` | `V` | Indica el nodo BancoEmisor del CEP<br>Varchar: 60 caracteres |
| 19 | `CENOMBRE` | `V` | Indica el nodo Nombre del Ordenante del CEP<br>Varchar: 60 caracteres |
| 20 | `CERFC` | `V` | Indica el nodo RFC del Ordenante del CEP<br>Varchar: 20 caracteres |
| 21 | `CECUENTA` | `V` | Indica el nodo Cuenta del Ordenante del CEP<br>Varchar: 20 caracteres |
| 22 | `CETIPOCTA` | `V` | Indica el nodo TipoCuenta del Ordenante del CEP<br>Varchar: 2 caracteres |
| 23 | `CTEXTOEXTRA1` | `V` | Texto extra 1 para interfaz configurable<br>Varchar: 50 caracteres |
| 24 | `CTEXTOEXTRA2` | `V` | Texto extra 2 para interfaz configurable<br>Varchar: 50 caracteres |
| 25 | `CTEXTOEXTRA3` | `V` | Texto extra 3 para interfaz configurable<br>Varchar: 50 caracteres |
| 26 | `CFECHAEXTRA` | `D` | Fecha extra para interfaz configurable |
| 27 | `CIMPORTEEXTRA1` | `F` | Importe extra 1 para interfaz configurable |
| 28 | `CIMPORTEEXTRA2` | `F` | Importe extra 2 para interfaz configurable |
| 29 | `CIMPORTEEXTRA3` | `F` | Importe extra 3 para interfaz configurable |
| 30 | `CIMPORTEEXTRA4` | `F` | Importe extra 4 para interfaz configurable |
| 31 | `CARCHIVO` | `V` | Nombre y extensión del archivo del CEP<br>Varchar: 254 caracteres |
| 32 | `CTIMESTAMP` | `V` | Concurrencia<br>Varchar: 23 caracteres |

---

### `NubeCuentas` — Tabla con las cuentas contables de la empresa

> Esta tabla almacena las cuentas contables de la empresa de CONTPAQi® Contabiliza.

> 2  V  Descripción de la cuenta contable  Varchar: 255 caracteres CNOMBRE

> I  Indica si la cuenta está activa o no  1 - Cuenta activa 0 - Cuenta inactiva

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CCUENTA` | `V` | Número de la cuenta contable<br>Varchar: 50 caracteres |

**Llaves e Índices:**
- `Indice: PK_NUBECUENTAS`
- `Llave:`

---

### `NubeDiarios` — Tabla con los diarios especiales de la empresa

> 3 - Efectivo ingresos  4 - Efectivo egresos

> Esta tabla almacena los diarios especiales de la empresa de CONTPAQi® Contabiliza.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CCODIGO` | `V` | Código del diario especial<br>Varchar: 12 caracteres |
| 2 | `CNOMBRE` | `V` | Descripción del diario especial<br>Varchar: 253 caracteres |
| 3 | `CTIPO` | `I` | Indica el tipo del diario especial<br>1 - De póliza 3 - Efectivo ingresos<br>2 - De movimientos 4 - Efectivo egresos |

**Llaves e Índices:**
- `Indice: PK_NUBEDIARIOS`
- `Llave:`

---

### `admLigasPago` — Tabla con las Ligas de Pago creadas por empresa

> Esta tabla almacena las Ligas de Pago creadas por empresa.

> 0 = Inicial 1 = Pendiente 2 = Aprobado 3 = Autorizado 4 = En proceso de revisión 5 = Disputa iniciada 6 = Rechazado 7 = Cancelado 8 = Reembolsado 9 = Contracargo

> 0 = Ninguno 1 = Dinero de la cuenta 5 = tarjeta de crédito 6 = Tarjeta de débito

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDLIGA` | `I` | Identificador de la liga de pago |
| 2 | `CIDCLIENTEPROVEEDOR` | `I` | Identificador del cliente |
| 3 | `CIDMONEDA` | `I` | Identificador de la moneda |
| 4 | `CIDPROVEEDORSERVICIO` | `I` | Identificador del proveedor del servicio web |
| 5 | `CFECHAINIVIG` | `D` | Fecha y hora de inicio de vigencia |
| 6 | `CFECHAFINVIG` | `D` | Fecha y hora de fin de vigencia |
| 7 | `CFECHAPAGO` | `D` | Fecha y hora del pago |
| 8 | `CESTADO` | `I` | Estado de la liga de pago<br>0 = Inicial<br>1 = Pendiente<br>2 = Aprobado<br>3 = Autorizado<br>4 = En proceso de revisión<br>5 = Disputa iniciada<br>6 = Rechazado<br>7 = Cancelado<br>8 = Reembolsado<br>9 = Contracargo |
| 9 | `CTIPOPAGO` | `I` | Tipo de pago realizado sobre la liga de pago<br>0 = Ninguno<br>1 = Dinero de la cuenta<br>5 = tarjeta de crédito<br>6 = Tarjeta de débito |
| 10 | `CLIGA` | `V` | Liga de pago generada |
| 11 | `CGUIDEXTERNALREF` | `V` | GUID de referencia para el pago<br>Varchar: 40 caracteres |
| 12 | `CGUIDLIGA` | `V` | GUID asignado a la liga de pago por el proveedor<br>Varchar: 40 caracteres |
| 13 | `CIDDOCTOABONO` | `I` | Identificador del documento de abono relacionado a la liga de<br>pago |
| 14 | `CTOTAL` | `F` | Importe total de la liga de pago |

**Llaves e Índices:**
- `Indice: PK_admLigasPago`
- `Indice: CIDDOCTOABONO`
- `Llave: CIDLIGA`
- `Llave: CIDDOCTOABONO`
- `Indice: CIDCLIENTEPROVEEDOR`
- `Llave: CIDCLIENTEPROVEEDOR`

---

### `admAsocLigasPagos` — Tabla de relación entre las ligas de pago

> Esta tabla almacena la relación entre las Ligas de Pago y el documento asociado.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDLIGA` | `I` | Identificador de la liga de pago |
| 2 | `CIDDOCTO` | `I` | Identificador de la factura pagada |
| 3 | `CIMPORTEPAGO` | `F` | Importe pagado, expresado en moneda nacional |

**Llaves e Índices:**
- `Indice: PK_admAsocLigasPagos`
- `Llave: CIDLIGA, CIDDOCTO`

---

### `admConfigProveedoresNube` — Tabla con la configuración de las

> Esta tabla almacena la configuración de las Ligas de Pago de la empresa.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDPROVEEDORSERVICIO` | `I` | Identificador del proveedor del servicio |
| 2 | `CACTIVO` | `I` | Indica si la liga está activa o no |
| 3 | `CTIENEVIGENCIA` | `I` | Indica si se le podrá definir vigencia la liga de pago o no |
| 4 | `CDIASVIGENCIA` | `I` | Días de vigencia que se tomará al generar la liga de pago |

**Llaves e Índices:**
- `Indice: PK_admConfigProveedoresNube`
- `Llave: CIDPROVEEDORSERVICIO`

---

## 4. Tablas Generales

### `IdxAdminPAQ` — Tabla de Índices

> Tablas generales

> Esta tabla almacena los índices de las tablas generales de CONTPAQi® Comercial.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDAUTOINCSQL` | `I` | Consecutivo interno de SQL. |
| 2 | `TABLA` | `V` | Nombre de la tabla.<br>Varchar:25 caracteres. |
| 3 | `NOMBRE` | `V` | Nombre del índice.<br>Varchar:50 caracteres. |
| 4 | `TIPO` | `V` | Indice.<br>Varchar: 1 caracter. |
| 5 | `GRUPO` | `V` | Grupo al que pertenece la tabla:<br>E = Empresa<br>G = Tabla común<br>S = Tabla de prioridades<br>F = Formas preimpresas<br>Varchar: 1 caracter. |
| 6 | `DESCRIPCIO` | `V` | Llave del índice.<br>Varchar: 253 caracteres. |
| 7 | `CASE` | `T` | Indica si es sensible a mayúsculas o minúsculas.<br>False = No<br>True = Sí |
| 8 | `UNIQUE` | `T` | Indica si el índice permite llaves duplicadas.<br>False = No<br>True = Sí |
| 9 | `DESCENDING` | `T` | Indica si el índice es ascendente o descendente.<br>False = Ascendente<br>True = Descendente |

---

### `Empresas` — Tabla de Empresas

> Esta tabla almacena la lista de las empresa creadas en CONTPAQi® Comercial.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDEMPRESA` | `I` | Identificador de la empresa. |
| 2 | `CNOMBREEMPRESA` | `V` | Nombre de la empresa.<br>Varchar: 150 caracteres. |
| 3 | `CRUTADATOS` | `V` | Ruta de la empresa.<br>Varchar: 253 caracteres. |
| 4 | `CRUTARESPALDOS` | `V` | Ruta de respaldo de la empresa.<br>Varchar: 253 caracteres. |

---

### `Formulas` — Tabla de Fórmulas

> Esta tabla almacena la descripción de las fórmulas usadas en la tabla Conceptos.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDFORMULA` | `I` | Identificador de la fórmula. |
| 2 | `CDESCRIPCION` | `V` | Descripción general de la fórmula.<br>Varchar: 150 caracteres. |
| 3 | `CNOMBRE` | `V` | Nombre de la fórmula.<br>Varchar: 50 caracteres. |
| 4 | `CAGRUPADOR` | `I` | Campo que calcula la fórmula. |
| 5 | `CDESCRIPCIONEJEMPLO` | `T` | Descripción detallada de la fórmula. |

---

### `Etiquetas` — Tabla de Configuración de etiquetas

> Esta tabla almacena la configuración las etiquetas de Códigos de Barras.

> 0 = Codabar (AA)  1 = Codabar (BA)  2 = Codabar (CA)  3 = Codabar (DA)  4 = Codabar (AB)  5 = Codabar (BB)  6 = Codabar (CB)  7 = Codabar (DB)  8 = Codabar (AC)  9 = Codabar (BC)  10 = Codabar (CC)  11 = Codabar (DC)  12 = Codabar (AD)  13 = Codabar (BD)  14 = Codabar (CD)  15 = Codabar (DD)  15 = Code 128A  16 = Code 128B  17 = Code 128C  18 = Code 2 of 5  19 = Code 39 extended (start)  20 = Code 39 extended  21 = Code 39 extended (Check digit)  22 = Code 39 extended (Start / Check digit)  23 = EAN 13 (IN/IN)  24 = EAN 13 (IN/OUT)  25 = EAN 13 (OUT/OUT)  26 = EAN 13 (OUT/IN)  27 = Interleaved 2 of 5  28 = Interleaved 2 of 5 (Checksum)  29 = Interleaved 2 of 5 (Bar / Checksum)  30 = Interleaved 2 of 5 (Bar)  31 = POSTNET  32 = UPC A  33 = UPC B

> 15  CAPARECENOMBRE  PRODUCTO

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDETIQUETA` | `I` | Identificador de etiquetas. |
| 2 | `CNOMBREETIQUETA` | `V` | Nombre de la etiqueta.<br>Varchar: 30 caracteres. |
| 3 | `CIMPRIMIRCONTORNO` | `I` | Indica si imprime el contorno de la etiqueta.<br>0 = No<br>1 = Sí |
| 4 | `CIDTIPOHOJA` | `I` | Identificador del formato de impresión de etiqueta. |
| 5 | `CPOSICIONCODIGOBARRASX` | `I` | Posición horizontal de la etiqueta medida en milímetros. |
| 6 | `CPOSICIONCODIGOBARRASY` | `I` | Posición vertical de la etiqueta medida en milímetros. |
| 7 | `CFORMATOCODIGOBARRAS` | `I` | Formato estándar del código de barras.<br>0 = Codabar (AA)<br>1 = Codabar (BA)<br>2 = Codabar (CA)<br>3 = Codabar (DA)<br>4 = Codabar (AB)<br>5 = Codabar (BB)<br>6 = Codabar (CB)<br>7 = Codabar (DB)<br>8 = Codabar (AC)<br>9 = Codabar (BC)<br>10 = Codabar (CC)<br>11 = Codabar (DC)<br>12 = Codabar (AD)<br>13 = Codabar (BD)<br>14 = Codabar (CD)<br>15 = Codabar (DD)<br>15 = Code 128A<br>16 = Code 128B<br>17 = Code 128C<br>18 = Code 2 of 5<br>19 = Code 39 extended (start)<br>20 = Code 39 extended<br>21 = Code 39 extended (Check digit)<br>22 = Code 39 extended (Start / Check digit)<br>23 = EAN 13 (IN/IN)<br>24 = EAN 13 (IN/OUT)<br>25 = EAN 13 (OUT/OUT)<br>26 = EAN 13 (OUT/IN)<br>27 = Interleaved 2 of 5<br>28 = Interleaved 2 of 5 (Checksum)<br>29 = Interleaved 2 of 5 (Bar / Checksum)<br>30 = Interleaved 2 of 5 (Bar)<br>31 = POSTNET<br>32 = UPC A<br>33 = UPC B |
| 8 | `CSUPLEMENTO` | `V` | Suplemento del código de barras.<br>Varchar: 5 caracteres. |
| 9 | `CORIENTACION` | `I` | Orientación del código de barras.<br>0 = Normal<br>1 = 90º<br>2 = 180º<br>3 = 270º |
| 10 | `CCOLORCODIGOBARRAS` | `I` | Color de la etiqueta. |
| 11 | `CDENSIDADCODIGOBARRAS` | `I` | Ancho de cada barra en el código de barras. |
| 12 | `CALTURACODIGOBARRAS` | `I` | Altura de cada barra en el código de barras. |
| 13 | `CANCHOCODIGOBARRAS` | `I` | Ancho de la etiqueta en pixeles. |
| 14 | `CAPARECETEXTOCODIGO` | `I` | Indica si despliega el código numérico del código de barras.<br>0 = No<br>1 = Sí |
| 15 | `CAPARECENOMBRE
PRODUCTO` | `I` | Indica si despliega el nombre del producto en la etiqueta.<br>0 = No<br>1 = Sí |
| 16 | `CPOSICIONNOMBRE
PRODUCTOX` | `I` | Posición horizontal del nombre en milímetros. |
| 17 | `CPOSICIONNOMBRE
PRODUCTOY` | `I` | Posición vertical del nombre en milímetros. |
| 18 | `CFUENTENOMBREPRODUCTO` | `V` | Tipografía utilizada en el nombre.<br>Varchar: 30 caracteres. |
| 19 | `CTAMNOMBREPRODUCTO` | `I` | Tamaño de la tipografía utilizada en el nombre. |
| 20 | `CCOLORNOMBREPRODUCTO` | `I` | Color de la tipografía utilizada en el nombre. |
| 21 | `CESTILONOMBREPRODUCTO` | `I` | Estilo utilizado en la tipografía del nombre. |
| 22 | `CTEXTONOMBREPRODUCTO` | `V` | Texto fijo que aparece junto al nombre.<br>Varchar: 30 caracteres. |
| 23 | `CALINEACIONNOMBRE
PRODUCTO` | `I` | Alineación del texto.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 24 | `CANCHONOMBREPRODUCTO` | `I` | Ancho en pixeles del nombre. |
| 25 | `CNUMLISTAPRECIO
PRODUCTO` | `I` | Número de la lista de precios del producto que se imprime en la<br>etiqueta.<br>0 = Ninguna<br>1 = Lista de Precios 1<br>2 = Lista de Precios 2<br>3 = Lista de Precios 3<br>4 = Lista de Precios 4<br>5 = Lista de Precios 5<br>6 = Lista de Precios 6<br>7 = Lista de Precios 7<br>8 = Lista de Precios 8<br>9 = Lista de Precios 9<br>10 = Lista de Precios 10 |
| 26 | `CPOSICIONPRECIO
PRODUCTOX` | `I` | Posición horizontal de la lista de precios en milímetros. |
| 27 | `CPOSICIONPRECIO
PRODUCTOY` | `I` | Posición vertical de la lista de precios en milímetros. |
| 28 | `CFUENTEPRECIOPRODUCTO` | `V` | Tipografía utilizada en la lista de precios.<br>Varchar: 30 caracteres. |
| 29 | `CTAMPRECIOPRODUCTO` | `I` | Tamaño de la tipografía utilizada en la lista de precios. |
| 30 | `CCOLORPRECIOPRODUCTO` | `I` | Color de la tipografía utilizada en la lista de precios. |
| 31 | `CESTILOPRECIOPRODUCTO` | `I` | Estilo utilizado en la tipografía en la lista de precios. |
| 32 | `CTEXTOPRECIOPRODUCTO` | `V` | Texto fijo que aparece junto a la lista de precios.<br>Varchar: 30 caracteres. |
| 33 | `CALINEACIONPRECIO
PRODUCTO` | `I` | Alineación del texto de la lista de precios.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 34 | `CANCHOPRECIOPRODUCTO` | `I` | Ancho en pixeles de la lista de precios. |
| 35 | `CAPARECECARACTERISTICA1` | `I` | Indica si despliega la característica 1 del producto.<br>0 = No<br>1 = Sí |
| 36 | `CPOSICIONCARACTERISTICA1X` | `I` | Posición horizontal de la característica 1 en milímetros. |
| 37 | `CPOSICIONCARACTERISTICA1Y` | `I` | Posición vertical de la característica 1 en milímetros. |
| 38 | `CFUENTECARACTERISTICA1` | `V` | Tipografía utilizada en la característica 1.<br>Varchar: 30 caracteres. |
| 39 | `CTAMCARACTERISTICA1` | `I` | Tamaño de la tipografía utilizada en la característica 1. |
| 40 | `CCOLORCARACTERISTICA1` | `I` | Color de la tipografía utilizada en la característica 1. |
| 41 | `CESTILOCARACTERISTICA1` | `I` | Estilo utilizado en la tipografía en la característica 1. |
| 42 | `CTEXTOCARACTERISTICA1` | `V` | Texto fijo que aparece junto a la característica 1.<br>Varchar: 30 caracteres. |
| 43 | `CALINEACION
CARACTERISTIC1` | `I` | Alineación del texto de la característica 1.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 44 | `CANCHOCARACTERISTICA1` | `I` | Ancho en pixeles de la característica 1. |
| 45 | `CAPARECECARACTERISTICA2` | `I` | Indica si despliega la característica 2 del producto.<br>0 = No<br>1 = Sí |
| 46 | `CPOSICIONCARACTERISTICA2X` | `I` | Posición horizontal de la característica 2 en milímetros. |
| 47 | `CPOSICIONCARACTERISTICA2Y` | `I` | Posición vertical de la característica 2 en milímetros. |
| 48 | `CFUENTECARACTERISTICA2` | `V` | Tipografía utilizada en la característica 2.<br>Varchar: 30 caracteres. |
| 49 | `CTAMCARACTERISTICA2` | `I` | Tamaño de la tipografía utilizada en la característica 2. |
| 50 | `CCOLORCARACTERISTICA2` | `I` | Color de la tipografía utilizada en la característica 2. |
| 51 | `CESTILOCARACTERISTICA2` | `I` | Estilo utilizado en la tipografía en la característica 2. |
| 52 | `CTEXTOCARACTERISTICA2` | `V` | Texto fijo que aparece junto a la característica 2.<br>Varchar: 30 caracteres. |
| 53 | `CALINEACION
CARACTERISTIC2` | `I` | Alineación del texto de la característica 2.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 54 | `CANCHOCARACTERISTICA2` | `I` | Ancho en pixeles de la característica 2. |
| 55 | `CAPARECECARACTERISTICA
3` | `I` | Indica si despliega la característica 3 del producto.<br>0 = No<br>1 = Sí |
| 56 | `CPOSICIONCARACTERISTICA
3X` | `I` | Posición horizontal de la característica 3 en milímetros. |
| 57 | `CPOSICIONCARACTERISTICA
3Y` | `I` | Posición vertical de la característica 3 en milímetros. |
| 58 | `CFUENTECARACTERISTICA3` | `V` | Tipografía utilizada en la característica 3.<br>Varchar: 30 caracteres. |
| 59 | `CTAMCARACTERISTICA3` | `I` | Tamaño de la tipografía utilizada en la característica 3. |
| 60 | `CCOLORCARACTERISTICA3` | `I` | Color de la tipografía utilizada en la característica 3. |
| 61 | `CESTILOCARACTERISTICA3` | `I` | Estilo utilizado en la tipografía en la característica 3. |
| 62 | `CTEXTOCARACTERISTICA3` | `V` | Texto fijo que aparece junto a la característica 3.<br>Varchar: 30 caracteres. |
| 63 | `CALINEACIONCARACTERISTI
C3` | `I` | Alineación del texto de la característica 3.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 64 | `CANCHOCARACTERISTICA3` | `I` | Ancho en pixeles de la característica 3. |
| 65 | `CNUMIMPUESTORETENCION` | `I` | Importe del impuesto o retención que aplica al producto.<br>0 = Ninguno<br>1 = Impuesto 1<br>2 = Impuesto 2<br>3 = Impuesto 3<br>4 = Retención 1<br>5 = Retención 2 |
| 66 | `CPOSICIONIMPUESTOX` | `I` | Posición horizontal del impuesto o retención en milímetros. |
| 67 | `CPOSICIONIMPUESTOY` | `I` | Posición vertical del impuesto o retención en milímetros. |
| 68 | `CFUENTEIMPUESTO` | `V` | Tipografía utilizada en el impuesto o retención.<br>Varchar: 30 caracteres. |
| 69 | `CTAMIMPUESTO` | `I` | Tamaño de la tipografía utilizada en el impuesto o retención. |
| 70 | `CCOLORIMPUESTO` | `I` | Color de la tipografía utilizada en el impuesto o retención. |
| 71 | `CESTILOIMPUESTO` | `I` | Estilo utilizado en la tipografía en el impuesto o retención. |
| 72 | `CTEXTOIMPUESTO` | `V` | Texto fijo que aparece junto al impuesto o retención.<br>Varchar: 30 caracteres. |
| 73 | `CALINEACIONIMPUESTO` | `I` | Alineación del texto del impuesto o retención.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 74 | `CANCHOIMPUESTO` | `I` | Ancho en pixeles del impuesto o retención. |
| 75 | `CAPARECENUMEROLOTE` | `I` | Indica si el número de lote se muestra en la etiqueta.<br>0 = No<br>1 = Sí |
| 76 | `CPOSICIONNUMEROLOTEX` | `I` | Posición horizontal del número de lote en milímetros. |
| 77 | `CPOSICIONNUMEROLOTEY` | `I` | Posición vertical del número de lote en milímetros. |
| 78 | `CFUENTENUMEROLOTE` | `V` | Tipografía utilizada en el número de lote.<br>Varchar: 30 caracteres. |
| 79 | `CTAMNUMEROLOTE` | `I` | Tamaño de la tipografía utilizada en el número de lote. |
| 80 | `CCOLORNUMEROLOTE` | `I` | Color de la tipografía utilizada en el número de lote. |
| 81 | `CESTILONUMEROLOTE` | `I` | Estilo utilizado en la tipografía en el número de lote. |
| 82 | `CTEXTONUMEROLOTE` | `V` | Texto fijo que aparece junto al número de lote.<br>Varchar: 30 caracteres. |
| 83 | `CALINEACIONNUMEROLOTE` | `I` | Alineación del texto del número de lote.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 84 | `CANCHONUMEROLOTE` | `I` | Ancho en pixeles del número de lote. |
| 85 | `CAPARECEFECHA
CADUCIDAD` | `I` | Indica si la fecha de caducidad del producto se muestra en la<br>etiqueta.<br>0 = No<br>1 = Sí |
| 86 | `CPOSICIONFECHACADUCIDADX` | `I` | Posición horizontal de la fecha de caducidad en milímetros. |
| 87 | `CPOSICIONFECHACADUCIDADY` | `I` | Posición vertical de la fecha de caducidad en milímetros. |
| 88 | `CFUENTEFECHACADUCIDAD` | `V` | Tipografía utilizada en la fecha de caducidad.<br>Varchar: 30 caracteres. |
| 89 | `CTAMFECHACADUCIDAD` | `I` | Tamaño de la tipografía utilizada en la fecha de caducidad. |
| 90 | `CCOLORFECHACADUCIDAD` | `I` | Color de la tipografía utilizada en la fecha de caducidad. |
| 91 | `CESTILOFECHACADUCIDAD` | `I` | Estilo utilizado en la tipografía en la fecha de caducidad. |
| 92 | `CTEXTOFECHACADUCIDAD` | `V` | Texto fijo que aparece junto a la fecha de caducidad.<br>Varchar: 30 caracteres. |
| 93 | `CALINEACIONFECHA
CADUCIDAD` | `I` | Alineación del texto de la fecha de caducidad:<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 94 | `CANCHOFECHACADUCIDAD` | `I` | Ancho en pixeles de la fecha de caducidad. |
| 95 | `CAPARECEFECHA
FABRICACION` | `I` | Indica si la fecha de fabricación del producto se muestra en la<br>etiqueta.<br>0 = No<br>1 = Sí |
| 96 | `CPOSICIONFECHAFABRICACIOX` | `I` | Posición horizontal de la fecha de fabricación en milímetros. |
| 97 | `CPOSICIONFECHAFABRICACIOY` | `I` | Posición vertical de la fecha de fabricación en milímetros. |
| 98 | `CFUENTEFECHAFABRICACION` | `V` | Tipografía utilizada en la fecha de fabricación.<br>Varchar: 30 caracteres. |
| 99 | `CTAMFECHAFABRICACION` | `I` | Tamaño de la tipografía utilizada en la fecha de fabricación. |
| 100 | `CCOLORFECHAFABRICACION` | `I` | Color de la tipografía utilizada en la fecha de fabricación. |
| 101 | `CESTILOFECHAFABRICACION` | `I` | Estilo utilizado en la tipografía en la fecha de fabricación. |
| 102 | `CTEXTOFECHAFABRICACION` | `V` | Texto fijo que aparece junto a la fecha de fabricación.<br>Varchar: 30 caracteres. |
| 103 | `CALINEACIONFECHA
FABRICAC` | `I` | Alineación del texto de la fecha de fabricación:<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 104 | `CANCHOFECHAFABRICACION` | `I` | Ancho en pixeles de la fecha de fabricación. |
| 105 | `CAPARECEPEDIMENTO` | `I` | Indica si el número de pedimento del producto se muestra en la<br>etiqueta.<br>0 = No<br>1 = Si |
| 106 | `CPOSICIONPEDIMENTOX` | `I` | Posición horizontal del pedimento en milímetros. |
| 107 | `CPOSICIONPEDIMENTOY` | `I` | Posición vertical del pedimento en milímetros. |
| 108 | `CFUENTEPEDIMENTO` | `V` | Tipografía utilizada en el pedimento.<br>Varchar: 30 caracteres. |
| 109 | `CTAMPEDIMENTO` | `I` | Tamaño de la tipografía utilizada en el pedimento. |
| 110 | `CCOLORPEDIMENTO` | `I` | Color de la tipografía utilizada en el pedimento. |
| 111 | `CESTILOPEDIMENTO` | `I` | Estilo utilizado en la tipografía en el pedimento. |
| 112 | `CTEXTOPEDIMENTO` | `V` | Texto fijo que aparece junto al pedimento.<br>Varchar: 30 caracteres. |
| 113 | `CALINEACIONPEDIMENTO` | `I` | Alineación del texto del pedimento.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 114 | `CANCHOPEDIMENTO` | `I` | Ancho en pixeles del pedimento. |
| 115 | `CAPARECEADUANA` | `I` | Indica si la agencia aduanal por dónde ingresó el producto se<br>muestra en la etiqueta.<br>0 = No<br>1 = Sí |
| 116 | `CPOSICIONADUANAX` | `I` | Posición horizontal de la agencia aduanal en milímetros. |
| 117 | `CPOSICIONADUANAY` | `I` | Posición vertical de la agencia aduanal en milímetros. |
| 118 | `CFUENTEADUANA` | `V` | Tipografía utilizada en la agencia aduanal.<br>Varchar: 30 caracteres. |
| 119 | `CTAMADUANA` | `I` | Tamaño de la tipografía utilizada en la agencia aduanal. |
| 120 | `CCOLORADUANA` | `I` | Color de la tipografía utilizada en la agencia aduanal. |
| 121 | `CESTILOADUANA` | `I` | Estilo utilizado en la tipografía de la agencia aduanal. |
| 122 | `CTEXTOADUANA` | `V` | Texto fijo que aparece junto a la agencia aduanal.<br>Varchar: 30 caracteres. |
| 123 | `CALINEACIONADUANA` | `I` | Alineación del texto de la agencia aduanal.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 124 | `CANCHOADUANA` | `I` | Ancho en pixeles de la agencia aduanal. |
| 125 | `CAPARECEFECHAPEDIMENTO` | `I` | Indica si la fecha del pedimento se muestra en la etiqueta.<br>0 = No<br>1 = Sí |
| 126 | `CPOSICIONFECHAPEDIMENT
OX` | `I` | Posición horizontal de la fecha del pedimento en milímetros. |
| 127 | `CPOSICIONFECHAPEDIMENT
OY` | `I` | Posición vertical de la fecha del pedimento en milímetros. |
| 128 | `CFUENTEFECHAPEDIMENTO` | `V` | Tipografía utilizada en la fecha del pedimento.<br>Varchar: 30 caracteres. |
| 129 | `CTAMFECHAPEDIMENTO` | `I` | Tamaño de la tipografía utilizada en la fecha del pedimento. |
| 130 | `CCOLORFECHAPEDIMENTO` | `I` | Color de la tipografía utilizada en la fecha del pedimento. |
| 131 | `CESTILOFECHAPEDIMENTO` | `I` | Estilo utilizado en la tipografía de la fecha del pedimento. |
| 132 | `CTEXTOFECHAPEDIMENTO` | `V` | Texto fijo que aparece junto a la fecha del pedimento.<br>Varchar: 30 caracteres. |
| 133 | `CALINEACIONFECHA
PEDIMENTO` | `I` | Alineación del texto de la fecha del pedimento.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 134 | `CANCHOFECHAPEDIMENTO` | `I` | Ancho en pixeles de la fecha del pedimento. |
| 135 | `CAPARECETIPOCAMBIO` | `I` | Indica si el tipo de cambio de la moneda en que se adquirió el<br>producto, cuando cruzó la frontera, se muestra en la etiqueta.<br>0 = No<br>1 = Sí |
| 136 | `CPOSICIONTIPOCAMBIOX` | `I` | Posición horizontal el tipo de cambio en milímetros. |
| 137 | `CPOSICIONTIPOCAMBIOY` | `I` | Posición vertical del tipo de cambio en milímetros. |
| 138 | `CFUENTETIPOCAMBIO` | `V` | Tipografía utilizada en el tipo de cambio.<br>Varchar: 30 caracteres. |
| 139 | `CTAMTIPOCAMBIO` | `I` | Tamaño de la tipografía utilizada en el tipo de cambio. |
| 140 | `CCOLORTIPOCAMBIO` | `I` | Color de la tipografía utilizada en el tipo de cambio. |
| 141 | `CESTILOTIPOCAMBIO` | `I` | Estilo utilizado en la tipografía del tipo de cambio. |
| 142 | `CTEXTOTIPOCAMBIO` | `V` | Texto fijo que aparece junto al tipo de cambio.<br>Varchar: 30 caracteres. |
| 143 | `CALINEACIONTIPOCAMBIO` | `I` | Alineación del texto del tipo de cambio.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 144 | `CANCHOTIPOCAMBIO` | `I` | Ancho en pixeles del tipo de cambio. |
| 145 | `CAPARECESERIE` | `I` | Indica si el número de serie del producto se muestra en la<br>etiqueta.<br>0 = No<br>1 = Sí |
| 146 | `CPOSICIONSERIEX` | `I` | Posición horizontal del número de serie en milímetros. |
| 147 | `CPOSICIONSERIEY` | `I` | Posición vertical del número de serie en milímetros. |
| 148 | `CFUENTESERIE` | `V` | Tipografía utilizada en el número de serie.<br>Varchar: 30 caracteres. |
| 149 | `CTAMSERIE` | `I` | Tamaño de la tipografía utilizada en el número de serie. |
| 150 | `CCOLORSERIE` | `I` | Color de la tipografía utilizada en el número de serie. |
| 151 | `CESTILOSERIE` | `I` | Estilo utilizado en la tipografía del número de serie. |
| 152 | `CTEXTOSERIE` | `V` | Texto fijo que aparece junto al número de serie.<br>Varchar: 30 caracteres. |
| 153 | `CALINEACIONSERIE` | `I` | Alineación del texto del número de serie.<br>0 = Izquierda<br>1 = Derecha<br>2 = Centrado |
| 154 | `CANCHOSERIE` | `I` | Ancho en pixeles del número de serie. |
| 155 | `CINCIVA` | `I` | Indica si el precio incluye IVA o no.<br>0 = No<br>1 = Sí |

---

### `FormatosEtiquetas` — Tabla de Formatos de impresión de

> Esta tabla almacena la configuración de las hojas donde se imprimen las etiquetas de códigos de barras de  CONTPAQi® Comercial

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDTIPOHOJA` | `I` | Identificador del formato de impresión de etiquetas. |
| 2 | `CNOMBREHOJA` | `V` | Nombre del formato de impresión de etiquetas.<br>Varchar: 30 caracteres. |
| 3 | `CLARGOPAPEL` | `F` | Largo del papel. |
| 4 | `CANCHOPAPEL` | `F` | Ancho del papel. Se mide en milímetros. |
| 5 | `CMARGENIZQUIERDO` | `F` | Margen izquierdo. Se mide en milímetros. |
| 6 | `CMARGENDERECHO` | `F` | Margen derecho. |
| 7 | `CMARGENINFERIOR` | `F` | Margen superior. |
| 8 | `CMARGENSUPERIOR` | `F` | Margen inferior. |
| 9 | `CNUMETIQUETAS` | `I` | Número de etiquetas a lo ancho. |
| 10 | `CNUMRENGLONES` | `I` | Número de etiquetas a lo largo. |

---

### `UsuariosActivos` — Tabla de Usuarios

> Esta tabla almacena los usuarios de CONTPAQi® Comercial.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDUSUARIO` | `I` | Identificador del usuario. |
| 2 | `CUSUARIO` | `V` | Nombre del usuario.<br>Varchar: 30 caracteres. |
| 3 | `CEMPRESA` | `V` | Empresa a la que pertenece el usuario.<br>Varchar: 150 caracteres. |

---

### `NubeEmpresas` — Tabla con la lista de empresas de CONTPAQi®

> Esta tabla almacena las listas de empresas de CONTPAQi® Contabiliza en base a la licencia.

| No. | Campo | Tipo | Descripción |
| :---: | :--- | :---: | :--- |
| 1 | `CIDEMPRESA` | `V` | Referencias a la empresa en Contablidad Nube:<br>GUID de la compañía<br>GUID de la instancia<br>Varchar: 253 caracteres |
| 2 | `CEMPRESA` | `V` | Nombre de la empresa<br>Varchar: 253 caracteres |
| 3 | `CRFC` | `V` | RFC de la empresa<br>Varchar: 20 caracteres |
| 4 | `CTIPO` | `V` | Indica si se trata de propietario o invitado<br>Varchar: 20 caracteres |
| 5 | `CPROPIETARIO` | `V` | Indica el nombre del propietario<br>Varchar: 150 caracteres |

**Llaves e Índices:**
- `Indice: PK_NUBEEMPRESA`
- `Llave:`

---
