using PolyConecta.Presentation.Models;

namespace PolyConecta.Presentation.Services;

public class OperationalDataStore
{
    public List<SalesOrderModel> SalesOrders { get; } = new();
    public List<MasterOrderModel> MasterOrders { get; } = new();
    public List<ManufacturingOrderModel> ManufacturingOrders { get; } = new();
    public List<WorkOrderModel> WorkOrders { get; } = new();
    public List<WorkCenterModel> WorkCenters { get; } = new();
    public List<EmployeeModel> Employees { get; } = new();
    public List<ProductModel> Products { get; } = new();
    public List<StockLocationModel> StockLocations { get; } = new();
    public List<StockPickingModel> StockPickings { get; } = new();
    public List<StockLotModel> StockLots { get; } = new();
    public List<QualityCheckModel> QualityChecks { get; } = new();
    public List<MassBalanceModel> MassBalances { get; } = new();

    public event Action? OnDataChanged;

    public OperationalDataStore()
    {
        SeedData();
    }

    private void SeedData()
    {
        // 1. Sales Order
        var salesOrder = new SalesOrderModel();
        SalesOrders.Add(salesOrder);

        // 2. Master Order
        var masterOrder = new MasterOrderModel();
        MasterOrders.Add(masterOrder);

        // 3. Manufacturing Orders (Extrusión, Impresión, Bolseo)
        var ofExt = new ManufacturingOrderModel
        {
            FolioOf = "OF-EXT-2026-01",
            ProcessType = "Extrusion",
            PlantName = "PIM Apodaca",
            TargetQuantity = 1250.0m,
            WorkOrderFolio = "WO-EXT-01"
        };
        var ofImp = new ManufacturingOrderModel
        {
            FolioOf = "OF-IMP-2026-01",
            ProcessType = "Printing",
            PlantName = "PIM Apodaca",
            TargetQuantity = 1250.0m,
            WorkOrderFolio = "WO-IMP-01"
        };
        var ofBol = new ManufacturingOrderModel
        {
            FolioOf = "OF-BOL-2026-01",
            ProcessType = "Bagging",
            PlantName = "Santa Cruz",
            TargetQuantity = 50.0m, // 50 Millares
            WorkOrderFolio = "WO-BOL-01"
        };
        ManufacturingOrders.AddRange(new[] { ofExt, ofImp, ofBol });

        // 4. Work Orders
        var woExt = new WorkOrderModel
        {
            FolioWo = "WO-EXT-01",
            ManufacturingOrderFolio = "OF-EXT-2026-01",
            OperationName = "Extrusión Coex 3 Capas",
            WorkCenterId = "PIM-EXT-01",
            WorkCenterName = "Coextrusora 3 Capas (PIM-EXT-01)",
            EmployeeId = "EMP-002",
            EmployeeName = "Juan Pérez (Operador Senior)",
            State = "Por programar"
        };
        var woImp = new WorkOrderModel
        {
            FolioWo = "WO-IMP-01",
            ManufacturingOrderFolio = "OF-IMP-2026-01",
            OperationName = "Impresión Flexográfica 2 Tintas",
            WorkCenterId = "STC-IMP-01",
            WorkCenterName = "Flexográfica 4C (STC-IMP-01)",
            EmployeeId = "EMP-003",
            EmployeeName = "Carlos Rivas (Operador Flexo)",
            State = "Por programar"
        };
        var woBol = new WorkOrderModel
        {
            FolioWo = "WO-BOL-01",
            ManufacturingOrderFolio = "OF-BOL-2026-01",
            OperationName = "Corte, Sello y Suaje Camiseta",
            WorkCenterId = "STC-BOL-01",
            WorkCenterName = "Bolseadora Alta Velocidad (STC-BOL-01)",
            EmployeeId = "EMP-004",
            EmployeeName = "Mateo López (Operador Bolseadora)",
            State = "Por programar"
        };
        WorkOrders.AddRange(new[] { woExt, woImp, woBol });

        // 5. Work Centers
        WorkCenters.Add(new WorkCenterModel { Id = "PIM-EXT-01", Name = "Coextrusora 3 Capas Coex-1", Plant = "PIM Apodaca", ProcessType = "Extrusión", CapacityKgPerHour = 150.0m, State = "Disponible", AssignedOperatorName = "Juan Pérez" });
        WorkCenters.Add(new WorkCenterModel { Id = "PIM-EXT-02", Name = "Coextrusora High-Speed 2", Plant = "PIM Apodaca", ProcessType = "Extrusión", CapacityKgPerHour = 220.0m, State = "Disponible", AssignedOperatorName = "Sin Asignar" });
        WorkCenters.Add(new WorkCenterModel { Id = "STC-IMP-01", Name = "Flexográfica SC 4 Color", Plant = "Santa Cruz", ProcessType = "Impresión", CapacityKgPerHour = 300.0m, State = "Disponible", AssignedOperatorName = "Carlos Rivas" });
        WorkCenters.Add(new WorkCenterModel { Id = "STC-BOL-01", Name = "Bolseadora Alta Velocidad 1", Plant = "Santa Cruz", ProcessType = "Bolseo", CapacityKgPerHour = 120.0m, State = "Disponible", AssignedOperatorName = "Mateo López" });
        WorkCenters.Add(new WorkCenterModel { Id = "STC-BOL-02", Name = "Bolseadora Lateral 2", Plant = "Santa Cruz", ProcessType = "Bolseo", CapacityKgPerHour = 100.0m, State = "Disponible", AssignedOperatorName = "Sin Asignar" });

        // 6. Employees
        Employees.Add(new EmployeeModel { Id = "EMP-001", Name = "Roosvelt V.", Role = "Planner MRP", Plant = "PIM Apodaca", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "Gestión OM-2026-0421" });
        Employees.Add(new EmployeeModel { Id = "EMP-002", Name = "Juan Pérez", Role = "Operador Extrusión", Plant = "PIM Apodaca", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "WO-EXT-01" });
        Employees.Add(new EmployeeModel { Id = "EMP-003", Name = "Carlos Rivas", Role = "Operador Impresión", Plant = "Santa Cruz", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "WO-IMP-01" });
        Employees.Add(new EmployeeModel { Id = "EMP-004", Name = "Mateo López", Role = "Operador Bolseo", Plant = "Santa Cruz", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "WO-BOL-01" });
        Employees.Add(new EmployeeModel { Id = "EMP-005", Name = "María Gómez", Role = "Inspector Calidad", Plant = "PIM / SC", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "Inspección Fichas Quality Checks" });
        Employees.Add(new EmployeeModel { Id = "EMP-006", Name = "Pedro Sánchez", Role = "Coordinador Logística", Plant = "PIM Apodaca", Shift = "Turno 1 Matutino", State = "Activo", CurrentAssignedTask = "Ruteo Traspasos Interplanta" });
        Employees.Add(new EmployeeModel { Id = "EMP-007", Name = "Ana Torres", Role = "Ventas / AC", Plant = "Corporativo", Shift = "Matutino", State = "Activo", CurrentAssignedTask = "Aprobación Comercial Pedidos" });
        Employees.Add(new EmployeeModel { Id = "EMP-008", Name = "Roberto Garza", Role = "Crédito y Cobranza", Plant = "Corporativo", Shift = "Matutino", State = "Activo", CurrentAssignedTask = "Autorización Crediticia ERP" });

        // 7. Products
        Products.Add(new ProductModel { Id = "PT-BOL-POL-6080-01", Code = "PT-BOL-POL-6080-01", Name = "Bolsa Polietileno High Clarity 60x80cm Cal 200", ProductType = "Producto Terminado", DefaultUom = "MIL", StandardCost = 425.00m, StockOnHandKg = 12500.0m, DefaultLocation = "STC/Stock/PT" });
        Products.Add(new ProductModel { Id = "INT-ROL-LDPE-600", Code = "INT-ROL-LDPE-600", Name = "Rollo Bobina LDPE 600mm Cal 50mic", ProductType = "Rollo Intermedio", DefaultUom = "KG", StandardCost = 38.50m, StockOnHandKg = 925.0m, DefaultLocation = "PIM/Stock/PT" });
        Products.Add(new ProductModel { Id = "MP-RES-LDPE-01", Code = "MP-RES-LDPE-01", Name = "Resina Polietileno Baja Densidad Virgin", ProductType = "Materia Prima Resina", DefaultUom = "KG", StandardCost = 28.00m, StockOnHandKg = 45000.0m, DefaultLocation = "PIM/Stock/MP" });
        Products.Add(new ProductModel { Id = "MP-PIG-BLANCO", Code = "MP-PIG-BLANCO", Name = "Pigmento Blanco Masterbatch 60% TiO2", ProductType = "Insumo Pigmento/Aditivo", DefaultUom = "KG", StandardCost = 85.00m, StockOnHandKg = 3200.0m, DefaultLocation = "PIM/Stock/MP" });

        // 8. Stock Locations
        StockLocations.Add(new StockLocationModel { Id = "PIM/Stock/MP", Name = "Materia Prima Planta PIM Apodaca", Plant = "PIM Apodaca", StockQtyKg = 45000.0m, ItemCount = 12 });
        StockLocations.Add(new StockLocationModel { Id = "PIM/Stock/PT", Name = "Producto Terminado / Rollos PIM", Plant = "PIM Apodaca", StockQtyKg = 925.0m, ItemCount = 3 });
        StockLocations.Add(new StockLocationModel { Id = "PIM/Stock/Cuarentena", Name = "Ubicación Aislamiento Cuarentena (.S)", Plant = "PIM Apodaca", IsQuarantine = true, StockQtyKg = 300.0m, ItemCount = 1 });
        StockLocations.Add(new StockLocationModel { Id = "TRANSIT/PIM-STC", Name = "Ubicación Traspaso en Tránsito Interplanta", Plant = "Tránsito Interplanta", IsTransit = true, StockQtyKg = 1250.0m, ItemCount = 4 });
        StockLocations.Add(new StockLocationModel { Id = "STC/Stock/MP", Name = "Materia Prima Planta Santa Cruz", Plant = "Santa Cruz", StockQtyKg = 18000.0m, ItemCount = 8 });
        StockLocations.Add(new StockLocationModel { Id = "STC/Stock/PT", Name = "Producto Terminado Planta Santa Cruz", Plant = "Santa Cruz", StockQtyKg = 22000.0m, ItemCount = 15 });

        // 9. Stock Picking
        StockPickings.Add(new StockPickingModel());

        // 10. Slots & Lots
        StockLots.Add(new StockLotModel { Id = "IV214-26-R001", SlotNumber = 1, GrossWeightKg = 305.0m, TareWeightKg = 5.0m, NetWeightKg = 300.0m, State = "Aprobado", IsQuarantine = false, LocationId = "PIM/Stock/PT", QualityCheckId = "QC-EXT-R001" });
        StockLots.Add(new StockLotModel { Id = "IV214-26-R002", SlotNumber = 2, GrossWeightKg = 315.0m, TareWeightKg = 5.0m, NetWeightKg = 310.0m, State = "Aprobado", IsQuarantine = false, LocationId = "PIM/Stock/PT", QualityCheckId = "QC-EXT-R002" });
        StockLots.Add(new StockLotModel { Id = "IV214-26-R003", SlotNumber = 3, GrossWeightKg = 320.0m, TareWeightKg = 5.0m, NetWeightKg = 315.0m, State = "Aprobado", IsQuarantine = false, LocationId = "PIM/Stock/PT", QualityCheckId = "QC-EXT-R003" });
        StockLots.Add(new StockLotModel { Id = "IV214-26-R004.S", SlotNumber = 4, GrossWeightKg = 305.0m, TareWeightKg = 5.0m, NetWeightKg = 300.0m, State = "Cuarentena (.S)", IsQuarantine = true, LocationId = "PIM/Stock/Cuarentena", QualityCheckId = "QC-EXT-R004" });

        // 11. Quality Checks
        QualityChecks.Add(new QualityCheckModel { Id = "QC-EXT-R001", LotId = "IV214-26-R001", InspectorName = "María Gómez", GaugeMeasuredMicrons = 50.0m, WidthMeasuredMm = 600.0m, CoronaDynes = 38.0m, Passed = true, Notes = "Calibre uniforme, corona OK 38 dynes." });
        QualityChecks.Add(new QualityCheckModel { Id = "QC-EXT-R002", LotId = "IV214-26-R002", InspectorName = "María Gómez", GaugeMeasuredMicrons = 50.5m, WidthMeasuredMm = 601.0m, CoronaDynes = 39.0m, Passed = true, Notes = "Calibre uniforme conforme." });
        QualityChecks.Add(new QualityCheckModel { Id = "QC-EXT-R003", LotId = "IV214-26-R003", InspectorName = "María Gómez", GaugeMeasuredMicrons = 49.8m, WidthMeasuredMm = 599.5m, CoronaDynes = 38.0m, Passed = true, Notes = "Rollo óptimo." });
        QualityChecks.Add(new QualityCheckModel { Id = "QC-EXT-R004", LotId = "IV214-26-R004.S", InspectorName = "María Gómez", GaugeMeasuredMicrons = 62.0m, WidthMeasuredMm = 612.0m, CoronaDynes = 30.0m, Passed = false, Notes = "RECHAZADO: Desviación de calibre (+24%) y baja tensión corona. Renombrado .S a Cuarentena." });

        // 12. Mass Balance
        MassBalances.Add(new MassBalanceModel());
    }

