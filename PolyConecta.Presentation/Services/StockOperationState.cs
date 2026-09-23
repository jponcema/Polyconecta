namespace PolyConecta.Presentation.Services;

/// <summary>
/// Tipo de operación de inventario. Define la naturaleza transaccional del movimiento y qué
/// evento dispara en CONTPAQi — el mismo catálogo de la sección 5 de la arquitectura.
/// </summary>
public class OperationType
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Origen { get; set; } = "";
    public string Destino { get; set; } = "";
    public string EventoContpaq { get; set; } = "";
    /// <summary>Código del tipo que revierte este movimiento, si lo hay.</summary>
    public string? Reversa { get; set; }
    public bool EsDevolucion { get; set; }
}

public class LotAllocation
{
    public string Lote { get; set; } = "";
    public decimal Cantidad { get; set; }
}

public class StockOperationLine
{
    public string Clave { get; set; } = "";
    public string Producto { get; set; } = "";
    public string Unidad { get; set; } = "KGS";
    public decimal Solicitado { get; set; }
    /// <summary>Lotes y cantidades que el almacenista declara que salen.</summary>
    public List<LotAllocation> Asignaciones { get; set; } = new();
    public decimal Declarado => Asignaciones.Sum(a => a.Cantidad);
    public decimal Entregado { get; set; }
    public decimal Pendiente => Math.Max(0, Solicitado - Entregado);
}

/// <summary>
/// Operación de inventario. La Recolección (MP → WIP) es la formalidad con la que Producción
/// le pide materia prima a Almacén y Almacén le da salida del stock.
/// </summary>
public class StockOperation
{
    public string Folio { get; set; } = "";
    public OperationType Tipo { get; set; } = new();
    public string OfFolio { get; set; } = "";
    public string Origen { get; set; } = "";
    public string Destino { get; set; } = "";
    public DateTime Fecha { get; set; } = new DateTime(2026, 9, 22);
    public string? BackorderDe { get; set; }
    public string? Warning { get; set; }
    public string? Error { get; set; }
    public List<StockOperationLine> Lineas { get; set; } = new();

    /// <summary>0 Borrador · 1 En espera (solicitada a almacén) · 2 Listo (lotes declarados) · 3 Hecho</summary>
    public int Step { get; set; }

    public string State => Step switch
    {
        0 => "Borrador",
        1 => "En espera",
        2 => "Listo",
        _ => "Hecho"
    };

    public bool EsParcial => Lineas.Any(l => l.Pendiente > 0);
    public string Operacion => Tipo.Nombre;
    public DateTime FechaLimite => Fecha.AddDays(1);
    public string ContpaqId { get; set; } = "";
    public decimal TotalSolicitado => Lineas.Sum(l => l.Solicitado);
    public decimal TotalEntregado => Lineas.Sum(l => l.Entregado);
}

public class StockOperationState
{
    private readonly InventoryState _inv;

    public StockOperationState(InventoryState inv) => _inv = inv;

    public event Action? OnChange;
    private void Notify() => OnChange?.Invoke();

    // ---------------------------------------------------------------- catálogo de tipos

    public const string RecoleccionPim = "PIM-REC-OUT";
    public const string RecoleccionStc = "STC-REC-OUT";

    public List<OperationType> Tipos { get; } = new()
    {
        new()
        {
            Codigo = RecoleccionPim,
            Nombre = "Recolección de materia prima",
            Origen = InventoryState.AlmacenMateriaPrima,
            Destino = InventoryState.WipPim,
            EventoContpaq = "Traspaso de almacén MP → WIP al validar",
            Reversa = "PIM-REC-RET"
        },
        new()
        {
            Codigo = "PIM-REC-RET",
            Nombre = "Devolución de recolección",
            Origen = InventoryState.WipPim,
            Destino = InventoryState.AlmacenMateriaPrima,
            EventoContpaq = "Traspaso de almacén WIP → MP al validar",
            EsDevolucion = true
        },
        new()
        {
            Codigo = RecoleccionStc,
            Nombre = "Recolección de rollos a conversión",
            Origen = "SC/Stock/MP",
            Destino = InventoryState.WipStc,
            EventoContpaq = "Traspaso de almacén MP → WIP al validar",
            Reversa = "STC-REC-RET"
        },
        new()
        {
            Codigo = "STC-REC-RET",
            Nombre = "Devolución de recolección",
            Origen = InventoryState.WipStc,
            Destino = "SC/Stock/MP",
            EventoContpaq = "Traspaso de almacén WIP → MP al validar",
            EsDevolucion = true
        }
    };

