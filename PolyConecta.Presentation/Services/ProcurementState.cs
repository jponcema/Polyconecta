namespace PolyConecta.Presentation.Services;

/// <summary>Acción de una regla de abastecimiento: qué documento genera cuando se aplica.</summary>
public enum ProcurementAction
{
    EntregarDeStock,
    Fabricar,
    TraspasarDeOtraPlanta
}

/// <summary>
/// Regla de abastecimiento. Las reglas de una ruta se encadenan: lo que una no cubre
/// se convierte en la necesidad que resuelve la siguiente.
/// </summary>
public class ProcurementRule
{
    public int Secuencia { get; set; }
    public ProcurementAction Accion { get; set; }
    public string Origen { get; set; } = "";
    public string Destino { get; set; } = "";
    public string TipoOperacion { get; set; } = "";

    public string AccionLabel => Accion switch
    {
        ProcurementAction.EntregarDeStock => "Entregar de stock",
        ProcurementAction.Fabricar => "Fabricar",
        _ => "Traspasar de otra planta"
    };
}

/// <summary>
/// Ruta de abastecimiento. MTSO ("reabastecer bajo pedido con excepción de stock disponible")
/// toma lo que hay y solo dispara la regla siguiente por el faltante; MTO dispara siempre.
/// </summary>
public class ProcurementRoute
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public bool ConsumeStockPrimero { get; set; } = true;
    public List<ProcurementRule> Reglas { get; set; } = new();
}

public enum RouteLevel { Predeterminada, Categoria, Producto, Linea }

/// <summary>Cómo se resolvió la ruta de una línea y en qué nivel se definió.</summary>
public record RouteResolution(ProcurementRoute Ruta, RouteLevel Nivel)
{
    public string NivelLabel => Nivel switch
    {
        RouteLevel.Linea => "definida en esta línea",
        RouteLevel.Producto => "heredada del producto",
        RouteLevel.Categoria => "heredada de la clasificación",
        _ => "ruta predeterminada"
    };
}

/// <summary>Una necesidad que el motor debe resolver: cantidad de un producto en una ubicación.</summary>
public record ProcurementNeed(string Clave, decimal Cantidad, string Ubicacion, int Nivel);

/// <summary>Documento que el motor generó al aplicar una regla.</summary>
public class ProcurementDocument
{
    public string Folio { get; set; } = "";
    public ProcurementAction Accion { get; set; }
    public string Clave { get; set; } = "";
    public string Producto { get; set; } = "";
    public decimal Cantidad { get; set; }
    public string Unidad { get; set; } = "";
    public string Origen { get; set; } = "";
    public string Destino { get; set; } = "";
    public int Nivel { get; set; }
    public string? Ruta { get; set; }
    public string Detalle { get; set; } = "";
    public string RutaUi { get; set; } = "";
}

/// <summary>Lo que el motor haría (simulación) o hizo (ejecución) con una línea de pedido.</summary>
public class ProcurementPlan
{
    public string Clave { get; set; } = "";
    public decimal Solicitado { get; set; }
    public decimal CubiertoDeStock { get; set; }
    public decimal PorFabricar { get; set; }
    public decimal PorTraspasar { get; set; }
    public List<ProcurementDocument> Documentos { get; set; } = new();
    public bool Ejecutado { get; set; }
}

public class ProcurementState
{
    private readonly InventoryState _inv;
    public ProcurementState(InventoryState inv) => _inv = inv;

    public event Action? OnChange;
    private void Notify() => OnChange?.Invoke();

    // ---------------------------------------------------------------- catálogo de rutas

