namespace PolyConecta.Presentation.Services;

public class BomLine
{
    public string Clave { get; set; } = "";
    public string Producto { get; set; } = "";
    public decimal Cantidad { get; set; }
    public string Unidad { get; set; } = "KGS";
}

public class SubProductLine
{
    public string Clave { get; set; } = "";
    public string Producto { get; set; } = "";
    public decimal Cantidad { get; set; }
    public string Unidad { get; set; } = "KGS";
    public bool Producido { get; set; }
    public string AlmacenDestino { get; set; } = "";
}

public class PlanningLine
{
    public string CentroTrabajo { get; set; } = "";
    public string Producto { get; set; } = "";
    public decimal Cantidad { get; set; }
    public string Unidad { get; set; } = "KGS";
    public decimal HorasAsignadas { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Operador { get; set; } = "";
}

public class ProductionLot
{
    public string Lote { get; set; } = "";
    public decimal Real { get; set; }
    public string Unidad { get; set; } = "KGS";
    public string Estado { get; set; } = "En revisión"; // En revisión | Aprobado | Rechazado
}

/// <summary>
/// Orden de Fabricación — autoreferenciada: la que no tiene OriginFolio es la "maestra"
/// (ligada al pedido); las demás encadenan hacia atrás vía OriginFolio. Ver spec 002,
/// "Nota de validación posterior — revisión 4".
/// </summary>
public class ManufacturingOrder
{
    public string Folio { get; set; } = "";
    public string ProcessType { get; set; } = ""; // Extrusion | Impresion | Bolseo
    public string ProcessLabel { get; set; } = "";
    public string Producto { get; set; } = "";
    public string Empresa { get; set; } = "POLYEMPAQUES Y DERIVADOS S.A. DE C.V.";
    public decimal Cantidad { get; set; }
    public string Unidad { get; set; } = "KGS";
    public decimal TiempoEstimadoHrs { get; set; }
    public int NumeroRollos { get; set; }
    public bool CalidadRequerida { get; set; } = true;
    public string AlmacenFalla { get; set; } = "PIM/Cuarentena";
    public DateTime FechaEsperada { get; set; }
    public string State { get; set; } = "Borrador"; // Borrador | Planeado | En progreso | Hecho
    public string? OriginFolio { get; set; }

    public List<BomLine> Componentes { get; set; } = new();
    public List<SubProductLine> Subproductos { get; set; } = new();
    public List<ProductionLot> Produccion { get; set; } = new();
    public List<PlanningLine> Planeacion { get; set; } = new();
    public int SequenceCounter { get; set; }

    public string NumeroLabel => ProcessType == "Bolseo" ? "Numero de empaques" : "No. Rollos";
    public decimal ProducidoTotal => Produccion.Where(p => p.Estado != "Rechazado").Sum(p => p.Real);
}

public class QualityControlState
{
    public required string ManufacturingOrderFolio { get; init; }
    public string Folio { get; set; } = "";
    public string Auditor { get; set; } = "Armando Silva";
    public string ProcessLabel { get; set; } = "";
}

public class ProcessCheck
{
    public string Proceso { get; set; } = "";
    public bool Activo { get; set; }
    public string Origen { get; set; } = "";
    public string Producto { get; set; } = "";
}

public class ShipmentLine
{
    public string Clave { get; set; } = "";
    public string Producto { get; set; } = "";
    public decimal Demanda { get; set; }
    public decimal Entregado { get; set; }
    public string Unidad { get; set; } = "PZA";
    public List<string> LotesSeleccionados { get; set; } = new();
}

public class InterplantTransferState
{
    public string Folio { get; set; } = "18293";
    public string Operacion { get; set; } = "Traspaso PIM a SC";
    public string Origen { get; set; } = "PIM/Stock/PT";
    public string Destino { get; set; } = "SC/Stock/MP";
    public DateTime FechaProgramada { get; set; } = new DateTime(2026, 5, 5);
    public DateTime FechaLimite { get; set; } = new DateTime(2026, 5, 6);
    public string ContpaqId { get; set; } = "";
    public int Step { get; set; } // 0 Borrador,1 En espera de operación,2 En espera,3 Listo,4 Hecho
    public string? Warning { get; set; }
    public string? Error { get; set; }
    public List<ShipmentLine> Lineas { get; set; } = new()
    {
        new() { Clave = "PT3413 C4235", Producto = "ROLLO TUB 20.5 370 (Maestro)", Demanda = 500, Entregado = 0, Unidad = "KGS" }
    };