    // Interactive Action Methods
    public void ConfirmSalesOrder(string folio)
    {
        var order = SalesOrders.FirstOrDefault(o => o.Folio == folio);
        if (order != null)
        {
            order.State = "Confirmado";
            var om = MasterOrders.FirstOrDefault(m => m.SalesOrderFolio == folio);
            if (om != null) om.State = "Confirmada";
            NotifyChanged();
        }
    }

    public void ValidateSales(string folio)
    {
        var order = SalesOrders.FirstOrDefault(o => o.Folio == folio);
        if (order != null)
        {
            order.SalesApproved = true;
            CheckAuthorization(order);
            NotifyChanged();
        }
    }

    public void ValidateCredit(string folio)
    {
        var order = SalesOrders.FirstOrDefault(o => o.Folio == folio);
        if (order != null)
        {
            order.CreditApproved = true;
            CheckAuthorization(order);
            NotifyChanged();
        }
    }

    private void CheckAuthorization(SalesOrderModel order)
    {
        if (order.SalesApproved && order.CreditApproved)
        {
            order.State = "Autorizado";
            var om = MasterOrders.FirstOrDefault(m => m.SalesOrderFolio == order.Folio);
            if (om != null) om.State = "Confirmada";
        }
    }

    public void LoadBomRecipe(string ofFolio)
    {
        var of = ManufacturingOrders.FirstOrDefault(m => m.FolioOf == ofFolio);
        if (of != null)
        {
            of.BomLoaded = true;
            of.BomRecipeSummary = "Receta Parseada & Reservada: Tolva A (33% LDPE Virgin), Tolva B (34% LLDPE + 5% UV), Tolva C (33% LDPE)";
            NotifyChanged();
        }
    }

