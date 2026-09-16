namespace PolyConecta.Presentation.Models;

public class SalesOrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Folio { get; set; } = "PED-2026-3192389";
    public string CustomerCode { get; set; } = "CLI-001";
    public string CustomerName { get; set; } = "Vidrios de México S.A. de C.V.";
    public string PtSku { get; set; } = "PT-BOL-POL-6080-01";
    public string PtDescription { get; set; } = "Bolsa Polietileno High Clarity 60x80cm Calibre 200";
    public decimal RequestedThousands { get; set; } = 50.0m;
    public decimal TargetWeightKg { get; set; } = 1250.0m;
    public string State { get; set; } = "Borrador"; // "Borrador", "Confirmado", "Autorizado", "En progreso", "Hecho"
    public bool SalesApproved { get; set; } = false;
    public bool CreditApproved { get; set; } = false;
    public DateTime CreationDate { get; set; } = DateTime.Now.AddDays(-2);
    public DateTime PromiseDate { get; set; } = DateTime.Now.AddDays(7);

    // Extrusión Specs (Campos de Usuario CONTPAQi)
    public string ExtResinType { get; set; } = "Polietileno Baja Densidad (LDPE Virgin)";
    public decimal ExtGaugeMicrons { get; set; } = 50.0m;
    public decimal ExtWidthMm { get; set; } = 600.0m;
    public string ExtCoronaTreatment { get; set; } = "38 Dynes/cm";
    public string ExtPigmentAdditives { get; set; } = "Pigmento Blanco Masterbatch 2% + Aditivo UV";

    // Conversión Specs (Campos de Usuario CONTPAQi)
    public decimal BagFinalWidthCm { get; set; } = 60.0m;
    public decimal BagFinalLengthCm { get; set; } = 80.0m;
    public decimal BagGussetCm { get; set; } = 10.0m;
    public string BagSealType { get; set; } = "Sello Fondo Reforzado + Suaje Camiseta";
    public string BagInksPantones { get; set; } = "2 Tintas Flexo (Azul Reflex + Blanco Opaco)";
    public decimal BagKgPerThousandRatio { get; set; } = 25.0m;

    // Cross Navigation Links
    public string MasterOrderFolio { get; set; } = "OM-2026-0421";
    public string ExtrusionOfFolio { get; set; } = "OF-EXT-2026-01";
    public string PrintingOfFolio { get; set; } = "OF-IMP-2026-01";
    public string BaggingOfFolio { get; set; } = "OF-BOL-2026-01";
    public string PickingFolio { get; set; } = "TR-PIM-STC-001";
}

public class MasterOrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FolioOm { get; set; } = "OM-2026-0421";
    public string SalesOrderFolio { get; set; } = "PED-2026-3192389";
    public string CustomerName { get; set; } = "Vidrios de México S.A. de C.V.";
    public string PtSku { get; set; } = "PT-BOL-POL-6080-01";
    public decimal TargetQuantityKg { get; set; } = 1250.0m;
    public decimal ProducedQuantityKg { get; set; } = 1225.0m;
    public decimal ScrapQuantityKg { get; set; } = 25.0m;
    public string State { get; set; } = "Borrador"; // "Borrador", "Confirmada", "En progreso", "Hecho"
    public List<string> ChildOfFolios { get; set; } = new() { "OF-EXT-2026-01", "OF-IMP-2026-01", "OF-BOL-2026-01" };
}

public class ManufacturingOrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FolioOf { get; set; } = "OF-EXT-2026-01";
    public string ProcessType { get; set; } = "Extrusion"; // "Extrusion", "Printing", "Bagging"
    public string MasterOrderFolio { get; set; } = "OM-2026-0421";
    public string PlantName { get; set; } = "PIM Apodaca";
    public decimal TargetQuantity { get; set; } = 1250.0m;
    public decimal ProducedQuantity { get; set; } = 0.0m;
    public decimal ScrapQuantity { get; set; } = 0.0m;
    public string State { get; set; } = "Borrador"; // "Borrador", "Confirmado", "En progreso", "Cierre Técnico", "Hecho"
    public string WorkOrderFolio { get; set; } = "WO-EXT-01";
    public string WorkCenterId { get; set; } = "Por programar";
    public string WorkCenterName { get; set; } = "Por programar";
    public bool BomLoaded { get; set; } = false;
    public string BomRecipeSummary { get; set; } = "Tolva A (33% LDPE Virgin), Tolva B (34% LLDPE + 5% UV), Tolva C (33% LDPE)";
    public bool ClosureExecuted { get; set; } = false;

    // Dual Registration (for Bolseo)
    public decimal ProducedThousands { get; set; } = 0.0m;
    public decimal NetWeightKg { get; set; } = 0.0m;
    public decimal ActualKgPerThousand { get; set; } = 0.0m;
}

public class WorkOrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FolioWo { get; set; } = "WO-EXT-01";
    public string ManufacturingOrderFolio { get; set; } = "OF-EXT-2026-01";
    public string OperationName { get; set; } = "Extrusión Coex 3 Capas";
    public string WorkCenterId { get; set; } = "Por programar";
    public string WorkCenterName { get; set; } = "Por programar";
    public string EmployeeId { get; set; } = "EMP-002";
    public string EmployeeName { get; set; } = "Juan Pérez (Operador Senior)";
    public DateTime? ScheduledDate { get; set; }
    public decimal DurationHours { get; set; } = 8.5m;
    public string State { get; set; } = "Por programar"; // "Por programar", "Listo", "En progreso", "Finalizado"
}