    public string State => Step switch
    {
        0 => "Borrador",
        1 => "En espera de operación",
        2 => "En espera",
        3 => "Listo",
        _ => "Hecho"
    };
}

/// <summary>
/// Recepción — segundo paso del traspaso interplanta: misma operación física que
/// InterplantTransferState, pero valida lo que entra a almacén (no lo que sale), por
/// lo que Origen/Destino quedan invertidos y lleva su propia selección de lotes.
/// </summary>
public class ReceptionState
{
    public string Folio { get; set; } = "18293";
    public string Operacion { get; set; } = "Recepción en almacén";
    public string Origen { get; set; } = "SC/Stock/MP";
    public string Destino { get; set; } = "SC/Stock/PT";
    public DateTime FechaProgramada { get; set; } = new DateTime(2026, 5, 5);
    public DateTime FechaLimite { get; set; } = new DateTime(2026, 5, 6);
    public string ContpaqId { get; set; } = "";
    public int Step { get; set; } // 0 Borrador,1 En espera,2 Listo,3 Hecho
    public string? Warning { get; set; }
    public string? Error { get; set; }
    public List<ShipmentLine> Lineas { get; set; } = new()
    {
        new() { Clave = "PT3413 C4235", Producto = "ROLLO TUB 20.5 370 (Maestro)", Demanda = 500, Entregado = 0, Unidad = "KGS" }
    };

    public string State => Step switch
    {
        0 => "Borrador",
        1 => "En espera",
        2 => "Listo",
        _ => "Hecho"
    };
}

public class DeliveryState
{
    public string Folio { get; set; } = "18294";
    public string Operacion { get; set; } = "Entrega a cliente";
    public string Origen { get; set; } = "SC/Stock/PT";
    public string Destino { get; set; } = "EMPRESA MEXICANA DE MANUFACTURA";
    public DateTime FechaProgramada { get; set; } = new DateTime(2026, 5, 5);
    public DateTime FechaLimite { get; set; } = new DateTime(2026, 5, 6);
    public string ContpaqId { get; set; } = "";
    public int Step { get; set; } // 0 Borrador,1 En espera,2 Listo,3 Hecho
    public string? Warning { get; set; }
    public string? Error { get; set; }
    public List<ShipmentLine> Lineas { get; set; } = new()
    {
        new() { Clave = "PT1113 C567", Producto = "BOLSA MEDIANA 44X84 C.430 BOL-004 [77]", Demanda = 5500, Entregado = 0, Unidad = "PZA" }
    };

    public string State => Step switch
    {
        0 => "Borrador",
        1 => "En espera",
        2 => "Listo",
        _ => "Hecho"
    };
}

public class Incidencia
{
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string CentroTrabajo { get; set; } = "";
    public string Tipo { get; set; } = "";
    public string Comentarios { get; set; } = "";
    public string HoraInicio { get; set; } = "";
    public string HoraFin { get; set; } = "";
}

/// <summary>
/// Scoped (per-session) shared state for the operational demo, so that navigating
/// between real routes never loses lo que el usuario ya capturó en esa sesión.
/// </summary>
public class OperationalFlowState
{
    public const string PedidoFolio = "IV310-26";

    public string CurrentOrderStage { get; private set; } = "Borrador";
    public string OrdenCompraCliente { get; private set; } = "3893";
    public string Agente { get; private set; } = "Celia Villarreal";
    public string ContpaqId { get; private set; } = "26200";

    public List<ProcessCheck> Procesos { get; } = new()
    {
        new() { Proceso = "Extrusion", Activo = true, Origen = "", Producto = "PT3413 C455" },
        new() { Proceso = "Impresion", Activo = true, Origen = "SC", Producto = "PT3413 C4235" },
        new() { Proceso = "Bolseo", Activo = true, Origen = "SC", Producto = "PT3413 C4236" }
    };