    public void AssignWorkOrderMachine(string woFolio, string machineId)
    {
        var wo = WorkOrders.FirstOrDefault(w => w.FolioWo == woFolio);
        var machine = WorkCenters.FirstOrDefault(mc => mc.Id == machineId);
        if (wo != null && machine != null)
        {
            wo.WorkCenterId = machine.Id;
            wo.WorkCenterName = machine.Name;
            wo.ScheduledDate = DateTime.Now;
            wo.State = "En progreso";

            machine.State = "En Producción";
            machine.CurrentWorkOrderFolio = wo.FolioWo;

            var of = ManufacturingOrders.FirstOrDefault(m => m.WorkOrderFolio == wo.FolioWo);
            if (of != null)
            {
                of.State = "En progreso";
                of.WorkCenterId = machine.Id;
                of.WorkCenterName = machine.Name;
            }

            var so = SalesOrders.FirstOrDefault();
            if (so != null && so.State == "Autorizado")
            {
                so.State = "En progreso";
            }

            NotifyChanged();
        }
    }

    public void WeighRoll(int slotNumber, decimal grossKg, decimal tareKg, bool passQuality)
    {
        var slot = StockLots.FirstOrDefault(l => l.SlotNumber == slotNumber);
        if (slot != null)
        {
            slot.GrossWeightKg = grossKg;
            slot.TareWeightKg = tareKg;
            slot.NetWeightKg = grossKg - tareKg;

            if (passQuality)
            {
                slot.State = "Aprobado";
                slot.IsQuarantine = false;
                slot.LocationId = "PIM/Stock/PT";
            }
            else
            {
                slot.State = "Cuarentena (.S)";
                slot.IsQuarantine = true;
                slot.Id = slot.Id.EndsWith(".S") ? slot.Id : slot.Id + ".S";
                slot.LocationId = "PIM/Stock/Cuarentena";
            }
            NotifyChanged();
        }
    }

