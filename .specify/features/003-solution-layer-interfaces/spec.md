# Feature Specification: 003-solution-layer-interfaces

**Feature Branch**: `003-solution-layer-interfaces`  
**Created**: 2026-09-14  
**Status**: Draft  
**Input**: User description: "De lo que actualmente está implementado vs lo que falta por definir. Especificación intermedia para definir las interfaces reales y contratos entre todas las capas de la solución PolyConecta (Domain, Infrastructure, Api, Presentation, Contpaq)."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Definición y Exposición de Contratos de Dominio e Infraestructura (Priority: P1)

Como Arquitecto de Software y Desarrollador Backend, quiero disponer de interfaces y contratos fuertemente tipados en `PolyConecta.Domain` e `PolyConecta.Infrastructure` para independizar la lógica de negocio de los detalles de persistencia EF Core y acceso a datos.

**Why this priority**: Es la base del principio de Clean Architecture (Inversión de Dependencias). Permite que la API y los servicios de dominio interactúen a través de abstracciones probables y desacopladas.

**Independent Test**: Se puede probar mediante pruebas unitarias en `PolyConecta.Domain.Tests` implementando repositorios mock y verificando el paso de eventos de dominio sin instanciar EF Core ni SQL Server.

**Acceptance Scenarios**:

1. **Given** un servicio de dominio que procesa una recepción de materia prima, **When** solicita guardar la entidad `MasterOrder`, **Then** interactúa únicamente a través de la interfaz `IOrderRepository` y `IUnitOfWork`.
2. **Given** la emisión de un evento de dominio (`OrderApprovedEvent`), **When** se invoca `IDomainEventPublisher`, **Then** el evento se encola y despacha sin acoplarse al framework de infraestructura.

---

### User Story 2 - Definición de Contratos REST API y Aplicación CQRS (Priority: P2)

Como Desarrollador de la Capa de Presentación o Integrador Externo, quiero contratos de respuesta REST API unificados y DTOs bien definidos en `PolyConecta.Api` para consumir la lógica de negocio de manera consistente con manejo de errores estandarizado (RFC 7807 Problem Details).

**Why this priority**: Garantiza una comunicación clara entre la interfaz SPA Odoo 19 / clientes externos y la API de .NET, reduciendo errores de serialización y discrepancias de nombres.

**Independent Test**: Ejecución de pruebas de integración con `WebApplicationFactory` probando endpoints con payload estandarizado (`ApiResponse<T>`) y validación de RFC 7807 en errores.

**Acceptance Scenarios**:

1. **Given** una petición GET o POST exitosa hacia `/api/v1/orders`, **When** se retorna la respuesta, **Then** el cuerpo cumple exactamente con el esquema de envolvente `ApiResponse<TData>` (incluyendo `Success`, `Data`, `Timestamp`, `TraceId`).
2. **Given** un error de validación de negocio (ej. firma inválida), **When** la API responde, **Then** retorna un HTTP 400 Bad Request con estructura `ProblemDetails` estandarizada.

---

### User Story 3 - Definición del Cliente API SPA Odoo 19 e Interfaces del Bridge CONTPAQi (Priority: P3)

Como Desarrollador Frontend y del Servicio Bridge CONTPAQi, quiero un contrato de SDK en JavaScript (`PolyAPI.client`) para la SPA Odoo 19 y una interfaz explícita `IContpaqSdkGateway` para el servicio Win32, asegurando la comunicación bidireccional cliente-servidor-ERP.

**Why this priority**: Cierra el ciclo de integración de la solución PolyConecta desde la interfaz de usuario en el navegador hasta el SDK nativo de CONTPAQi Comercial Premium.

**Independent Test**: Simulación de llamadas de la SPA con el cliente `PolyAPI` y ejecución de llamadas de prueba contra el gateway mock de CONTPAQi.

**Acceptance Scenarios**:

1. **Given** una acción en la SPA Odoo 19 (ej. clic en Smart Button de aprobación), **When** se invoca `PolyAPI.client.post()`, **Then** la solicitud se envía con los headers de correlación y maneja el estado visual de la UI.
2. **Given** una solicitud de creación de documento en CONTPAQi, **When** la API la envía al Bridge, **Then** el Bridge la ejecuta a través de la interfaz `IContpaqSdkGateway` de forma asíncrona con retry y circuit breaker.

