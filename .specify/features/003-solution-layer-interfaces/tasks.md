# Tasks: SPEC-003 Solution Layer Interfaces & Real Contracts

**Feature Branch**: `003-solution-layer-interfaces`  
**Date**: 2026-09-14  
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

---

## Phase 1: Domain Layer Interfaces (`PolyConecta.Domain`)

- [X] T001 Create `IRepository<TEntity, TId>` generic interface in `PolyConecta.Domain/Common/IRepository.cs`
- [X] T002 Create `IOrderRepository` interface in `PolyConecta.Domain/Repositories/IOrderRepository.cs`
- [X] T003 Create `IMaterialRepository` interface in `PolyConecta.Domain/Repositories/IMaterialRepository.cs`
- [X] T004 Create `ISupplierRepository` interface in `PolyConecta.Domain/Repositories/ISupplierRepository.cs`
- [X] T005 Create `IOutboxRepository` interface in `PolyConecta.Domain/Repositories/IOutboxRepository.cs`
- [X] T006 Create `IDomainEvent` and `IDomainEventPublisher` interfaces in `PolyConecta.Domain/Events/IDomainEventPublisher.cs`
- [X] T007 Create `IP2PValidationService` interface in `PolyConecta.Domain/Services/IP2PValidationService.cs`
- [X] T008 Create `IO2CApprovalService` interface in `PolyConecta.Domain/Services/IO2CApprovalService.cs`
- [X] T009 Create `IBridgeSyncService` interface in `PolyConecta.Domain/Services/IBridgeSyncService.cs`

---

## Phase 2: Infrastructure Layer Contracts & Adapters (`PolyConecta.Infrastructure`)

- [X] T010 Create `IUnitOfWork` interface in `PolyConecta.Infrastructure/Common/IUnitOfWork.cs` and implement in `UnitOfWork.cs`
- [X] T011 Implement `OrderRepository` in `PolyConecta.Infrastructure/Repositories/OrderRepository.cs`
- [X] T012 Implement `MaterialRepository` in `PolyConecta.Infrastructure/Repositories/MaterialRepository.cs`
- [X] T013 Implement `SupplierRepository` in `PolyConecta.Infrastructure/Repositories/SupplierRepository.cs`
- [X] T014 Implement `OutboxRepository` in `PolyConecta.Infrastructure/Repositories/OutboxRepository.cs`
- [X] T015 Create `IOutboxProcessor` interface in `PolyConecta.Infrastructure/Outbox/IOutboxProcessor.cs`
- [X] T016 Create `IDataSeeder` interface in `PolyConecta.Infrastructure/Persistence/IDataSeeder.cs`

---

## Phase 3: Api Layer Contracts & Application Handlers (`PolyConecta.Api`)

- [X] T017 Create `ApiResponse<TData>` envelope DTO in `PolyConecta.Api/Common/ApiResponse.cs`
- [X] T018 Create `PagedResult<TData>` pagination DTO in `PolyConecta.Api/Common/PagedResult.cs`
- [X] T019 Create `ICommandHandler` and `IQueryHandler` CQRS interfaces in `PolyConecta.Api/Application/ICqrsHandlers.cs`
- [X] T020 Create RFC 7807 `ProblemDetailsMiddleware` in `PolyConecta.Api/Middleware/ProblemDetailsMiddleware.cs`

---

## Phase 4: Presentation JavaScript SDK (`PolyConecta.Presentation`)

- [X] T021 Create JavaScript SDK `PolyAPI.client` in `PolyConecta.Presentation/wwwroot/js/poly-api-client.js` with `get`, `post`, `put`, `delete` methods and error handling.

---

## Phase 5: Contpaq Bridge Contracts (`PolyConecta.Contpaq`)

- [X] T022 Create `IContpaqSdkGateway` interface in `PolyConecta.Contpaq/Sdk/IContpaqSdkGateway.cs`
- [X] T023 Create `IWebhookDispatcher` interface in `PolyConecta.Contpaq/Webhooks/IWebhookDispatcher.cs`
- [X] T024 Create `ICircuitBreaker` interface in `PolyConecta.Contpaq/Resilience/ICircuitBreaker.cs`

---

## Phase 6: Unit & Integration Testing

- [X] T025 Add domain repository interface unit tests in `tests/PolyConecta.Domain.Tests/InterfaceContractsTests.cs`
- [X] T026 Add API response envelope unit tests in `tests/PolyConecta.IntegrationTests/ApiResponseTests.cs`