    public void ExecuteClosure(string ofFolio)
    {
        var of = ManufacturingOrders.FirstOrDefault(m => m.FolioOf == ofFolio);
        if (of != null)
        {
            of.ClosureExecuted = true;
            of.State = "Cierre Técnico";
            var mb = MassBalances.FirstOrDefault(m => m.ManufacturingOrderFolio == ofFolio);
            if (mb != null)
            {
                mb.IsClosed = true;
                mb.ContpaqDeductionStatus = "Afectación Consolidada Ejecutada en CONTPAQi Premium (1,250 kg MP descontados)";
            }
            NotifyChanged();
        }
    }

    public void DispatchInterplantTransfer(string pickingId)
    {
        var picking = StockPickings.FirstOrDefault(p => p.Id == pickingId);
        if (picking != null)
        {
            picking.State = "En Tránsito";
            picking.ContpaqDocumentRef = "En Tránsito PIM -> SC (Sin afectación CONTPAQi)";
            NotifyChanged();
        }
    }

    public void ReceiveInterplantTransfer(string pickingId)
    {
        var picking = StockPickings.FirstOrDefault(p => p.Id == pickingId);
        if (picking != null)
        {
            picking.State = "Validado / Concretado";
            picking.ContpaqDocumentRef = "Traspaso ERP CONTPAQi #TR-8821 Sincronizado OK";

            var ofBol = ManufacturingOrders.FirstOrDefault(m => m.FolioOf == "OF-BOL-2026-01");
            if (ofBol != null)
            {
                ofBol.State = "En progreso";
            }
            NotifyChanged();
        }
    }

    public void CompleteBagging(string ofFolio, decimal thousands, decimal netKg)
    {
        var of = ManufacturingOrders.FirstOrDefault(m => m.FolioOf == ofFolio);
        if (of != null)
        {
            of.ProducedThousands = thousands;
            of.NetWeightKg = netKg;
            of.ActualKgPerThousand = thousands > 0 ? netKg / thousands : 0;
            of.State = "Hecho";

            var so = SalesOrders.FirstOrDefault();
            if (so != null)
            {
                so.State = "Hecho";
            }

            var om = MasterOrders.FirstOrDefault();
            if (om != null)
            {
                om.State = "Hecho";
            }
            NotifyChanged();
        }
    }

    private void NotifyChanged() => OnDataChanged?.Invoke();
}