    public OperationType GetTipo(string codigo) =>
        Tipos.FirstOrDefault(t => t.Codigo == codigo) ?? Tipos[0];

    // ---------------------------------------------------------------- documentos

    public List<StockOperation> Operaciones { get; } = new();

    private int _secuencia = 48213;
    private string NuevoFolio(string planta, bool salida) => $"{planta}/{(salida ? "OUT" : "IN")}/{++_secuencia}";

    public StockOperation? Get(string folio) => Operaciones.FirstOrDefault(o => o.Folio == folio);

    public List<StockOperation> DeOf(string ofFolio) =>
        Operaciones.Where(o => o.OfFolio == ofFolio).ToList();

    public bool TieneRecoleccion(string ofFolio) =>
        Operaciones.Any(o => o.OfFolio == ofFolio && !o.Tipo.EsDevolucion);

    // ---------------------------------------------------------------- emisión

    /// <summary>
    /// La recolección nace de la mano de la Orden de Fabricación, en Borrador. Mientras la OF esté
    /// en borrador sus líneas se mantienen sincronizadas con los componentes que edite el Planner.
    /// No mueve inventario: eso ocurre cuando el almacenista la valida al momento de la salida.
    /// </summary>
    public StockOperation AsegurarRecoleccion(string ofFolio, IEnumerable<BomLine> componentes, string planta = "PIM")
    {
        var tipo = GetTipo(planta == "PIM" ? RecoleccionPim : RecoleccionStc);
        var lineas = componentes.Select(c => new StockOperationLine
        {
            Clave = c.Clave,
            Producto = c.Producto,
            Unidad = c.Unidad,
            Solicitado = c.Cantidad
        }).ToList();

        var existente = Operaciones.FirstOrDefault(o => o.OfFolio == ofFolio && !o.Tipo.EsDevolucion && o.BackorderDe == null);
        if (existente != null)
        {
            // Solo en Borrador: una vez confirmada, la solicitud ya no sigue a la BoM.
            if (existente.Step == 0) { existente.Lineas = lineas; Notify(); }
            return existente;
        }

        var op = new StockOperation
        {
            Folio = NuevoFolio(planta, salida: true),
            Tipo = tipo,
            OfFolio = ofFolio,
            Origen = tipo.Origen,
            Destino = tipo.Destino,
            Step = 0,
            Lineas = lineas
        };
        Operaciones.Add(op);
        Notify();
        return op;
    }

    /// <summary>
    /// El Planner confirma la OF: su recolección se libera a Almacén, que valida al momento
    /// en que el material sale físicamente.
    /// </summary>
    public void ConfirmarRecoleccion(string ofFolio)
    {
        foreach (var op in Operaciones.Where(o => o.OfFolio == ofFolio && o.Step == 0))
            op.Step = 1;
        Notify();
    }

    /// <summary>Devolución del saldo de WIP al almacén. La cantidad la captura el operador.</summary>
    public StockOperation EmitirDevolucion(string ofFolio, string planta = "PIM")
    {
        var tipo = GetTipo(planta == "PIM" ? "PIM-REC-RET" : "STC-REC-RET");
        var saldo = _inv.SaldoWip(ofFolio);
        var op = new StockOperation
        {
            Folio = NuevoFolio(planta, salida: false),
            Tipo = tipo,
            OfFolio = ofFolio,
            Origen = tipo.Origen,
            Destino = tipo.Destino,
            Step = 1,
            Lineas = saldo.GroupBy(l => l.Clave).Select(g => new StockOperationLine
            {
                Clave = g.Key,
                Producto = _inv.GetProducto(g.Key)?.Nombre ?? g.Key,
                Unidad = _inv.GetProducto(g.Key)?.Unidad ?? "KGS",
                Solicitado = g.Sum(x => x.Cantidad)
            }).ToList()
        };
        Operaciones.Add(op);
        Notify();
        return op;
    }