    public List<ProcurementRoute> Rutas { get; } = new()
    {
        new()
        {
            Codigo = "MTSO-BOL",
            Nombre = "Bolsa: entregar de stock y fabricar el faltante",
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.EntregarDeStock, Origen = "SC/Stock/PT", Destino = "Customers",     TipoOperacion = "STC-OUT-DIR" },
                new() { Secuencia = 2, Accion = ProcurementAction.Fabricar,        Origen = "SC/Produccion", Destino = "SC/Stock/PT", TipoOperacion = "STC-BOL-MO" }
            }
        },
        new()
        {
            Codigo = "MTSO-IMP",
            Nombre = "Rollo impreso: consumir stock, si no imprimir",
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.EntregarDeStock, Origen = "SC/Stock/MP",   Destino = "SC/Produccion", TipoOperacion = "STC-REC-OUT" },
                new() { Secuencia = 2, Accion = ProcurementAction.Fabricar,        Origen = "SC/Produccion", Destino = "SC/Stock/MP",   TipoOperacion = "STC-IMP-MO" }
            }
        },
        new()
        {
            Codigo = "MTSO-EXT",
            Nombre = "Rollo maestro: stock en SC, traspaso desde PIM, o extruir",
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.EntregarDeStock,        Origen = "SC/Stock/MP",      Destino = "SC/Produccion",     TipoOperacion = "STC-REC-OUT" },
                new() { Secuencia = 2, Accion = ProcurementAction.TraspasarDeOtraPlanta,  Origen = "PIM/Stock/Rollos", Destino = "SC/Stock/MP",       TipoOperacion = "PIM-TR-OUT" },
                new() { Secuencia = 3, Accion = ProcurementAction.Fabricar,               Origen = "PIM/Produccion",   Destino = "PIM/Stock/Rollos",  TipoOperacion = "PIM-MO" }
            }
        },
        new()
        {
            Codigo = "MTO-BOL",
            Nombre = "Bolsa: fabricar siempre (ignora existencia)",
            ConsumeStockPrimero = false,
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.Fabricar, Origen = "SC/Produccion", Destino = "SC/Stock/PT", TipoOperacion = "STC-BOL-MO" }
            }
        },
        new()
        {
            // La compra de resina queda fuera del alcance del MVP: la MP se consume del almacén.
            // Si no alcanza, la necesidad queda expuesta en el plan en vez de desaparecer.
            Codigo = "MP-STOCK",
            Nombre = "Materia prima: consumir del almacén",
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.EntregarDeStock, Origen = "PIM/Stock/MP", Destino = "PIM/Produccion", TipoOperacion = "PIM-REC-OUT" }
            }
        },
        new()
        {
            Codigo = "STOCK",
            Nombre = "Solo entregar de stock (no fabrica)",
            Reglas =
            {
                new() { Secuencia = 1, Accion = ProcurementAction.EntregarDeStock, Origen = "SC/Stock/PT", Destino = "Customers", TipoOperacion = "STC-OUT-DIR" }
            }
        }
    };

    public ProcurementRoute? GetRuta(string? codigo) =>
        codigo == null ? null : Rutas.FirstOrDefault(r => r.Codigo == codigo);

    // ------------------------------------------------- asignación en tres niveles

    /// <summary>Nivel 1: por clasificación de producto. Es el que cubre el catálogo completo.</summary>
    public Dictionary<ProductClass, string> RutaPorCategoria { get; } = new()
    {
        [ProductClass.Bolsa] = "MTSO-BOL",
        [ProductClass.RolloImpreso] = "MTSO-IMP",
        [ProductClass.RolloMaestro] = "MTSO-EXT",
        [ProductClass.RolloLiso] = "MTSO-EXT",
        [ProductClass.MateriaPrima] = "MP-STOCK"
    };

    /// <summary>Nivel 2: excepción por producto concreto.</summary>
    public Dictionary<string, string> RutaPorProducto { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Nivel 3: lo que Atención a Clientes elige en la línea del pedido. Manda sobre todo lo demás.</summary>
    public Dictionary<string, string> RutaPorLinea { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Resolución en cascada línea → producto → categoría → predeterminada.
    /// AC orquesta: lo que elija en la línea gana siempre.
    /// </summary>
    public RouteResolution Resolver(string clave, string? claveLinea = null)
    {
        var id = claveLinea ?? clave;
        if (RutaPorLinea.TryGetValue(id, out var porLinea) && GetRuta(porLinea) is { } rl)
            return new(rl, RouteLevel.Linea);

        if (RutaPorProducto.TryGetValue(clave, out var porProd) && GetRuta(porProd) is { } rp)
            return new(rp, RouteLevel.Producto);

        var prod = _inv.GetProducto(clave);
        if (prod != null && RutaPorCategoria.TryGetValue(prod.Clasificacion, out var porCat) && GetRuta(porCat) is { } rc)
            return new(rc, RouteLevel.Categoria);

        return new(Rutas.Last(), RouteLevel.Predeterminada);
    }

    public void AsignarRutaLinea(string clave, string? rutaCodigo)
    {
        if (string.IsNullOrEmpty(rutaCodigo)) RutaPorLinea.Remove(clave);
        else RutaPorLinea[clave] = rutaCodigo;
        Notify();
    }

    // ------------------------------------------------- lista de materiales encadenada

    /// <summary>Qué consume cada producto y en qué proporción. Permite encadenar necesidades hacia atrás.</summary>
    private static readonly Dictionary<string, (string Proceso, (string Clave, decimal Factor)[] Componentes)> Receta =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["PT1113 C567"]  = ("Bolseo",    new[] { ("PT3413 C4235", 0.025m) }),
            ["PT3413 C4235"] = ("Impresión", new[] { ("PT3413 C455", 1.0m) }),
            ["PT3413 C455"]  = ("Extrusión", new[] { ("PACT", 0.68m), ("GA502022", 0.30m), ("MP0032", 0.02m) })
        };

    // ---------------------------------------------------------------- motor

    private int _secuenciaDoc = 0;

    /// <summary>
    /// Ejecuta las reglas para una necesidad y encadena hacia atrás por la receta.
    /// simular = true no reserva nada ni genera folios definitivos: es lo que AC ve antes de autorizar.
    /// </summary>
    public ProcurementPlan Planificar(string clave, decimal cantidad, string pedidoFolio, bool simular = true)
    {
        var plan = new ProcurementPlan { Clave = clave, Solicitado = cantidad, Ejecutado = !simular };
        if (!simular) _secuenciaDoc = 0;
        Resolver(new ProcurementNeed(clave, cantidad, "Customers", 1), plan, pedidoFolio, simular);
        if (!simular) Notify();
        return plan;
    }

    private void Resolver(ProcurementNeed need, ProcurementPlan plan, string pedidoFolio, bool simular)
    {
        if (need.Cantidad <= 0 || need.Nivel > 6) return;

        var resolucion = Resolver(need.Clave);
        var ruta = resolucion.Ruta;
        var producto = _inv.GetProducto(need.Clave);
        var unidad = producto?.Unidad ?? "KGS";
        var restante = need.Cantidad;

        foreach (var regla in ruta.Reglas.OrderBy(r => r.Secuencia))
        {
            if (restante <= 0) break;

            switch (regla.Accion)
            {
                case ProcurementAction.EntregarDeStock:
                {
                    // MTO ignora la existencia: no consume stock, deja todo para la regla de fabricación.
                    if (!ruta.ConsumeStockPrimero) break;

                    var disponible = _inv.Disponible(need.Clave, regla.Origen);
                    var toma = Math.Min(restante, disponible);
                    if (toma <= 0) break;

                    if (!simular) ReservarLotes(need.Clave, regla.Origen, toma, pedidoFolio);

                    plan.CubiertoDeStock += need.Nivel == 1 ? toma : 0;
                    plan.Documentos.Add(NuevoDoc(regla, need, producto, toma, unidad, ruta,
                        need.Nivel == 1 ? "Entrega al cliente desde existencia" : "Reserva de existencia para producción",
                        simular, pedidoFolio));
                    restante -= toma;
                    break;
                }

                case ProcurementAction.TraspasarDeOtraPlanta:
                {
                    var disponible = _inv.Disponible(need.Clave, regla.Origen);
                    var toma = Math.Min(restante, disponible);
                    if (toma <= 0) break;

                    if (!simular) ReservarLotes(need.Clave, regla.Origen, toma, pedidoFolio);

                    plan.PorTraspasar += toma;
                    plan.Documentos.Add(NuevoDoc(regla, need, producto, toma, unidad, ruta,
                        $"Hay existencia en {regla.Origen}: se traslada en vez de fabricar", simular, pedidoFolio));
                    restante -= toma;
                    break;
                }

                case ProcurementAction.Fabricar:
                {
                    plan.PorFabricar += need.Nivel == 1 ? restante : 0;
                    plan.Documentos.Add(NuevoDoc(regla, need, producto, restante, unidad, ruta,
                        "Sin existencia suficiente: se fabrica el faltante", simular, pedidoFolio));

                    // Encadenamiento: la OF vuelve a lanzar necesidades por sus componentes.
                    if (Receta.TryGetValue(need.Clave, out var receta))
                    {
                        foreach (var (compClave, factor) in receta.Componentes)
                            Resolver(new ProcurementNeed(compClave, Math.Round(restante * factor, 2), regla.Origen, need.Nivel + 1),
                                     plan, pedidoFolio, simular);
                    }
                    restante = 0;
                    break;
                }
            }
        }

        // Sin regla que la cubra: la necesidad queda expuesta en lugar de desaparecer en silencio.
        if (restante > 0)
        {
            plan.Documentos.Add(new ProcurementDocument
            {
                Folio = "—",
                Accion = ProcurementAction.EntregarDeStock,
                Clave = need.Clave,
                Producto = producto?.Nombre ?? need.Clave,
                Cantidad = restante,
                Unidad = unidad,
                Origen = "—",
                Destino = need.Ubicacion,
                Nivel = need.Nivel,
                Ruta = ruta.Codigo,
                Detalle = $"Sin cobertura: la ruta {ruta.Codigo} no alcanza a cubrir esta cantidad"
            });
        }
    }

    private ProcurementDocument NuevoDoc(ProcurementRule regla, ProcurementNeed need, ProductRef? producto,
        decimal cantidad, string unidad, ProcurementRoute ruta, string detalle, bool simular, string pedidoFolio)
    {
        _secuenciaDoc++;
        var folio = simular
            ? "(simulado)"
            : regla.Accion switch
            {
                ProcurementAction.Fabricar => $"{ProcesoPrefijo(need.Clave)}-2026-{1000 + _secuenciaDoc}",
                ProcurementAction.TraspasarDeOtraPlanta => $"PIM/OUT/{50000 + _secuenciaDoc}",
                _ => $"SC/OUT/{50000 + _secuenciaDoc}"
            };

        var ui = regla.Accion switch
        {
            ProcurementAction.Fabricar => "/fabricacion",
            ProcurementAction.TraspasarDeOtraPlanta => "/traslados",
            _ => "/entregas"
        };

        return new ProcurementDocument
        {
            Folio = folio,
            Accion = regla.Accion,
            Clave = need.Clave,
            Producto = producto?.Nombre ?? need.Clave,
            Cantidad = cantidad,
            Unidad = unidad,
            Origen = regla.Origen,
            Destino = regla.Destino,
            Nivel = need.Nivel,
            Ruta = ruta.Codigo,
            Detalle = detalle,
            RutaUi = ui
        };
    }

    private static string ProcesoPrefijo(string clave) => clave switch
    {
        "PT1113 C567" => "BOL",
        "PT3413 C4235" => "IMP",
        _ => "EXT"
    };

    private void ReservarLotes(string clave, string ubicacion, decimal cantidad, string pedidoFolio)
    {
        var restante = cantidad;
        foreach (var lote in _inv.LotesDisponibles(clave, ubicacion).OrderBy(l => l.Lote))
        {
            if (restante <= 0) break;
            var toma = Math.Min(restante, lote.Cantidad);
            if (_inv.Reservar(lote.Lote, pedidoFolio, toma)) restante -= toma;
        }
    }
}
