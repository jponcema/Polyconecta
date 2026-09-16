# Contract: `IOperationalWorkflowService`

Generalización de los métodos de acción que ya existen en `OperationalDataStore` (interactivos y funcionalmente correctos hoy), expuestos como interfaz para que una implementación futura pueda llamar a `PolyConecta.Api` sin tocar ningún `.razor`.

```csharp
namespace PolyConecta.Presentation.Services;

public interface IOperationalWorkflowService
{
    // Lectura (para poblar las pantallas)
    SalesOrderModel? GetSalesOrder(string folio);
    MasterOrderModel? GetMasterOrder(string folioOm);
    IReadOnlyList<ManufacturingOrderModel> GetProcessOrders(string folioOm);
    IReadOnlyList<WorkOrderModel> GetWorkOrders(string folioOf);
    IReadOnlyList<WorkCenterModel> GetWorkCenters();
    IReadOnlyList<StockLotModel> GetSlots(string folioOf);
    MassBalanceModel? GetMassBalance(string folioOf);
    StockPickingModel? GetPicking(string pickingId);

    // Fase 1-2: Pedido & aprobaciones
    void ConfirmSalesOrder(string folio);
    void ValidateSales(string folio);
    void ValidateCredit(string folio);

    // Fase 3: MRP & BOM
    void LoadBomRecipe(string folioOf);
    void AssignWorkOrderMachine(string folioWo, string machineId);

    // Fase 4: Báscula & calidad
    void WeighRoll(int slotNumber, decimal grossKg, decimal tareKg, bool passQuality);

    // Fase 5: Balance de masa & cierre
    void ExecuteClosure(string folioOf);

    // Fase 6: Logística
    void DispatchInterplantTransfer(string pickingId);
    void ReceiveInterplantTransfer(string pickingId);

    // Fase 7: Conversión
    void CompleteBagging(string folioOf, decimal thousands, decimal netKg);

    // Notificación de cambios (para StateHasChanged en los componentes suscritos)
    event Action? OnDataChanged;
}
```

## Reglas del contrato

- Cada método MUST ser el único punto de entrada para su mutación — ningún componente `.razor` MUST leer o escribir los modelos (`SalesOrderModel`, etc.) directamente.
- La implementación en memoria (`OperationalDataStore`) se registra `Scoped`. Una futura implementación `ApiOperationalWorkflowService` (fuera de alcance de esta spec) llamaría a `PolyConecta.Api` usando `HttpClient` y traduciría las respuestas a los mismos modelos de vista — sin cambiar esta interfaz salvo que se vuelva `async` (cambio esperado y aceptado en esa migración futura, documentado aquí para no sorprender al equipo).
- Los modelos (`SalesOrderModel`, `MasterOrderModel`, etc.) siguen siendo los de `Presentation.Models.OperationalModels` — esta spec no los redefine; solo asegura que haya una sola fuente de verdad para poblarlos.