    // ---------------------------------------------------------------- declaración de lotes

    public void AsignarLote(StockOperation op, StockOperationLine linea, string lote, decimal cantidad)
    {
        if (cantidad <= 0) return;
        var existente = linea.Asignaciones.FirstOrDefault(a => a.Lote == lote);
        if (existente != null) existente.Cantidad = cantidad;
        else linea.Asignaciones.Add(new LotAllocation { Lote = lote, Cantidad = cantidad });
        RecalcularEstado(op);
        Notify();
    }

    public void QuitarLote(StockOperation op, StockOperationLine linea, LotAllocation alloc)
    {
        linea.Asignaciones.Remove(alloc);
        RecalcularEstado(op);
        Notify();
    }

    private static void RecalcularEstado(StockOperation op)
    {
        if (op.Step >= 3) return;
        op.Step = op.Lineas.Any(l => l.Declarado > 0) ? 2 : 1;
    }

    /// <summary>
    /// Comprueba si el almacén puede cubrir lo pendiente, como en las demás operaciones.
    /// No mueve nada: solo informa.
    /// </summary>
    public void ComprobarDisponibilidad(StockOperation op)
    {
        op.Error = null;
        op.Warning = null;
        var faltantes = new List<string>();

        foreach (var linea in op.Lineas.Where(l => l.Pendiente > 0))
        {
            var disponible = op.Tipo.EsDevolucion
                ? _inv.SaldoWip(op.OfFolio).Where(l => l.Clave == linea.Clave).Sum(l => l.Cantidad)
                : _inv.Disponible(linea.Clave, op.Origen);

            if (disponible < linea.Pendiente)
                faltantes.Add($"{linea.Clave}: hay {disponible:N1} de {linea.Pendiente:N1} {linea.Unidad}");
        }

        op.Warning = faltantes.Count == 0
            ? $"Disponibilidad completa en {op.Origen}."
            : "Disponibilidad parcial — " + string.Join(" · ", faltantes) + ". Puede validar por parcialidades y dejar backorder.";
        Notify();
    }

    // ---------------------------------------------------------------- validación