public class WorkCenterModel
{
    public string Id { get; set; } = "PIM-EXT-01";
    public string Name { get; set; } = "Coextrusora 3 Capas Coex-1";
    public string Plant { get; set; } = "PIM Apodaca";
    public string ProcessType { get; set; } = "Extrusión";
    public decimal CapacityKgPerHour { get; set; } = 150.0m;
    public string State { get; set; } = "Disponible"; // "En Producción", "Disponible", "Mantenimiento"
    public string CurrentWorkOrderFolio { get; set; } = "-";
    public string AssignedOperatorName { get; set; } = "Juan Pérez";
}

public class EmployeeModel
{
    public string Id { get; set; } = "EMP-001";
    public string Name { get; set; } = "Roosvelt V.";
    public string Role { get; set; } = "Planner MRP";
    public string Plant { get; set; } = "PIM Apodaca";
    public string Shift { get; set; } = "Turno 1 Matutino";
    public string State { get; set; } = "Activo";
    public string CurrentAssignedTask { get; set; } = "Planeación OM-2026-0421";
}

public class ProductModel
{
    public string Id { get; set; } = "PT-BOL-POL-6080-01";
    public string Code { get; set; } = "PT-BOL-POL-6080-01";
    public string Name { get; set; } = "Bolsa Polietileno High Clarity 60x80cm Calibre 200";
    public string ProductType { get; set; } = "Producto Terminado"; // "Producto Terminado", "Rollo Intermedio", "Materia Prima Resina", "Insumo Pigmento/Aditivo"
    public string DefaultUom { get; set; } = "MIL";
    public decimal StandardCost { get; set; } = 425.00m;
    public decimal StockOnHandKg { get; set; } = 12500.0m;
    public string DefaultLocation { get; set; } = "STC/Stock/PT";
}

public class StockLocationModel
{
    public string Id { get; set; } = "PIM/Stock/MP";
    public string Name { get; set; } = "Materia Prima Planta PIM Apodaca";
    public string Plant { get; set; } = "PIM Apodaca";
    public bool IsQuarantine { get; set; } = false;
    public bool IsTransit { get; set; } = false;
    public decimal StockQtyKg { get; set; } = 45000.0m;
    public int ItemCount { get; set; } = 12;
}

public class StockPickingModel
{
    public string Id { get; set; } = "TR-PIM-STC-001";
    public string PickingType { get; set; } = "Traspaso Interplanta 2 Pasos";
    public string SourceLocationId { get; set; } = "PIM/Stock/PT";
    public string TransitLocationId { get; set; } = "TRANSIT/PIM-STC";
    public string DestLocationId { get; set; } = "STC/Stock/MP";
    public string State { get; set; } = "Borrador"; // "Borrador", "En Tránsito", "Validado / Concretado"
    public string ContpaqDocumentRef { get; set; } = "Traspaso ERP Pendiente";
    public decimal QuantityKg { get; set; } = 1250.0m;
    public int RollCount { get; set; } = 4;
}

public class StockLotModel
{
    public string Id { get; set; } = "IV214-26-R001";
    public int SlotNumber { get; set; } = 1;
    public decimal GrossWeightKg { get; set; } = 305.0m;
    public decimal TareWeightKg { get; set; } = 5.0m;
    public decimal NetWeightKg { get; set; } = 300.0m;
    public string State { get; set; } = "Aprobado"; // "Aprobado", "Cuarentena (.S)", "Pendiente"
    public bool IsQuarantine { get; set; } = false;
    public string LocationId { get; set; } = "PIM/Stock/PT";
    public string QualityCheckId { get; set; } = "QC-EXT-R001";
}

public class QualityCheckModel
{
    public string Id { get; set; } = "QC-EXT-R001";
    public string LotId { get; set; } = "IV214-26-R001";
    public string InspectorName { get; set; } = "María Gómez";
    public decimal GaugeMeasuredMicrons { get; set; } = 50.0m;
    public decimal WidthMeasuredMm { get; set; } = 600.0m;
    public decimal CoronaDynes { get; set; } = 38.0m;
    public bool Passed { get; set; } = true;
    public string Notes { get; set; } = "Inspección conforme a norma ISO 9001";
}

public class MassBalanceModel
{
    public string Id { get; set; } = "MB-OF-EXT-2026-01";
    public string ManufacturingOrderFolio { get; set; } = "OF-EXT-2026-01";
    public decimal RawMaterialInputKg { get; set; } = 1250.0m;
    public decimal GoodRollsWeightKg { get; set; } = 925.0m;
    public decimal QuarantineRollsWeightKg { get; set; } = 300.0m;
    public decimal ScrapWeightKg { get; set; } = 25.0m;
    public decimal TotalExtrudedKg { get; set; } = 1250.0m;
    public decimal VariancePercent { get; set; } = 0.0m;
    public bool IsClosed { get; set; } = false;
    public string ContpaqDeductionStatus { get; set; } = "Pendiente Cierre Técnico";
}
