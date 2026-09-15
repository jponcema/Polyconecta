namespace PolyConecta.Domain.Entities;

public class PolyLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int CidAlmacenContpaq { get; set; }
    public string LocationCode { get; set; } = string.Empty; // e.g. "PIM/Stock/MP"
    public string LocationName { get; set; } = string.Empty;
    public string PlantCode { get; set; } = string.Empty; // "PIM", "STC", "MTM"
    public string WarehouseType { get; set; } = string.Empty; // "RawMaterial", "Production", "FinishedGoods", "Quarantine", "Transit"
    public bool IsActive { get; set; } = true;
}
