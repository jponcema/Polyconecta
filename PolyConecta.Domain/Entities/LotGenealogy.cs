namespace PolyConecta.Domain.Entities;

public class LotGenealogy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ParentLotNumber { get; set; } = string.Empty;
    public string ChildLotNumber { get; set; } = string.Empty;
    public Guid? SourceRollId { get; set; }
    public Guid? TargetRollId { get; set; }
    public decimal QuantityConsumedKg { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
