namespace PolyConecta.Contpaq.Sdk;

/// <summary>
/// Native SDK Gateway Interface for CONTPAQi Comercial Premium Win32 SDK Interop.
/// </summary>
public interface IContpaqSdkGateway
{
    Task<bool> ConnectAsync(string companyDatabasePath, string user = "SUPERVISOR", string password = "", CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
    Task<int> CreateDocumentAsync(int documentTypeId, object documentHeader, IEnumerable<object> documentLines, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> QueryCatalogAsync<T>(string sqlQuery, CancellationToken cancellationToken = default);
}
