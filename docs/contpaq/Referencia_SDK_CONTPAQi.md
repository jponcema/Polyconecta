# Manual de Referencia del SDK de los sistemas CONTPAQi®

> **Fecha de edición original / última modificación:** 30 de junio de 2026  

> **Formato:** Markdown Estructurado de Referencia Técnica

---
## 📋 Tabla de Contenidos

- [1. Manual de Referencia del SDK  de los sistemas CONTPAQi®](#1-manual-de-referencia-del-sdk-de-los-sistemas-contpaqi)
- [2. Introducción](#2-introducción)
- [3. Requerimientos para trabajar con el SDK](#3-requerimientos-para-trabajar-con-el-sdk)
- [4. Recomendaciones y consideraciones importantes](#4-recomendaciones-y-consideraciones-importantes)
  - [4.1 Funciones obligatorias](#41-funciones-obligatorias)
  - [4.2 Trabajando con documentos](#42-trabajando-con-documentos)
  - [4.3 Trabajando con productos o clientes](#43-trabajando-con-productos-o-clientes)
  - [4.4 Timbrar documentos](#44-timbrar-documentos)
  - [4.5 Cuándo usar funciones de alto nivel y cuando de bajo nivel](#45-cuándo-usar-funciones-de-alto-nivel-y-cuando-de-bajo-nivel)
  - [4.6  Recomendaciones para el manejo de cadenas](#46-recomendaciones-para-el-manejo-de-cadenas)
- [5. Funciones generales](#5-funciones-generales)
  - [5.1 Inicialización / Terminación](#51-inicialización-terminación)
  - [5.2 Manejo de errores](#52-manejo-de-errores)
- [6. Funciones de empresas](#6-funciones-de-empresas)
  - [6.1 Navegación](#61-navegación)
  - [6.2 Apertura / Cierre](#62-apertura-cierre)
  - [6.3 Ejemplos](#63-ejemplos)
- [7. Funciones de documentos](#7-funciones-de-documentos)
  - [7.1 Bajo nivel – Lectura/Escritura ](#71-bajo-nivel-lecturaescritura)
  - [7.2 Bajo nivel - Búsqueda/Navegación](#72-bajo-nivel---búsquedanavegación)
  - [7.3 Alto nivel – Lectura/Escritura ](#73-alto-nivel-lecturaescritura)
  - [7.4 Alto nivel - Búsqueda/Navegación](#74-alto-nivel---búsquedanavegación)
- [8. Funciones de movimientos](#8-funciones-de-movimientos)
  - [8.1 Bajo nivel – Lectura/Escritura ](#81-bajo-nivel-lecturaescritura)
  - [8.2 Bajo nivel - Búsqueda/Navegación](#82-bajo-nivel---búsquedanavegación)
  - [8.3 Alto nivel – Lectura/Escritura ](#83-alto-nivel-lecturaescritura)
- [9. Funciones de timbrado](#9-funciones-de-timbrado)
- [10. Funciones de clientes/proveedores](#10-funciones-de-clientesproveedores)
  - [10.1 Bajo nivel – Lectura/Escritura ](#101-bajo-nivel-lecturaescritura)
  - [10.2 Bajo nivel - Búsqueda/Navegación](#102-bajo-nivel---búsquedanavegación)
  - [10.3 Alto nivel – Lectura/Escritura ](#103-alto-nivel-lecturaescritura)
- [11. Funciones de productos](#11-funciones-de-productos)
  - [11.1 Bajo nivel – Lectura/Escritura ](#111-bajo-nivel-lecturaescritura)
  - [11.2 Bajo nivel - Búsqueda/Navegación](#112-bajo-nivel---búsquedanavegación)
  - [11.3 Alto nivel – Lectura/Escritura ](#113-alto-nivel-lecturaescritura)
- [12. Funciones de addendas](#12-funciones-de-addendas)
  - [12.1 Bajo nivel – Lectura/Escritura ](#121-bajo-nivel-lecturaescritura)
- [13. Funciones de direcciones](#13-funciones-de-direcciones)
  - [13.1 Bajo nivel – Lectura/Escritura ](#131-bajo-nivel-lecturaescritura)
  - [13.2 Bajo nivel - Búsqueda/Navegación](#132-bajo-nivel---búsquedanavegación)
  - [13.3 Alto nivel – Lectura/Escritura ](#133-alto-nivel-lecturaescritura)
- [14. Funciones de existencias](#14-funciones-de-existencias)
  - [14.1 Bajo nivel – Lectura/Escritura ](#141-bajo-nivel-lecturaescritura)
- [15. Funciones de costo histórico](#15-funciones-de-costo-histórico)
  - [15.1 Bajo nivel – Lectura/Escritura ](#151-bajo-nivel-lecturaescritura)
- [16. Funciones de conceptos de documentos](#16-funciones-de-conceptos-de-documentos)
  - [16.1 Bajo nivel – Lectura/Escritura ](#161-bajo-nivel-lecturaescritura)
  - [16.2 Bajo nivel - Búsqueda/Navegación](#162-bajo-nivel---búsquedanavegación)
- [17. Funciones de parámetros](#17-funciones-de-parámetros)
  - [17.1 Bajo nivel – Lectura/Escritura ](#171-bajo-nivel-lecturaescritura)
- [18. Funciones del catálogo de clasificaciones](#18-funciones-del-catálogo-de-clasificaciones)
  - [18.1 Bajo nivel – Lectura/Escritura ](#181-bajo-nivel-lecturaescritura)
  - [18.2 Bajo nivel - Búsqueda/Navegación](#182-bajo-nivel---búsquedanavegación)
- [19. Funciones del catálogo de valores de clasificaciones](#19-funciones-del-catálogo-de-valores-de-clasificaciones)
  - [19.1 Bajo nivel – Lectura/Escritura ](#191-bajo-nivel-lecturaescritura)
  - [19.2 Bajo nivel - Búsqueda/Navegación](#192-bajo-nivel---búsquedanavegación)
  - [19.3 Alto nivel – Lectura/Escritura ](#193-alto-nivel-lecturaescritura)
- [20. Constantes del SDK](#20-constantes-del-sdk)
- [21. Tipos de datos abstractos del SDK](#21-tipos-de-datos-abstractos-del-sdk)
- [22. Equivalencias de tipos de datos](#22-equivalencias-de-tipos-de-datos)
- [23. Casos prácticos](#23-casos-prácticos)
  - [23.1 Alta de dirección de cliente mediante SDK](#231-alta-de-dirección-de-cliente-mediante-sdk)
  - [23.2 Emitir factura de gasolina (producto con IEPS)](#232-emitir-factura-de-gasolina-producto-con-ieps)
  - [23.3 Registrar una relación CFDI mediante UUID](#233-registrar-una-relación-cfdi-mediante-uuid)
  - [23.4 Registrar y saldar pagos (REP)](#234-registrar-y-saldar-pagos-rep)
  - [23.5 Registrar medicamentos en el módulo de inventario (productos con lote)](#235-registrar-medicamentos-en-el-módulo-de-inventario-productos-con-lote)
  - [23.6 Emisión del complemento de Carta Porte 3.1 mediante SDK](#236-emisión-del-complemento-de-carta-porte-31-mediante-sdk)
    - [23.6.1 Escenario 1: Documento con complemento previamente creado en el sistema](#2361-escenario-1-documento-con-complemento-previamente-creado-en-el-sistema)
    - [23.6.2 Escenario 2: Documento sin complemento previamente creado en el sistema](#2362-escenario-2-documento-sin-complemento-previamente-creado-en-el-sistema)

---

## 1. Manual de Referencia del SDK  de los sistemas CONTPAQi®

### 1..1 Manual de Referencia del SDK de los sistemas CONTPAQi®

Última modificación: 30 de junio de 2026

---

## 2. Introducción

### 2..1 ¿Qué es un SDK?

Software Development Kit (SDK) o kit de desarrollo de software. Es generalmente un conjunto de herramientas de desarrollo que le permite a un programador crear aplicaciones para un sistema bastante concreto, por ejemplo ciertos paquetes de software, frameworks, plataformas de hardware, ordenadores, videoconsolas, sistemas operativos, etcétera.

En el caso de CONTPAQi Factura Electrónica®, el SDK es un conjunto de archivos que contienen funciones publicadas, las cuales pueden ser usadas por desarrolladores externos para manipular (consultar o modificar) información de la base de datos de estos sistemas.

### 2..2 ¿Cómo funciona?

Las funciones disponibles en el SDK se comunican con CONTPAQi Factura Electrónica® a través de métodos de clases, estas a su vez, hacen llamados a las clases “base” de CONTPAQi Factura Electrónica®, es decir, a las clases usadas dentro de dichos sistemas.

El SDK controla la concurrencia en un ambiente multiusuario, es decir las funciones dan el soporte para los bloqueos y protegen los accesos. (Permite operar como si se tratara de una estación de CONTPAQi Factura Electrónica®). Protege las bases de datos, sus relaciones y sigue las reglas de negocio de dichos sistemas.

---

## 3. Requerimientos para trabajar con el SDK

### 3..1 Ambiente

- CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica® instalado (monousuario o como estación).
- Entorno de programación. Editor/Compilador del lenguaje elegido (VB / Delphi / C / Plataforma .net, etc).
- Si estás programando en VBA (Excel) el SDK solo funciona en Microsoft® Office de 32 bits.
- Verifica contar con la licencia requerida por las funciones. Algunas funciones, como las de timbrado requieren licencias de un número de usuarios específico:

Sistema

Versión

Función

Usuarios

Licenciamiento

Número de empresas

CONTPAQi Factura Electrónica®

14.1.0

fEmiteDocumento

```pascal
fTimbraXML
fTimbraNominaXML
```

2 usuarios o superior 5 usuarios o superior 5 usuarios o superior

Anual

Multiempresa (MultiRFC)

CONTPAQi Comercial Premium®

12.1.0

fEmiteDocumento

5 usuarios o superior

Archivos usados por el SDK

Todos estos archivos son utilizados por el SDK:

Archivo

Descripción

Ubicación

```csharp
MGW_SDK.dll
```

Es la interfase del SDK con CONTPAQi Factura Electrónica®.

```csharp
Libreria de encadenado, aquí se encuentran las funciones del SDK.
```

C:\Archivos de programa\Compacw\Facturacion

MGWServicios

Es la interfase del SDK con Comercial Premium.

```csharp
Libreria de encadenado, aquí se encuentran las funciones del SDK.
```

C:\Program Files (x86)\Compac\COMERCIAL

> [!IMPORTANT]
> Recuerda:
Para el caso del sistema de CONTPAQi Factura Electrónica®, los archivos se encuentran en la carpeta Facturacion.

> [!IMPORTANT]
> Importante:
Se debe tener especial cuidado con el control de versiones con el SDK en la que se desarrolla una aplicación y la versión del sistema con la que se va a interactuar. 
Es decir, no se recomienda desarrollar una aplicación con el SDK de CONTPAQi Factura Electrónica®10.0.0 para interactuar con un CONTPAQi Factura Electrónica® 12.0.0.

---

## 4. Recomendaciones y consideraciones importantes

### 4..1 Tips y conceptos básicos

- Siempre ten en cuenta que las funciones del SDK están en C++, el objetivo al declarar las funciones en tu lenguaje es pasar los tipos de datos que C++ pueda recibir. Busca el tipo de datos en tu lenguaje que coincida mejor con el tipo de C++.
- En C++ todas las cadenas son de tipo Char*, por lo que si en tu lenguaje de programación utilizas el tipo String estos siempre se deberán pasar Por Valor.
- Antes de hacer accesos mediante el SDK, asegurarse que CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica® funciona correctamente y que la información que está generando es correcta.
- Estar familiarizado con la estructura de la base de datos de CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica®.
- Tener claro y bien conceptualizado el fin y el alcance de la aplicación a desarrollar.
- Ir por “partes”, es decir: Primero crear la conexión a la base de datos, inicializar el SDK y generar un documento desde la aplicación; posteriormente verificar que funciona correctamente (que se crea sin problemas el documento en CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica®).
- Modularizar el código (Si el entorno de programación lo permite). Esto es crear diversos módulos para separar funcionalidad global y local.
Ejemplo: Usar un módulo en el cual se realice la declaración de constantes, variables globales, estructuras de datos y enlace a las funciones del archivo MGW_SDK.DLL; y usar otro modulo para las funciones creadas por el desarrollador y que modificaran la información que se recibe y envía de la base de datos de CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica®.
Esto facilitará la portabilidad y la reutilización de código, así como el mantenimiento y actualización de la funcionalidad.
- Revisar que los documentos y sus movimientos se graban/actualizan de manera correcta en CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica®.
- Validar desde la aplicación que se desarrolla que los datos que se envían sea consistente y que tenga el formato correcto.
- Probar continuamente la aplicación con todas las posibles combinaciones que permita.

---

### 4.1 Funciones obligatorias

```csharp
Son las funciones que forzosamente deben incluirse en cualquier aplicación que use el SDK.
```

El método, a grandes ragos, se compone de:

- Inicializar el SDK al inicio de cada proceso: fInicializaSDK.
Esta función se llama una sola vez al iniciar un proceso o acción completa.

```csharp
Ejemplo: El alta de un documento y todos sus movimientos. Se inicia el SDK, se hace el llamado a todas las funciones requeridas y luego se termina el SDK.
```

- Funciones para abrir y cerrar empresa:

```csharp
Se usan para indicar las bases de datos de la empresa a la cual afectará la aplicación que hace uso del SDK. (fAbreEmpresa / fCierraEmpresa)
```

Sólo se puede trabajar en una empresa a la vez (a menos que se corran la misma aplicación dos veces).

- Incluir la función fError del SDK para recuperar la descripción de los posibles errores. La mayoría de las funciones regresan un código de error, donde 0 indica que no se presentaron errores y un número diferente de 0 cuando ocurrió algún error.

Se utiliza la función fError para recuperar la descripción de dicho error.

- Usar siempre la función fTerminaSDK para liberar todos los recursos solicitados por el SDK, al final de cada proceso completo. Ésta función se llama una sola vez al finalizar un proceso o acción completa.

```csharp
Estructura general de una aplicación desarrollada con el SDK.
```

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Tu función o proceso completo

Cerrar Empresa

Terminar SDK

---

### 4.2 Trabajando con documentos

Cuando se trabaje con documentos siempre se deben afectar.

Al crear documentos, la existencia y los costos se afectan, sin embargo los acumulados del sistema no, por lo que es necesario afectarlos después de crear documentos con sus movimientos correspondientes.

En el SDK existen dos tipos de afectación, una para los documentos de cargo y abono y otra para los demás tipos de documento.

```csharp
Estructura general de una aplicación que da de alta documentos y sus movimientos con el SDK.
```

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Alta de documento

Alta de movimientos

Afectar documento

Cerrar Empresa

Terminar SDK

```csharp
Estructura general de una aplicación que da de alta documentos de Cargo y Abono con el SDK.
```

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Alta de documento Cargo/Abono

Afectar documento

Cerrar Empresa

Terminar SDK

> [!IMPORTANT]
> Nota:
Las funciones de afectación de documentos son:
fAfectaDocto_Param () y fAfectaDocto (), bajo y alto nivel respectivamente.

Estructura general de un documento que maneja series y/o pedimentos

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Alta de documento

Alta de movimientos

Alta del movimiento con series o pedimentos

Calcula los movimentos con series o pedimentos

Afectar documento

Cerrar Empresa

Terminar SDK

---

### 4.3 Trabajando con productos o clientes

Estructura general para dar de alta productos o clientes

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Alta de producto o cliente

Cerrar Empresa

Terminar SDK

Estructura general para editar productos

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Busca el producto o cliente

Edita el producto o cliente

Modifica el campo del producto o el cliente

Guarda el producto o cliente

Cerrar Empresa

Terminar SDK

---

### 4.4 Timbrar documentos

Estructura general para crear un documento y timbrarlo

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Alta de documento

Alta de movimientos

Afectar documento

Inicializa información de la licencia

Emite/Timbra el documento

Entrega el documento

Cerrar Empresa

Terminar SDK

Estructura general para timbrar un XML creado por un tercero

Establecer el directorio del MGW_SDK

Inicializar SDK

Abrir Empresa

Inicializa información de la licencia Timbra el XML

Cerrar Empresa

Terminar SDK

---

### 4.5 Cuándo usar funciones de alto nivel y cuando de bajo nivel

En términos generales se recomienda usar las funciones de alto nivel debido a que estas realizan todo los procesos necesarios para mantener las reglas de negocio y la base de datos estable.

Cualquier lenguaje de programación que soporte estructuras de datos podrá hacer uso de las funciones de alto nivel, la razón es que como generalidad las funciones de alto nivel efectúan operaciones con registros completos.

Las funciones de bajo nivel permiten más flexibilidad en cuanto que datos se graban el la base de datos, pero implican más trabajo, por realizar escritura campo por campo, y complejidad pues se tienen que validar diversos puentos para no romper las reglas de negocio, por lo que para su uso se requiere mas precisión al desarrollar el proceso.

Estas funciones se pueden usar en cualquier lenguaje de programación, más son de carácter obligatorio en aquellos que no manejen estructuras de datos. Por ejemplo Visual FoxPro.

Ejemplo: Dar de alta de datos extras del catálogo sólo se puede efectuar con las funciones de “bajo nivel”

Algunos lenguajes como Visual FoxPro no soportan el uso de estructuras de datos, por lo que forzosamente se deben usar las funciones de bajo nivel.

Restricciones al usar funciones de bajo nivel

Las funciones de bajo nivel permiten la escritura campo a campo en la BD de CONTPAQi Comercial Premium®, sin embargo existen campos que no pueden ser modificadas por dichas funciones pues son valores que calcula o modifica CONTPAQi Comercial Premium® o CONTPAQi Factura Electrónica®.

| Campo | Razón |
| --- | --- |
| cIdDocumento | Es un dato autogenerado. |
| cIdDocumentoDe | Depende de la plantilla del documento. |
| cIdConcepto | Es un dato autogenerado. |
| cIdCteProv | Es un dato autogenerado. |
| cIdAgente | Es un dato autogenerado. |
| cIdConcepto | Es un dato autogenerado. |
| cNeto | Es un campo calculado. |
| cTotal | Es un campo calculado. |
| cAfectado | Es un campo protegido. |
| cNaturaleza | Es un dato autogenerado. |
| cDocumentoOrigen | Es un dato autogenerado. |
| cPlantillacUsaProveedor | Es un campo calculado. |
| cUsaCliente | Es un dato autogenerado. |
| cNetocTotalUnidades | Es un campo calculado. |
| cBanObsevaciones | Es un dato autogenerado. |
| cBanDatosEnvio | Es un dato autogenerado. |
| cBanCondCredito | Es un dato autogenerado. |
| CUnidadesPendientes | Es un campo calculado. |
| cTimeStamp | Es un dato autogenerado. |

---

### 4.6  Recomendaciones para el manejo de cadenas

#### 4.6.1 Recomendaciones para el manejo de cadenas

```csharp
La forma en que cada lenguaje de programación define los tipos de datos cadena es varía entre lenguajes (en cuanto a su tamaño en bytes). Por esta razón los tipos de datos manejados por distintos lenguajes pueden presentar problemas al pasar información al SDK. En C++ Builder y Delphi éste inconveniente no se presenta.
```

Al usar el SDK en Visual Basic. Para llenar los campos cadena que forman parte de la estructura, es necesario llenar con espacios en blanco las variables tipo cadena hasta alcanzar la longitud requerida por el SDK, por la diferencia que existe con este lenguaje al manejar los tipos de datos.

El error que se produce cuando no se llenan adecuadamente las estructuras es “código no existe”. Para contrarrestar este error se usan dos funciones de manipulación de cadenas.

La función para llenar espacios en Visual Basic es la siguiente:

Para realizar comparaciones dentro de VB es necesario quitar el caracter nulo.

---

## 5. Funciones generales

---

### 5.1 Inicialización / Terminación

```pascal
fInicializaSDK ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInicializaSDK()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Inicializa el SDK de CONTPAQi Comercial Premium®. Se requiere llamar esta función al inicio de cualquier aplicación que utilice el SDK.
 
Establece la conexión entre la aplicación desarrollada y la Base de datos de CONTPAQi Comercial Premium®. Su uso es obligatorio.

**Ejemplo:**
If lError <> 0 Then MensajeError lError End End If

```pascal
El siguiente código inicializa el SDK de CONTPAQi Comercial Premium® y asigna el resultado a una variable entera que se evalúa posteriormente; si su valor es distinto de 0 (cero) la aplicación se detiene.
lError = fInicializaSDK()
```

```pascal
fTerminaSDK ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fTerminaSDK ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** No tiene valor de retorno.

**Descripción:**
Libera todos los recursos solicitados por el SDK, se requiere llamar al terminar de utilizar el SDK.

**Ejemplo:**
SI Error <> 0 Error

```pascal
Termina SDK{
VAR Error: ENTERO
fInicializaSDK
Error = fTerminaSDK
ENTONCES
SI NO
fTerminaSDK
FIN SI
}
```

- **:** Para utilizar esta función es necesario principalmente inicializar SDK. 
Puede ser utilizada fTerminaSDK desde un botón o por medio de una ejecución de form closing.
Nota:
Es muy importante que siempre se termine la sesión de SDK ya que el servicio puede quedarse colgado y ocasionar problemas en los siguientes inicios de sesión.

```pascal
fSetNombrePAQ ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetNombrePAQ(aSistema)`

- **Parámetros:** Nombre Tipo Uso Descripción
aSistema Cadena Por referencia Nombre del sistema al que se
 conectará el SDK.
 
Importante:
Para establecer una conexión a CONTPAQi Factura Electrónica® este parámetro deberá ser igual a “CONTPAQ I Facturacion”.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función define el sistema al que se conectará el SDK. Sino se usa esta función la conexión por omisión será al sistemaCONTPAQi Comercial Premium®.
 Si se desea establecer una conexión a CONTPAQi Factura Electrónica® el parámetro aSistema deberá ser CONTPAQ I Facturacion y se deberá utilizar en vez de la función fInicializaSDK().

**Ejemplo:**
SI Error <> 0 Error

```pascal
Set Nombre PAQ{
VAR Error: ENTERO
VAR aNombrePAQ: CADENA
Error = fSetNombrePAQ recibe aNombrePAQ
ENTONCES
SI NO
fSetNombrePAQ
FIN SI
}
```

---

### 5.2 Manejo de errores

fError ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fError(aNumError, aMensaje, aLen )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aNumError

Entero

Por valor

Número del error.

aMensaje

Cadena

Por referencia

Descripción del error.

aLen

Entero

Por valor

Longitud del mensaje de error.

- **Retorna:** aMensaje: Al finalizar la función este parámetro contiene el mensaje de error correspondiente al número de error especificado en aNumError.

**Descripción:**
Esta función recupera el mensaje de error del SDK.

**Ejemplo:**
If lError <> 0 Then fError lError, lMensaje, 350 End End If

```pascal
El siguiente código asigna a la variable lError el resultado de la función fInicializaSDK(), en caso de que suceda algún error (valor distinto de 0), la función fError se ejecuta obteniendo el mensaje correspondiente al número de error enviado, mostrando una longitud de mensaje de 350 caracteres.
lError = fInicializaSDK()
```

---

## 6. Funciones de empresas

---

### 6.1 Navegación

fPosPrimerEmpresa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerEmpresa(aIdEmpresa, aNombreEmpresa, aDirectorioEmpresa )`

- **Parámetros:** Nombre Tipo Uso Descripción
aIdEmpresa Entero Por Referencia Identificador de la empresa.
aNombreEmpresa Cadena Por Referencia Nombre de la empresa.
aDirectorioEmpresa Cadena Por Referencia Directorio de la empresa.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdEmpresa: Al finalizar la función este parámetro contiene el identificador de la primera empresa registrada en la Base de Datos.
aNombreEmpresa: Al finalizar la función este parámetro contiene el nombre de la primera empresa registrada en la Base de Datos.
aDirectorioEmpresa: Al finalizar la función este parámetro contiene el directorio de la primera empresa registrada en la base de datos.

**Descripción:**
Esta función se posiciona en el primer registro de la base de datos de empresas de CONTPAQi Comercial Premium®, modifica los parámetros aNombreEmpresa y aDirectorioEmpresa, en los cuales guarda el nombre de la primera empresa y su ruta, correspondientemente.

**Ejemplo:**
El siguiente código indica a la aplicación que se posicione en el primer registro de empresas de la base de datos de CONTPAQi Comercial Premium®. fPosPrimerEmpresa(lIdEmpresa,lNombreEmpresa,lDirectorioEmpresa)

```pascal
El siguiente código indica a la aplicación que se posicione en el primer registro de empresas de la base de datos de CONTPAQi Comercial Premium®.
fPosPrimerEmpresa(lIdEmpresa,lNombreEmpresa,lDirectorioEmpresa)
```

fPosSiguienteEmpresa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteEmpresa (aIdEmpresa, aNombreEmpresa, aDirectorioEmpresa )`

- **Parámetros:** Nombre Tipo Uso Descripción
aIdEmpresa Entero Por Referencia Identificador de la empresa.
aNombreEmpresa Cadena Por Referencia Nombre de la empresa.
aDirectorioEmpresa Cadena Por Referencia Directorio de la empresa.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdEmpresa: Al finalizar la función este parámetro contiene el identificador de la primera empresa registrada en la base de datos.
aNombreEmpresa: Al finalizar la función este parámetro contiene el nombre de la primera empresa registrada en la base de datos.
aDirectorioEmpresa: Al finalizar la función este parámetro contiene el directorio de la primera empresa registrada en la base de datos.

**Descripción:**
Esta función avanza al siguiente registro en la tabla de Empresas de CONTPAQi Comercial Premium®®; en caso de que no exista un siguiente registro, la función retorna un valor distinto de 0 (cero).

**Ejemplo:**
El siguiente código termina el SDK de CONTPAQi Comercial Premium®®. fPosSiguienteEmpresa (lIdEmpresa,lNombreEmpresa,lDirectorioEmpresa)

```pascal
El siguiente código termina el SDK de CONTPAQi Comercial Premium®®.
fPosSiguienteEmpresa (lIdEmpresa,lNombreEmpresa,lDirectorioEmpresa)
```

> [!IMPORTANT]
> Nota:
Considera que para los parámetros de tipo cadena por referencia, se puede utilizar el tipo de datos Stringbuilder.

---

### 6.2 Apertura / Cierre

fAbreEmpresa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAbreEmpresa (aDirectorioEmpresa )`

- **Parámetros:** Nombre Tipo Uso Descripción
aDirectorioEmpresa Cadena Por Referencia Directorio de la empresa.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función abre la empresa que corresponde a la ruta especificada en el parámetro
aDirectorioEmpresa.

**Ejemplo:**
El siguiente código indica a la aplicación que abra la empresa ubicada el la ruta C:\Compacw\Empresas\EmpresaEjemplo. lDirectorioEmpresa = “C:\Compacw\Empresas\EmpresaEjemplo” fAbreEmpresa (lDirectorioEmpresa)

```pascal
El siguiente código indica a la aplicación que abra la empresa ubicada el la ruta
C:\Compacw\Empresas\EmpresaEjemplo.
lDirectorioEmpresa = “C:\Compacw\Empresas\EmpresaEjemplo”
fAbreEmpresa (lDirectorioEmpresa)
```

fCierraEmpresa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCierraEmpresa ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** No tiene valor de retorno.

**Descripción:**
Cierra la conexión con la empresa activa en la aplicación que usa el SDK.

**Ejemplo:**
El siguiente código cierra la empresa activa. fCierraEmpresa()

```pascal
El siguiente código cierra la empresa activa.
fCierraEmpresa()
```

---

### 6.3 Ejemplos

Pseudocódigo “Funciones de empresas”

```pascal
Buscar empresa {
```

```pascal
VAR aIdEmpresa, error: ENTERO
```

```pascal
VAR aNombreEmpresa, aDirectorioEmpresa: CADENA (StringBuilder)
```

```pascal
VAR alDirectorioEmpresa: String
```

Error = fPosPrimerEmpresa recibe REFERENCIA aIdEmpresa, PARÁMETRO aNombreEmpresa, PARÁMETRO aDirectorioEmpresa

HACER

SI

aNombreEmpresa = EMPRESA QUE BUSCAMOS

```pascal
ENTONCES
```

```pascal
alDirectorioEmpresa convierte : CADENA (String) aDirectorioEmpresa
```

fAbrirEmpresa recibe PARÁMETRO alDirectorioEmpresa

CORTAR

```pascal
SI NO
```

fPosSiguienteEmpresa recibe REFERENCIA aIdEmpresa, PARÁMETRO aNombreEmpresa, PARÁMETRO aDirectorioEmpresa

```pascal
FIN SI
```

MIENTRAS verdadero

```pascal
}
```

Comentario de retroalimentación

Las funciones fPosPrimerEmpresa () y fPosSiguienteEmpresa () están marcadas como que reciben tres referencias a parámetros:

| Nombre Tipo Uso Descripción aIdEmpresa Entero Por Referencia Identificador de la empresa. aNombreEmpresa Cadena Por Referencia Nombre de la empresa. aDirectorioEmpresa Cadena Por Referencia Directorio de la empresa. |
| --- |

La situación real es que solo aIdEmpresa es enviado como referencia para el caso de aNombreEmpresa y aDirectorioEmpresa es necesario enviar un parámetro pero que a su vez se pueda consumir como una referencia, para este caso un StringBuilder cumple esos requisitos.

> [!IMPORTANT]
> Considera que:

Si se intenta mandar una referencia en lugar de un parámetro marcara un error de incompatibilidad de código.

Si se utiliza directamente una cadena no se podrá consumir el valor de esta.

La función aDirectorioEmpresa se tiene documentada indicando que recibe una referencia al parámetro, pero en realidad recibe el parámetro simple de tipo cadena.

Nombre Tipo Uso Descripción

aDirectorioEmpresa Cadena Por Referencia Directorio de la empresa.

Si se envía como referencia marcara error de sintaxis.

---

## 7. Funciones de documentos

---

### 7.1 Bajo nivel – Lectura/Escritura 

#### 7.1.1 Bajo nivel – Lectura/Escritura

fInsertarDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertarDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Adiciona un nuevo registro en la tabla de Documentos en modo de inserción.

**Ejemplo:**
fInsertarDocumento Error = fInsertarDocumento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
fSetDatoDocumento recibe VAR aCampo: CADENA, VAR aValor: CADENA
fGuardaDocumento
FIN SI
}
```

- **Comentarios::** Para que la función fInsertarDocumento pueda establecer un nuevo registro a la tabla de documentos, es necesario indicar mediante la función fSetDatoDocumento los registros de la tabla Documentos a afectar; por ejemplo: 
fSetDatoDocumento recibe VAR aCampo: CADENA, VAR aValor: CADENA 
 
Después de la inserción de los valores a afectar, se utiliza la función fGuardaDocumento, la cual no lleva parámetros; si no se utiliza esta función, no se agregará el nuevo registro a la tabla de documentos. 
fGuardaDocumento

```pascal
fEditarDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditarDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Activa el modo de edición de un registro en la tabla de Documentos.

**Ejemplo:**
SI Error <> 0 Error

```pascal
fEditarDocumento
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fEditarDocumento
fSetDatoDocumento recibe VAR aCampo: CADENA, VAR aValor: CADENA
fGuardaDocumento
FIN SI
}
```

**Comentarios:**
Para poder editar un documento, es necesario posicionarnos sobre él y esto se consigue llevando a cabo una búsqueda del documento. 
En la documentación se observa que utilizan la función fBuscaDocumento con el parámetro lLlaveDocto:
fBuscaDocumento recibe lLlaveDocto 
 
Pero un método utilizado actualmente que realiza la misma funcionalidad, es la función fBuscarDocumento, que recibe 3 parámetros directamente; como se describe en el ejemplo a continuación:
 fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

```pascal
fGuardaDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Guarda los cambios realizados a un documento.

**Ejemplo:**
Guarda Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento
fEditarDocumento
fSetDatoDocumento
Error = fGuardaDocumento
ENTONCES
SI NO
fGuardaDocumento
FIN SI
}
```

**Comentarios:**
Esta función no recibe parámetros; es utilizada cuando un documento recibe algún tipo de edición. Si no se utiliza la función fGuardaDocumento, no se aplicarán las modificaciones que se hayan realizado.

fCancelarModificacionDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cancela las modificaciones al registro actual de documentos. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
fCancelarModificacionDocumento SI Error <> 0 Error fCancelarModificacionDocumento

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Es necesario realizar una búsqueda del documento, y si lo encuentra, aplica el procedimiento de cancelación a las modificaciones al registro actual de documentos:
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

fBorraDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorraDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Borra un registro en la tabla de Documentos.

**Ejemplo:**
fBorraDocumento SI Error <> 0 Error fBorraDocumento

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Para poder borrar un documento, es necesario llevar a cabo una búsqueda del documento para posicionarse sobre él.
En la documentación se observa que utilizan la función fBuscaDocumento con el parámetro lLlaveDocto 
fBuscaDocumento recibe lLlaveDocto 
 
Pero un método utilizado actualmente que realiza lo mismo, es la función fBuscarDocumento, que recibe 3 parámetros directamente, como se describe en el ejemplo a continuación: 
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

fCancelaDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelaDocumento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cancela documentos de CONTPAQi Comercial Premium®®.

**Ejemplo:**
Cancela Documento SI Error <> 0 Error fCancelaDocumento

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fEditarDocumento
fGuardaDocumento
FIN SI
}
```

**Comentarios:**
Para posicionarnos sobre el documento a cancelar, utilizamos la función fBuscarDocumento por sus parámetros aCodConcepto, aSerie, aFolio:
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

fBorraDocumento_CW ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorraDocumento_CW ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Borra un documento de CONTPAQi Comercial Premium®® y si este estuviera contabilizado, también borra la póliza correspondiente en CONTPAQi® Contabilidad.

**Ejemplo:**
Borra Documento CW SI Error <> 0 Error fBorraDocumento_CW

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Para posicionarnos sobre el documento a borrar, utilizamos la función fBuscarDocumento por sus parámetros aCodConcepto, aSerie, aFolio:
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

fCancelaDocumento_CW ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelaDocumento_CW ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cancela un documento de CONTPAQi Comercial Premium®® y borra la poliza correspondiente en CONTPAQi® Contabilidad.

**Ejemplo:**
Cancela Documento CW SI Error <> 0 Error fCancelaDocumento_CW

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Para posicionarnos sobre el documento a cancelar utilizamos la función fBuscarDocumento por sus parámetros aCodConcepto, aSerie, aFolio:
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA

fAfectaDocto_Param ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAfectaDocto (aCodConcepto, aSerie, aFolio, aAfecta)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por valor

Código del concepto del documento.

aSerie

Cadena

Por valor

Serie del documento

aFolio

Doble

Por valor

Folio del documento

aAfecta

Lógico (Bool)

Por valor

Verdadero o falso. Afectar o desafectar.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función utiliza aCodConcepto, aSerie, y aFolio como llave del documento y aAfecta para afectar o desafectarlo.

**Ejemplo:**
Afecta Documento Parámetros SI Error <> 0 Error fAfectaDocto_Param

```pascal
{
VAR Error: ENTERO
Error = fAfectaDocto_Param recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: DOUBLE, VAR aAfecta: BOOL
ENTONCES
SI NO
FIN SI
}
```

fSaldarDocumento_Param ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSaldarDocumento_Param (aCodConcepto_Pagar, aSerie_Pagar, aFolio_Pagar
aCodConcepto_Pago, aSerie_Pago, aFolio_Pago, aImporte, aIdMoneda, aFecha)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto_Pagar

Cadena

Por valor

Código del concepto del documento a pagar.

aSerie_Pagar

Cadena

Por valor

Serie del documento a pagar.

aFolio_Pagar

Doble

Por valor

Folio del documento a pagar.

aCodConcepto_Pago

Cadena

Por valor

Código del concepto del documento que paga.

aSerie_Pago

Cadena

Por valor

Serie del documento que paga.

aFolio_Pago

Cadena

Por valor

Folio del documento que paga.

aImporte

Doble

Por valor

Importe del pago.

aIdMoneda

Entero

Por valor

Moneda del pago.

aFecha

Cadena

Por valor

Fecha del pago.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función asocia documentos y salda sus importes.

**Ejemplo:**
Saldar Documento Parámetros SI Error <> 0 Error fSaldarDocumento_Param

```pascal
{
VAR Error: ENTERO
Error = fSaldarDocumento_Param recibe VAR aCodConcepto: CADENA,
VAR aSerie_Parar:
CADENA, VAR aFolio_Pagar: DOUBLE, VAR aCodConcepto_Pago:
CADENA, VAR aSerie_Pago: CADENA, VAR aFolio_Pago: DOUBLE, VAR aImporte:
DOUBLE, VAR aIdMoneda: INT, VAR aFecha: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
El parámetro aFolio_Pago está marcado como tipo CADENA: 

Parámetros 

Nombre 

Tipo 

Uso 

Descripción 

 

aFolio_Pago 

Cadena 

Por valor 

Folio del documento que paga. 

 
 
Pero la situación real es que este tipo de dato es de tipo DOUBLE según el código fuente de la librería del SDK:
 

Parámetros 

Nombre 

Tipo 

Uso 

Descripción 

 

aFolio_Pago 

Double

Por valor 

Folio del documento que paga. 

 
fSaldarDocumento_Param recibe VAR aCodConcepto_Pagar: CADENA, VAR 
aSerie_Parar: CADENA, VAR aFolio_Pagar: DOUBLE, VAR aCodConcepto_Pago: 
CADENA, VAR aSerie_Pago: CADENA, VAR aFolio_Pago: DOUBLE

fBorrarAsociacion_Param ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorrarAsociacion (aCodConcepto_Pagar, aSerie_Pagar, aFolio_Pagar CodConcepto_Pago, aSerie_Pago, aFolio_Pago)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto_Pagar

Cadena

Por valor

Código del concepto del documento pagado.

aSerie_Pagar

Cadena

Por valor

Serie del documento pagado.

aFolio_Pagar

Doble

Por valor

Folio del documento pagado.

aCodConcepto_Pago

Cadena

Por valor

Código del concepto del documento que pagó.

aSerie_Pago

Cadena

Por valor

Serie del documento que pagó.

aFolio_Pago

Cadena

Por valor

Folio del documento que pagó.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función la asociación de documentos.

**Ejemplo:**
Borrar Asociación Parámetros SI Error <> 0 Error fSaldarDocumento_Param

```pascal
{
VAR Error: ENTERO
Error = fBorrarAsociacion_Param recibe VAR aCodConcepto_Pagar: CADENA, VAR
aSerie_Parar: CADENA, VAR aFolio_Pagar: DOUBLE, VAR aCodConcepto_Pago:
CADENA, VAR aSerie_Pago: CADENA, VAR aFolio_Pago: DOUBLE
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
El parámetro aFolio_Pago está marcado como tipo CADENA:
 

Parámetros 

Nombre 

Tipo 

Uso 

Descripción 

 

aFolio_Pago 

Cadena 

Por valor 

Folio del documento que pagó. 

 
Pero la situación real es que este tipo de dato es de tipo DOUBLE según el código fuente de la librería del SDK:

Parámetros 

Nombre 

Tipo 

Uso 

Descripción 

 

aFolio_Pago 

Double

Por valor 

Folio del documento que pagó. 

 
fBorrarAsociacion_Param recibe VAR aCodConcepto_Pagar: CADENA, VAR 
aSerie_Parar: CADENA, VAR aFolio_Pagar: DOUBLE, VAR aCodConcepto_Pago: 
CADENA, VAR aSerie_Pago: CADENA, VAR aFolio_Pago: DOUBLE, VAR aImporte: 
DOUBLE, VAR aIdMoneda: INT, VAR aFecha: CADENA

```pascal
fSetDatoDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoDocumento (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino

aValor

Cadena

Por valor

Valor de escritura

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de documentos.

**Ejemplo:**
Set Dato Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fEditarDocumento
fSetDatoDocumento recibe VAR aCampo: CADENA, VAR aValor: CADENA
fGuardaDocumento
FIN SI
}
```

**Comentarios:**
Para poder setear un documento, es necesario posicionarnos sobre él y esto es llevándose a cabo una búsqueda del documento. En la documentación se observa que utilizan la función fBuscaDocumento con el parámetro lLlaveDocto:
 
fBuscaDocumento recibe lLlaveDocto 
 
Pero un método utilizado actualmente que realiza la misma funcionalidad, es la función fBuscarDocumento que recibe 3 parámetros directamente, como se describe en el ejemplo y a continuación: 
 
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: 
CADENA, VAR aFolio: CADENA 
 
Una vez encontrado, lo ponemos en modo de edición con la función fEditarDocumento para poder aplicar los cambios o actualizaciones correspondientes en modo de programación de bajo nivel.
 
Realizado lo anterior, se guardan las modificaciones realizadas empleando la función fGuardaDocumento:
 
fEditarDocumento 
fSetDatoDocumento recibe VAR aCampo: CADENA, VAR aValor: CADENA 
fGuardaDocumento

```pascal
fLeeDatoDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoDocumento (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino

aValor

Cadena

Por referencia

Valor de escritura

alen

Entero

Por valor

Longitud del dato de lectura

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de documentos.

**Ejemplo:**
Lee Dato Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fLeeDatoDocumento recibe VAR aCampo: CADENA, REFERENCIA aValor: CADENA,
VAR aLongitud: ENTERO
FIN SI
}
```

fSiguienteFolio ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSiguienteFolio(aCodigoConcepto, aSerie, aFolio )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoConcepto

Cadena

Por valor

Código del concepto del documento.

aSerie

Cadena

Por referencia

Serie del documento.

aFolio

Doble

Por referencia

Folio del documento.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aSerie: Al finalizar la función este parámetro contiene el valor de la serie del documento especificado.
aFolio: Al finalizar la función este parámetro contiene el siguiente folio del documento especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de documentos.

**Ejemplo:**
Siguiente Folio aSerie: SI Error <> 0 Error fSiguienteFolio

```pascal
{
VAR Error: ENTERO
Error = fSiguienteFolio recibe VAR aCodConcepto: CADENA, REFERENCIA
CADENA, REFERENCIA aFolio: DOUBLE
ENTONCES
SI NO
FIN SI
}
```

```pascal
fSetFiltroDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetFiltroDocumento(aFechaInicio, aFechaFin, aCodigoConcepto, aCodigoCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aFechaInicio

Cadena

Por valor

Fecha inicial del rango.

aFechaFin

Cadena

Por valor

Fecha final del rango.

aCodigoConcepto

Cadena

Por valor

Código del concepto a filtrar.

aCodigoCteProv

Cadena

Por valor

Código del Cliente/Proveedor a filtrar.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función aplica un filtro a los documentos de acuerdo a su código y al código del cliente/proveedor en un rango de fechas especificados.

**Ejemplo:**
Set Filtro Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error fSetFiltroDocumento recibe VAR aFechaInicio: CADENA, VAR aFechaFin:
CADENA, VAR aCodigoConcepto: CADENA, VAR aCodigoCteProv: CADENA
ENTONCES
SI NO
fSetFiltroDocumento
FIN SI
}
```

fCancelaFiltroDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelaFiltroDocumento ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cancela el ultimo filtro activo de documentos.

**Ejemplo:**
Cancela Filtro Documento Error = fCancelaFiltroDocumento SI Error <> 0 Error fCancelaFiltroDocumento

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fDocumentoImpreso ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fDocumentoImpreso (aImpreso)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aImpreso

Lógico (bool)

Por referencia

Valor lógico.
Verdadero o falso.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cambia la bandera de documento impreso.
Es necesario estar en el registro del documento que se quiere actualizar la bandera.

**Ejemplo:**
Documento Impreso Error = fDocumentoImpreso recibe REFERENCIA aImpreso: BOOL SI Error <> 0 Error fDocumentoImpreso

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

---

### 7.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscarDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscarDocumento (aCodConcepto, aSerie, aFolio)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por valor

Código del concepto del documento.

aSerie

Cadena

Por valor

Serie del documento.

aFolio

Cadena

Por valor

Folio del documento.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un documento por su llave, si lo encuentra se posiciona en el registro correspondiente.

**Ejemplo:**
Buscar Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fBuscarDocumento
FIN SI
}
```

```pascal
fBuscarIdDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscarIdDocumento (aIdDocumento)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDocumento

Entero

Por valor

Identificador del documento.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un documento por su identificador.

**Ejemplo:**
Buscar Id Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarIdDocumento recibe VAR aIdDocumento: ENTERO
ENTONCES
SI NO
fBuscarIdDocumento
FIN SI
}
```

fPosPrimerDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerDocumento ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el primer registro de la tabla de documentos.

**Ejemplo:**
Posicionar Primer Documento Error = fPosPrimerDocumento SI Error <> 0 Error fPosPrimerDocumento

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosUltimoDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoDocumento ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el último registro de la tabla de documentos.

**Ejemplo:**
Posicionar Ultimo Documento Error = fPosUltimoDocumento SI Error <> 0 Error fPosUltimoDocumento

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosSiguienteDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteDocumento ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de documentos.

**Ejemplo:**
Posicionar Siguiente Documento Error = fPosSiguienteDocumento SI Error <> 0 Error fPosSiguienteDocumento

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosAnteriorDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorDocumento ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de documentos.

**Ejemplo:**
Posicionar Anterior Documento Error = fPosAnteriorDocumento SI Error <> 0 Error fPosAnteriorDocumento

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosBOF ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOF ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Documentos.

**Ejemplo:**
Posicionar BOF Error = fPosBOF SI Error <> 0 Error fPosBOF

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosEOF ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOF ()`

- **Parámetros:** No usa

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Documentos.

**Ejemplo:**
Posicionar EOF Error = fPosEOF SI Error <> 0 Error fPosEOF

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

---

### 7.3 Alto nivel – Lectura/Escritura 

#### 7.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaDocumento (aIdDocumento, aDocumento )`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdDocumento: Al finalizar la función este parámetro contiene el identificador del nuevo documento.

**Descripción:**
Esta función da de alta documentos de cargo o abono.

**Ejemplo:**
AltaDocumento REFERENCIA tDocumento: SDK REFERENCIA tDocumento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fSiguienteFolio recibe VAR aCodigoConcepto: CADENA,
REFERENCIA aSerie: CADENA, REFERENCIA aFolio: DOUBLE
VAR aCodConcepto: tDocumento
VAR aSerie: tDocumento
VAR aCodClienteProveedor: tDocumento
VAR aFecha: tDocumento
Error = fAltaDocumento recibe REFERENCIA aIdDocumento: ENTERO,
ENTONCES
SI NO
fAltaDocumento
FIN SI
}
```

```pascal
fAltaDocumentoCargoAbono ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaDocumentoCargoAbono (aDocumento)`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función da de alta documentos de cargo o abono.

**Ejemplo:**
Documento Cargo Abono REFERENCIA tDocumento: SDK tDocumento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aCodConcepto: tDocumento
VAR aFolio: tDocumento
VAR aSerie: tDocumento
VAR aFecha: tDocumento
VAR aCodClienteProveedor: tDocumento
VAR aNumMoneda: tDocumento
VAR aTipoCambio: tDocumento
VAR aImporte: tDocumento
Error = fAltaDocumentoCargoAbono recibe REFERENCIA
ENTONCES
SI NO
fAltaDocumentoCargoAbono
FIN SI
}
```

fAfectaDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAfectaDocto (aLlaveDocto, aAfecta)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aLlaveDocto

tLlaveDocto

Por valor

Tipo de dato abstracto.

aAfecta

Lógico (Bool)

Por valor

Verdadero o falso. 
Afectar o desafectar.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función utiliza aLlaveDocto como llave del documento y aAfecta para afectar o desafectarlo.

**Ejemplo:**
Afecta Documento REFERENCIA lLlaveDocto: SDK SI Error <> 0 Error fAfectaDocto

```pascal
{
VAR Error: ENTERO
VAR aCodConcepto: tLlaveDocto
VAR aSerie: tLlaveDocto
VAR aFolio: tLlaveDocto
Error = fAfectaDocto recibe REFERENCIA tLlaveDocto, VAR aAfecta: BOOL)
ENTONCES
SI NO
FIN SI
}
```

fSaldarDocumento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSaldarDocumento (aDoctoaPagar, aDoctoPago, aImporte, aIdMoneda, aFecha)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aDoctoaPagar

tLlaveDocto

Por valor

Tipo de dato abstracto.

aDoctoPago

tLlaveDocto

Por valor

Tipo de dato abstracto.

aImporte

Doble

Por valor

Importe del pago.

aIdMoneda

Entero

Por valor

Moneda del pago.

aFecha

Cadena

Por valor

Fecha del pago.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función asocia documentos y salda sus importes.

**Ejemplo:**
Saldar Documento Error = fSaldarDocumento recibe REFERENCIA astDoctoaPagar, SI Error <> 0 Error fSaldarDocumento

```csharp
{
REFERENCIA astDoctoaPagar : SDK.RegLlavedoc
REFERENCIA astDoctoPago : SDK.RegLlavedoc
VAR Error: ENTERO
REFERENCIA astDoctoPago, VAR aImporte: DOUBLE, VAR aMoneda:
ENTERO, VAR aFecha: CADENA
ENTONCES
SI NO
FIN SI
}
```

fBorrarAsociacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorrarAsociacion (aDoctoaPagar, aDoctoPago)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aDoctoaPagar

tLlaveDocto

Por valor

Tipo de dato abstracto.

aDoctoPago

tLlaveDocto

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función la asociación de documentos.

**Ejemplo:**
Borrar Asociación Error = fBorrarAsociacion recibe REFERENCIA astDocAPagar, REFERENCIA astDocPago SI Error <> 0 Error fBorrarAsociacion

```csharp
{
REFERENCIA astDoctoaPagar : SDK.RegLlavedoc
REFERENCIA astDoctoPago : SDK.RegLlavedoc
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fRegresaIVACargo ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaIVACargo (aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasaCero, aNetoTasaExcenta, aNetoOtrasTasas, aIVATasa15, aIVATasa10, aIVAOtrasTasas)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aLlaveDocto

tLlaveDocto

Por valor

Tipo de dato abstracto.

aNetoTasa15

Doble

Por referencia

Base de la tasa de 15%

aNetoTasa10

Doble

Por referencia

Base de la tasa de 10%

aNetoTasaCero

Doble

Por referencia

Base de la tasa cero

aNetoTasaExcenta

Doble

Por referencia

Base de productos exentos

aNetoOtrasTasas

Doble

Por referencia

Base de otras tasas

aIVATasa15

Doble

Por referencia

IVA de la tasa de 15%

aIVATasa10

Doble

Por referencia

IVA de la tasa de 10%

aIVAOtrasTasas

Doble

Por referencia

IVA de otras tasas

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función regresa el desglose de IVA de un documento.

**Ejemplo:**
Regresa IVA Cargo REFERENCIA RegLlavedoc: SDK Error = fRegresaIVACargo recibe REFERENCIA RegLlavedoc, SI Error <> 0 Error fRegresaIVACargo

```pascal
{
VAR Error: ENTERO
VAR aNetoTasa15: DOUBLE, VAR aNetoTasa10: DOUBLE, VAR
aNetoTasaCero: DOUBLE, VAR aNetoTasaExcenta: DOUBLE, VAR
aNetoOtrasTasas: DOUBLE, VAR aIVATasa15: DOUBLE, VAR aIVATasa10:
DOUBLE, VAR aIVAOtrasTasas: DOUBLE
ENTONCES
SI NO
FIN SI
}
```

fGetTamSelloDigitalYCadena ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGetTamSelloDigitalYCadena (atPtrPassword, aEspSelloDig, aEspCadOrig)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

atPtrPassword

Cadena

Por referencia

Contraseña del certificado.

aEspSelloDig

Entero

Por referencia

Tamaño del Sello digital.

aEspCadOrig

Entero

Por referencia

Tamaño de la Cadena original.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Con esta función se obtiene el tamaño de la cadena original y el sello digital, mismas que se guardarán en las variables aEspSelloDig y aEspCadOrig.

**Ejemplo:**
Get Tamaño Sello Digital y Cadena SI Error <> 0 Error fGetTamSelloDigitalYCadena recibe REFERENCIA atPtrPassword:

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
CADENA, REFERENCIA aEspSelloDig: ENTERO, REFERENCIA
aEspCadOrig: ENTERO
FIN SI
}
```

fGetSelloDigitalYCadena ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGetSelloDigitalYCadena (char *atPtrPassword, char* atPtrSelloDigital, char* atPtrCadenaOriginal)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

atPtrPassword

Cadena

Por referencia

Contraseña del certificado.

atPtrSelloDigital

Cadena

Por referencia

Sello digital.

atPtrCadenaOriginal

Cadena

Por referencia

Cadena original.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Con esta función se obtiene el sello digital y la cadena original de un CFD.

**Ejemplo:**
Get Sello Digital y Cadena SI Error <> 0 Error fGetSelloDigitalYCadena

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumen recibe REFERENCIA atPtrPassword:
CADENA, REFERENCIA aPtrSelloDigital: CADENA, REFERENCIA
aPtrCadenaOriginal: CADENA
ENTONCES
SI NO
FIN SI
}
```

fInicializaLicenseInfo()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInicializaLicenseInfo (aSistema)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aSistema

Unsigned char 

Por valor

Sistema:
1 = CONTPAQi Factura Electrónica®

- **Retorna:** Valores enteros:
kSIN_ERRORES = • 0 (cero) que significa que se pudo conectar y obtener información del Servidor de Licencias, aunque la verificación del número de usuarios se hace hasta el uso de la función fEmitirDocumento.
!kSIN_ERRORES = • -1 que significa que hubo un error al intentar obtener información del Servidor de Licencias del sistema especificado.

**Descripción:**
Esta función verifica que el sistema esté activado y tenga una licencia válida.

**Ejemplo:**
Inicializa Licence Info SI Error <> 0 Error fInicializaLicenseInfo

```pascal
{
VAR Error: ENTERO
Error = fInicializaLicenseInfo recibe VAR aSistema:BYTE
ENTONCES
SI NO
FIN SI
}
```

```pascal
fEmitirDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEmitirDocumento (aCodConcepto, aSerie, aFolio, aPassword, aArchivoAdicional)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por referencia

Código del concepto

aSerie

Cadena

Por referencia

Serie del documento

aFolio

Doble

Por valor

Folio del documento

aPassword

Cadena

Por referencia

Contraseña del certificado de sello digital

aArchivoAdicional

Cadena

Por referencia

Nombre del archivo con el complemento, este archivo ya debe existir en la carpeta “Adicionales” dentro de la empresa.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) si no hubo error.
!kSIN_ERRORES = -1 que significa que hubo un error con la Licencia (la licencia es para menos de 10 usuarios, es temporal, de evaluación, no está activada, etc.)
!kSIN_ERRORES = Un número de error positivo del que se puede obtener la descripción con la función fError.

**Descripción:**
Para poder utilizar la función fEmitirDocumento, se deberá ejecutar primero la función fInicializaLicenseInfo.
Esta función requiere una liciencia monousuario. Si cuentas con un licenciamiento anual además se requeire que la licencia sea multiempresa.
 
Esta función solo soporta las divisas, EstadoDeCuentaBancario, EstadoDeCuentaCombustible, PrestadoresDeServiciosDeCFD y la combinacion de estos.

**Ejemplo:**
Emitir Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fInicializaLicenseInfo recibe VAR aSistema:BYTE
ENTONCES
SI NO
fEmitirDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: DOUBLE, VAR aPassword: CADENA,
VAR aArchivoAdicional: CADENA
FIN SI
}
```

fDocumentoUUID()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fDocumentoUUID (aCodigoConcepto, aSerie, aFolio, atPtrCFDIUUID)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por referencia

Código del concepto

aSerie

Cadena

Por referencia

Serie del documento

aFolio

Doble

Por valor

Folio del documento

atPtrCFDIUUID

Cadena

Por referencia

Cadena para colocar el valor de UUID

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función despliega el UUID de un documento.

**Ejemplo:**
Documento UUID SI Error <> 0 Error fDocumentoUUID

```pascal
{
VAR Error: ENTERO
Error = fDocumentoUUID recibe REFERENCIA aCodConcepto: CADENA,
REFERENCIA aSerie: CADENA, VAR aFolio: DOUBLE, REFERENCIA
atPtrCFDIUUID: CADENA
ENTONCES
SI NO
FIN SI
}
```

fGetSerieCertificado ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGetSerieCertificado (atPtrPassword, aPtrSerieCertificado)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

atPtrPassword

Cadena

Por referencia

Contraseña del certificado

aPtrSerieCertificado

Cadena

Por referencia

Serie del certificado

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función regresa la serie de un certificado utilizado por una factura electrónica.

**Ejemplo:**
Get Serie Certificado SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
fGetSerieCertificado recibe REFERENCIA atPtrPassword: CADENA,
REFERENCIA aSerieCertificado: CADENA
FIN SI
}
```

fActivarPrecioCompra ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fActivarPrecioCompra (aActivar)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aActivar

Entero

Por valor

0 = No busca el precio
1 = Valor asumido (busca el precio)

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función determina si al momento de registrar una compra vía SDK se ejecutará la función que busca el último precio de compra registrado en caso de que el precio sea igual a cero.

fDocumentoDevuelto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fDocumentoDevuelto (aDevuelto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aDevuelto

Entero

Por valor

0 = No busca el precio
1 = Valor asumido (busca el precio)

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función ajusta el estado de un documento en devuelto o no devuelto.

**Ejemplo:**
Documento Devuelto SI Error <> 0 Error fDocumentoDevuelto

```pascal
{
VAR Error: ENTERO
Error = fDocumentoDevuelto recibe REFERENCIA aDevuelto: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fEntregEnDiscoXML ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEntregEnDiscoXML (aCodConcepto, aSerie, aFolio, aFormato, aFormatoAmig)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por referencia

Código del concepto

aSerie

Cadena

Por referencia

Serie del documento

aFolio

Doble

Por valor

Folio del documento

aFormato

Entero

Por valor

Formato de entrega (0 = XML, 1
= PDF)
 
Nota: Al seleccionar la opción de entrega 1= PDF, por disposición fiscal también se generará el XML.

aFormatoAmig

Cadena

Por referencia

Plantilla de impresión

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función entrega el XML en un archivo.

**Ejemplo:**
Entrega en Disco XML SI Error <> 0 Error fEntregEnDiscoXML

```pascal
{
VAR Error: ENTERO
Error = fEntregEnDiscoXML recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: DOUBLE, VAR aFormato: ENTERO, VAR
aFormatoAmigable: CADENA
ENTONCES
SI NO
FIN SI
}
```

fObtieneDatosCFDI ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fDocumentoDevuelto (aDevuelto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

atPtrPassword

Cadena

Por referencia

Contraseña del certificado

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
La función fObtieneDatosCFDI obtiene los datos del CFDI del documento previamente definido con la función fBuscarDocumento. Esta función almacena en variables globales los datos del CFDI dentro del mismo SDK para posteriormente ser leídos con la función fLeeDatoCFDI.

**Ejemplo:**
Obtiene y Lee Dato CFDI SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
Error = fObtieneDatosCFDI recibe VAR atPtrPassword
ENTONCES
SI NO
Error = fLeeDatoCFDI recibe REFERENCIA aValor: CADENA,
VAR aDato: ENTERO
ENTONCES
SI NO
fLeeDatoCFDI
FIN SI
FIN SI
FIN SI
}
```

```pascal
fLeeDatoCFDI ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoCFDI (aValor, aDato)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aValor

Cadena

Por referencia

Cadena donde se regresará el
dato requerido

aDato

Entero

Por valor

1 = Serie del Certificado del Emisor
2 = Folio Fiscal (UUID)
3 = Número de Serie del Certificado del SAT
4 = Fecha y Hora de Certificación
5 = Sello Digital del CFDI
6 = Sello SAT
7 = Cadena Original del Complemento de Certificación Digital del SAT
8 = Método de Pago
9 = Lugar de expedición
10 = Régimen Fiscal
11 = Folio Fiscal de origen*
12 = Serie del Folio Fiscal de origen*
13 = Fecha del Folio Fiscal de origen*
14 = Monto del Folio Fiscal de origen*
 
* Para documentación de Deuda o Pago en Parcialidades

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
La función fLeeDatoCFDI lee los datos previamente accedidos con la función fObtieneDatosCFDI.
 
La función recibe como parámetros, la cadena donde copiará el dato requerido y un entero donde se indica qué dato se desea y regresará un número de error en caso de existir alguno.

**Ejemplo:**
Obtiene y Lee Dato CFDI SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR
aSerie: CADENA, VAR aFolio: CADENA
ENTONCES
SI NO
Error = fObtieneDatosCFDI recibe VAR atPtrPassword
ENTONCES
SI NO
Error = fLeeDatoCFDI recibe REFERENCIA aValor: CADENA,
VAR aDato: ENTERO
ENTONCES
SI NO
fLeeDatoCFDI
FIN SI
FIN SI
FIN SI
}
```

---

### 7.4 Alto nivel - Búsqueda/Navegación

```pascal
fBuscaDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaDocumento (aLlaveDocto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aLlaveDocto

tLlaveDocto

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un documento por su llave, si lo encuentra se posiciona en el registro correspondiente.

**Ejemplo:**
BuscaDocumento REFERENCIA RegLlaveDoc: SDK SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR RegLlaveDoc.aCodConcepto: CADENA
VAR RegLlaveDoc.aSerie: CADENA
VAR RegLlaveDoc.aFolio: DOUBLE
Error = fBuscaDocumento recibe REFERENCIA RegLlaveDoc
ENTONCES
SI NO
fBuscaDocumento
FIN SI
}
```

---

## 8. Funciones de movimientos

---

### 8.1 Bajo nivel – Lectura/Escritura 

#### 8.1.1 Bajo nivel – Lectura/Escritura

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertarMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Adiciona un nuevo registro en la tabla de Movimientos en modo de inserción.

**Ejemplo:**
Insertar Movimiento Error = fInsertarDocumento Error = fInsertarMovimiento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
fSetDatoMovimiento recibe VAR aCampo: CADENA, VAR aValor:CADENA
fGuardaMovimiento
FIN SI
}
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditarMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Movimientos.

**Ejemplo:**
Editar Movimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:CADENA, VAR aFolio: CADENA
Error = fBuscarMovimiento recibe VAR aIdMovimiento: ENTERO
ENTONCES
SI NO
Error = fEditarMovimiento
ENTONCES
SI NO
fSetDatoMovimiento recibe VAR aCampo: CADENA, VAR aValor: CADENA
fGuardaMovimiento
FIN SI
FIN SI
}
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Guarda los cambios realizados a un movimiento.

**Ejemplo:**
SI Error <> 0 Error

```pascal
El siguiente código indica a la aplicación que guarde cierto registro en la tabla de Documentos. Esta función se llama después de que se utiliza la función fInsertarMovimiento() o fEditarMovimiento() y se graban los valores en los campos correspondientes.
Guarda Movimiento{
VAR Error: ENTERO
fBuscarDocumento
fBuscarIdMovimiento
ENTONCES
SI NO
fEditarMovimiento
fSetDatoMovimiento
fGuardaMovimiento
FIN SI
}
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelaCambiosMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función cancela las modificaciones al registro actual de movimientos. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
Cancela Cambios Movimiento SI Error <> 0 Error SI Error <> 0 Error fCancelaCambiosMovimiento

```pascal
{
VAR Error: ENTERO
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA, VAR aFolio: CADENA
Error = fBuscarMovimiento recibe VAR aIdMovimiento: ENTERO
ENTONCES
SI NO
Error = fEditarIdMovimiento
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoCaracteristicas_Param (aIdMovimiento, aIdMovtoCaracteristicas, aUnidades, 
aValorCaracteristica1, aValorCaracteristica2, 
aValorCaracteristica3)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Cadena

Por valor

Identificador del movimiento.

aIdMovtoCaracteristicas

Cadena

Por valor

Identificador del movimiento con características.

aUnidades

Cadena

Por valor

Unidades.

aValorCaracteristica1

Cadena

Por valor

Valor de la característica 1.

aValorCaracteristica2

Cadena

Por valor

Valor de la característica 2.

aValorCaracteristica3

Cadena

Por valor

Valor de la característica 3.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función inserta un movimiento con características.

**Ejemplo:**
Alta Movimiento Caracteristicas Parametros SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie:
CADENA, VAR aFolio: CADENA
Error = fAltaMovimientoCaracteristicas_Param recibe VAR aIdMovimiento:
CADENA, VAR aIdMovtoCaracteristicas: CADENA, VAR aUnidades: CADENA,
VAR aValorCaracteristica1: CADENA, VAR aValorCaracteristica2: CADENA,
VAR aValorCaracteristica3: CADENA
ENTONCES
SI NO
fAltaMovimientoCaracteristicas_Param
FIN SI
}
```

```pascal
fAltaMovtoCaracteristicasUnidades_Param ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovtoCaracteristicasUnidades_Param (aIdMovimiento, aIdMovtoCaracteristicas,
aUnidad, aUnidades, aUnidadesNC, aValorCaracteristica1, aValorCaracteristica2, aValorCaracteristica3)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Cadena

Por valor

Identificador del movimiento.

aIdMovtoCaracteristicas

Cadena

Por valor

Identificador del movimiento con características.

aUnidad

Cadena

Por valor

Abreviatura de la unidad de compra venta

aUnidades

Cadena

Por valor

Las unidades del movimiento de características.

aUnidadesNC

Cadena

Por valor

Abreviatura de la unidad de compra venta no convertible.

aValorCaracteristica1

Cadena

Por valor

Valor de la característica 1.

aValorCaracteristica2

Cadena

Por valor

Valor de la característica 2.

aValorCaracteristica3

Cadena

Por valor

Valor de la característica 3.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función da de alta movimiento de características con unidades de compra venta.

**Ejemplo:**
Alta Movimiento Caracteristicas Parametros SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie
CADENA, VAR aFolio: CADENA
Error = fAltaMovtoCaracteristicasUnidades_Param recibe VAR
aIdMovimiento: CADENA, VAR aIdMovtoCaracteristicas: CADENA, VAR
aUnidad: CADENA, VAR aUnidades: CADENA, VAR aUnidadesNC: CADENA,
VAR aValorCaracteristica1: CADENA, VAR aValorCaracteristica2: CADENA,
VAR aValorCaracteristica3: CADENA
ENTONCES
SI NO
fAltaMovtoCaracteristicasUnidades_Param
FIN SI
}
```

```pascal
fAltaMovimientoSeriesCapas_Param ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoSeriesCapas _Param (aIdMovimiento, aUnidades, aTipoCambio, aSeries,
aPedimento, aAgencia, aFechaPedimento, aNumeroLote, aFechaFabricacion, aFechaCaducidad)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Cadena

Por valor

Identificador del movimiento.

aUnidades

Cadena

Por valor

Unidad de peso y medida.

aTipoCambio

Cadena

Por valor

Tipo de cambio.

aSeries

Cadena

Por valor

Series.

aPedimento

Cadena

Por valor

Referencia del pedimento.

aAgencia

Cadena

Por valor

Referencia de la agencia.

aFechaPedimento

Cadena

Por valor

Fecha del pedimento.

aNumeroLote

Cadena

Por valor

Número de lote.

aFechaFabricacion

Cadena

Por valor

Fecha de fabricación.

aFechaCaducidad

Cadena

Por valor

Fecha de caducidad.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función agrega el movimiento de numero de serie, lote y/o pedimento asociados un movimiento cuyo producto maneje cualquiera de estas posibles configuraciones.

**Ejemplo:**
Alta Movimiento Series Capas Parametros SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fAltaDocumento
fAltaMovimiento
Error = fAltaMovimientoSeriesCapas_Param recibe VAR aIdMovimiento:
CADENA, VAR aUnidades: CADENA, VAR aTipoCambio: CADENA, VAR
aSeries: CADENA, VAR aPedimento: CADENA, VAR aAgencia: CADENA, VAR
aFechaPedimento: CADENA, VAR aNumeroLote: CADENA, VAR
aFechaFabricacion: CADENA, VAR aFechaCaducidad: CADENA
ENTONCES
SI NO
fAltaMovimientoSeriesCapas_Param
FIN SI
}
```

fCalculaMovtoSerieCapa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCalculaMovtoSerieCapa (aIdMovimiento)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Eentero largo

Por valor

Identificador del movimiento a recalcular.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función recalcula el movimiento cuando este pertenece a un producto con series, lotes o pedimentos.

**Ejemplo:**
Calcula Movimiento Series Capas SI Error <> 0 Error fCalculaMovtoSerieCapa

```pascal
{
VAR Error: ENTERO
fAltaDocumento
fAltaMovimiento
fAltaMovimientoSeriesCapas_Param
Error = fCalculaMovtoSerieCapa recibe VAR lIdMovimiento: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fObtieneUnidadesPendientes ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fObtieneUnidadesPendientes (aConceptoDocto, aCodigoProducto, aCodigoAlmacen, Unidades)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aConceptoDocto

Cadena

Por valor

Código del concepto del documento a buscar.

aCodigoProducto

Cadena

Por valor

Código del producto a buscar su unidades pendientes.

aCodigoAlmacen

Cadena

Por valor

Código del almacén a buscar si es igual a 0 (cero) busca en todos los almacenes.

aUnidades

Cadena

Por referencia

Valor de retorno con las unidades pendientes.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error. 
 
aUnidades: Al finalizar la función este parámetro contiene las unidades pendientes.

**Descripción:**
Esta función obtiene la cantidad de unidades pendientes de cierto concepto de documento para un almacén/almacenes de un determinado producto en toda la historia del sistema.

**Ejemplo:**
Obtiene Unidades Pendientes SI Error <> 0 Error fObtieneUnidadesPendientes

```pascal
{
VAR Error: ENTERO
Error = fObtieneUnidadesPendientes recibe VAR aConceptoDocto: CADENA,
VAR aCodigoProducto: CADENA, VAR aCodigoAlmacen: CADENA,
REFERENCIA: aUnidades: CADENA
ENTONCES
SI NO
FIN SI
}
```

fObtieneUnidadesPendientesCarac ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fObtieneUnidadesPendientesCarac (aConceptoDocto, aCodigoProducto, aCodigoAlmacen,
aValorCaracteristica1, aValorCaracteristica2, aValorCaracteristica3, aUnidades)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aConceptoDocto

Cadena

Por valor

Código del concepto del documento a buscar.

aCodigoProducto

Cadena

Por valor

Código del producto a buscar su unidades pendientes.

aCodigoAlmacen

Cadena

Por valor

Código del almacén a buscar si es igual a 0 (cero) busca en todos los almacenes.

aValorCaracteristica1

Cadena

Por valor

Valor característica 1

aValorCaracteristica2

Cadena

Por valor

Valor característica 2

aValorCaracteristica3

Cadena

Por valor

Valor característica 3

aUnidades

Cadena

Por referencia

Valor de retorno con las unidades pendientes.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error. 
 
aUnidades: Al finalizar la función este parámetro contiene las unidades pendientes.

**Descripción:**
Esta función obtiene la cantidad de unidades pendientes de cierto concepto de documento para un almacén/almacenes de un determinado producto con caracteristicas en toda la historia del sistema.

**Ejemplo:**
Obtiene Unidades Pendientes Caracteristicas SI Error <> 0 Error fObtieneUnidadesPendientesCarac

```pascal
{
VAR Error: ENTERO
Error = fObtieneUnidadesPendientesCarac recibe VAR aConceptoDocto:
CADENA, VAR aCodigoProducto: CADENA, VAR aCodigoAlmacen: CADENA,
VAR aValorCaracteristica1: CADENA, VAR aValorCaracteristica2: CADENA,
VAR aValorCaracteristica3: CADENA, REFERENCIA: aUnidades: CADENA
ENTONCES
SI NO
FIN SI
}
```

fModificaCostoEntrada ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fModificaCostoEntrada (aIdMovimiento, aCostoEntrada)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Cadena

Por valor

Identificador del movimiento a
modificar.

aCostoEntrada

Cadena

Por valor

Valor del costo a asignar al
movimiento.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función modifica el costo de una entrada de inventario.

**Ejemplo:**
Modifica Costo Entrada SI Error <> 0 Error fModificaCostoEntrada

```pascal
{
VAR Error: ENTERO
Error = fModificaCostoEntrada recibe VAR aIdMovimiento: CADENA, VAR
aCostoEntrada: CADENA
ENTONCES
SI NO
FIN SI
}
```

```pascal
fSetDatoMovimiento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoMovimiento (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de documentos.

**Ejemplo:**
Set Dato Movimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento
fBuscarIdMovimiento
Error = fEditarMovimiento
ENTONCES
SI NO
Error = fSetDatoMovimiento recibe VAR aCampo: CADENA, VAR aValor: CADENA
ENTONCES
SI NO
fGuardaMovimiento
FIN SI
FIN SI
}
```

```pascal
fLeeDatoMovimiento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoMovimiento (aCampo, aValr, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por referencia

Valor de escritura

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de documentos.

**Ejemplo:**
Lee Dato Movimiento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
fBuscarDocumento
fBuscarIdMovimiento
Error = fLeeDatoMovimiento recibe VAR aCampo: CADENA, REFERENCIA
aValor: CADENA, VAR aLen: ENTERO
ENTONCES
SI NO
fLeeDatoMovimiento
FIN SI
}
```

---

### 8.2 Bajo nivel - Búsqueda/Navegación

```pascal
fSetFiltroMovimiento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetFiltroMovimiento(aIdDocumento )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDocumento

Long

Por valor

Identificador del documento.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función aplica un filtro de movimientos de acuerdo al documento indicado.

**Ejemplo:**
Set Filtro Movimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
Error = fSetFiltroMovimiento recibe VAR aIdDocumento: ENTERO
ENTONCES
SI NO
fSetFiltroMovimiento
FIN SI
FIN SI
}
```

fCancelaFiltroMovimiento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelaFiltroMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función aplica un filtro de movimientos de acuerdo al documento indicado.

**Ejemplo:**
Cancela Filtro Movimiento SI Error <> 0 Error SI Error <> 0 Error Error = fCancelaFiltroMovimiento SI Error <> 0 Error fCancelaFiltroMovimiento

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
Error = fSetFiltroMovimiento recibe VAR aIdDocumento: ENTERO
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
FIN SI
}
```

```pascal
fBuscarIdMovimiento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscarIdMovimiento (aIdMovimiento)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aaIdMovimiento

Entero largo

Por valor

Identificador del documento.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un movimiento por su identificador. Si lo encuentra se posiciona en el registro correspondiente.

**Ejemplo:**
Buscar Id Movimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
Error = fBuscarIdMovimiento recibe VAR aIdMovimiento: ENTERO
ENTONCES
SI NO
fBuscarIdMovimiento
FIN SI
FIN SI
}
```

fPosPrimerMovimiento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el primer registro de la tabla de movimientos.

**Ejemplo:**
Posicionar Primer Movimiento SI Error <> 0 Error Error = fPosPrimerMovimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
Error = fLeeDatoMovimiento recibe VAR aCampo: CADENA, REFERENCIA aValor,
VAR aLen: ENTERO
ENTONCES
SI NO
fLeeDatoMovimiento
FIN SI
FIN SI
FIN SI
}
```

fPosUltimoMovimiento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el último registro de la tabla de documentos.

**Ejemplo:**
Posicionar Ultimo Movimiento SI Error <> 0 Error Error = fPosUltimoMovimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
Error = fLeeDatoMovimiento recibe VAR aCampo: CADENA, REFERENCIA
aValor, VAR aLen: ENTERO
ENTONCES
SI NO
fLeeDatoMovimiento
FIN SI
FIN SI
FIN SI
}
```

fPosSiguienteMovimiento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de documentos.

**Ejemplo:**
Posicionar Siguiente Movimiento SI Error <> 0 Error Error = fPosSiguienteMovimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
Error = fLeeDatoMovimiento recibe VAR aCampo: CADENA, REFERENCIA aValor,
VAR aLen: ENTERO
ENTONCES
SI NO
fLeeDatoMovimiento
FIN SI
FIN SI
FIN SI
}
```

fPosAnteriorMovimiento ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorMovimiento ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de documentos.

**Ejemplo:**
Posicionar Anterior Movimiento SI Error <> 0 Error Error = fPosAnteriorMovimiento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
Error = fLeeDatoMovimiento recibe VAR aCampo: CADENA, REFERENCIA aValor,
VAR aLen: ENTERO
ENTONCES
SI NO
fLeeDatoMovimiento
FIN SI
FIN SI
FIN SI
}
```

fPosMovimientoBOF ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosMovimientoBOF ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Movimientos.

**Ejemplo:**
Posicionar Movimiento BOF SI Error <> 0 Error Error = fPosMovimientoBOF SI Error <> 0 Error fPosMovimientoBOF

```pascal
{
VAR Error: ENTERO
Error = fBuscarDocumento recibe VAR aCodConcepto: CADENA, VAR aSerie: CADENA,
VAR aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosMovimientoEOF ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosMovimientoEOF ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Documentos.

**Ejemplo:**
SI Error <> 0 Error Error = fPosMovimientoEOF SI Error <> 0 Error fPosMovimientoEOF

```pascal
Posicionar Movimiento EOF{
VAR Error: ENTERO
Error = fBuscarDocumento recibe PARAMETRO aCodConcepto: CADENA, PARAMETRO aSerie: CADENA, PARAMETRO aFolio: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

---

### 8.3 Alto nivel – Lectura/Escritura 

#### 8.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaMovimiento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimiento (aIdDocumento, aIdMovimiento, astMovimiento)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDocumento

Entero largo

Por valor

Identificador del movimiento.

aIdMovimiento

Entero largo

Por referencia

Identificador del documento.

astMovimiento

tMovimiento

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdMovimiento: Al finalizar la función este parámetro contiene el identificador del nuevo movimiento.

**Descripción:**
Esta función da de alta un nuevo registro en la tabla de Movimientos.

**Ejemplo:**
OBJETO aMovimiento: tMovimiento

```pascal
Crear_movimiento( recibe ENTERO idDocumento )
{
VAR idMovimiento: ENTERO
VAR aConsecutivo : aMovimiento
VAR aUnidades : aMovimiento
VAR aPrecio : aMovimiento
VAR aCosto : aMovimiento
VAR aCodProdSer : aMovimiento
VAR aCodAlmacen : aMovimiento
VAR aReferencia : aMovimiento
VAR aCodClasificacion : aMovimiento
regresar fAltaMovimiento( recibe PARAMETRO idDocumento, REFERENCIA idMovimiento,
REFERENCIA aMovimiento);
}
```

```pascal
fAltaMovimientoEx ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoEx (aIdMovimiento, aTipoProducto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Entero largo

Por referencia

Identificador del documento.

aTipoProducto

tTipoProducto

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función da de alta de un movimiento los datos adicionales de un producto con series, lotes, pedimientos o características.

**Ejemplo:**
OBJETO aTipoProducto: tTipoProducto

```pascal
Crear_movimiento_Ex ( recibe ENTERO idMovimiento )
{
VAR aSeriesCapas : aTipoProducto
VAR aCaracteristicas : aTipoProducto
regresar fAltaMovimientoEx( recibe PARAMETRO idMovimiento,
REFERENCIA aTipoProducto);
}
```

```pascal
fAltaMovimientoCDesct ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoCDesct (aIdDocumento, aIdMovimiento, astMovimiento)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDocumento

Entero largo

Por valor

Identificador del documento.

aIdMovimiento

Entero largo

Por Referencia

Identificador del movimiento

astMovimiento

tMovmientoDesc

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función da de alta un nuevo registro en la tabla de Movimientos.
Esta función incluye Importes y Porcentajes de Descuentos, a diferencia de la función fAltaMovimiento.

**Ejemplo:**
OBJETO MovimientoCDesc: tMovimientoDesc

```pascal
Crear_movimiento_descuento( recibe ENTERO idDocumento )
{
VAR idMovimientoCDesc: ENTERO
VAR aConsecutivo : MovimientoCDesc
VAR aUnidades : MovimientoCDesc
VAR aPrecio : MovimientoCDesc
VAR aCosto : MovimientoCDesc
VAR aPorcDesct1 : MovimientoCDesc
VAR aImporteDesc1 : MovimientoCDesc
VAR aPorcDesct2 : MovimientoCDesc
VAR aImporteDesc2 : MovimientoCDesc
VAR aPorcDesct3 : MovimientoCDesc
VAR aImporteDesc3 : MovimientoCDesc
VAR aPorcDesct4 : MovimientoCDesc
VAR aImporteDesc4 : MovimientoCDesc
VAR aPorcDesct5 : MovimientoCDesc
VAR aImporteDesc5 : MovimientoCDesc
VAR aCodProdSer : MovimientoCDesc
VAR aCodAlmacen : MovimientoCDesc
VAR aReferencia : MovimientoCDesc
VAR aCodClasificacion : MovimientoCDesc
regresar fAltaMovimientoCDesct( recibe PARAMETRO idDocumento,
REFERENCIA idMovimientoCDesc, REFERENCIA MovimientoCDesc);
}
```

```pascal
fAltaMovimientoCaracteristicas ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoCaracteristicas (aIdMovimiento, aIdMovtoCaracteristicas, aCaracteristicas)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Entero largo

Por valor

Identificador del movimiento.

aIdMovtoCaracteristicas

Entero largo

Por referencia

Identificador del documento.

aCaracteristicas

tCaracteristicas

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdMovtoCaracteristicas: Al finalizar la función este parámetro contiene el identificador del nuevo movimiento.

**Descripción:**
Esta función inserta un movimiento con características.

**Ejemplo:**
OBJETO aCaracteristicas : tCaracterisiticas

```pascal
Crear_movimiento_caracteristicas( recibe ENTERO idMovimiento )
{
VAR idMovtoCaracteristicas: ENTERO
VAR aUnidades : aCaracteristicas
VAR aValorCaracteristica1 : aCaracteristicas
VAR aValorCaracteristica2 : aCaracteristicas
VAR aValorCaracterisitica3 : aCaracteristicas
regresar fAltaMovimientoCaracteristicas( recibe PARAMETRO idMovimiento,
REFERENCIA idMovtoCaracteristicas, REFERENCIA aCaracteristicas);
}
```

```pascal
fAltaMovtoCaracteristicasUnidades ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovtoCaracteristicasUnidades (aIdMovimiento, aIdMovtoCaracteristicas,
aCaracteristicasUnidades)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

Entero largo

Por valor

Identificador del movimiento.

aIdMovtoCaracteristicas

Entero largo

Por referencia

Identificador del documento.

aCaracteristicasUnidades

tCaracteristicasUnidades

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdMovtoCaracteristicas: Al finalizar la función este parámetro contiene el identificador del nuevo movimiento.

**Descripción:**
Esta función da de alta movimiento de características con unidades de compra venta.

**Ejemplo:**
OBJETO aCaracteristicasUnidades: tCaracteristicasUnidades

```pascal
Crear_movimiento_caracteristicasUnidades( recibe ENTERO idMovimiento )
{
VAR idMovtoCaracteristicas: ENTERO
VAR aUnidad : aCaracteristicasUnidades
VAR aUnidades : aCaracteristicasUnidades
VAR aUnidadesNC : aCaracteristicasUnidades
VAR aValorCaracteristica1 : aCaracteristicasUnidades
VAR aValorCaracteristica2 : aCaracteristicasUnidades
VAR aValorCaracteristica3 : aCaracteristicasUnidades
regresar fAltaMovimientoCaracteristicasUnidades( recibe PARAMETRO idMovimiento,
PARAMETRO idMovtoCaracteristicas, PARAMETRO aCaracteristicasUnidades);
}
```

```pascal
fAltaMovimientoSeriesCapas ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaMovimientoSeriesCapas (aIdMovimiento, aSeriesCapas)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdMovimiento

long

Por valor

Identificador del movimiento.

aSeriesCapas

tSeriesCapas

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función agrega el movimiento de número de serie, lote y/o pedimento asociados a un movimiento cuyo producto maneje cualquiera de estas posibles configuraciones.

**Ejemplo:**
OBJETO aSerieCapa: tSeriesCapas

```pascal
Crear_movimiento_SeriesCapas ( recibe ENTERO idMovimiento )
{
VAR aUnidades : aSerieCapa
VAR aTipoCambio : aSerieCapa
VAR aSeries : aSerieCapa
VAR aPedimento : aSerieCapa
VAR aAgencia : aSerieCapa
VAR aFechaPedimiento : aSerieCapa
VAR aNumeroLote : aSerieCapa
VAR aFechaFabricacion : aSerieCapa
VAR aFechaCaducidad : aSerieCapa
regresar fAltaMovimientoSeriesCapas( recibe PARAMETRO idMovimiento,
REFERENCIA aSerieCapa);
}
```

---

## 9. Funciones de timbrado

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fTimbraXML`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Se envía la información del XML para ser procesada por el PAC.

**Ejemplo:**
Timbra XML SI Error <> 0 Error

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema byte
VAR Error: ENTERO
VAR rutaXML: CADENA
VAR codConcepto: CADENA
VAR UUID: StringBuilder
VAR rutaDDA: CADENA
VAR rutaResultado: CADENA
VAR aPass: CADENA
VAR rutaFormato: CADENA
Error = fTimbraXML recibe rutaXML, codConcepto, UUID, rutaDDA, rutaResultado, aPass, rutaFormato
ENTONCES
SI NO
fTimbraXML
FIN SI
}
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fTimbraNominaXML`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Se envía la información del XML de nómina para ser procesada por el PAC.

**Ejemplo:**
Timbra Nomina XML SI Error <> 0 Error

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema byte
VAR Error: ENTERO
VAR rutaXML: CADENA
VAR codConcepto: CADENA
VAR UUID: StringBuilder
VAR rutaDDA: CADENA
VAR rutaResultado: CADENA
VAR aPass: CADENA
VAR rutaFormato: CADENA
Error = fTimbraNominaXML recibe rutaXML, codConcepto, UUID, rutaDDA, rutaResultado, aPass, rutaFormato
ENTONCES
SI NO
fTimbraNominaXML
FIN SI
}
```

---

## 10. Funciones de clientes/proveedores

---

### 10.1 Bajo nivel – Lectura/Escritura 

#### 10.1.1 Bajo nivel – Lectura/Escritura

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaCteProv ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Adiciona un nuevo registro en la tabla de Clientes / Proveedores en modo de inserción.

**Ejemplo:**
INSERTAR CLIENTE-PROVEEDOR error = ejecutar fInsertaCteProv SI error = 0 PARAMETRO aLong MIENTRAS aInserCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor FIN HACER FIN MIENTRAS

```pascal
{
VAR idCteProv CADENA(StringBuilder)
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aInserCampos ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor,
ejecutar fEditaCteProv
ejecutar fSetDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor
ejecutar fGuardaCteProv
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fEditaCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaCteProv ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Clientes/Proveedores.

**Ejemplo:**
INSERTAR CLIENTE-PROVEEDOR error = ejecutar fInsertaCteProv SI error = 0 PARAMETRO aLong MIENTRAS aInserCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor FIN HACER FIN MIENTRAS

```pascal
{
VAR idCteProv CADENA(StringBuilder)
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aInserCampos ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor,
ejecutar fEditaCteProv
ejecutar fSetDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor
ejecutar fGuardaCteProv
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos; algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fGuardaCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaCteProv ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Guarda los cambios realizados a un registro de cliente/proveedor.

**Ejemplo:**
INSERTAR CLIENTE-PROVEEDOR error = ejecutar fInsertaCteProv SI error = 0 PARAMETRO aLong MIENTRAS aInserCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor FIN HACER FIN MIENTRAS

```pascal
{
VAR idCteProv CADENA(StringBuilder)
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aInserCampos ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor,
ejecutar fEditaCteProv
ejecutar fSetDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor
ejecutar fGuardaCteProv
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

fBorraCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorraCteProv ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Borra un registro en la tabla de Clientes / Proveedores.

**Ejemplo:**
BORRAR CLIENTE-PROVEEDOR (recibe aIdCteProv) SI error = 0 ejecutar fBorrarCteProv

```pascal
{
VAR error ENTERO
error ejecutar fBuscaIdCteProv recibe PARAMETRO aIdCteProv
ENTONCES
FIN ENTONCES
}
```

fCancelarModificacionCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionCteProv ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función cancela las modificaciones al registro actual de Clientes / Proveedores. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
CANCELAR EDICION SI error = 0 MIENTRAS aEditCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor PARAMETRO aValor SI error <> 0 ejecutar fCancelarModificacionCteProv FIN HACER FIN MIENTRAS

```pascal
{
VAR aCodCteProv CADENA
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aEditCampos ENTERO
Error = ejecutar fBuscaCteProv recibe PARAMETRO aCodCteProv
ENTONCES
ejecutar fEditaCteProv
error = ejecutar fSetDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

fEliminarCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEliminarCteProv (aCodigoCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoCteProv

Cadena

Por valor

Código del Cliente / Proveedor.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función elimina un Cliente / Proveedor usando su código.

**Ejemplo:**
El siguiente código elimina un Cliente / Proveedor, si lo encuentra lo borra, en caso contrario envía el mensaje de error correspondiente: Elimina Cliente Proveedor SI Error <> 0 Error fEliminarCteProv

```pascal
{
VAR Error: ENTERO
Error = fEliminarCteProv recibe PARAMETRO aCodCliente: CADENA
ENTONCES
SI NO
FIN SI
}
```

```pascal
fSetDatoCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoCteProv (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de Cliente/Proveedor.

**Ejemplo:**
INSERTAR CLIENTE-PROVEEDOR error = ejecutar fInsertaCteProv SI error = 0 PARAMETRO aLong MIENTRAS aInserCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor FIN HACER FIN MIENTRAS

```pascal
{
VAR idCteProv CADENA(StringBuilder)
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aInserCampos ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor,
ejecutar fEditaCteProv
ejecutar fSetDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor
ejecutar fGuardaCteProv
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fLeeDatoCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoCteProv (aCampo, aValr, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de Cliente / Proveedor.

**Ejemplo:**
INSERTAR CLIENTE-PROVEEDOR error = ejecutar fInsertaCteProv SI error = 0 PARAMETRO aLong MIENTRAS aInserCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor FIN HACER FIN MIENTRAS

```pascal
{
VAR idCteProv CADENA(StringBuilder)
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aInserCampos ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor,
ejecutar fEditaCteProv
ejecutar fSetDatoCteProv recibe PARAMETRO aCampo, PARAMETRO aValor
ejecutar fGuardaCteProv
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

---

### 10.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscaCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaCteProv (aCodCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodCteProv

Cadena

Por valor

Código del Cliente/Proveedor.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un Cliente/Proveedor por su código.

**Ejemplo:**
CANCELAR EDICION SI error = 0 MIENTRAS aEditCampos > 0 HACER aCampo = nuevo campo aValor = nuevo valor PARAMETRO aValor SI error <> 0 ejecutar fCancelarModificacionCteProv FIN HACER FIN MIENTRAS

```pascal
{
VAR aCodCteProv CADENA
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
VAR aEditCampos ENTERO
Error = ejecutar fBuscaCteProv recibe PARAMETRO aCodCteProv
ENTONCES
ejecutar fEditaCteProv
error = ejecutar fSetDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para las funciones fLeeDatoCteProv y fSetDatoCteProv en el documento estructura de la BDD comercial (COM_BDD) tabla admClientes.
 
aCampo = Nombre del campo, aValor = Valor del campo
 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fBuscaIdCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdCteProv (aIdCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdCteProv

Entero

Por valor

Identificador del Cliente/Proveedor.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un Cliente/Proveedor por su Identificador.

**Ejemplo:**
BORRAR CLIENTE-PROVEEDOR (recibe aIdCteProv) SI error = 0 ejecutar fBorrarCteProv

```pascal
{
VAR error ENTERO
error ejecutar fBuscaIdCteProv recibe PARAMETRO aIdCteProv
ENTONCES
FIN ENTONCES
}
```

fPosPrimerCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdCteProv (aIdCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdCteProv

Entero

Por valor

Identificador del Cliente/Proveedor.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un Cliente/Proveedor por su Identificador.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosPrimerCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosEOFteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosEOFteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

fPosUltimoCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoCteProv()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el último registro de la tabla de Clientes/Proveedores.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosUltimoCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosBOFCteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosBOFCteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

fPosSiguienteCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteCteProv()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Clientes / Proveedores.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosPrimerCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosEOFteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosEOFteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

fPosAnteriorCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorCteProv()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de Clientes / Proveedores.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosUltimoCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosBOFCteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosBOFCteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

fPosBOFCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOFCteProv()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Documentos.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosUltimoCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosBOFCteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosBOFCteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

fPosEOFCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFCteProv()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Documentos.

**Ejemplo:**
RECORRIDO CLIENTE-PROVEEDOR error = ejecutar fPosPrimerCteProv SI error = 0 HACER PARAMETRO aValor, PARAMETRO aLong SI ejecutar fPosEOFCteProv = VERDADERO Cortar Ejecutar fPosSiguienteProv MIENTRAS ejecutar fPosEOFCteProv = FALSO

```pascal
{
VAR error ENTERO
VAR aCampo CADENA
VAR aValor CADENA
VAR aLong ENTERO
ENTONCES
ejecutar fLeeDatoCteProv recibe PARAMETRO aCampo,
ENTONCES
FIN ENTONCES
FIN ENTONCES
}
```

---

### 10.3 Alto nivel – Lectura/Escritura 

#### 10.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaCteProv (aIdCteProv, astCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdCteProv

Entero

Por referencia

Identificador del Cliente/Proveedor.

astCteProv

tCteProv

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdCteProv: Al finalizar la función este parámetro contiene el identificador del nuevo Cliente/Proveedor.

**Descripción:**
Esta función da de alta un nuevo Cliente / Proveedor.

**Ejemplo:**
Alta Cliente Proveedor OBJ tCteProv: SDK REFERENCIA tCteProv SI Error <> 0 Error

```pascal
{
VAR Error, idClienteProveedor: ENTERO
VAR aCodigoCliente: tCteProv
VAR aRazonSocial: tCteProv
VAR cRFC: tCteProv
VAR cFechaAlta: tCteProv
Error = Ejecuta fAltaCteProv recibe REFERENCIA idClienteProveedor,
ENTONCES
SI NO
fAltaCteProv
FIN SI
}
```

**Comentarios:**
Para la referencia de tCteProv los campos utilizables dependerán de la necesidad del desarrollador para el alta del cliente-proveedor.

fActualizaCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fActualizaCteProv (aCodigoCteProv, astCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoCteProv

Cadena

Por referencia

Identificador del Cliente/Proveedor.

astCteProv

tCteProv

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función actualiza un Cliente / Proveedor por medio su código.

**Ejemplo:**
Actualiza Cliente Proveedor OBJ tCteProv: SDK REFERENCIA tCteProv SI Error <> 0 Error fActualizaCteProv

```pascal
{
VAR Error: ENTERO
VAR aCodigoCliente: tCteProv
VAR aRazonSocial: tCteProv
Error = Ejecuta fActualizaCteProv recibe PARAMETRO aCodigoCteProv: CADENA,
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Para la referencia de tCteProv los campos utilizables dependerán de la necesidad del desarrollador para la actualización del cliente-proveedor.

fLlenaRegistroCteProv ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLlenaRegistroCteProv (astCteProv, aEsAlta )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

astCteProv

tCteProv

Por valor

Tipo de dato abstracto

aEsAlta

Entero

Por valor

1 = Nuevo Cliente / Proveedor.
2 = Actualizacion Cliente / Proveedor.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función asigna al registro de la tabla de Clientes / Proveedores los valores de la estructura de datos astCteProv.

**Ejemplo:**
Llena Registro Cliente Proveedor OBJ tCteProv: SDK Error = Ejecuta fInsertaCteProv SI Error <> 0 Error Error = Ejecuta fLlenaRegistroCteProv recibe REFERENCIA tCteProv, SI Error <> 0 Error fLlenaRegistroCteProv

```pascal
{
VAR Error: ENTERO
VAR aCodigoCliente: tCteProv
VAR aRazonSocial: tCteProv
VAR aNombreMoneda: tCteProv
VAR aFechaAlta: tCteProv
VAR aRFC: tCteProv
ENTONCES
SI NO
PARAMETRO aEsAlta: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

---

## 11. Funciones de productos

---

### 11.1 Bajo nivel – Lectura/Escritura 

#### 11.1.1 Bajo nivel – Lectura/Escritura

fInsertaProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Adiciona un nuevo registro en la tabla de productos en modo de inserción.

**Ejemplo:**
Inserta Producto Error = Ejecuta fInsertaProducto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fSetDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aValor: CADENA
Ejecuta fGuardaProducto
FIN SI
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para la función fSetDatoProducto en el documento de base de datos, tabla Productos del sistema CONTPAQi Factura Electrónica® y tabla admProductos del sistema CONTPAQi Comercial Premium®. 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fEditaProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Productos.

**Ejemplo:**
Edita Producto SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
Error = Ejecuta fEditaProducto
ENTONCES
SI NO
Ejecuta fSetDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aValor: CADENA
Ejecuta fGuardaProducto
FIN SI
FIN SI
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para la función fSetDatoProducto en el documento de base de datos, tabla Productos del sistema CONTPAQi Factura Electrónica® y tabla admProductos del sistema CONTPAQi Comercial Premium®. 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fGuardaProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Guarda los cambios realizados a un registro de productos.

**Ejemplo:**
Guarda Producto SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
Error = Ejecuta fEditaProducto
ENTONCES
SI NO
Ejecuta fSetDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aValor: CADENA
Ejecuta fGuardaProducto
FIN SI
FIN SI
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para la función fSetDatoProducto en el documento de base de datos, tabla Productos del sistema CONTPAQi Factura Electrónica® y tabla admProductos del sistema CONTPAQi Comercial Premium®. 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

fBorraProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorraProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Borra un registro en la tabla de productos.

**Ejemplo:**
Borra Producto SI Error <> 0 Error Ejecuta fBorraProducto

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
FIN SI
}
```

fCancelarModificacionProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función cancela las modificaciones al registro actual de productos. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
Cancela Modificación Producto SI Error <> 0 Error SI Error <> 0 Error Ejecuta fCancelarModificacionProducto

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
Error = Ejecuta fEditaProducto
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fEliminarProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEliminarProducto (aCodigoProducto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función elimina un producto usando su código.

**Ejemplo:**
Eliminar Producto SI Error <> 0 Error fEliminarProducto

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fEliminarProducto recibe PARAMETRO aCodigoProducto: CADENA
ENTONCES
SI NO
FIN SI
}
```

```pascal
fSetDatoProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoProducto (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla Productos.

**Ejemplo:**
Set Dato Producto SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
Error = Ejecuta fEditaProducto
ENTONCES
SI NO
Ejecuta fSetDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aValor: CADENA
Ejecuta fGuardaProducto
FIN SI
FIN SI
}
```

**Comentarios:**
Se puede consultar el nombre de cada campo utilizable para la función fSetDatoProducto en el documento de base de datos, tabla Productos del sistema CONTPAQi Factura Electrónica® y tabla admProductos del sistema CONTPAQi Comercial Premium®. 
Se puede asignar un valor a la gran mayoría de campos, algunos tienen restricciones que hay que cumplir y otros tantos como el ID no son editables.

```pascal
fLeeDatoProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoProducto (aCampo, aValr, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por referencia

Valor de lectura

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de productos.

**Ejemplo:**
Set Dato Producto SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: CADENA(StringBuilder)
Error = Ejecuta fBuscaProducto recibe VAR aCodProducto: CADENA
ENTONCES
SI NO
Error = Ejecuta fLeeDatoProducto recibe PARAMETRO aCampo: CADENA, aValor,
PARAMETRO aLong: ENTERO
ENTONCES
SI NO
fLeeDatoProducto
FIN SI
FIN SI
}
```

fRecuperaTipoProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRecuperaTipoProducto(aUnidades, aSerie, aLote, aPedimento, aCaracteristicas)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aUnidades

Lógico (bool)

Por referencia

Valor lógico. Verdadero o Falso. 
Maneja unidades o no.

aSerie

Lógico (bool)

Por referencia

Valor lógico. Verdadero o Falso. 
Maneja series o no.

aLote

Lógico (bool)

Por referencia

Valor lógico. Verdadero o Falso. 
Maneja lotes o no.

aPedimento

Lógico (bool)

Por referencia

Valor lógico. Verdadero o Falso. 
Maneja pedimentos o no.

aCaracteristicas

Lógico (bool)

Por referencia

Valor lógico. Verdadero o Falso. 
Maneja caracterisricas o no.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aUnidades: Al finalizar la función este parámetro indica si el producto maneja unidades o no.
aSerie: Al finalizar la función este parámetro indica si el producto maneja series o no.
aLote: Al finalizar la función este parámetro indica si el producto maneja lotes o no.
aPedimento: Al finalizar la función este parámetro indica si el producto maneja pedimentos o no.
aCaracteristicas: Al finalizar la función este parámetro indica si el producto maneja características o no.

**Descripción:**
Esta función define el tipo de producto, indicando si maneja series, lotes, pedimentos, unidades y/o características.

**Ejemplo:**
Recupera Tipo Producto SI Error <> 0 Error Error = Ejecuta fRecuperaTipoProducto recibe REFERENCIA aUnidades, REFERENCIA aSerie, REFERENCIA aLote, REFERENCIA aPedimento, REFERENCIA aCaracteristica SI Error <> 0 Error fRecuperaTipoProducto

```pascal
{
VAR Error: ENTERO
VAR aUnidades, aSerie, aLote, aPedimento, aCaracteristica: BOOL
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fRegresaPrecioVenta ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRecosteoProducto (aCodigoProducto, aEjercicio, aPeriodo, aCodigoClasificacion1,
aCodigoClasificacion2, aCodigoClasificacion3, aCodigoClasificacion4, aCodigoClasificacion5, aCodigoClasificacion6, aNombreBitacora, aSobreEscribirBitacora , aEsCalculoArimetico)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoConcepto

Cadena

Por valor

Código del concepto.

aCodigoCliente

Cadena

Por valor

Código del cliente.

aCodigoProducto

Cadena

Por valor

Código del producto.

aPrecioVenta

Cadena

Por referencia

Precio de venta.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aPrecioVenta: Al finalizar la función este parámetro contiene el precio de venta del producto solicitado.

**Descripción:**
Esta función obtiene el precio de venta de un producto de un determinado cliente para un concepto de documento en especifico.

**Ejemplo:**
Regresa Precio Venta Error = Ejecuta fRegresaPrecioVenta recibe PARAMETRO aCodigoConcepto: SI Error <> 0 Error fRegresaPrecioVenta

```pascal
{
VAR Error: ENTERO
VAR aPrecioVenta: CADENA(StringBuilder)
CADENA, PARAMETRO aCodigoCliente: CADENA, PARAMETRO aCodigoProducto:
CADENA, PARAMETRO aPrecioVenta
ENTONCES
SI NO
FIN SI
}
```

---

### 11.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscaProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaProducto (aCodProducto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodProducto

Cadena

Por valor

Código del producto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un producto por su código.

**Ejemplo:**
Busca Producto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
fBuscaProducto
FIN SI
}
```

```pascal
fBuscaIdProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdProducto (aIdProducto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdProducto

Entero

Por valor

Identificador del producto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca un producto por su Identificador.

**Ejemplo:**
Busca Id Producto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaIdProducto recibe PARAMETRO aIdProducto: ENTERO
ENTONCES
SI NO
fBuscaIdProducto
FIN SI
}
```

fPosPrimerProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el primer registro de la tabla de Productos.

**Ejemplo:**
Posicionar Primer Producto Error = Ejecuta fPosPrimerProducto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aNomProducto: CADENA(StringBuilder)
ENTONCES
SI NO
Ejecuta fLeeDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aNomProducto, PARAMETRO aLong: ENTERO
FIN SI
}
```

fPosUltimoProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el último registro de la tabla de Productos.

**Ejemplo:**
Posicionar Ultimo Producto Error = Ejecuta fPosUltimoProducto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aNomProducto: CADENA(StringBuilder)
ENTONCES
SI NO
Ejecuta fLeeDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aNomProducto, PARAMETRO aLong: ENTERO
FIN SI
}
```

fPosSiguienteProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Productos.

**Ejemplo:**
Posicionar Siguiente Producto Error = Ejecuta fPosSiguienteProducto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aNomProducto: CADENA(StringBuilder)
ENTONCES
SI NO
Ejecuta fLeeDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aNomProducto, PARAMETRO aLong: ENTERO
FIN SI
}
```

fPosAnteriorProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Productos.

**Ejemplo:**
Posicionar Anterior Producto Error = Ejecuta fPosAnteriorProducto SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aNomProducto: CADENA(StringBuilder)
ENTONCES
SI NO
Ejecuta fLeeDatoProducto recibe PARAMETRO aCampo: CADENA,
PARAMETRO aNomProducto, PARAMETRO aLong: ENTERO
FIN SI
}
```

fPosBOFProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOFProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Productos.

**Ejemplo:**
Posicionar BOF Producto SI Error <> 0 Error Error = Ejecuta fPosBOFProducto SI Error <> 0 Error fPosBOFProducto

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosEOFProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFProducto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Productos.

**Ejemplo:**
Posicionar EOF Producto SI Error <> 0 Error Error = Ejecuta fPosEOFProducto SI Error <> 0 Error fPosEOFProducto

```pascal
{
VAR Error: ENTERO
Error = Ejecuta fBuscaProducto recibe PARAMETRO aCodProducto: CADENA
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

---

### 11.3 Alto nivel – Lectura/Escritura 

#### 11.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaProducto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaProducto (aIdProducto, astProducto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdProducto

Entero

Por referencia

Identificador del producto.

astProducto

tProducto

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdProducto: Al finalizar la función este parámetro contiene el identificador del nuevo producto.

**Descripción:**
Esta función da de alta un nuevo producto.

**Ejemplo:**
OBJETO aProducto: tProducto

```pascal
Alta_producto(){
VAR IdProducto: ENTERO
VAR cCodigoProducto : aProducto
VAR cNombreProducto : aProducto
VAR cDescripcionProducto : aProducto
VAR cTipoProducto : aProducto
VAR cFechaAltaProducto : aProducto
VAR cFechaBajaProducto : aProducto
VAR cStatusProducto : aProducto
VAR cControlExistencia : aProducto
VAR cMetodoCosteo : aProducto
VAR cCodigoUnidadBase : aProducto
VAR cCodigoUnidadNoConvertible : aProducto
VAR cPrecio1 : aProducto
VAR cPrecio2 : aProducto
VAR cPrecio3 : aProducto
VAR cPrecio4 : aProducto
VAR cPrecio5 : aProducto
VAR cPrecio6 : aProducto
VAR cPrecio7 : aProducto
VAR cPrecio8 : aProducto
VAR cPrecio9 : aProducto
VAR cPrecio10 : aProducto
VAR cImpuesto1 : aProducto
VAR cImpuesto2 : aProducto
VAR cImpuesto3 : aProducto
VAR cRetencion1 : aProducto
VAR cRetencion2 : aProducto
VAR cNombreCaracteristica1 : aProducto
VAR cNombreCaracteristica2 : aProducto
VAR cNombreCaracteristica3 : aProducto
VAR cCodigoValorCaracterisitica1 : aProducto
VAR cCodigoValorCaracterisitica2 : aProducto
VAR cCodigoValorCaracterisitica3 : aProducto
VAR cCodigoValorCaracterisitica4 : aProducto
VAR cCodigoValorCaracterisitica5 : aProducto
VAR cCodigoValorCaracterisitica6 : aProducto
VAR cTextoExtra1 : aProducto
VAR cTextoExtra2 : aProducto
VAR cTextoExtra3 : aProducto
VAR cFechaExtra : aProducto
VAR cImporteExtra1 : aProducto
VAR cImporteExtra2 : aProducto
VAR cImporteExtra3 : aProducto
VAR cImporteExtra4 : aProducto
Ejecutar fAltaProducto (recibe REFERENCIA IdProducto, REFERENCIA Producto);
}
```

fActualizaProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fActualizaProducto (aCodigoProducto, astCteProv)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Entero largo

Por referencia

Código del producto.

astProducto

tProducto

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función actualiza un producto.

**Ejemplo:**
OBJETO Producto: tProducto Ejecutar fActualizaProducto (recibe PARAMETRO codigoProducto,

```pascal
Actualizar_producto(){
VAR codigoProducto: CADENA
VAR cCodigoProducto : aProducto
VAR cNombreProducto : aProducto
VAR cDescripcionProducto : aProducto
VAR cTipoProducto : aProducto
VAR cFechaAltaProducto : aProducto
VAR cFechaBajaProducto : aProducto
VAR cStatusProducto : aProducto
VAR cControlExistencia : aProducto
VAR cMetodoCosteo : aProducto
VAR cCodigoUnidadBase : aProducto
VAR cCodigoUnidadNoConvertible : aProducto
VAR cPrecio1 : aProducto
VAR cPrecio2 : aProducto
VAR cPrecio3 : aProducto
VAR cPrecio4 : aProducto
VAR cPrecio5 : aProducto
VAR cPrecio6 : aProducto
VAR cPrecio7 : aProducto
VAR cPrecio8 : aProducto
VAR cPrecio9 : aProducto
VAR cPrecio10 : aProducto
VAR cImpuesto1 : aProducto
VAR cImpuesto2 : aProducto
VAR cImpuesto3 : aProducto
VAR cRetencion1 : aProducto
VAR cRetencion2 : aProducto
VAR cNombreCaracteristica1 : aProducto
VAR cNombreCaracteristica2 : aProducto
VAR cNombreCaracteristica3 : aProducto
VAR cCodigoValorCaracterisitica1 : aProducto
VAR cCodigoValorCaracterisitica2 : aProducto
VAR cCodigoValorCaracterisitica3 : aProducto
VAR cCodigoValorCaracterisitica4 : aProducto
VAR cCodigoValorCaracterisitica5 : aProducto
VAR cCodigoValorCaracterisitica6 : aProducto
VAR cTextoExtra1 : aProducto
VAR cTextoExtra2 : aProducto
VAR cTextoExtra3 : aProducto
VAR cFechaExtra : aProducto
VAR cImporteExtra1 : aProducto
VAR cImporteExtra2 : aProducto
VAR cImporteExtra3 : aProducto
VAR cImporteExtra4 : aProducto
REFERENCIA Producto);
}
```

fLlenaRegistroProducto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLlenaRegistroCteProv (astProducto, aEsAlta )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

astProducto

tProducto

Por valor

Tipo de dato abstracto. 

aEsAlta

Entero

Por valor

1 = Nuevo Producto.
2 = Actualizacion Producto.

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función asigna al registro de la tabla de productos los valores de la estructura de datos astCteProv.

**Ejemplo:**
OBJETO Producto: tProducto Ejecutar fLlenaRegistroProducto(recibe REFERENCIA Producto,

```pascal
LlenaRegistro_producto(){
VAR aEsAlta: ENTERO
VAR cCodigoProducto : aProducto
VAR cNombreProducto : aProducto
VAR cDescripcionProducto : aProducto
VAR cTipoProducto : aProducto
VAR cFechaAltaProducto : aProducto
VAR cFechaBajaProducto : aProducto
VAR cStatusProducto : aProducto
VAR cControlExistencia : aProducto
VAR cMetodoCosteo : aProducto
VAR cCodigoUnidadBase : aProducto
VAR cCodigoUnidadNoConvertible : aProducto
VAR cPrecio1 : aProducto
VAR cPrecio2 : aProducto
VAR cPrecio3 : aProducto
VAR cPrecio4 : aProducto
VAR cPrecio5 : aProducto
VAR cPrecio6 : aProducto
VAR cPrecio7 : aProducto
VAR cPrecio8 : aProducto
VAR cPrecio9 : aProducto
VAR cPrecio10 : aProducto
VAR cImpuesto1 : aProducto
VAR cImpuesto2 : aProducto
VAR cImpuesto3 : aProducto
VAR cRetencion1 : aProducto
VAR cRetencion2 : aProducto
VAR cNombreCaracteristica1 : aProducto
VAR cNombreCaracteristica2 : aProducto
VAR cNombreCaracteristica3 : aProducto
VAR cCodigoValorCaracterisitica1 : aProducto
VAR cCodigoValorCaracterisitica2 : aProducto
VAR cCodigoValorCaracterisitica3 : aProducto
VAR cCodigoValorCaracterisitica4 : aProducto
VAR cCodigoValorCaracterisitica5 : aProducto
VAR cCodigoValorCaracterisitica6 : aProducto
VAR cTextoExtra1 : aProducto
VAR cTextoExtra2 : aProducto
VAR cTextoExtra3 : aProducto
VAR cFechaExtra : aProducto
VAR cImporteExtra1 : aProducto
VAR cImporteExtra2 : aProducto
VAR cImporteExtra3 : aProducto
VAR cImporteExtra4 : aProducto
PARAMETRO aEsAlta);
}
```

---

## 12. Funciones de addendas

---

### 12.1 Bajo nivel – Lectura/Escritura 

#### 12.1.1 Bajo nivel – Lectura/Escritura

fInsertaDatoCompEducativo ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaDatoCompEducativo(int aIdServicio, int aNumCampo, char *aDato )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdServicio

Entero

Por valor

Identificador del servicio

aNumCampo

Entero

Por valor

Número de campo

aDato

Cadena

Por referencia

Valor a insertar

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función inserta un registro correspondiente a los datos adicionales para el complemento educativo del catálogo servicios.

**Ejemplo:**
SI Error <> 0 Error fInsertaDatoCompEducativo

```pascal
Inserta Complemento Educativo{
VAR Error: ENTERO
Ejecuta fInsertaDatoCompEducativo recibe VAR aIdServicio: ENTERO, VAR aNumCampo: ENTERO, VAR aDato: CADENA
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Para insertar el complemento educativo es necesario utiliza la función fInsertaDatoCompEducativo como lo muestra el ejemplo; esta función requiere de tres parámetros, los cuales son:

Id del servicio

Número de campo (se muestra el listado de los campos más adelante)

Valor que se le asignará al campo
 
Los campos requeridos por el complemento educativo son los siguientes:

Nombre del alumno

CURP del alumno

Nivel educativo

Autorización o reconocimiento

RFC de quien realiza el pago

fInsertaDatoAddendaDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaDatoAddendaDocto(aIdAddenda, aIdCatalogo, aNumCampo, aDato)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdAddenda

Entero

Por valor

Identificador de la Addenda

aIdCatalogo

Entero

Por valor

Identificador del documento

aNumCampo

Entero

Por valor

Número de campo

aDato

Cadena

Por referencia

Valor a insertar

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Agrega los datos de la addenda para los documentos.

**Ejemplo:**
SI Error <> 0 Error fInsertaDatoAddendaDocto

```pascal
Inserta Dato Addenda Documento{
VAR Error: ENTERO
Ejecuta fAltaDocumento
Ejecuta fInsertaDatoAddendaDocto recibe VAR aIdAddenda: ENTERO, VAR aIdCatalogo: ENTERO, VAR aNumCampo: ENTERO, VAR aDato: CADENA
ENTONCES
SI NO
FIN SI
}
```

fObtieneLicencia ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fObtieneLicencia (aCodActiva, aCodSitio, aSerie, aTagVersion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodActiva

Cadena

Por referencia

Variable en la que regresa el código de activación del sistema.

aCodSitio

Cadena

Por referencia

Variable en la que regresa el código de sitio del sistema.

aSerie

Cadena

Por referencia

Variable en la que regresa el número de serie del sistema.

aTagVersion

Cadena

Por referencia

Variable en la que regresa el versión del sistema.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función regresa la licencia del producto.
Nota:
Antes de llamar la función fObtieneLicencia se deberá llamar la función fInicializaLicenseInfo.

**Ejemplo:**
Obtiene Licencia Error = fObtieneLicencia recibe aCodActiva, aCodSitio, aSerie, aTagVersion SI Error <> 0 Error fObtieneLicencia

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema BYTE
VAR Error: ENTERO
VAR aCodActiva: StringBuilder
VAR aCodSitio: StringBuilder
VAR aSerie: StringBuilder
VAR aTagVersion: StringBuilder
ENTONCES
SI NO
FIN SI
}
```

**Comentarios:**
Nota:
Este proceso funciona únicamente con CONTPAQi Factura Electrónica®.

fObtienePassProxy ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fObtienePassProxy(aPassProxy )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aPassProxy

Cadena

Por referencia

Variable en la que regresa la contraseña del proxy.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Regresa la contraseña del proxy.

**Ejemplo:**
El siguiente código regresa la contraseña del proxy. Obtiene Pass Proxy Error = fObtienePassProxy recibe aPassProxy SI Error <> 0 Error fObtienePassProxy

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema BYTE
VAR Error: ENTERO
VAR aPassProxy: STRINGBUILDER
ENTONCES
SI NO
FIN SI
}
```

```pascal
fTimbraXML ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fTimbraXML( char *aRutaXML, char *aCodConcepto, char *aUUID, char *aRutaDDA, char
*aRutaResultado, char *aPass, char *aRutaFormato )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aRutaXML

Cadena

Por referencia

Variable donde se especifica la ruta y archivo donde está ubicado el XML a timbrar.

aCodConcepto

Cadena

Por referencia

Variable donde se pasa el código del concepto a utilizar para timbrar el XML. Este concepto deberá estar configurado como CFDI.

aUUID

Cadena

Por referencia

Variable donde se regresa el UUID del XML timbrado.

aRutaDDA

Cadena

Por referencia

Variable donde se especifica la ruta y archivo DDA que contiene información adicional del XML.

aRutaResultado

Cadena

Por referencia

Variable donde se especifica la ruta donde se generará el XML, HTML y las imágenes para la entrega en formato amigable.

aPass

Cadena

Por referencia

Variable donde se especifica
la contraseña del certificado para timbrar el XML.

aRutaFormato

Cadena

Por referencia

Variable con la ruta y archivo del formato de impresión.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función timbra un XML creado con una aplicación de un tercero.
El XML deberá estar sin emitir, sin sello y sin certificado.
Esta función requeire una liciencia de 2 o más usuarios. Si cuentas con un licenciamiento anual además se requeire que la licencia sea multiempresa.

**Ejemplo:**
El siguiente código timbra un XML. Timbra XML SI Error <> 0 Error

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema BYTE
VAR Error: ENTERO
VAR rutaXML: CADENA
VAR codConcepto: CADENA
VAR UUID: STRINGBUILDER
VAR rutaDDA: CADENA
VAR rutaResultado: CADENA
VAR aPass: CADENA
VAR rutaFormato: CADENA
Error = fTimbraXML recibe rutaXML, codConcepto, UUID, rutaDDA, rutaResultado, aPass, rutaFormato
ENTONCES
SI NO
fTimbraXML
FIN SI
}
```

```pascal
fTimbraNominaXML ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fTimbraNominaXML( char *aRutaXML, char *aCodConcepto, char *aUUID, char *aRutaDDA, char *aRutaResultado, char *aPass, char *aRutaFormato )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aRutaXML

Cadena

Por referencia

Variable donde se especifica la ruta y archivo donde está ubicado el XML a timbrar.

aCodConcepto

Cadena

Por referencia

Variable donde se pasa el código del concepto a utilizar para timbrar el XML. Este concepto deberá estar configurado como CFDI.

aUUID

Cadena

Por referencia

Variable donde se regresa el UUID del XML timbrado.

aRutaDDA

Cadena

Por referencia

Variable donde se especifica la ruta y archivo DDA que contiene información adicional del XML.

aRutaResultado

Cadena

Por referencia

Variable donde se especifica la ruta donde se generará el XML, HTML y las imágenes para la entrega en formato amigable.

aPass

Cadena

Por referencia

Variable donde se especifica
la contraseña del certificado para timbrar el XML.

aRutaFormato

Cadena

Por referencia

Variable con la ruta y archivo del formato de impresión.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función timbra un XML de una nómina creado con una aplicación de un tercero.
El XML deberá estar sin emitir, sin sello y sin certificado. Es obligatorio que el XML lleve el domicilio del emisor.
Si deseas ver en la impresión del formato amigable algún dato del complemento de nómina se deberá insertar en el DDA.
Esta función requiere una liciencia de 5 o más usuarios. Si cuentas con un licenciamiento anual además se requiere que la licencia sea multiempresa.

**Ejemplo:**
Timbra Nomina XML SI Error <> 0 Error

```pascal
{
fInicializaLicenseInfo recibe VAR aSistema BYTE
VAR Error: ENTERO
VAR rutaXML: CADENA
VAR codConcepto: CADENA
VAR UUID: STRINGBUILDER
VAR rutaDDA: CADENA
VAR rutaResultado: CADENA
VAR aPass: CADENA
VAR rutaFormato: CADENA
Error = fTimbraNominaXML recibe rutaXML, codConcepto, UUID, rutaDDA, rutaResultado, aPass, rutaFormato
ENTONCES
SI NO
fTimbraNominaXML
FIN SI
}
```

---

## 13. Funciones de direcciones

---

### 13.1 Bajo nivel – Lectura/Escritura 

#### 13.1.1 Bajo nivel – Lectura/Escritura

fInsertaDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Adiciona un nuevo registro en la tabla de Direcciones en modo de inserción.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error Ejecuta fInsertaDireccion SI Error <> 0 Error

```pascal
Inserta Direccion{
VAR idCte: CADENA(StringBuilder)
VAR Error: ENTERO
Ejecuta fBuscaCteProv recibe PARAMETRO aCodCteProv: CADENA
ENTONCES
SI NO
Ejecuta fLeeDatoCteProv recibe PARAMETRO aCampo: CADENA,
PARAMETRO idCte, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
Ejecuta fSetDatoDireccion recibe PARAMETRO aCampo: CADENA,
PARAMETRO aValor: CADENA
Ejecuta fGuardaDireccion
ENTONCES
SI NO
fGuardaDireccion
FIN SI
FIN SI
FIN SI
}
```

**Comentarios:**
Para agregar datos en la función fSetDatoDireccion se deberán agregar por lo menos los campos obligatorios para insertar una dirección. En el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

```pascal
fEditaDireccion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Direcciones.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error

```pascal
Edita Dirección{
VAR Error: ENTERO
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
Ejecuta fEditaDireccion
Ejecuta fSetDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
Ejecuta fGuardaDireccion
ENTONCES
SI NO
fGuardaDireccion
FIN SI
FIN SI
}
```

**Comentarios:**
Para agregar datos en la función fSetDatoDireccion se deberán agregar por lo menos los campos obligatorios para insertar una dirección. En el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

```pascal
fGuardaDireccion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Guarda los cambios realizados a un registro de productos.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error

```pascal
Guarda Dirección{
VAR Error: ENTERO
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
Ejecuta fEditaDireccion
Ejecuta fSetDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
Ejecuta fGuardaDireccion
ENTONCES
SI NO
fGuardaDireccion
FIN SI
FIN SI
}
```

fCancelarModificacionDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función cancela las modificaciones al registro actual de direcciones. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
SI Error <> 0 Error Ejecuta fCancelarModificacionDireccion SI Error <> 0 Error fCancelarModificacionDireccion

```pascal
Cancela Modificación Dirección{
VAR Error: ENTERO
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
Ejecuta fEditaDireccion
Ejecuta fSetDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Comentario:** Para agregar datos en la función fSetDatoDireccion se deberán agregar por lo menos los campos obligatorios para insertar una dirección. En el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

```pascal
fLeeDatoDireccion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoDireccion (aCampo, aValr, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de Direcciones.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Lee Dato Dirección{
VAR Error: ENTERO
VAR aValor: CADENA(StringBuilder)
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Comentario:** Para agregar datos en la función fLeeDatoDireccion se puede consultar el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

```pascal
fSetDatoDireccion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoDireccion (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de Cliente / Proveedor.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error

```pascal
Set Dato Dirección{
VAR Error: ENTERO
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
Ejecuta fEditaDireccion
Ejecuta fSetDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
Ejecuta fGuardaDireccion
ENTONCES
SI NO
fGuardaDireccion
FIN SI
FIN SI
}
```

---

### 13.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscaDireccionEmpresa ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaDireccionEmpresa ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca la dirección de la empresa.

**Ejemplo:**
SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Busca Dirección Empresa{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
Ejecuta fBuscaDireccionEmpresa
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Comentario:** Para agregar datos en la función fLeeDatoDireccionse se puede consultar el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

```pascal
fBuscaDireccionCteProv ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaDireccionCteProv (aCodCteProv, aTipoDireccion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Código del cliente/proveedor.

aValor

Cadena

Por referencia

Tipo de dirección 0 = Fiscal, 1 = Envío

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca una dirección de un cliente/proveedor.

**Ejemplo:**
SI Error <> 0 Error

```pascal
Busca Dirección Cliente Proveedor{
VAR Error: ENTERO
Ejecuta fBuscaDireccionCteProv recibe PARAMETRO aCodCteProv: CADENA, PARAMETRO aTipoDireccion: BYTE
ENTONCES
SI NO
fBuscaDireccionCteProv
FIN SI
}
```

```pascal
fBuscaDireccionDocumento ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaDireccionDocumento (aIdDocumento, aTipoDireccion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDocumento

Entero largo

Por valor

Identificador del documento.

aValor

Cadena

Por valor

Tipo de dirección 0 = Fiscal, 1 = Envío

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función busca una dirección de un documento.

**Ejemplo:**
PARAMETRO aTipoDireccion: BYTE SI Error <> 0 Error

```pascal
Busca Dirección Documento{
VAR Error: ENTERO
Ejecuta fBuscaDireccionDocumento recibe PARAMETRO aIdDocumento: ENTERO,
ENTONCES
SI NO
fBuscaDireccionDocumento
FIN SI
}
```

fPosPrimerDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el primer registro de la tabla de Direcciones.

**Ejemplo:**
Ejecuta fPosPrimerDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar Primer Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Comentario:** Para leer datos utilizando la función fLeeDatoDireccion se puede consultar el documento de referencia de las bases de datos del sistema CONTPAQi® que se esté trabajando, se pueden consultar todos los campos, descripción y tipo de dato.

fPosUltimaDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimaDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el ultimo registro de la tabla de Direcciones.

**Ejemplo:**
Ejecuta fPosUltimaDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar Ultima Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosSiguienteDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Direcciones.

**Ejemplo:**
Ejecuta fPosSiguienteDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar Siguiente Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosAnteriorDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de Direcciones.

**Ejemplo:**
Ejecuta fPosAnteriorDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar Anterior Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosBOFDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOFDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Esta función informa si el registro activo se encuentra en el inicio de la tabla de Direcciones.

**Ejemplo:**
Ejecuta fPosBOFDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar BOF Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fPosEOFDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFDireccion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Esta función informa si el registro activo se encuentra en el fin de la tabla de Direcciones

**Ejemplo:**
Ejecuta fPosEOFDireccion SI Error <> 0 Error SI Error <> 0 Error Regresa aValor

```pascal
Posicionar EOF Dirección{
VAR aValor: CADENA(StringBuilder)
VAR Error: ENTERO
ENTONCES
SI NO
Ejecuta fLeeDatoDireccion recibe PARAMETRO aCampo: CADENA, PARAMETRO
aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

---

### 13.3 Alto nivel – Lectura/Escritura 

#### 13.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaDireccion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaDireccion (aIdDireccion, astDireccion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdDireccion

Entero

Por referencia

Identificador de la dirección.

astDireccion

tDireccion

Por valor

Tipo de dato abstracto.

Importante
Al usar esta función de alto nivel, es necesario asignar al campo cTipoDireccion alguno de los siguientes valores:
 1 = Domicilio Fiscal
 2 = Domicilio Envío

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.
 
aIdDireccion: Al finalizar la función este parámetro contiene el identificador del nuevo producto.

**Descripción:**
Esta función da de alta una nueva dirección.

**Ejemplo:**
REFERENCIA tDireccion: SDK SI Error <> 0 Error SI Error <> 0 Error

```pascal
Alta Dirección{
VAR Error, idDireccion: ENTERO
Ejecuta fBuscaCteProv recibe PARAMETRO aCodCteProv: CADENA
ENTONCES
SI NO
VAR cTipoDireccion: tDireccion
VAR cPais: tDireccion
VAR cCodigoPostal: tDireccion
VAR cEstado: tDireccion
VAR cCiudad: tDireccion
VAR cColonia: tDireccion
VAR cNombreCalle: tDireccion
VAR cNumeroExterior: tDireccion
Ejecuta fAltaDireccion recibe REFERENCIA idDireccion, REFERENCIA tDireccion
ENTONCES
SI NO
fAltaDireccion
FIN SI
FIN SI
}
```

**Comentarios:**
Conforme al objeto tDireccion, se agregarán los datos que sean requeridos y mínimos según la solicitud para dar de alta una dirección. En el ejemplo se muestran algunos, esto es dependiendo de la funcionalidad que se desarrolle en el proyecto 
Para mas detalles de los campos para tenerse en cuenta en el uso de la estructura tDireccion, se puede consultar el documento COM_BDD en la tabla de domicilios, este documento contiene la estructura de la Base de Datos y se encuentra en C:\Program Files (x86)\Compac\COMERCIAL\Ayuda. Donde podemos revisar los datos importantes a considerar como lo son CIDCATALOGO, CTIPOCATALOGO, CTIPODIRECCION, entre otros.

fActualizaDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fActualizaProducto (astDireccion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

astDireccion

tDireccion

Por valor

Tipo de dato abstracto.

Importante
Al usar esta función de alto nivel, es necesario asignar al campo cTipoDireccion alguno de los siguientes valores:
 1 = Domicilio Fiscal
 2 = Domicilio Envío

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función actualiza la dirección del registro de Cliente/Proveedor activo.

**Ejemplo:**
REFERENCIA tDireccion: SDK SI Error <> 0 Error Ejecuta fActualizaDireccion recibe REFERENCIA tDireccion SI Error <> 0 Error fActualizaDireccion

```pascal
Actualiza Dirección{
VAR Error: ENTERO
Ejecuta fBuscaCteProv recibe PARAMETRO aCodCteProv: CADENA
ENTONCES
SI NO
VAR cCodCteProv: tDireccion
VAR cTipoDireccion: tDireccion
VAR cTipoCatalogo: tDireccion
VAR cCodigoPostal: tDireccion
VAR cEstado: tDireccion
VAR cCiudad: tDireccion
VAR cColonia: tDireccion
VAR cNombreCalle: tDireccion
VAR cNumeroExterior: tDireccion
ENTONCES
SI NO
FIN SI
FIN SI
}
```

- **Comentario:** Para más detalles de los campos para tenerse en cuenta en el uso de la estructura tDireccion, se puede consultar el documento COM_BDD en la tabla de domicilios, este documento contiene la estructura de la Base de Datos y se encuentra en C:\Program Files (x86)\Compac\COMERCIAL\Ayuda. Donde podemos revisar los datos importantes a considerar como lo son CIDCATALOGO, CTIPOCATALOGO, CTIPODIRECCION, entre otros.

fLlenaRegistroDireccion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLlenaRegistroDireccion (astDireccion, aEsAlta )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

astDireccion

tDireccion

Por valor

Tipo de dato abstracto.

aEsAlta

Entero

Por valor

1 = Nueva dirección.
2 = Actualización.

Importante
Al usar esta función de alto nivel, es necesario asignar al campo cTipoDireccion alguno de los siguientes valores:
 1 = Domicilio Fiscal
 2 = Domicilio Envío

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función asigna al registro de la base de datos los valores de la estructura de datos de la Dirección.

**Ejemplo:**
REFERENCIA tDireccion: SDK Ejecuta fInsertaDireccion SI Error <> 0 Error Ejecuta fLlenaRegistroDireccion recibe REFERENCIA tDireccion, PARAMETRO SI Error <> 0 Error

```pascal
Llena Registro Dirección{
VAR Error: ENTERO
ENTONCES
SI NO
VAR cNombreCalle: tDireccion
VAR cNumeroExterior: tDireccion
VAR cColonia: tDireccion
VAR cCodigoPostal: tDireccion
VAR cCiudad: tDireccion
VAR cEstado: tDireccion
VAR cPais: tDireccion
aEsAlta: ENTERO
ENTONCES
SI NO
Ejecuta fGuardaDireccion
FIN SI
FIN SI
}
```

---

## 14. Funciones de existencias

---

### 14.1 Bajo nivel – Lectura/Escritura 

#### 14.1.1 Bajo nivel – Lectura/Escritura

fRegresaExistencia ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaExistencia (aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, aExistencia)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCodigoAlmacen

Cadena

Por valor

Código del almacén.

aAnio

Cadena

Por valor

Año.

aMes

Cadena

Por valor

Mes.

aDia

Cadena

Por valor

Día.

aExistencia

Doble

Por referencia

Existencia

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aExistencia: Al finalizar la función este parámetro contiene la existencia del producto requerido.

**Descripción:**
Esta función regresa la existencia de un producto en un almacén a una determinada fecha.

**Ejemplo:**
Regresa Existencia SI Error <> 0 Error fRegresaExistencia

```pascal
{
VAR Error: ENTERO
Error = fRegresaExistencia recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCodigoAlmacen: CADENA, PARAMETRO aAnio: CADENA, PARAMETRO aMes: CADENA, PARAMETRO aDia: CADENA, REFERENCIA aExistencia: DOUBLE
ENTONCES
SI NO
FIN SI
}
```

fRegresaExistenciaCaracteristicas ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaExistenciaCaracteristicas (aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia,
aValorCaracteristica1, aValorCaracteristica2, aValorCaracteristica3, aExistencia)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCodigoAlmacen

Cadena

Por valor

Código del almacén.

aAnio

Cadena

Por valor

Año.

aMes

Cadena

Por valor

Mes.

aDia

Cadena

Por valor

Día.

aValorCaracteristica1

Cadena

Por valor

Valor característica 1.

aValorCaracteristica2

Cadena

Por valor

Valor característica 2.

aValorCaracteristica3

Cadena

Por valor

Valor característica 3.

aExistencia

Doble

Por referencia

Existencia

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aExistencia: Al finalizar la función este parámetro contiene la existencia del producto requerido.

**Descripción:**
Esta función regresa la existencia de un producto con características en un almacén a una determinada fecha.

**Ejemplo:**
Regresa Existencia Caracteristicas SI Error <> 0 Error fRegresaExistenciaCaracteristicas

```pascal
{
VAR Error: ENTERO
Error = fRegresaExistenciaCaracteristicas recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCodigoAlmacen: CADENA, PARAMETRO aAnio: CADENA, PARAMETRO aMes: CADENA, PARAMETRO aDia: CADENA, PARAMETRO aValorCaracteristica1: CADENA, PARAMETRO aValorCaracteristica2: CADENA, PARAMETRO aValorCaracteristica3: CADENA, REFERENCIA aExistencia: DOUBLE
ENTONCES
SI NO
FIN SI
}
```

---

## 15. Funciones de costo histórico

---

### 15.1 Bajo nivel – Lectura/Escritura 

#### 15.1.1 Bajo nivel – Lectura/Escritura

fRegresaCostoPromedio ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaCostoPromedio (aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, aCostoPromedio)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCodigoAlmacen

Cadena

Por valor

Código del almacén.
0 (cero) – Todos los almacenes.

aAnio

Cadena

Por valor

Año.

aMes

Cadena

Por valor

Mes.

aDia

Cadena

Por valor

Día.

aCostoPromedio

Cadena

Por referencia

Costo promedio

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aCostoPromedio: Al finalizar la función este parámetro contiene el costo promedio del producto requerido.

**Descripción:**
Esta función se encarga de obtener el costo promedio de un producto en determinada fecha para todos los almacenes o para uno solo.

**Ejemplo:**
Regresa Costo Promedio SI Error <> 0 Error fRegresaCostoPromedio

```pascal
{
VAR Error: ENTERO
Error = fRegresaCostoPromedio recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCodigoAlmacen: CADENA, PARAMETRO aAnio: CADENA, PARAMETRO aMes: CADENA, PARAMETRO aDia: CADENA, PARAMETRO aCostoPromedio: STRINGBUILDER
ENTONCES
SI NO
FIN SI
}
```

fRegresaUltimoCosto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaUltimoCosto (aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia,
aUltimoCosto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCodigoAlmacen

Cadena

Por valor

Código del almacén.
0 (cero) – Todos los almacenes.

aAnio

Cadena

Por valor

Año.

aMes

Cadena

Por valor

Mes.

aDia

Cadena

Por valor

Día.

aUltimoCosto

Cadena

Por referencia

Último costo.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aUltimoCosto: Al finalizar la función este parámetro contiene el ultimo costo del producto requerido.

**Descripción:**
Esta función se encarga de obtener el último costo de un producto en determinada fecha para todos los almacenes o para uno solo.

**Ejemplo:**
Regresa Ultimo Costo SI Error <> 0 Error fRegresaUltimoCosto

```pascal
{
VAR Error: ENTERO
Error = fRegresaUltimoCosto recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCodigoAlmacen: CADENA, PARAMETRO aAnio: CADENA, PARAMETRO aMes: CADENA, PARAMETRO aDia: CADENA, PARAMETRO aUltmoCosto: STRINGBUILDER
ENTONCES
SI NO
FIN SI
}
```

fRegresaCostoEstandar ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaCostoEstandar (aCodigoProducto, aCostoEstandar)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCostoEstandar

Cadena

Por referencia

Costo estándar.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aCostoEstandar: Al finalizar la función este parámetro contiene el costo estándar del producto requerido.

**Descripción:**
Esta función se encarga de obtener el costo estándar de un producto.

**Ejemplo:**
Regresa Costo Estandar SI Error <> 0 Error fRegresaCostoEstandar

```pascal
{
VAR Error: ENTERO
Error = fRegresaCostoEstandar recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCostoEstandar: STRINGBUILDER
ENTONCES
SI NO
FIN SI
}
```

fRegresaCostoCapa ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresaCostoCapa (aCodigoProducto, aCodigoAlmacen, aUnidades, aImporteCosto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoProducto

Cadena

Por valor

Código del producto.

aCodigoAlmacen

Cadena

Por valor

Código del almacén.

aUnidades

Doble

Por valor

Unidades a costear.

aImporteCosto

Cadena

Por referencia

Importe del costo de la unidades recibidas.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aImporteCosto: Al finalizar la función este parámetro contiene el costo UEPS o PEPS del producto requerido.

**Descripción:**
Esta función obtiene el costo UEPS o PEPS de un producto en un almacén en base a una cantidad de unidades proporcionadas.

**Ejemplo:**
Regresa Costo Capa SI Error <> 0 Error fRegresaCostoCapa

```pascal
{
VAR Error: ENTERO
Error = fRegresaCostoCapa recibe PARAMETRO aCodigoProducto: CADENA, PARAMETRO aCodigoAlmacen: CADENA, PARAMETRO aUnidades: DOBLE, PARAMETRO aImporteCosto: STRINGBUILDER
ENTONCES
SI NO
FIN SI
}
```

---

## 16. Funciones de conceptos de documentos

---

### 16.1 Bajo nivel – Lectura/Escritura 

#### 16.1.1 Bajo nivel – Lectura/Escritura

```pascal
fLeeDatoConceptoDocto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoConceptoDocto (aCampo, aValor, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee un campo del registro actual de conceptos documentos.

**Ejemplo:**
Lee Dato Concepto Documento SI Error <> 0 Error SI Error <> 0 Error FLeeDatoConceptoDocto

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fBuscaConceptoDocto recibe PARAMETRO aCodConcepto: CADENA
ENTONCES
SI NO
fLeeDatoConceptoDocto recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fRegresPorcentajeImpuesto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fRegresPorcentajeImpuesto (aIdConceptoDocumento, aIdClienteProveedor, aIdProducto,
aPorcentajeImpuesto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdConceptoDocumento

Entero

Por valor

Identificador del concepto del documento.

aIdClienteProveedor

Entero

Por valor

Identificador del cliente o proveedor.

aIdProducto

Entero

Por valor

Identificador del producto.

aPorcentajeImpuesto

Doble

Por referencia

Porcentaje de impuesto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aPorcentajeImpuesto: Al finalizar la función este parámetro contiene el porcentaje del impuesto requerido.

**Descripción:**
Esta función regresa el porcentaje de impuesto de un concepto documento, del cual se obtiene su configuración y se busca el porcentaje de la tabla de Clientes/Proveedores, Productos o de Parámetros generales.

**Ejemplo:**
Regresa Porcentaje Impuesto SI Error <> 0 Error fRegresPorcentajeImpuesto

```pascal
{
VAR Error: ENTERO
VAR aPorcentajeImpuesto: DOBLE
Error = fRegresPorcentajeImpuesto PARAMETRO aIdConceptoDocumento: ENTERO, PARAMETRO aIdClienteProveedor: ENTERO, PARAMETRO aIdProducto: ENTERO, REFERENCIA aPorcentajeImpuesto
ENTONCES
SI NO
FIN SI
}
```

```pascal
fEditaConceptoDocto()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función activa el modo de edición de un registro del catálogo Conceptos.

**Ejemplo:**
Edita Concepto Documento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aCampo, aValor: STRINGBUILDER
Error = fBuscaConceptoDocto recibe PARAMETRO: aCodConcepto: CADENA
ENTONCES
SI NO
fEditaConceptoDocto
Error = fSetDatoConceptoDocto recibe aCampo, aValor
ENTONCES
SI NO
fGuardaConceptoDocto
FIN SI
FIN SI
}
```

```pascal
fSetDatoConceptoDocto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoConceptoDocto (const char *aCampo, char *aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por referencia

Nombre del campo 

aValor

Cadena

Por referencia

Valor del campo

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla Conceptos.

**Ejemplo:**
Set Dato Concepto Documento SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aCampo, aValor: STRINGBUILDER
Error = fBuscaConceptoDocto recibe PARAMETRO: aCodConcepto: CADENA
ENTONCES
SI NO
fEditaConceptoDocto
Error = fSetDatoConceptoDocto recibe aCampo, aValor
ENTONCES
SI NO
fGuardaConceptoDocto
FIN SI
FIN SI
}
```

```pascal
fGuardaConceptoDocto()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaConceptoDocto()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función guarda los cambios efectuados al registro de la tabla Conceptos.

---

### 16.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscaConceptoDocto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaConceptoDocto (aCodConcepto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodConcepto

Cadena

Por valor

Código del concepto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca un concepto por su código.

**Ejemplo:**
Busca Concepto Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaConceptoDocto recibe PARAMETRO lCodConcepto: CADENA
ENTONCES
SI NO
fBuscaConceptoDocto
FIN SI
}
```

```pascal
fBuscaIdConceptoDocto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdConceptoDocto (aIdConcepto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdConcepto

Entero

Por valor

Identificador del concepto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca un concepto por su Identificador.

**Ejemplo:**
Busca Id Concepto Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdConceptoDocto recibe PARAMETRO aIdConcepto: ENTERO
ENTONCES
SI NO
fBuscaIdConceptoDocto
FIN SI
}
```

fPosPrimerConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el primer registro de la tabla de Conceptos.

**Ejemplo:**
Posición Primer Concepto Documento Error = fPosPrimerConceptoDocto SI Error <> 0 Error fPosPrimerConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosUltimaConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimaConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el último registro de la tabla de Conceptos.

**Ejemplo:**
Posición Ultima Concepto Documento Error = fPosUltimaConceptoDocto SI Error <> 0 Error fPosUltimaConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosSiguienteConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Conceptos.

**Ejemplo:**
Posición Siguiente Concepto Documento Error = fPosSiguienteConceptoDocto SI Error <> 0 Error fPosSiguienteConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosAnteriorConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de Conceptos.

**Ejemplo:**
Posición Anterior Concepto Documento Error = fPosAnteriorConceptoDocto SI Error <> 0 Error fPosAnteriorConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosBOFConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Conceptos.

**Ejemplo:**
Posición BOF Concepto Documento Error = fPosBOFConceptoDocto SI Error <> 0 Error fPosBOFConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosEOFConceptoDocto ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFConceptoDocto ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Conceptos.

**Ejemplo:**
Posición EOF Concepto Documento Error = fPosEOFConceptoDocto SI Error <> 0 Error fPosEOFConceptoDocto

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

---

## 17. Funciones de parámetros

---

### 17.1 Bajo nivel – Lectura/Escritura 

#### 17.1.1 Bajo nivel – Lectura/Escritura

```pascal
fLeeDatoParametros ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoParametros (aCampo, aValor, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee un campo del registro actual de parámetros.

**Ejemplo:**
Lee Dato Parametros SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fLeeDatoParametros recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
fLeeDatoParametros
FIN SI
}
```

```pascal
fEditaParametros ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaParametros ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función activa el modo de edición de un registro de los Parámetros.

**Ejemplo:**
Edita Parametros SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fLeeDatoParametros recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
fEditaParametros
FIN SI
}
```

```pascal
fSetDatoParametros ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoParametros(aCampo, aValor )`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por referencia

Nombre del campo 

aValor

Cadena

Por referencia

Valor del campo

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla Parámetros.

**Ejemplo:**
SetDatoParametros SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fLeeDatoParametros recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
Error=fEditaParametros
ENTONCES
SI NO
Error = fSetDatoParametros
ENTONCES
SI NO
fGuardaParametros
FIN SI
FIN SI
FIN SI
}
```

```pascal
fGuardaParametros ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaParametros()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función guarda los cambios efectuados al registro de la tabla Parámetros.

---

## 18. Funciones del catálogo de clasificaciones

---

### 18.1 Bajo nivel – Lectura/Escritura 

#### 18.1.1 Bajo nivel – Lectura/Escritura

```pascal
fEditaClasificacion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaClasificacion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Clasificaciones.

**Ejemplo:**
Edita Clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdClasificacion recibe PARAMETRO aIdClasificacion: ENTERO
ENTONCES
SI NO
fEditaClasificacion
FIN SI
}
```

```pascal
fGuardaClasificacion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaClasificacion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Guarda los cambios realizados a un registro de clasificaciones.

**Ejemplo:**
Guarda Clasificacion SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdClasificacion recibe PARAMETRO aIdClasificacion: ENTERO
ENTONCES
SI NO
fEditaClasificacion
ENTONCES
SI NO
Error = fSetDatoClasificacion recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
ENTONCES
SI NO
fSetDatoClasificacion
Error = fGuardaClasificacion
ENTONCES
SI NO
fGuardaClasificacion
FIN SI
FIN SI
FIN SI
FIN SI
}
```

fCancelarModificacionClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionClasificacion ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función cancela las modificaciones al registro actual de clasificaciones. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
Cancelar Modificacion Clasificacion SI Error <> 0 Error Error = fCancelarModificacionClasificacion SI Error <> 0 Error fCancelarModificacionClasificacion

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdClasificacion recibe PARAMETRO aIdClasificacion: ENTERO
ENTONCES
SI NO
fEditaClasificacion
ENTONCES
SI NO
FIN SI
FIN SI
}
```

fActualizaClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fActualizaClasificacion (aClasificacionDe, aNumClasificacion, aNombreClasificacion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aClasificacionDe

Entero

Por valor

Clasificación de:
1 – Agente
2 – Cliente
3 – Proveedor
4 – Almacén
5 – Producto.

aNumClasificacion

Entero

Por valor

Número de la clasificación (1-6)

aNombreClasificacion

Cadena

Por valor

Texto a actualizar en la clasificación.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función actualiza la dirección del registro de Cliente/Proveedor activo.

**Ejemplo:**
Actualiza Clasificacion SI Error <> 0 Error fActualizaClasificacion

```pascal
{
VAR Error: ENTERO
Error = fActualizaClasificacion recibe PARAMETRO aClasificacionDe: ENTERO, PARAMETRO aNumClasificacion: ENTERO, PARAMETRO aNombreClasificacion: CADENA
ENTONCES
SI NO
FIN SI
}
```

```pascal
fLeeDatoClasificacion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoClasificacion (aCampo, aValr, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de Clasificaciones.

**Ejemplo:**
Lee Dato Clasificacion SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fBuscaIdClasificacion recibe PARAMETRO aIdClasificacion: ENTERO
ENTONCES
SI NO
Error = fLeeDatoClasificacion recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
fLeeDatoClasificacion
FIN SI
FIN SI
}
```

```pascal
fSetDatoClasificacion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoClasificacion (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino.

aValor

Cadena

Por referencia

Valor de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de Clasificaciones.

**Ejemplo:**
Set Dato Clasificacion SI Error <> 0 SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdClasificacion recibe PARAMETRO aIdClasificacion: ENTERO
ENTONCES
Error = fEditaClasificacion
ENTONCES
SI NO
Error = fSetDatoClasificacion recibe PARAMETRO aCampo: CADENA, aValor: CADENA
ENTONCES
SI NO
fSetDatoClasificacion
FIN SI
FIN SI
FIN SI
}
```

---

### 18.2 Bajo nivel - Búsqueda/Navegación

```pascal
fBuscaClasificacion ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaClasificacion (aClasificacionDe, aNumClasificacion)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aClasificacionDe

Entero

Por valor

Clasificación de
1 – Agente
2 – Cliente
3 – Proveedor
4 – Almacén
5 – Producto.

aNumClasificacion

Entero

Por valor

Número de la lasificacion (1-6)

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca una clasificación de acuerdo a los parámetros recibidos y se posiciona en el registro correspondiente.

**Ejemplo:**
Busca clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaClasificacion recibe PARAMETRO aClasificacionDe: ENTERO,PARAMETRO aNumClasificacion: ENTERO
ENTONCES
SI NO
fBuscaClasificacion
FIN SI
}
```

```pascal
fBuscaIdConceptoDocto ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdConceptoDocto (aIdConcepto)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdConcepto

Entero

Por valor

Identificador del concepto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca un concepto por su Identificador.

**Ejemplo:**
Busca Id Concepto Documento SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdConceptoDocto recibe PARAMETRO aIdConcepto: ENTERO
ENTONCES
SI NO
fBuscaIdConceptoDocto
FIN SI
}
```

fPosPrimerClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el primer registro de la tabla Clasificaciones.

**Ejemplo:**
Posicion Primer Clasificacion Error = fPosPrimerClasificacion SI Error <> 0 Error fPosPrimerClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosUltimoClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el último registro de la tabla Clasificaciones.

**Ejemplo:**
Posicion Primer Clasificacion Error = fPosPrimerClasificacion SI Error <> 0 Error fPosPrimerClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosSiguienteClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla Clasificaciones.

**Ejemplo:**
Posicion Siguiente Clasificacion Error = fPosSiguienteClasificacion SI Error <> 0 Error fPosSiguienteClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosAnteriorClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla Clasificaciones.

**Ejemplo:**
Posicion Anterior Clasificacion Error = fPosAnteriorClasificacion SI Error <> 0 Error fPosAnteriorClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosBOFClasificacion ()

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOFClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla Clasificaciones.

**Ejemplo:**
Posicion BOF Clasificacion Error = fPosBOFClasificacion SI Error <> 0 Error fPosBOFClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

fPosEOFClasificacion ()

- **Disponibilidad:** ACONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFClasificacion()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla Clasificaciones.

**Ejemplo:**
Posicion EOF Clasificacion Error = fPosEOFClasificacion SI Error <> 0 Error fPosEOFClasificacion

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

---

## 19. Funciones del catálogo de valores de clasificaciones

---

### 19.1 Bajo nivel – Lectura/Escritura 

#### 19.1.1 Bajo nivel – Lectura/Escritura

```pascal
fInsertaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fInsertaValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Adiciona un nuevo registro en la tabla de Valores de Clasificación en modo de inserción.

**Ejemplo:**
Inserta Valor Clasificacion Error = fInsertaValorClasif SI Error <> 0 Error fInsertaValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fEditaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEditaValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Activa el modo de Edición de un registro en la tabla de Valores de Clasificación.

**Ejemplo:**
Edita Valor Clasificacion SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
Error = fEditaValorClasif
ENTONCES
SI NO
Error = fEditaValorClasif
FIN SI
FIN SI
}
```

```pascal
fGuardaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fGuardaValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Guarda los cambios realizados a un registro de Valores de Clasificación.

**Ejemplo:**
Guarda Valor Clasificacion SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
Error = fEditaValorClasif
ENTONCES
SI NO
fEditaValorClasif
Error = fSetDatoValorClasif recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
ENTONCES
SI NO
fSetDatoValorClasif
Error = fGuardaValorClasif
ENTONCES
SI NO
fGuardaValorClasif
FIN SI
FIN SI
FIN SI
FIN SI
}
```

```pascal
fBorraValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBorraValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Borra un registro en la tabla de Valores de Clasificación.

**Ejemplo:**
Borra Valor Clasificacion SI Error <> 0 Error Error = fBorraValorClasif SI Error <> 0 Error fBorraValorClasif

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
ENTONCES
SI NO
FIN SI
FIN SI
}
```

```pascal
fCancelarModificacionValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fCancelarModificacionValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función cancela las modificaciones al registro actual de Valores de Clasificación. El registro debe estar en modo de edición o inserción.

**Ejemplo:**
Cancela Modificacion Clasificacion SI Error <> 0 Error SI Error <> 0 Error Error = fCancelarModificacionClasificacion SI Error <> 0 Error fCancelarModificacionClasificacion

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
Error = fEditaValorClasif
ENTONCES
SI NO
fEditaValorClasif
ENTONCES
SI NO
FIN SI
FIN SI
FIN SI
}
```

```pascal
fEliminarValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fEliminarValorClasif (aClasificacionDe, aNumClasificacion, aCodValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aClasificacionDe

Entero

Por valor

Clasificación de
1 – Agente
2 – Cliente
3 – Proveedor
4 – Almacen
5 – Producto.

aNumClasificacion

Entero

Por valor

Numero de la clasificacion (1-6)

aCodValorClasif

Cadena

Por valor

Código del valor clasificación producto

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función elimina un registro de la tabla Valores de Clasificación usando su código.

**Ejemplo:**
Eliminar Valor Clasificacion SI Error <> 0 Error SI Error <> 0 Error fEliminarValorClasif

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
Error = fEliminarValorClasif recibe PARAMETRO aClasificacionDe: ENTERO, PARAMETRO aNumClasificacion: ENTERO, PARAMETRO aCodigoValorClasificacion: CADENA
ENTONCES
SI NO
FIN SI
FIN SI
}
```

```pascal
fSetDatoValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fSetDatoValorClasif (aCampo, aValor)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función escribe el valor indicado en el campo correspondiente en el registro activo de la tabla de Valores de Clasificación.

**Ejemplo:**
Set Dato Valor Clasificacion SI Error <> 0 Error SI Error <> 0 Error SI Error <> 0 Error FSetDatoValorClasif

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
Error = fEditaValorClasif
ENTONCES
SI NO
fEditaValorClasif
Error = fSetDatoValorClasif recibe PARAMETRO aCampo: CADENA, PARAMETRO aValor: CADENA
ENTONCES
SI NO
FIN SI
FIN SI
FIN SI
}
```

---

### 19.2 Bajo nivel - Búsqueda/Navegación

```pascal
fLeeDatoValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLeeDatoValorClasif (aCampo, aValor, aLen)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCampo

Cadena

Por valor

Campo destino 

aValor

Cadena

Por valor

Valor de escritura

aLen

Entero

Por valor

Longitud del dato de lectura.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aValor: Al finalizar la función este parámetro contiene el valor del campo especificado.

**Descripción:**
Esta función lee el valor indicado del campo correspondiente en el registro activo de la tabla de Valores de Clasificación.

**Ejemplo:**
Lee Dato Valor Clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR aValor: STRINGBUILDER
Error = fLeeDatoValorClasif recibe PARAMETRO aCampo: CADENA, aValor, PARAMETRO aLen: ENTERO
ENTONCES
SI NO
fLeeDatoValorClasif
FIN SI
}
```

```pascal
fBuscaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaClasificacion (aClasificacionDe, aNumClasificacion, aCodValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aClasificacionDe

Entero

Por valor

Clasificación de
1 – Agente
2 – Cliente
3 – Proveedor
4 – Almacen
5 – Producto.

aNumClasificacion

Entero

Por valor

Numero de la clasificacion (1-6)

aCodValorClasif

Cadena

Por valor

Código del Valor Clasificacion Producto

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca una clasificacion de acuerdo a los parámetros recibidos y se posiciona en el registro correspondiente.

**Ejemplo:**
Busca Valor Clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaValorClasif recibe PARAMETRO aClasificacionDe: ENTERO, PARAMETRO aNumClasificacion: ENTERO, PARAMETRO aCodValorClasif: CADENA
ENTONCES
SI NO
fBuscaValorClasif
FIN SI
}
```

```pascal
fBuscaIdValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaIdValorClasif (aIdValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdValorClasif

Entero

Por valor

Identificador del valor de clasificación.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función busca un valor de clasificación por su Identificador y se posiciona en el registro correspondiente.

**Ejemplo:**
Busca Id Valor Clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
Error = fBuscaIdValorClasif recibe PARAMETRO aIdValorClasif: ENTERO
ENTONCES
SI NO
fBuscaIdValorClasif
FIN SI
}
```

```pascal
fPosPrimerValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosPrimerValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el primer registro de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion Primer Valor Clasificacion Error = fPosPrimerValorClasif SI Error <> 0 Error fPosPrimerValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosUltimoValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el último registro de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion Ultimo Valor Clasificacion Error = fPosUltimoValorClasif SI Error <> 0 Error fPosUltimoValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosUltimoValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosUltimoValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función se ubica en el último registro de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion Siguiente Valor Clasificacion Error = fPosSiguienteValorClasif SI Error <> 0 Error fPosSiguienteValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosSiguienteValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosSiguienteValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero.
0 (cero) – Falso.

**Descripción:**
Esta función se ubica en el siguiente registro de la posición actual de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion Siguiente Valor Clasificacion Error = fPosSiguienteValorClasif SI Error <> 0 Error fPosSiguienteValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosAnteriorValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosAnteriorValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito.
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error.

**Descripción:**
Esta función se ubica en el registro anterior de la posición actual de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion Anterior Valor Clasificacion Error = fPosAnteriorValorClasif SI Error <> 0 Error fPosAnteriorValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosBOFValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosBOFValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el inicio de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion BOF Valor Clasificacion Error = fPosBOFValorClasif SI Error <> 0 Error fPosBOFValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

```pascal
fPosEOFValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fPosEOFValorClasif ()`

- **Parámetros:** *Sin parámetros*

- **Retorna:** Valores enteros:
1 (uno) – Verdadero. 0 (cero) – Falso.

**Descripción:**
Informa si el registro activo se encuentra en el fin de la tabla de Valores de Clasificación.

**Ejemplo:**
Posicion EOF Valor Clasificacion Error = fPosEOFValorClasif SI Error <> 0 Error fPosEOFValorClasif

```pascal
{
VAR Error: ENTERO
ENTONCES
SI NO
FIN SI
}
```

---

### 19.3 Alto nivel – Lectura/Escritura 

#### 19.3.1 Alto nivel – Lectura/Escritura

```pascal
fAltaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fAltaValorClasif (aIdValorClasif, astValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aIdValorClasif

Entero

Por referencia

Identificador de la dirección.

astValorClasif

tValorClasif

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error 
 
aIdValorClasif: Al finalizar la función este parámetro contiene el identificador del nuevo valor de clasificación.

**Descripción:**
Esta función da de alta un nuevo valor de clasificación.

**Ejemplo:**
Alta Valor Clasificacion SI Error <> 0 Error

```pascal
{
VAR Error: ENTERO
VAR tValor: tValorClasificacion
Error = fAltaValorClasif REFERENCIA aValor: ENTERO, REFERENCIA tValor
ENTONCES
SI NO
fAltaValorClasif
FIN SI
}
```

```pascal
fActualizaValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fBuscaClasificacion (aClasificacionDe, aNumClasificacion, aCodValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

aCodigoValorClasif

Cadena

Por valor

Código del valor de clasificación.

astValorClasif

tValorClasif

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función actualiza el valor de clasificación del registro especificado por el parametro
aCodigoValorClasif.

**Ejemplo:**
Actualiza Valor Clasificacion SI Error <> 0 Error fActualizaValorClasif

```pascal
{
VAR Error: ENTERO
VAR tValor: tValorClasificacion
Error = fActualizaValorClasif PARAMETRO aCodigoValorClasificacion: CADENA, REFERENCIA tValor
ENTONCES
SI NO
FIN SI
}
```

```pascal
fLlenaRegistroValorClasif ()
```

- **Disponibilidad:** CONTPAQi Factura Electrónica® 14.1.0
CONTPAQi Comercial Premium® 12.1.0

- **Sintaxis:** `fLlenaRegistroValorClasif (astValorClasif)`

- **Parámetros:** Nombre

Tipo

Uso

Descripción

astValorClasif

tValorClasif

Por valor

Tipo de dato abstracto.

- **Retorna:** Valores enteros: 
kSIN_ERRORES = 0 (cero) – La operación fue realizada con éxito. 
!kSIN_ERRORES = Diferente de 0 (cero) – Código del error

**Descripción:**
Esta función asigna al registro de la base de datos los valores de la estructura de datos del valor de clasificación.

**Ejemplo:**
Llena Registro Valor Clasificacion Error = fLlenaRegistroValorClasif REFERENCIA tValor SI Error <> 0 Error fLlenaRegistroValorClasif

```pascal
{
VAR Error: ENTERO
VAR tValor: tValorClasificacion
ENTONCES
SI NO
FIN SI
}
```

---

## 20. Constantes del SDK

### 20..1 Constantes de longitud

| Nombre | Longitud | Descripción |
| --- | --- | --- |
| kLongFecha | 23 | Longitud máxima de caracteres para los campos de fechas. |
| kLongSerie | 11 | Longitud máxima de caracteres para las series. |
| kLongCodigo | 30 | Longitud máxima de caracteres usada para los códigos. |
| kLongNombre | 60 | Longitud máxima de caracteres para los nombres. |
| kLongReferencia | 20 | Longitud máxima de caracteres para las referencias. |
| kLongDescripcion | 60 | Longitud máxima de caracteres para las descripciones. |
| kLongCuenta | 100 | Longitud máxima de caracteres para las cuentas. |
| kLongMensaje | 3000 | Longitud máxima de caracteres para los mensajes. |
| kLongNombreProducto | 255 | Longitud máxima de caracteres para los nombres de producto. |
| kLongAbreviatura | 3 | Longitud máxima de caracteres para las abreviaturas. |
| kLongCodValorClasif | 3 | Longitud máxima de caracteres para los valores de clasificación. |
| kLongDenComercial | 50 | Longitud máxima de caracteres para la denominación comercial. |
| kLongRepLegal | 50 | Longitud máxima de caracteres para el representante legal. |
| kLongTextoExtra | 50 | Longitud máxima de caracteres para los textos extra. |
| kLongRFC | 20 | Longitud máxima de caracteres para el RFC. |
| kLongCURP | 20 | Longitud máxima de caracteres para el CURP. |
| kLongDesCorta | 20 | Longitud máxima de caracteres para descripciones cortas. |
| kLongNumeroExtInt | 6 | Longitud máxima de caracteres para el número exterior/interior. |
| kLongNumeroExpandido | 30 | Longitud máxima de caracteres para el número expandido. |
| kLongCodigoPostal | 6 | Longitud máxima de caracteres para el código postal. |
| kLongTelefono | 15 | Longitud máxima de caracteres para números de teléfono. |
| kLongEmailWeb | 50 | Longitud máxima de caracteres para direcciones de correo electrónico. |
| kLongSelloSat | 175 | Longitud máxima de caracteres para el sello del SAT |
| kLonSerieCertSAT | 20 | Longitud máxima de caracteres para la serie del certificado del SAT. |
| kLongFechaHora | 35 | Longitud máxima de caracteres para la fecha y hora. |
| kLongSelloCFDI | 175 | Longitud máxima de caracteres para el sello del CFDI. |
| kLongCadOrigComplSAT | 500 | Longitud máxima de caracteres para la cadena original. |
| kLongitudUUID | 36 | Longitud máxima de caracteres para el UUID. |
| kLongitudRegimen | 100 | Longitud máxima de caracteres para el régimen fiscal de la empresa. |
| kLongitudMoneda | 60 | Longitud máxima de caracteres para la moneda. |
| kLongitudFolio | 16 | Longitud máxima de caracteres para el folio. |
| kLongitudMonto | 30 | Longitud máxima de caracteres para el monto. |
| kLogitudLugarExpedicion | 400 | Longitud máxima de caracteres para el lugar de expedición. |

---

## 21. Tipos de datos abstractos del SDK

### 21..1 Definición de las estructuras de datos

Documentos – RegDocumento – tDocumento

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aFolio | Doble | NA | Folio del documento. |
| aNumMoneda | Entero | NA | Moneda del documento. 1 = Pesos MN, 2 = Moneda extranjera. |
| aTipoCambio | Doble | NA | Tipo de cambio del documento. |
| aImporte | Doble | NA | Importe del documento. Sólo se usa en documentos de cargo/abono. |
| aDescuentoDoc1 | Doble | NA | No tiene uso, valor por omisión = 0 (cero). |
| aDescuentoDoc2 | Doble | NA | No tiene uso, valor por omisión = 0 (cero). |
| aSistemaOrigen | Entero | NA | Valor mayor a 5 que indica una aplicación diferente a los PAQ's. |
| aCodConcepto | Cadena | kLongCodigo + 1 | Código del concepto del documento. |
| aSerie | Cadena | kLongSerie + 1 | Serie del documento. |
| aFecha | Cadena | kLongFecha + 1 | Fecha del documento. Formato mm/dd/aaaa Las “/” diagonales son parte del formato. |
| aCodigoCteProv | Cadena | kLongCodigo + 1 | Código del Cliente/Proveedor. |
| aCodigoAgente | Cadena | kLongCodigo + 1 | Código del Agente. |
| aReferencia | Cadena | kLongReferencia + 1 | Referencia del Documento. |
| aAfecta | Entero | NA | No tiene uso, valor por omisión = 0 (cero). |
| aGasto1 | Double | NA | Valor por omisión = 0 (cero). |
| aGasto2 | Double | NA | Valor por omisión = 0 (cero). |
| aGasto3 | Double | NA | Valor por omisión = 0 (cero). |

Llave del Documento – RegLlaveDoc – tLlaveDoc*

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aConsepto | Cadena | kLongCodigo + 1 | Código del concepto del documento. |
| aSerie | Cadena | kLongSerie + 1 | Serie del documento. |
| aFolio | Doble | NA | Folio del documento. |

Movimientos – RegMovimiento – tMovimiento

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aConsecutivo | Entero | NA | Consecutivo del movimiento. |
| aUnidades | Doble | NA | Unidades del movimiento. |
| aPrecio | Doble | NA | Precio del movimiento (para doctos. de venta ). |
| aCosto | Doble | NA | Costo del movimiento (para doctos. de compra). |
| aCodProdSer | Cadena | kLongCodigo + 1 | Códogo del producto o servicio. |
| aCodAlmacen | Cadena | kLongCodigo + 1 | Código del Almacén. |
| aReferencia | Cadena | kLongReferencia + 1 | Referencia del movimiento. |
| aCodClasificacion | Cadena | kLongCodigo + 1 | Código de la clasificacuión |

Movimientos – RegMovimiento – tMovimientoDesc

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aConsecutivo | Entero | NA | Consecutivo del movimiento. |
| aUnidades | Doble | NA | Unidades del movimiento. |
| aPrecio | Doble | NA | Precio del movimiento (para doctos. de venta ). |
| aCosto | Doble | NA | Costo del movimiento (para doctos. de compra). |
| aPorcDescto1 | Doble | NA | Porcentaje del Descuento 1 |
| aImporteDescto1 | Doble | NA | Importe del Descuento 1 |
| aPorcDescto2 | Doble | NA | Porcentaje del Descuento 2 |
| aImporteDescto2 | Doble | NA | Importe del Descuento 2 |
| aPorcDescto3 | Doble | NA | Porcentaje del Descuento 3 |
| aImporteDescto3 | Doble | NA | Importe del Descuento 3 |
| aPorcDescto4 | Doble | NA | Porcentaje del Descuento 4 |
| aImporteDescto4 | Doble | NA | Importe del Descuento 4 |
| aPorcDescto5 | Doble | NA | Porcentaje del Descuento 5 |
| aImporteDescto5 | Doble | NA | Importe del Descuento 5 |
| aCodProdSer | Cadena | kLongCodigo + 1 | Códogo del producto o servicio. |
| aCodAlmacen | Cadena | kLongCodigo + 1 | Código del Almacén. |
| aReferencia | Cadena | kLongReferencia + 1 | Referencia del movimiento. |
| aCodClasificacion | Cadena | kLongCodigo + 1 | Código de la clasificacuión |

Movimientos con Serie/Capas – SeriesCapas – tSeriesCapas

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aUnidades | Doble | NA | Unidades del movimiento. |
| aTipoCambio | Doble | NA | Tipo de cambio del movimiento. |
| aSeries | Cadena | kLongCodigo + 1 | Series del movimiento. |
| aPedimento | Cadena | kLongDescripcion + 1 | Pedimento del movimiento. |
| aAgencia | Cadena | kLongDescripcion + 1 | Agencia aduanal del movimiento. |
| aFechaPedimento | Cadena | kLongFecha + 1 | Fecha de pedimento del movimiento. |
| aNumeroLote | Cadena | kLongDescripcion + 1 | Número de lote del movimiento. |
| aFechaFabricacion | Cadena | kLongFecha + 1 | Fecha de fabricación del movimiento. |
| aFechaCaducidad | Cadena | kLongFecha + 1 | Fecha de Caducidad del movimiento. |

Movimientos con Caracteristicas – Caracteristicas – tCaracteristicas

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aUnidades | Doble | NA | Unidades del movimiento. |
| aValorCaracteristica1 | Cadena | kLongDescripcion + 1 | Valor de la xaracteristica 1 del movimiento. |
| aValorCaracteristica2 | Cadena | kLongDescripcion + 1 | Valor de la xaracteristica 2 del movimiento. |
| aValorCaracteristica3 | Cadena | kLongDescripcion + 1 | Valor de la xaracteristica 3 del movimiento. |

Movimientos con datos adicionales – RegTipoProducto – tTipoProducto

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aSeriesCapas | aSeriesCapas | NA | Tipo de dato abstracto: tSeriesCapas. |
| aCaracteristicas | aCaracteristicas | NA | Tipo de dato abstracto: Caracteristicas. |

Llave de aperturas – RegLlaveAper - tLlaveAper

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| aCodCaja | Cadena | kLongCodigo + 1 | Código de la caja. |
| aFechaApe | Cadena | kLongFecha + 1 | Fecha de apertura. |

Productos – RegProducto – tProducto

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| cCodigoProducto | Cadena | kLongCodigo + 1 | Código del producto. |
| cNombreProducto | Cadena | kLongNombre + 1 | Nombre del producto. |
| cDescripcionProducto | Cadena | kLongNombreProducto + 1 | Descripción del producto. |
| cTipoProducto | Entero | NA | 1- Producto, 2 - Paquete, 3 - Servicio |
| cFechaAltaProducto | Cadena | kLongFecha + 1 | Fecha de alta del producto. |
| cFechaBaja | Cadena | kLongFecha + 1 | Fecha de baja del producto. |
| cStatusProducto | Entero | NA | 0 - Baja Lógica, 1 – Alta |
| cControlExistencia | Entero | NA | Control de exixtencia. |
| cMetodoCosteo | Entero | NA | 1 - Costo Promedio Base a Entradas, 2 - Costo Promedio Base a Entradas Almacen 3 - Último costo, 4 - UEPS, 5 - PEPS, 6 - Costo específico, 7 - Costo Estandar. |
| cCodigoUnidadBase | Cadena | kLongCodigo + 1 | Código de la unidad base. |
| cCodigoUnidadNoConvertible | Cadena | kLongCodigo + 1 | Código de la unidad no convertible. |
| cPrecio1 | Doble | NA | Lista de precios 1. |
| cPrecio2 | Doble | NA | Lista de precios 2. |
| cPrecio3 | Doble | NA | Lista de precios 3. |
| cPrecio4 | Doble | NA | Lista de precios 4. |
| cPrecio5 | Doble | NA | Lista de precios 5. |
| cPrecio6 | Doble | NA | Lista de precios 6. |
| cPrecio7 | Doble | NA | Lista de precios 7. |
| cPrecio8 | Doble | NA | Lista de precios 8. |
| cPrecio9 | Doble | NA | Lista de precios 9. |
| cPrecio10 | Doble | NA | Lista de precios 10. |
| cImpuesto1 | Doble | NA | Impuesto 1. |
| cImpuesto2 | Doble | NA | Impuesto 2. |
| cImpuesto3 | Doble | NA | Impuesto 3. |
| cRetencion1 | Doble | NA | Retención 1. |
| cRetencion2 | Doble | NA | Retención 2. |
| cNombreCaracteristica1 | Cadena | kLongAbreviatura + 1 | Nombre de la caracteristica 1. |
| cNombreCaracteristica2 | Cadena | kLongAbreviatura + 1 | Nombre de la caracteristica 2. |
| cNombreCaracteristica3 | Cadena | kLongAbreviatura + 1 | Nombre de la caracteristica 3. |
| cCodigoValorClasificacion1 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 1. |
| cCodigoValorClasificacion2 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 2. |
| cCodigoValorClasificacion3 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 3. |
| cCodigoValorClasificacion4 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 4. |
| cCodigoValorClasificacion5 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 5. |
| cCodigoValorClasificacion6 | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación 6. |
| cTextoExtra1 | Cadena | kLongTextoExtra + 1 | Texto extra 1. |
| cTextoExtra2 | Cadena | kLongTextoExtra + 1 | Texto extra 2. |
| cTextoExtra3 | Cadena | kLongTextoExtra + 1 | Texto extra 3. |
| cFechaExtra | Cadena | kLongFecha + 1 | Fecha extra |
| cImporteExtra1 | Doble | NA | Importe Extra 1. |
| cImporteExtra2 | Doble | NA | Importe Extra 2. |
| cImporteExtra3 | Doble | NA | Importe Extra 3. |
| cImporteExtra4 | Doble | NA | Importe Extra 4. |

Cliente/Proveedor-RegCteProv-TcteProv

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| cCodigoCliente | Cadena | kLongCodigo + 1 | Código del Cliente / Proveedor. |
| cRazonSocial | Cadena | kLongNombre + 1 | Razón social. |
| cFechaAlta | Cadena | kLongFecha + 1 | Fecha de alta. |
| cRFC | Cadena | kLongRFC + 1 | RFC. |
| cCURP | Cadena | kLongCURP + 1 | CURP. |
| cDenComercial | Cadena | kLongDenComercial + 1 | Denominación comercial. |
| cRepLegal | Cadena | kLongRepLegal + 1 | Representante legal. |
| cNombreMoneda | Cadena | kLongNombre + 1 | Nombre de la moneda. |
| cListaPreciosCliente | Entero | NA | Lista de precios. |
| cDescuentoMovto | Doble | NA | Descuento. |
| cBanVentaCredito | Entero | NA | Bandera de venta a crédito. 0 – No se permite, 1 – Se permite. |
| cCodigoValorClasificacionCliente1 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 1. |
| cCodigoValorClasificacionCliente2 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 2. |
| cCodigoValorClasificacionCliente3 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 3. |
| cCodigoValorClasificacionCliente4 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 4. |
| cCodigoValorClasificacionCliente5 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 5. |
| cCodigoValorClasificacionCliente6 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 6. |
| cTipoCliente | Entero | NA | 1 – Cliente, 2 – Cliente/Proveedor, 3 – Proveedor. |
| cEstatus | Entero | NA | Estado: 0 – Inactivo, 1 – Activo. |
| cFechaBaja | Cadena | kLongFecha + 1 | Fecha de baja. |
| cFechaUltimaRevision | Cadena | kLongFecha + 1 | Fecha de última revisión. |
| cLimiteCreditoCliente | Doble | NA | Limite de crédito. |
| cDiasCreditoCliente | Entero | NA | Días de crédito del cliente. |
| cBanExcederCredito | Entero | NA | Bandera de exceder crédito. 0 – No se permite, 1 – Se permite. |
| cDescuentoProntoPago | Doble | NA | Descuento por pronto pago. |
| cDiasProntoPago | Entero | NA | Días para pronto pago. |
| cInteresMoratorio | Doble | NA | Interes moratorio. |
| cDiaPago | Entero | NA | Día de pago. |
| cDiasRevision | Entero | NA | Días de revisión. |
| cMensajeria | Cadena | kLongDesCorta + 1 | Mensajeria. |
| cCuentaMensajeria | Cadena | kLongDescripcion + 1 | Cuenta de mensajeria. |
| cDiasEmbarqueCliente | Entero | NA | Dias de embarque del cliente. |
| cCodigoAlmacen | Cadena | kLongCodigo + 1 | Código del almacén. |
| cCodigoAgenteVenta | Cadena | kLongCodigo + 1 | Código del agente de venta. |
| cCodigoAgenteCobro | Cadena | kLongCodigo + 1 | Código del agente de cobro. |
| cRestriccionAgente | Entero | NA | Restricción de agente. |
| cImpuesto1 | Doble | NA | Impuesto 1. |
| cImpuesto2 | Doble | NA | Impuesto 2. |
| cImpuesto3 | Doble | NA | Impuesto 3. |
| cRetencionCliente1 | Doble | NA | Retención al cliente 1. |
| cRetencionCliente2 | Doble | NA | Retención al cliente 2. |
| cCodigoValorClasificacionProveedor1 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 1. |
| cCodigoValorClasificacionProveedor2 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 2. |
| cCodigoValorClasificacionProveedor3 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 3. |
| cCodigoValorClasificacionProveedor4 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 4. |
| cCodigoValorClasificacionProveedor5 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 5. |
| cCodigoValorClasificacionProveedor6 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 6. |
| cLimiteCreditoProveedor | Doble | NA | Limite de credito del proveedor. |
| cDiasCreditoProveedor | Entero | NA | Días de credito del proveedor. |
| cTiempoEntrega | Entero | NA | Tiempo de entrega. |
| cDiasEmbarqueProveedor | Entero | NA | Días de embarque. |
| cImpuestoProveedor1 | Doble | NA | Impuesto proveedor 1. |
| cImpuestoProveedor2 | Doble | NA | Impuesto proveedor 2. |
| cImpuestoProveedor3 | Doble | NA | Impuesto proveedor 3. |
| cRetencionProveedor1 | Doble | NA | Retención proveedor 1. |
| cRetencionProveedor2 | Doble | NA | Retención proveedor 2. |
| cBanInteresMoratorio | Entero | NA | Bandera de cálculo de interes moratorio. 0 – No se calculan, 1 – Si se calculan. |
| cTextoExtra1 | Cadena | kLongTextoExtra + 1 | Texto extra 1. |
| cTextoExtra2 | Cadena | kLongTextoExtra + 1 | Texto extra 2. |
| cTextoExtra3 | Cadena | kLongTextoExtra + 1 | Texto extra 3. |
| cFechaExtra | Cadena | kLongFecha + 1 | Fecha extra. |
| cImporteExtra1 | Doble | NA | Importe extra 1. |
| cImporteExtra2 | Doble | NA | Importe extra 2. |
| cImporteExtra3 | Doble | NA | Importe extra 3. |
| cImporteExtra4 | Doble | NA | Importe extra 4. |
| cCodigoValorClasificacionProveedor1 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 1. |
| cCodigoValorClasificacionProveedor2 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 2. |
| cCodigoValorClasificacionProveedor3 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 3. |
| cCodigoValorClasificacionProveedor4 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 4. |
| cCodigoValorClasificacionProveedor5 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 5. |
| cCodigoValorClasificacionProveedor6 | Cadena | kLongCodValorClasif + 1 | Código del valor de clasificación 6. |
| cLimiteCreditoProveedor | Doble | NA | Limite de credito del proveedor. |
| cDiasCreditoProveedor | Entero | NA | Días de credito del proveedor. |
| cTiempoEntrega | Entero | NA | Tiempo de entrega. |
| cDiasEmbarqueProveedor | Entero | NA | Días de embarque. |
| cImpuestoProveedor1 | Doble | NA | Impuesto proveedor 1. |
| cImpuestoProveedor2 | Doble | NA | Impuesto proveedor 2. |
| cImpuestoProveedor3 | Doble | NA | Impuesto proveedor 3. |
| cRetencionProveedor1 | Doble | NA | Retención proveedor 1. |
| cRetencionProveedor2 | Doble | NA | Retención proveedor 2. |
| cBanInteresMoratorio | Entero | NA | Bandera de cálculo de interes moratorio. 0 – No se calculan, 1 – Si se calculan. |
| cTextoExtra1 | Cadena | kLongTextoExtra + 1 | Texto extra 1. |
| cTextoExtra2 | Cadena | kLongTextoExtra + 1 | Texto extra 2. |
| cTextoExtra3 | Cadena | kLongTextoExtra + 1 | Texto extra 3. |
| cFechaExtra | Cadena | kLongFecha + 1 | Fecha extra. |
| cImporteExtra1 | Doble | NA | Importe extra 1. |
| cImporteExtra2 | Doble | NA | Importe extra 2. |
| cImporteExtra3 | Doble | NA | Importe extra 3. |
| cImporteExtra4 | Doble | NA | Importe extra 4. |

Valor de cClasificación - RegValorClasificacion - TValorClasificacion

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| cClasificacionDe | Entero | NA | Clasificación. |
| cNumClasificacion | Entero | NA | Número de la clasificación. |
| cCodigoValorClasificacion | Cadena | kLongCodValorClasif + 1 | Código del valor de la clasificación. |
| cValorClasificacion | Cadena | kLongDescripcion + 1 | Valor de la clasificación. |

Unidad - RegUnidad - TUnidad

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| cNombreUnidad | Cadena | kLongNombre + 1 | Nombre de la unidad. |
| cAbreviatura | Cadena | kLongAbreviatura + 1 | Abreviatura. |
| cDespliegue | Cadena | kLongAbreviatura + 1 | Valor de despliegue. |

Direcciones – RegDireccion– tDireccion

| Campo | Tipo | Longitud | Descripción |
| --- | --- | --- | --- |
| cCodCteProv | Cadena | kLongCodigo + 1 | Código cliente / proveedor. |
| cTipoCatalogo | Entero | NA | Tipo de catálogo. |
| cTipoDireccion | Entero | NA | Tipo de dirección. |
| cNombreCalle | Cadena | kLongDescripcion + 1 | Calle. |
| cNumeroExterior | Cadena | kLongNumeroExtInt + 1 | Número exterior. |
| cNumeroInterior | Cadena | kLongNumeroExtInt + 1 | Número interior. |
| cColonia | Cadena | kLongDescripcion + 1 | Colonia. |
| cCodigoPostal | Cadena | kLongCodigoPostal + 1 | Código postal. |
| cTelefono1 | Cadena | kLongTelefono + 1 | Telefono 1. |
| cTelefono2 | Cadena | kLongTelefono + 1 | Telefono 2. |
| cTelefono3 | Cadena | kLongTelefono + 1 | Telefono 3. |
| cTelefono4 | Cadena | kLongTelefono + 1 | Telefono 4. |
| cEmail | Cadena | kLongEmailWeb + 1 | Correo electrónico. |
| cDireccionWeb | Cadena | kLongEmailWeb + 1 | Página web. |
| cCiudad | Cadena | kLongDescripcion + 1 | Ciudad, |
| cEstado | Cadena | kLongDescripcion + 1 | Estado. |
| cPais | Cadena | kLongDescripcion + 1 | País. |
| cTextoExtra | Cadena | kLongDescripcion + 1 | Texto extra. |

---

## 22. Equivalencias de tipos de datos

Contenido y tamaño

Visual Basic

C++

C#

Jscript

Visual FoxPro

Datos desconocidos

no disponible

VARIANT

Derive los tipos y después vincule al nodo Derived Types.

Object

Variant

Decimal

Decimal (estructura de .NET Framework)

DECIMAL

decimal

decimal

no disponible

Fecha

Date (estructura de .NET Framework)

DATE

DateTime

DateTime ObjetoDate

Date DateTime

Carácter SBCS (1 byte)

no disponible

signed char int8

no disponible

sbyte

Character

Carácter Unicode (2 bytes)

Char (estructura de .NET Framework)

wchar_t

char

char

no disponible

Secuencia de caracteres Unicode

String (clase de .NET Framework)

wchar_t*

string

String

VarChar

Booleano (depende de la plataforma)

Boolean (estructura de .NET Framework)

VARIANT_BOOL

bool

boolean

Logical

1 byte

SByte (Tipo de datos, Visual Basic)(estructura de .NET Framework)

signed char

sbyte

no disponible

no disponible

2 bytes

Short (estructura de .NET Framework)

signed short int int16

short

short

no disponible

4 bytes

Integer (estructura de .NET Framework)

long, (long int, signed long int)

int

int

Integer

8 bytes

Long (estructura de .NET Framework)

int64

long

long

Float

1 byte sin signo

Byte (estructura de .NET Framework)

BYTE bool

byte

byte

Integer

2 bytes sin signo

UShort (Tipo de datos, Visual Basic)(estructura de .NET Framework)

unsigned short

ushort

no disponible

no disponible

4 bytes sin signo

UInteger (Tipo de datos) (estructura de .NET Framework)

unsigned

```pascal
int yunsigned long
```

uint

no disponible

no disponible

8 bytes sin signo

ULong (Tipo de datos, Visual Basic)(estructura de .NET Framework)

unsigned _int64

ulong

no disponible

no disponible

Punto flotante de 4 bytes

Single (estructura de .NET Framework)

float

float

float

Float

Punto flotante de 8 bytes

Double (estructura de .NET Framework)

double

double

Double

Double

Secuencia de caracteres modificable (Buffer)

StringBuilder (clase de .NET Framework)

wchar_t* o char* (parámetro de salida/puntero)

StringBuilder

no disponible

no disponible

---

## 23. Casos prácticos

---

### 23.1 Alta de dirección de cliente mediante SDK

Una empresa dedicada a la distribución de productos necesita mantener actualizada la información de contacto de sus clientes y/o proveedores dentro del sistema CONTPAQi Comercial Premium®, especialmente las direcciones fiscales y de envío.

En muchos casos, un cliente puede contar con múltiples direcciones, por lo que resulta fundamental registrarlas correctamente.

Para optimizar este proceso y evitar capturas manuales, la empresa utiliza el SDK de CONTPAQi, ya que mediante este procedimiento, se garantiza que la información del cliente esté completa, estandarizada y disponible para su uso en operaciones comerciales y fiscales.

Para lograrlo, se hace uso de dos funciones principales del SDK:

- fBuscaCteProv() que permite localizar y posicionar un cliente o proveedor previamente registrado.
- fAltaDireccion() que registra una nueva dirección asociada al cliente o proveedor.

#### 23.1.1 Configuración en el sistema de CONTPAQi Comercial Premium®

Paso

Acción

Antes de ejecutar el proceso, es necesario que el cliente o proveedor ya se encuentre registrado en el sistema:

Implementación C# if(nError != 0 ) MessageBox.Show("Error Alta de la direccion: " + MessageBox.Show("Se dio de Alta la direccion de manera

```csharp
private void Direccion_btn_Click(object sender, EventArgs e)
{
SDK.tDireccion lDireccion = new SDK.tDireccion(); int nError = 0;
nError = SDK.fBuscaCteProv("CL01");
if (nError != 0)
{
MessageBox.Show("Error Buscar cliente: " + SDK.rError(nError));
}
else
{
int idDireccion = 1;
lDireccion.cCiudad = "San Pedro Tlaquepaque"; lDireccion.cPais = "Mexico"; lDireccion.cEstado = "Jalisco"; lDireccion.cCodigoPostal = "45638"; lDireccion.cCodCteProv = "CL01"; lDireccion.cColonia = "Los Puestos";
lDireccion.cNombreCalle = "C. Francisco I. Madero"; lDireccion.cNumeroExterior = "1029";
lDireccion.cTipoDireccion = 0; //FISCAL = 0, ENVIO = 1;
lDireccion.cTipoCatalogo = 1; // 1 = Clientes
nError = SDK.fAltaDireccion(ref idDireccion, ref lDireccion);
{
SDK.rError(nError));
}
else
{
correcta");
}
}
```

Resultado Al ejecutar el proceso de manera exitosa, la dirección queda asociada al cliente o proveedor indicado y puede consultarse desde la pestaña Domicilios dentro de su registro en CONTPAQi Comercial Premium®.

---

### 23.2 Emitir factura de gasolina (producto con IEPS)

Una empresa dedicada al transporte o distribución requiere emitir facturas por la venta de gasolina, un producto que está gravado con el IEPS (Impuesto Especial sobre Producción y Servicios). Este tipo de operaciones deben cumplir con los requisitos fiscales del SAT y con los criterios contables internos, especialmente por el manejo del IEPS.

La empresa utiliza el SDK para automatizar la emisión de las facturas, asegurando que el IEPS sea desglosado correctamente, y que la operación quede registrada como una cuenta por cobrar a favor de la empresa.

Para lograrlo, se hace uso de dos funciones principales del SDK:

- fAltaDocumento () que permite generar un documento con encabezado.
- fAltaMovimiento () que registra el movimiento del producto dentro del documento.

#### 23.2.1 Configuración en el sistema de CONTPAQi Comercial Premium®

Paso

Acción

Ingresa al menú Redefinir Empresa y selecciona la pestaña 6. Impuestos y retenciones.

Configura el impuesto IEPS como primer impuesto y en segundo lugar el IVA:

Ve al menú Configuración y elige la opción Conceptos. Selecciona el concepto que se utilizará para realizar las siguientes modificaciones:

Ingresa al catálogo de Clientes, para seleccionar la opción Desglosar IEPS en CFD:

Por último ingresa al catálogo de Productos para asignar el porcentaje correspondiente a cada uno de los impuestos aplicables:

Implementación C#

```csharp
private void factura_Click(object sender, EventArgs e)
{
int nError = 0;
SDK.tDocumento ltDocumento = new SDK.tDocumento();
SDK.tMovimiento ltMovimiento = new SDK.tMovimiento();
int lIdDocumento = 0;
int lIdMovimiento = 0;
ltDocumento.aCodConcepto = "4"; //Concepto del documento
ltDocumento.aCodigoCteProv = "003"; //Codigo del cliente
ltDocumento.aSerie = "";
ltDocumento.aFolio = 0;
ltDocumento.aFecha = DateTime.Today.ToString("MM/dd/yyyy");
ltDocumento.aSistemaOrigen = 205;
ltDocumento.aNumMoneda = 1;
ltDocumento.aTipoCambio = 1;
ltDocumento.aReferencia = "Documento SDK";
nError = SDK.fAltaDocumento(ref lIdDocumento, ref ltDocumento);
if (nError != 0)
{
MessageBox.Show(SDK.rError(nError));
}
ltMovimiento.aCodAlmacen = "1";
ltMovimiento.aCodProdSer = "GAS "; //Código delproducto
ltMovimiento.aUnidades = 5;
ltMovimiento.aPrecio = 200;
nError = SDK.fAltaMovimiento(lIdDocumento, ref lIdMovimiento, ref ltMovimiento);
if (nError != 0)
{
MessageBox.Show(SDK.rError(nError));
}
else
{
MessageBox.Show("Documento guardado con exito con folio ");
}
}
```

Al emitir la factura, los impuestos se muestran desglosados en el archivo XML:

---

### 23.3 Registrar una relación CFDI mediante UUID

En algunos escenarios, el documento con el que se desea relacionar un CFDI no existe dentro de la empresa o fue generado en otro sistema. En estos casos, la autoridad permite establecer la relación utilizando directamente el UUID del CFDI previamente timbrado.

La función fAgregarRelacionCFDI2 facilita este proceso, ya que permite registrar una o varias relaciones mediante los UUID de los comprobantes fiscales, sin necesidad de que estos existan como documentos dentro del sistema administrativo.

Para lograrlo, se hace uso de dos funciones principales del SDK:

- fAgregarRelacionCFDI2() que registra la relación CFDI utilizando directamente el UUID del comprobante relacionado.
- fEmitirDocumento() que timbra el documento incorporando las relaciones CFDI previamente registradas.

#### 23.3.1 Configuración en el sistema de CONTPAQi Comercial Premium®

El siguiente ejemplo parte de un documento previamente creado dentro del sistema, en este caso una nota de crédito, sobre la cual se agregará una relación CFDI.

La función fAgregarRelacionCFDI2 recibe el concepto, serie y folio del documento que será timbrado, el tipo de relación SAT y el UUID del CFDI con el que se desea establecer la relación.

```pascal
Una vez registrada correctamente la relación, se utiliza la función fEmitirDocumento para realizar el timbrado del documento.
```

Durante este proceso el SDK incorpora automáticamente el UUID relacionado dentro del nodo CfdiRelacionados del XML generado.

Paso

Acción

CFDI a relacionar:

Al ejecutar fAgregarRelacionCFDI2 se relaciona el UUID:

```pascal
Se emite documento con fEmitirDocumento de la nota de crédito:
```

La relación está completa:

Implementación C# //Datos del documento al que relacionaré el UUID //Se agrega la relación //Se timbra la nota de crédito

```csharp
private void btnAgregaRelacionCFDI2_Click(object sender, EventArgs e)
{
int nError = 0;
string aCodConceptoR = "8";//Nota de crédito
string aSerieR = "";
string aFolioR = "14";
string aTipoRelacion = "01";
string aUUID = "00000000-3357-4eca-922a-20bacc0431f5";//UUID a relacionar del documento timbrado timbrado
nError = SDK.fAgregarRelacionCFDI2(aCodConceptoR, aSerieR, aFolioR, aTipoRelacion, aUUID);
if (nError != 0)
{
MessageBox.Show("Error: " + SDK.rError(nError));
}
else
{
MessageBox.Show("UUID: " + aUUID + " relacionado");
nError = SDK.fEmitirDocumento(aCodConceptoR, aSerieR, Convert.ToDouble(aFolioR), "12345678a", "");
if (nError != 0)
{
MessageBox.Show("Error: " + SDK.rError(nError));
}
else
{
MessageBox.Show("Documento timbrado exitosamente");
}
}
}
```

---

### 23.4 Registrar y saldar pagos (REP)

Una empresa necesita automatizar la gestión de pagos de sus clientes para emitir los Recibos Electrónicos de Pago (REP) conforme a la normativa fiscal vigente. La integración del SDK del sistema administrativo permite agilizar el proceso de conciliación bancaria y garantizar la correcta aplicación de los pagos a las facturas correspondientes.

Para lograrlo, se utilizan las siguientes funciones clave del SDK:

- fAltaDocumento: Genera un documento de factura a crédito con los datos de la operación.
- fAltaMovimiento: Agrega los movimientos del documento generado.
- fAltaDocumentoCargoAbono: Permite registrar un documento de pago que afecte el saldo de un cliente.
- fSaldarDocumento_Param: Vincula el pago con la factura, saldando la deuda total o parcialmente.
- fSiguienteFolio: Obtiene el siguiente folio disponible para un concepto de documento, evitando duplicidades.
- fLeeDatoDocumento: Función para leer el dato del documento según se especifique el campo de la BD que quiera obtener.
 
Funciones de bajo nivel:
- fEditarDocumento: Activa el documento en modo de edición.
- fSetDatoDocumento: Escribe el contenido de la variable valor en el campo de la tabla de documentos.
- fGuardaDocumento: Esta función se llama después de que se hace un set a un campo de la tabla de documentos.

#### 23.4.1 Configuración en el sistema de CONTPAQi Factura Electrónica®

El siguiente código crea un documento de factura a crédito, tras darlo de alta por bajo nivel le indicamos que serán pagos en parcialidades mediante SDK utilizando las funciones de documentos de bajo nivel. Al haber indicado que el documento será a parcialidades se continúa y se da de alta el movimiento del documento.

```pascal
Habiendo generado el documento de tipo factura a crédito, se crea un documento de cargo abono. Este documento de cargo abono será un pago del cliente con concepto de documento 10. Este documento nos entrega un folio y para poder extraerlo se emplea la función fLeeDatoDocumento, que obtiene el valor de CFOLIO del documento de cargo abono.
```

Una vez que se creó el documento cargo abono y obtuvimos el valor del folio, se realiza el saldado del documento inicial que es nuestra factura a crédito mediante la función fSaldarDocumento_Param.

Paso

Acción

Se crea el documento factura Crédito concepto 4

Se edita el método de pago:

Se crea documento de pago del cliente:

Al aplicar la función fSaldarDocumento_Param se asocia el documento de pago a la factura a crédito:

Implementación C# //Función para dar de alta de documento tipo factura a credito //Por bajo nivel le asigno el metodo de pago con el campo CCANTPARCI al documento credo //Agregamos movimiento a mi factura utilizando la esctructura tMovimiento //Doy de alta el movimiento de mi documento //Con los campos de la estructura para e concepto 10 se crea el documento cargo abono //Leemos el folio del documento cargo abono para poder utilizarlo al saldar el documento /*Utilizamos la función fSaldarDocumento_Param para saldar factura concepto 4 con el documento cargo abono creado concepto 10 En la función principalmente van los datos del documento a pagar y enseguida los del documento con el que voy a pagar*/

```csharp
private void btnCargoAbono_Click(object sender, EventArgs e)
{
SDK.tDocumento lDocto = new SDK.tDocumento();
SDK.tMovimiento lMovto = new SDK.tMovimiento();
StringBuilder serie = new StringBuilder();
int nError = 0;
double folio = 0;
int idDocto = 0;
int idMovto = 0;
nError = SDK.fSiguienteFolio("4", serie, ref folio);
if (nError != 0)
{
MessageBox.Show("Error 1: " + SDK.rError(nError));
}
else
{
//Llenamos datos de estructura tDocumento para el documento de factura a credito
lDocto.aCodConcepto = "4";//Factura credito
lDocto.aFolio = folio;
lDocto.aSerie = "";
lDocto.aFecha = DateTime.Today.ToString("MM/dd/yyyy");
lDocto.aCodigoCteProv = "CL01";
lDocto.aTipoCambio = 1;
lDocto.aNumMoneda = 1;
lDocto.aSistemaOrigen = 202;//202 factura, 205 comercial
lDocto.aAfecta = 1;
nError = SDK.fAltaDocumento(ref idDocto, ref lDocto);
if (nError != 0)
{
MessageBox.Show("Error fAltaDocumento: " + SDK.rError(nError));
}
else
{
MessageBox.Show("Documento creado con folio " + folio.ToString());
SDK.fEditarDocumento();
SDK.fSetDatoDocumento("CCANTPARCI", "2");//Método de pago, el valor 1 = Pago en una sola exhibición y 2 = Pago en parcialidades o diferido
SDK.fGuardaDocumento();
lMovto.aCodProdSer = "SER001";
lMovto.aPrecio = 1000;
lMovto.aUnidades = 1;
lMovto.aCodAlmacen = "1";
nError = SDK.fAltaMovimiento(idDocto, ref idMovto, ref lMovto);
if (nError != 0)
{
MessageBox.Show("Error fAltaMovimiento: " + SDK.rError(nError));
}
else
{
MessageBox.Show("Movimiento creado");
//Crear documento de pago (cargo abono) del cliente concepto 10 con la función fAltaDocumentoCargoAbono
double folioDos = 0;
StringBuilder serieDos = new StringBuilder();
SDK.fSiguienteFolio("10", serieDos, ref folioDos);//Concepto 10 = Pago del cliente
lDocto.aCodConcepto = "10";
lDocto.aFecha = DateTime.Today.ToString("MM/dd/yyyy");
lDocto.aCodigoCteProv = "CL01";
lDocto.aNumMoneda = 1;
lDocto.aTipoCambio = 1;
lDocto.aSistemaOrigen = 202;//202 factura, 205 comercial
lDocto.aFolio = folioDos;
lDocto.aImporte = 299;
nError = SDK.fAltaDocumentoCargoAbono(ref lDocto);
if (nError != 0)
{
MessageBox.Show("Error fAltaDocumento: " + SDK.rError(nError));
}
else
{
//Obtenemos el folio del documento creado con funcion de bajo nivel fLeeDatoDocumento
StringBuilder aValor = new StringBuilder("");
SDK.fLeeDatoDocumento("CFOLIO", aValor, 64);
MessageBox.Show("Documento pago del cliente creado con folio " + aValor);
double folioPago = Double.Parse(aValor.ToString());
string fechaPago = DateTime.Today.ToString("MM/dd/yyyy");
nError = SDK.fSaldarDocumento_Param("4", "", folio, "10", "", folioPago, 299, 1, fechaPago);
if (nError != 0)
{
MessageBox.Show("Error fSaldarDocumento_Param: " + SDK.rError(nError));
}
else
{
MessageBox.Show("Documento saldado");
}
}
}
}
}
}
```

---

### 23.5 Registrar medicamentos en el módulo de inventario (productos con lote)

Una empresa distribuidora de medicamentos trabaja con productos farmacéuticos que requieren un estricto control por lote y fecha de caducidad, conforme a las normativas sanitarias vigentes.

Con la integración del SDK del sistema administrativo, se busca automatizar el proceso de registro de entradas al inventario, asegurando que cada medicamento esté correctamente vinculado a su lote, fecha de fabricación y caducidad, facilitando de esta forma la trazabilidad de cada uno.

Para lograrlo, se hace uso de tres funciones principales del SDK:

- fAltaDocumento () que permite generar un documento de entrada con los datos básicos de la operación.
- fAltaMovimiento () que registra el movimiento del producto dentro del documento.
- fAltaMovimientoSeriesCapas () que asocia el producto con su lote y fechas relevantes.

#### 23.5.1 Configuración en el sistema de CONTPAQi Comercial Premium®

Paso

Acción

Ingresa al catálogo de Productos, para activar la opción de control por lote: Nota Esto permite que el sistema registre y gestione los movimientos del producto.

Implementación C#

```csharp
private void button_lote_Click(object sender, EventArgs e)
{
int lError = 0;
SDK.tDocumento ltDocumento = new SDK.tDocumento();
SDK.tMovimiento ltMovimiento = new SDK.tMovimiento();
SDK.tSeriesCapas ltSeriesCapas = new SDK.tSeriesCapas();
int lIdDocumento = 0;
int lIdMovimiento = 0;
ltDocumento.aCodConcepto = "34"; //CONCEPTO DEL DOCUMENTO
ltDocumento.aFecha = DateTime.Today.ToString("MM/dd/yyyy");
ltDocumento.aSerie = "";// INDICAR SERIE PARA IDENTIFICACION DEL DOCUMENTO
ltDocumento.aSistemaOrigen = 205; //205=COMERCIAL
ltDocumento.aNumMoneda = 1; // INDICAR TIPO DE MONEDA
ltDocumento.aTipoCambio = 1; // INDICAR TIPO DE CAMBIO
ltDocumento.aFolio = 0;
lError = SDK.fAltaDocumento(ref lIdDocumento, ref ltDocumento);
if (lError != 0)
{
MessageBox.Show(SDK.rError(lError));
}
ltMovimiento.aCodProdSer = "PR08"; //CODIGO DEL PRODUCTO
ltMovimiento.aCodAlmacen = "1";
ltMovimiento.aCosto = 100;
ltMovimiento.aReferencia = "";
lError = SDK.fAltaMovimiento(lIdDocumento, ref lIdMovimiento, ref ltMovimiento);
if (lError != 0)
{
MessageBox.Show(SDK.rError(lError));
}
else
{
MessageBox.Show("Documento guardado con exito");
}
ltSeriesCapas.aTipoCambio = 1.0000; //INDICAR EL TIPO DE CAMBIO
ltSeriesCapas.aNumeroLote = "AB250715B"; //INDICAR EL NOMBRE DEL LOTE ltSeriesCapas.aFechaFabricacion = "06/10/2025";
ltSeriesCapas.aFechaCaducidad = "07/10/2025";
ltSeriesCapas.aUnidades = 100; // INDICAR LA CANTIDAD DE UNIDADES
lError = SDK.fAltaMovimientoSeriesCapas(lIdMovimiento, ref ltSeriesCapas); //ALTA DEL MOVIMIENTO SERIES CAPAS
if (lError != 0)
{
MessageBox.Show(SDK.rError(lError));
}
else
{
MessageBox.Show("Documento SeriesCapa creado con exito");
}
}
}
```

Al generar el documento de entrada en el sistema, este queda debidamente registrado: Y el medicamento queda vinculado al lote con fechas de fabricación y caducidad:

> [!IMPORTANT]
> Recuerda
 
Todas las fechas que se ingresen por medio del SDK, tanto en funciones de alto como de bajo nivel, deberán capturarse en formato MM/DD/YYYY. Por ejemplo: el día 16 de marzo de 2025 se representa como "03/16/2025".

> [!IMPORTANT]
> Importante
 

La función fAltaMovimientoSeriesCapas agrega la información de lote y/o pedimento asociado a un movimiento de entrada.

En documentos de salida, se buscarán y tomarán automáticamente los pedimentos/lotes registrados, respetando las fechas de elaboración o importación.

Se soluciona el problema común de registros con fecha errónea "12/30/1899", asignando correctamente las fechas ingresadas.

Cuando existan movimientos de salida sin fecha asignada, pero con el mismo lote o pedimento, se tomará la existencia correcta de las capas disponibles.

---

### 23.6 Emisión del complemento de Carta Porte 3.1 mediante SDK

La autoridad fiscal modificó la forma en que se emiten los documentos con el complemento Carta Porte, estableciendo una estructura fija en el XML.

El sistema CONTPAQi Comercial Premium®, junto con el SDK para desarrolladores, permite la emisión de CFDI con el complemento Carta Porte 3.1.

El SDK permite emitir documentos con dicho complemento, pero no cubrir su llenado o modificación dentro del sistema.

Requerimientos técnicos:

Para poder implementar este caso de uso, es necesario contar con:

- Versión mínima de CONTPAQi Comercial Premium® 9.1.1.
- Versión mínima de CONTPAQi SDK 16.3.0.
- Instalación estable y funcional de ambos sistemas, con un desarrollo previo que permita acceder a una empresa mediante SDK.
- Conocimiento de las reglas de llenado del complemento Carta Porte 3.1 estipuladas por el SAT (consultables en: Carta Porte 3.1 SAT).
- Un documento dentro de CONTPAQi Comercial Premium® al que se quiera adjuntar el complemento.
- CSD vigente con su contraseña (pueden ser DEMO), previamente agregado en la configuración del concepto que se usará para la emisión.

Recomendaciones de uso:

Contar con una licencia Comercial con al menos 5 usuarios, lo cual es recomendable para un correcto funcionamiento al momento de emitir documentos.

#### 23.6.1 Implementación en el sistema de CONTPAQi Comercial Premium®

La emisión de documentos con el complemento Carta Porte 3.1 mediante el SDK de CONTPAQi Comercial Premium® puede realizarse en dos escenarios:

Escenario 1: El documento ya existe en el sistema y cuenta con el complemento Carta Porte 3.1 previamente generado y correctamente llenado.

Escenario 2: El documento ya está creado en el sistema, pero aún no tiene asociado el complemento Carta Porte 3.1.

```csharp
En ambos casos, la emisión se realiza utilizando la función fEmitirDocumento, que es la encargada de ejecutar el proceso desde el SDK. Su declaración es la siguiente:
```

[DllImport("MGWServicios.DLL")]

```pascal
public static extern Int32 fEmitirDocumento(
```

```pascal
[MarshalAs(UnmanagedType.LPStr)] string aCodConcepto,
```

```pascal
[MarshalAs(UnmanagedType.LPStr)] string aSerie,double aFolio,
```

```pascal
[MarshalAs(UnmanagedType.LPStr)] string aPassword,
```

```pascal
[MarshalAs(UnmanagedType.LPStr)] string aArchivoAdicional
```

```pascal
);
```

> [!IMPORTANT]
> Nota
 
La función fEmitirDocumento se utiliza de la misma manera que en versiones anteriores; su declaración no ha cambiado.

---

#### 23.6.1 Escenario 1: Documento con complemento previamente creado en el sistema

El documento ya existe en el sistema CONTPAQi Comercial Premium® y cuenta con el complemento Carta Porte 3.1 previamente generado y correctamente llenado.

```csharp
El primer paso consiste en identificar los datos del documento que se desea emitir. Esta información puede obtenerse directamente desde el sistema CONTPAQi Comercial Premium® o mediante una implementación personalizada que consulte los datos a través del SDK.
```

> [!IMPORTANT]
> Nota
 
La contraseña del CSD y el archivo adicional son datos personalizados; por lo tanto, no pueden consultarse mediante SDK y deben proporcionarse manualmente.

Paso

Acción

Documento asociado:

Complemento Carta Porte:

#region EMITIR DOCUMENTO CARTA PORTE //variables //Datos correspondientes al documento que se va intentar emitir y que deben ser datos existentes dentro de su empresa. aCodigoConcepto #endregion

```csharp
Emitir el documento utilizando la función fEmitirDocumento:
public static void EmitirDocumentoCP()
{
string aCodigoConcepto = "Factura4.0"; double aFolio = 21;
string aSerie = "CP";
string aContraseña = "12345678a";//Contraseña del CSD configurado dentro del concepto asignado en
string aArchivoAdicional = "";
codigoDeError = MGWServicios.fEmitirDocumento(aCodigoConcepto, aSerie, aFolio, aContraseña, aArchivoAdicional);
//en caso de que la función retorne un código diferente de 0 indicara que no se ejecutó con éxito if (codigoDeError != 0)
{
Console.WriteLine("Se genero el error " + codigoDeError); Console.WriteLine("Descripción: " + MGWServicios.rError(codigoDeError));
}
else
{
Console.WriteLine("Documento emitido");
}
}
```

> [!IMPORTANT]
> Nota
 
En el ejemplo los datos se asignan a variables locales, pero también pueden enviarse como parámetros a la función EmitirDocumentoCP.
 
Si los parámetros enviados a la función fEmitirDocumento son correctos y cumplen con las reglas de llenado, el documento será emitido exitosamente. En caso contrario, se retornará un mensaje de error que indicará el motivo por el cual no pudo completarse la emisión.

---

#### 23.6.2 Escenario 2: Documento sin complemento previamente creado en el sistema

```pascal
En este segundo escenario, el proceso de emisión del documento se realizará mediante la función fEmitirDocumento. Sin embargo, debido a que el documento no cuenta con el complemento Carta Porte incluido de origen, será necesario adjuntar dicha información de manera externa durante el proceso de emisión.
```

Para lograrlo, se hará uso del parámetro aArchivoAdicional, el cual permite incorporar información adicional al documento.

¿Qué recibirá el parámetro aArchivoAdicional?

El documento requiere la información correspondiente al complemento Carta Porte, por lo que dichos datos deberán enviarse como un archivo de texto.

```csharp
Dentro del aplicativo existe esta funcionalidad que permite adjuntar información de complementos a un documento mediante un archivo con extensión .ini o .xml, y es la que se utilizará para completar el documento desde el SDK.
```

El valor que se le de a la variable aArchivoAdicional deberá llevar la siguiente estructura:

Tipo de dato: String

Valor: “Complemento:[ruta del archivo xml y/o ini]”

La palabra Complemento indica el tipo de archivo que se intentará adjuntar al documento. Esta debe ir seguida del caracter ":" (dos puntos), el cual indica el inicio de la ruta donde el SDK buscará el archivo .ini o .xml del que tomará la información del complemento.

> [!IMPORTANT]
> Nota
 
Puedes consultar más información relacionada con este tema en este apartado Agregar archivo XML con complemento.

> [!IMPORTANT]
> Importante
 
La estructura del archivo .ini puede consultarse en el documento CartaPorte.ini, el cual se encuentra ubicado en la siguiente ruta:
C:\Program Files (x86)\Compac\COMERCIAL

A continuación, te mostramos un ejemplo de cómo implementar esta funcionalidad:

Paso

Acción

Implementación C#: //variables // Palabra Complemento posteriormente el carácter : y a continuación la ruta donde se encuentra la información del complemento seguida del nombre y extensión del archivo //hay que recordar que en caso de que la función retorne un código diferente de 0 indicara que no se ejecutó con éxito por lo que se puede utilizar una variable para consultar el valor que retorne la función. Nota Aunque el ejemplo muestra un archivo con extensión .ini, también es posible utilizar un archivo con extensión .xml. Consideraciones importantes Es indispensable que el sistema pueda identificar que el archivo .ini contiene información del complemento Carta Porte.

```csharp
public static void EmitirDocumento(string aCodigoConcepto, double aFolio, string aSerie, string aContraseña)
{
string aArchivoAdicional = @"Complemento:C:\Compac\Empresas\Esquemas\COMERCIAL\CartaPorte.ini"; MGWServicios.fEmitirDocumento(aCodigoConcepto, aSerie, aFolio, aContraseña, aArchivoAdicional);
}
Para realizar la emisión de la factura mediante fEmitirDocumento, el archivo .ini deberá encontrarse dentro de una carpeta llamada Adicionales, ubicada dentro del directorio correspondiente a la empresa.
```

Identificación del complemento Carta Porte: Para que el sistema reconozca correctamente que el archivo .ini contiene información correspondiente al complemento de Carta Porte, es obligatorio que la primera línea del archivo contenga la siguiente etiqueta: [CartaPorte3.1] Posteriormente, las secciones correspondientes a transportes, ubicaciones, figuras, entre otras, pueden organizarse de forma flexible, siempre y cuando los valores cumplan con lo establecido en la guía de llenado del complemento Carta Porte. Ejemplo: [CartaPorte3.1] IdCCP = CCC945d7 - 600e-43f5 - 9b7b - ebbee802f670 TranspInternac = No TotalDistRec = 254. RegistroISTMO = No [Transporte1] CodigoMedio = MT01 Clave = 01 PermSCT = TPAF02 NumPermisoSCT = 554486 ConfigVehicular = VL AseguraRespCivil = AXA PolizaRespCivil = 56775ht56 PlacaVM = L785JH AnioModeloVM=2023 AseguraCarga=222 PolizaCarga=777 [Figura1] TipoFigura = 01 NombreFigura = Figura Transporte 01 RFCFigura = AAA010101000 NumLicencia = UIFUY847584 [Ubicacion1] TipoUbicacion = Origen IDUbicacion = OR000001 RFCRemitenteDestinatario = EKU9003173C9 NombreRemitenteDestinatario = DEMO FechaHoraSalidaLlegada = 2024 - 01 - 30T16: 26:49 [Ubicacion1.Domicilio] Pais = MEX CodigoPostal = 26015 Estado = COA Municipio = 025 Localidad = 06 Colonia = 2613 Calle = pablo N NumeroExterior = 435 [Ubicacion2] TipoUbicacion=Destino IDUbicacion=DE000001 RFCRemitenteDestinatario=IIA040805DZ4 NombreRemitenteDestinatario=INDISTRIA ILUMINADORA DE ALMACENES SA DE CV FechaHoraSalidaLlegada=2024-01-30T16:26:49 DistanciaRecorrida = 254.00 [Ubicacion2.Domicilio] Pais = MEX CodigoPostal = 29960 Estado = CHP Municipio = 065 Localidad = 10 Colonia = 2034 Calle = PABLO N NumeroExterior = 444 [Mercancias] NumTotalMercancias=1 UnidadPeso=KGM PesoBrutoTotal=1.000 PesoNetoTotal=1000.000 CargoPorTasacion=1000.00 [Mercancia1] BienesTransp = 43211500 Descripcion = PRODUCTO 01 Cantidad = 20.00 ClaveUnidad = H87 Unidad = Pieza PesoEnKg = 1 En estos archivos tanto archivo INI como XML, podrás incluir cualquier información del complemento sin ninguna restricción, sólo deberás verificar el XSD del Complemento de Carta Porte y la Guía de llenado correspondiente al "Medio de transporte".

Ejemplo del complemento en formato XML: < cartaporte31:CartaPorte Version = "3.1" IdCCP="CCC4fc1c-9e24-4263-92f7-a4ed59a7c9a0" TranspInternac="No" TotalDistRec="40.00"> <cartaporte31:Ubicaciones > < cartaporte31:Ubicacion TipoUbicacion = "Origen" IDUbicacion="OR000001" RFCRemitenteDestinatario="JES900109Q90" NombreRemitenteDestinatario="Empresa Intensivo 2025" FechaHoraSalidaLlegada="2025-09-12T11:57:16"> <cartaporte31:Domicilio Pais = "MEX" Estado="JAL" Municipio="039" Localidad="03" Colonia="3743" CodigoPostal="44500" /> </cartaporte31:Ubicacion > < cartaporte31:Ubicacion TipoUbicacion = "Destino" IDUbicacion="DE000001" RFCRemitenteDestinatario="FIBA990603BU7" NombreRemitenteDestinatario="ALONSO FRIAS BERUMEN" FechaHoraSalidaLlegada="2025-09-12T11:57:16" DistanciaRecorrida="40.00"> <cartaporte31:Domicilio Pais = "MEX" Estado="JAL" Municipio="097" Localidad="11" Colonia="0011" CodigoPostal="45645" /> </cartaporte31:Ubicacion > </ cartaporte31:Ubicaciones > < cartaporte31:Mercancias PesoBrutoTotal = "1." UnidadPeso="KGM" NumTotalMercancias="1"> <cartaporte31:Mercancia BienesTransp = "01010101" Descripcion="LAPIZ" Cantidad="400.00" ClaveUnidad="H87" Unidad="Pieza" PesoEnKg="1" MaterialPeligroso="No" /> <cartaporte31:Autotransporte PermSCT = "TPXX00" NumPermisoSCT="00000000000000000000"> <cartaporte31:IdentificacionVehicular ConfigVehicular = "C2R2" PlacaVM="VL097TR" AnioModeloVM="2023" PesoBrutoVehicular="0.30" /> <cartaporte31:Seguros AseguraRespCivil = "AFIRME" PolizaRespCivil="123456789" /> </cartaporte31:Autotransporte > </ cartaporte31:Mercancias > < cartaporte31:FiguraTransporte > < cartaporte31:TiposFigura TipoFigura = "01" RFCFigura="GOOJ890604KL8" NombreFigura="JESUS GONZALES ORTIZ" NumLicencia="987654321" /> </cartaporte31:FiguraTransporte > </ cartaporte31:CartaPorte > Importante Es fundamental cumplir con todos los requerimientos establecidos por la autoridad fiscal para el uso correcto del complemento Carta Porte 3.1. El cumplimiento de estas disposiciones garantiza la validez fiscal del documento y evita posibles inconsistencias durante procesos de validación o auditoría. En caso de tener dudas sobre el diseño, estructura o generación adecuada de los complementos de Carta Porte 3.1, se recomienda consultar la información oficial publicada por el Servicio de Administración Tributaria (SAT), donde se detallan las guías de llenado, catálogos, ejemplos y normatividad vigente relacionada con este complemento. La información oficial puede consultarse directamente en los canales y documentos publicados por el SAT. Página del complemento: http://omawww.sat.gob.mx/tramitesyservicios/Paginas/complemento_carta_porte.htm. La ruta del archivo de excel con los nuevos catálogos: http://omawww.sat.gob.mx/tramitesyservicios/Paginas/documentos/CatalogosCartaPorte31.xls. Ruta del XSD del CCP 3.1: http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte31.xsd. Ruta del XSLT para la secuencia de cadena original CCP 3.1: http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte31.xslt. Ruta matriz de errores CCP 3.1: http://omawww.sat.gob.mx/tramitesyservicios/Paginas/documentos/Matriz_Errores_CCP_V31.xls. Ruta XSD catálogos CCP: http://www.sat.gob.mx/sitio_internet/cfd/catalogos/CartaPorte/catCartaPorte.xsd. Ruta del estándar: http://omawww.sat.gob.mx/tramitesyservicios/Paginas/documentos/Carta_Porte_31.pdf.

---