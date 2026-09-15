namespace PolyConecta.Domain.Entities;

public class MasterOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FolioOm { get; set; } = string.Empty; // "OM-2026-0421"
    public int CidDocumentoPedido { get; set; } // CONTPAQi admDocumentos CIDDOCUMENTO
    public string CustomerCode { get; set; } = string.Empty;
    public string PtSku { get; set; } = string.Empty;
    public decimal TargetQuantityKg { get; set; }
    public string Status { get; set; } = "Draft"; // "Draft", "Approved", "In_Progress", "Completed", "Cancelled"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<SubOrder> SubOrders { get; set; } = new();
}
