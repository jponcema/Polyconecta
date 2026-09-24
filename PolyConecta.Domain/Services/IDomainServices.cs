using PolyConecta.Domain.Entities;

namespace PolyConecta.Domain.Services;

/// <summary>
/// Domain service for Procure-to-Pay (P2P) goods receipt validations.
/// </summary>
public interface IP2PValidationService
{
    Task<bool> ValidatePurchaseOrderReceiptAsync(string poNumber, string supplierCode, decimal receivedQuantity, CancellationToken cancellationToken = default);
}

/// <summary>
/// Domain service for Order-to-Cash (O2C) 3-digital signatures workflow.
/// <summary>
/// Domain service interface for queuing CONTPAQi ERP transactions.
/// </summary>
public interface IBridgeSyncService
{
    Task<Guid> QueueErpTransactionAsync(string documentType, object payload, CancellationToken cancellationToken = default);
}