    public List<ManufacturingOrder> ManufacturingOrders { get; } = new()
    {
        new()
        {
            Folio = "BOL-2026-0001",
            ProcessType = "Bolseo",
            ProcessLabel = "Bolseo",
            Producto = "BOLSA MEDIANA 44X84 C.430 BOL-004 [77]",
            Cantidad = 20000m,
            Unidad = "Millares",
            TiempoEstimadoHrs = 8,
            NumeroRollos = 0,
            AlmacenFalla = "SC/Scrap",
            FechaEsperada = new DateTime(2026, 9, 23),
            OriginFolio = null,
            Componentes = { new() { Clave = "PT3413 C4235", Producto = "ROLLO TUB 20.5 370 (Impreso)", Cantidad = 500, Unidad = "kgs" } },
            Subproductos = { new() { Producto = "Scrap resina residual tpte (BD) de rollo impreso", Cantidad = 10, Unidad = "KGS", AlmacenDestino = "SC/Scrap" } },
            Produccion =
            {
                new() { Lote = "R001-IV310-26", Real = 10000, Unidad = "Millares", Estado = "Aprobado" },
                new() { Lote = "R002-IV310-26", Real = 10000, Unidad = "Millares", Estado = "Aprobado" }
            },
            Planeacion = { new() { CentroTrabajo = "BOLS-001", Producto = "BOLSA MEDIANA 44X84 C.430 BOL-004 [77]", Cantidad = 20000, Unidad = "Millares", HorasAsignadas = 8, FechaInicio = new DateTime(2026, 9, 22, 8, 0, 0), FechaFin = new DateTime(2026, 9, 22, 16, 0, 0), Operador = "Alfonso Ortega" } },
            SequenceCounter = 2
        },
        new()
        {
            Folio = "IMP-2026-0001",
            ProcessType = "Impresion",
            ProcessLabel = "Impresión",
            Producto = "ROLLO TUB 20.5 370 (Impreso)",
            Cantidad = 500m,
            Unidad = "KGS",
            TiempoEstimadoHrs = 8,
            NumeroRollos = 5,
            AlmacenFalla = "SC/Scrap",
            FechaEsperada = new DateTime(2026, 9, 23),
            OriginFolio = "BOL-2026-0001",
            Componentes = { new() { Producto = "ROLLO TUB 20.5 370 (Maestro)", Cantidad = 500, Unidad = "KGS" } },
            Subproductos = { new() { Producto = "Scrap resina residual tpte (BD)", Cantidad = 10, Unidad = "KGS", AlmacenDestino = "SC/Scrap" } },
            Produccion =
            {
                new() { Lote = "R001-IV310-26", Real = 100, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R002-IV310-26", Real = 95, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R003-IV310-26", Real = 105, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R004-IV310-26", Real = 100, Unidad = "KGS", Estado = "En revisión" },
                new() { Lote = "R005-IV310-26", Real = 100, Unidad = "KGS", Estado = "En revisión" }
            },
            Planeacion =
            {
                new() { CentroTrabajo = "IMP-001", Producto = "ROLLO TUB 20.5 370 (Impreso)", Cantidad = 250, Unidad = "KGS", FechaInicio = new DateTime(2026, 9, 19), FechaFin = new DateTime(2026, 9, 19), Operador = "Alejandro Varela" },
                new() { CentroTrabajo = "IMP-002", Producto = "ROLLO TUB 20.5 370 (Impreso)", Cantidad = 250, Unidad = "KGS", FechaInicio = new DateTime(2026, 9, 20), FechaFin = new DateTime(2026, 9, 20), Operador = "Alejandro Varela" }
            },
            SequenceCounter = 5
        },
        new()
        {
            Folio = "EXT-2026-0001",
            ProcessType = "Extrusion",
            ProcessLabel = "Extrusión",
            Producto = "ROLLO TUB 20.5 370 (Maestro)",
            Cantidad = 500m,
            Unidad = "KGS",
            TiempoEstimadoHrs = 24,
            NumeroRollos = 5,
            AlmacenFalla = "SC/Scrap",
            FechaEsperada = new DateTime(2026, 9, 23),
            OriginFolio = "IMP-2026-0001",
            Componentes =
            {
                new() { Clave = "PACT", Producto = "PAC TPTE", Cantidad = 340, Unidad = "KGS" },
                new() { Clave = "GA502022", Producto = "LINEAL BUTENO", Cantidad = 150, Unidad = "KGS" },
                new() { Clave = "MP0032", Producto = "DESLIZANTE ANTIBLOCKBC110", Cantidad = 10, Unidad = "KGS" }
            },
            Subproductos = { new() { Producto = "Scrap resina residual tpte (BD)", Cantidad = 10, Unidad = "KGS", AlmacenDestino = "PIM/Cuarentena" } },
            Produccion =
            {
                new() { Lote = "R001-IV310-26", Real = 100, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R002-IV310-26", Real = 95, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R003-IV310-26", Real = 105, Unidad = "KGS", Estado = "Aprobado" },
                new() { Lote = "R004-IV310-26", Real = 100, Unidad = "KGS", Estado = "En revisión" },
                new() { Lote = "R005-IV310-26", Real = 100, Unidad = "KGS", Estado = "En revisión" }
            },
            Planeacion =
            {
                new() { CentroTrabajo = "COEXT-001", Producto = "ROLLO TUB 20.5 370", Cantidad = 250, Unidad = "KGS", FechaInicio = new DateTime(2026, 9, 19), FechaFin = new DateTime(2026, 9, 19), Operador = "Alejandro Varela" },
                new() { CentroTrabajo = "COEXT-002", Producto = "ROLLO TUB 20.5 370", Cantidad = 250, Unidad = "KGS", FechaInicio = new DateTime(2026, 9, 20), FechaFin = new DateTime(2026, 9, 20), Operador = "Alejandro Varela" }
            },
            SequenceCounter = 5
        }
    };

    public InterplantTransferState Traslado { get; } = new();
    public ReceptionState Recepcion { get; } = new();
    public DeliveryState Entrega { get; } = new();

    public List<Incidencia> Incidencias { get; } = new()
    {
        new() { Fecha = new DateTime(2026, 5, 5), CentroTrabajo = "EXT-001", Tipo = "Reventon", Comentarios = "Cambio de mallas", HoraInicio = "19:00", HoraFin = "07:00" },
        new() { Fecha = new DateTime(2026, 5, 5), CentroTrabajo = "EXT-011", Tipo = "Falta de operador", Comentarios = "No hubo operador en horario", HoraInicio = "20:00", HoraFin = "07:00" }
    };

    public event Action? OnChange;

    public void SetOrderStage(string stage) { CurrentOrderStage = stage; Notify(); }

    public void AutorizarManufactura()
    {
        if (CurrentOrderStage == "Confirmado") CurrentOrderStage = "Autorizado";
        Notify();
    }

    public ManufacturingOrder? GetOrder(string folio) => ManufacturingOrders.FirstOrDefault(o => o.Folio == folio);

    public IReadOnlyList<ManufacturingOrder> GetSecondaries(string folio) =>
        ManufacturingOrders.Where(o => o.OriginFolio == folio).ToList();

    public List<ProductionLot> GetLotesDisponiblesTraslado() =>
        GetOrder("EXT-2026-0001")?.Produccion.Where(l => l.Estado == "Aprobado").ToList() ?? new();

    public List<ProductionLot> GetLotesDisponiblesEntrega() =>
        GetOrder("BOL-2026-0001")?.Produccion.Where(l => l.Estado == "Aprobado").ToList() ?? new();

    public void AgregarComponente(string folio, string clave, string producto, decimal cantidad, string unidad)
    {
        var of = GetOrder(folio);
        if (of == null) return;
        of.Componentes.Add(new BomLine { Clave = clave, Producto = producto, Cantidad = cantidad, Unidad = unidad });
        Notify();
    }

    public void QuitarComponente(string folio, BomLine line)
    {
        GetOrder(folio)?.Componentes.Remove(line);
        Notify();
    }

    public void Planear(string folio)
    {
        var of = GetOrder(folio);
        if (of == null || of.Componentes.Count == 0) return; // guard: FR-004 (requiere componentes)
        of.State = "Planeado";
        Notify();
    }

    public void AgregarPlaneacion(string folio, string centroTrabajo, string producto, decimal cantidad, string unidad, decimal horasAsignadas, DateTime inicio, DateTime fin, string operador)
    {
        var of = GetOrder(folio);
        if (of == null) return;
        of.Planeacion.Add(new PlanningLine { CentroTrabajo = centroTrabajo, Producto = producto, Cantidad = cantidad, Unidad = unidad, HorasAsignadas = horasAsignadas, FechaInicio = inicio, FechaFin = fin, Operador = operador });
        if (of.State == "Planeado") of.State = "En progreso";
        if (CurrentOrderStage == "Autorizado") CurrentOrderStage = "En progreso";
        Notify();
    }

    public void RegistrarPesajeRollo(string folio, decimal pesoBruto, decimal tara)
    {
        var of = GetOrder(folio);
        if (of == null) return;
        of.SequenceCounter++;
        of.Produccion.Add(new ProductionLot
        {
            Lote = $"R{of.SequenceCounter:000}-{PedidoFolio}",
            Real = pesoBruto - tara,
            Unidad = "KGS",
            Estado = "En revisión"
        });
        Notify();
    }

    public void AprobarLote(ProductionLot lote) { lote.Estado = "Aprobado"; Notify(); }

    public void RechazarLote(ProductionLot lote)
    {
        lote.Estado = "Rechazado";
        if (!lote.Lote.EndsWith(".S", StringComparison.Ordinal)) lote.Lote += ".S";
        Notify();
    }

    public void CerrarProduccion(string folio)
    {
        var of = GetOrder(folio);
        if (of == null) return;
        if (of.CalidadRequerida && of.Produccion.Any(l => l.Estado == "En revisión")) return; // hard-stop: falta calidad
        of.State = "Hecho";
        if (ManufacturingOrders.All(o => o.State == "Hecho"))
            CurrentOrderStage = "Hecho";
        Notify();
    }

    public void ComprobarDisponibilidadTraslado() => Notify();

    public void ComprobarDisponibilidadRecepcion() => Notify();

    public void ComprobarDisponibilidadEntrega() => Notify();

    public void ValidarTraslado()
    {
        if (Traslado.Step >= 4) { Notify(); return; }
        if (Traslado.Step == 3)
        {
            var ok = CerrarSalida(Traslado.Lineas, GetLotesDisponiblesTraslado(), out var warning, out var error, "Traslado");
            Traslado.Warning = warning;
            Traslado.Error = error;
            if (!ok) { Notify(); return; } // 0 capturado: la salida de inventario no ocurrió, no se avanza la etapa
        }
        Traslado.Step++;
        Notify();
    }

    public void ValidarRecepcion()
    {
        if (Recepcion.Step >= 3) { Notify(); return; }
        if (Recepcion.Step == 2)
        {
            var ok = CerrarSalida(Recepcion.Lineas, GetLotesDisponiblesTraslado(), out var warning, out var error, "Recepción");
            Recepcion.Warning = warning;
            Recepcion.Error = error;
            if (!ok) { Notify(); return; }
        }
        Recepcion.Step++;
        Notify();
    }

    public void ValidarEntrega()
    {
        if (Entrega.Step >= 3) { Notify(); return; }
        if (Entrega.Step == 2)
        {
            var ok = CerrarSalida(Entrega.Lineas, GetLotesDisponiblesEntrega(), out var warning, out var error, "Entrega");
            Entrega.Warning = warning;
            Entrega.Error = error;
            if (!ok) { Notify(); return; }
        }
        Entrega.Step++;
        Notify();
    }

    /// <summary>
    /// Cierra la operación de inventario de un documento de logística: calcula lo realmente
    /// capturado (suma de lotes seleccionados) contra la demanda. Si no se capturó nada,
    /// rechaza el cierre (la salida/entrada de inventario no ocurrió) — devuelve false y no
    /// modifica Entregado. Si es parcial, permite el cierre pero deja una advertencia.
    /// </summary>
    private static bool CerrarSalida(List<ShipmentLine> lineas, List<ProductionLot> pool, out string? warning, out string? error, string nombreDocumento)
    {
        decimal real = 0, demanda = 0;
        foreach (var l in lineas)
        {
            real += l.LotesSeleccionados.Sum(f => pool.FirstOrDefault(x => x.Lote == f)?.Real ?? 0);
            demanda += l.Demanda;
        }

        if (real == 0)
        {
            warning = null;
            error = $"No se puede validar: no hay lotes capturados. Selecciona al menos un lote antes de cerrar el {nombreDocumento.ToLowerInvariant()}.";
            return false;
        }

        foreach (var l in lineas)
            l.Entregado = l.LotesSeleccionados.Sum(f => pool.FirstOrDefault(x => x.Lote == f)?.Real ?? 0);

        error = null;
        warning = real < demanda
            ? $"{nombreDocumento} parcial: {real:N1} de {demanda:N1} {lineas.FirstOrDefault()?.Unidad}."
            : null;
        return true;
    }

    public void ResetAll()
    {
        CurrentOrderStage = "Borrador";
        foreach (var of in ManufacturingOrders)
        {
            of.State = "Borrador";
            of.Componentes.Clear();
            of.Subproductos.ForEach(s => s.Producido = false);
            of.Produccion.Clear();
            of.Planeacion.Clear();
            of.SequenceCounter = 0;
        }
        Traslado.Step = 0;
        Traslado.Warning = null;
        Traslado.Error = null;
        Traslado.Lineas.ForEach(l => { l.Entregado = 0; l.LotesSeleccionados.Clear(); });
        Recepcion.Step = 0;
        Recepcion.Warning = null;
        Recepcion.Error = null;
        Recepcion.Lineas.ForEach(l => { l.Entregado = 0; l.LotesSeleccionados.Clear(); });
        Entrega.Step = 0;
        Entrega.Warning = null;
        Entrega.Error = null;
        Entrega.Lineas.ForEach(l => { l.Entregado = 0; l.LotesSeleccionados.Clear(); });
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}