    /// <summary>
    /// Almacén valida lo que realmente sale. Una producción grande no se surte de golpe:
    /// lo no cubierto genera un backorder ligado a la misma OF.
    /// </summary>
    public StockOperation? Validar(StockOperation op, out string? error)
    {
        error = null;
        op.Error = null;
        op.Warning = null;

        if (op.Step >= 3) { error = "El documento ya fue validado."; op.Error = error; Notify(); return null; }
        if (op.Lineas.All(l => l.Declarado <= 0))
        {
            error = "Declare al menos un lote antes de validar.";
            op.Error = error; Notify(); return null;
        }

        foreach (var linea in op.Lineas)
        {
            if (linea.Declarado > linea.Pendiente)
            {
                error = $"{linea.Clave}: lo declarado ({linea.Declarado:N1}) excede lo pendiente ({linea.Pendiente:N1}).";
                op.Error = error; Notify(); return null;
            }
        }

        // Pre-chequeo de TODAS las asignaciones antes de mover nada: una validación que falla a media
        // aplicación dejaría parte del material movido con el documento todavía en Borrador.
        var demandaPorLote = op.Lineas
            .SelectMany(l => l.Asignaciones)
            .GroupBy(a => a.Lote)
            .ToDictionary(g => g.Key, g => g.Sum(a => a.Cantidad));

        foreach (var (lote, cantidad) in demandaPorLote)
        {
            var disponible = op.Tipo.EsDevolucion
                ? _inv.SaldoWip(op.OfFolio).Where(l => l.Lote == lote).Sum(l => l.Cantidad)
                : _inv.DisponibleDeLote(lote, op.Origen);

            if (cantidad > disponible)
            {
                error = $"Lote {lote}: se declararon {cantidad:N1} pero solo hay {disponible:N1} en {op.Origen}.";
                op.Error = error; Notify(); return null;
            }
        }

        foreach (var linea in op.Lineas)
        {
            foreach (var a in linea.Asignaciones)
            {
                var ok = op.Tipo.EsDevolucion
                    ? _inv.DevolverDeWip(a.Lote, a.Cantidad, op.Origen, op.OfFolio, op.Destino)
                    : _inv.MoverAWip(a.Lote, a.Cantidad, op.Destino, op.OfFolio);

                if (!ok)
                {
                    error = $"Lote {a.Lote}: el movimiento fue rechazado por el inventario.";
                    op.Error = error; Notify(); return null;
                }
            }
            linea.Entregado += linea.Declarado;
            linea.Asignaciones.Clear();
        }

        op.Step = 3;
        op.ContpaqId = $"TR-{op.Folio.Split('/').Last()}";

        // Parcialidad: el remanente viaja a un backorder, no se pierde.
        StockOperation? backorder = null;
        var pendientes = op.Lineas.Where(l => l.Pendiente > 0).ToList();
        if (pendientes.Count > 0)
        {
            var planta = op.Origen.StartsWith("PIM", StringComparison.OrdinalIgnoreCase) ? "PIM" : "SC";
            backorder = new StockOperation
            {
                Folio = NuevoFolio(planta, !op.Tipo.EsDevolucion),
                Tipo = op.Tipo,
                OfFolio = op.OfFolio,
                Origen = op.Origen,
                Destino = op.Destino,
                BackorderDe = op.Folio,
                // El remanente de una recolección ya liberada nace liberado: Almacén sigue esperando darle salida.
                Step = 1,
                Lineas = pendientes.Select(l => new StockOperationLine
                {
                    Clave = l.Clave,
                    Producto = l.Producto,
                    Unidad = l.Unidad,
                    Solicitado = l.Pendiente
                }).ToList()
            };
            Operaciones.Add(backorder);
            op.Warning = $"Entrega parcial. Se generó el backorder {backorder.Folio} por el remanente.";
        }

        Notify();
        return backorder;
    }

    public void Cancelar(StockOperation op)
    {
        if (op.Step >= 3) return;
        Operaciones.Remove(op);
        Notify();
    }

    // ---------------------------------------------------------------- cierre técnico

    /// <summary>
    /// El cierre técnico se bloquea mientras quede saldo sin declarar en WIP.
    /// No hay arrastre a otra OF: el sobrante vuelve a MP y se vuelve a recolectar.
    /// </summary>
    public bool PuedeCerrarOf(string ofFolio, out string? motivo)
    {
        var saldo = _inv.SaldoWipTotal(ofFolio);
        if (saldo > 0)
        {
            motivo = $"Quedan {saldo:N1} en WIP sin declarar. Devuelva a almacén o declare como scrap antes de cerrar.";
            return false;
        }
        var abiertos = Operaciones.Where(o => o.OfFolio == ofFolio && o.Step < 3).ToList();
        if (abiertos.Count > 0)
        {
            motivo = $"Hay {abiertos.Count} documento(s) de recolección sin validar: {string.Join(", ", abiertos.Select(a => a.Folio))}.";
            return false;
        }
        motivo = null;
        return true;
    }

    public decimal TotalRecolectado(string ofFolio) =>
        Operaciones.Where(o => o.OfFolio == ofFolio && !o.Tipo.EsDevolucion).SelectMany(o => o.Lineas).Sum(l => l.Entregado);

    public decimal TotalDevuelto(string ofFolio) =>
        Operaciones.Where(o => o.OfFolio == ofFolio && o.Tipo.EsDevolucion).SelectMany(o => o.Lineas).Sum(l => l.Entregado);
}