---

### Edge Cases

- ¿Qué ocurre si la base de datos o el SDK de CONTPAQi están fuera de línea durante el despacho de contratos de Outbox?
  - El sistema usa el patrón Outbox con retry exponencial (`ICircuitBreaker`) y registra la falla en la cola de mensajes muertos (DLQ).
- ¿Cómo se manejan errores de tipos o descalces de campos entre los DTOs de la API y los modelos de CONTPAQi `adm*`?
  - Los mapeadores explícitos implementan validación en tiempo de compilación y transformaciones estrictas con manejo de nulos por defecto.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: La capa `PolyConecta.Domain` DEBE definir las interfaces base de repositorio `IRepository<TEntity, TId>`, `IOrderRepository`, `IMaterialRepository`, `ISupplierRepository` y `IOutboxRepository`.
- **FR-002**: La capa `PolyConecta.Domain` DEBE definir las interfaces para eventos de dominio (`IDomainEvent`, `IDomainEventPublisher`, `IDomainEventHandler<TEvent>`) y servicios de dominio (`IP2PValidationService`, `IO2CApprovalService`, `IBridgeSyncService`).
- **FR-003**: La capa `PolyConecta.Infrastructure` DEBE implementar `IUnitOfWork`, la infraestructura de persistencia EF Core para todos los repositorios y la interfaz de seeder de datos `IDataSeeder`.
- **FR-004**: La capa `PolyConecta.Api` DEBE estandarizar la envolvente de respuesta HTTP `ApiResponse<TData>`, `PagedResult<TData>`, la interfaz de handlers de comandos/consultas (`ICommandHandler<TCommand, TResult>`, `IQueryHandler<TQuery, TResult>`) y el middleware de manejo global de excepciones RFC 7807 (`ProblemDetails`).
- **FR-005**: La capa `PolyConecta.Presentation` DEBE definir la interfaz del cliente JavaScript (`PolyAPI.client`) con métodos unificados (`get`, `post`, `put`, `delete`), manejo de tokens/headers y binding a componentes Odoo 19 (Kanban, Form, Smart Buttons).
- **FR-006**: La capa `PolyConecta.Contpaq` DEBE definir la interfaz del Gateway SDK nativo `IContpaqSdkGateway` (`Connect`, `Disconnect`, `CreateDocument`, `QueryCatalog`), la interfaz de despacho de webhooks `IWebhookDispatcher` y las políticas de resiliencia `ICircuitBreaker`.

### Key Entities

- **ApiResponse<TData>**: Envolvente DTO unificada para todas las respuestas REST de la API.
- **PagedResult<TData>**: DTO para respuestas con paginación, conteo total y metadatos de navegación.
- **IContpaqSdkGateway**: Interfaz de abstracción sobre el SDK nativo C++ (`MGW_SDK.dll`) de CONTPAQi Comercial Premium.
- **IUnitOfWork**: Interfaz de transacción atómica para operaciones de persistencia en EF Core.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: El 100% de las dependencias entre capas se realizan a través de interfaces explícitas en C# o TypeScript/JS, sin acoplamiento a implementaciones concretas.
- **SC-002**: Todas las respuestas HTTP de `PolyConecta.Api` siguen el formato único estandarizado `ApiResponse<T>` con un tiempo de overhead por envolvente menor a 1 ms.
- **SC-003**: El 100% de las pruebas unitarias e integraciones de la solución (`dotnet test PolyConecta.slnx`) compilan y pasan sin advertencias de interfaz no implementada o tipos nulos.

## Assumptions

- Se mantiene la arquitectura de 5 proyectos planos en la raíz de la solución (`PolyConecta.Presentation`, `PolyConecta.Api`, `PolyConecta.Domain`, `PolyConecta.Infrastructure`, `PolyConecta.Contpaq`).
- Las tecnologías principales continúan siendo .NET 8 C#, Entity Framework Core, Odoo 19 Enterprise Web SPA (JS ES6 / HTML5) y C++ P/Invoke DLL Interop para CONTPAQi.
