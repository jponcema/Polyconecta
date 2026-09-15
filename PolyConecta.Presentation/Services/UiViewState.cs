namespace PolyConecta.Presentation.Services;

public class UiViewState
{
    public string DocumentType { get; private set; } = "Pedido";
    public string ViewMode { get; private set; } = "List"; // "List" or "Kanban"
    public string SearchQuery { get; private set; } = string.Empty;
    public List<string> PreconfiguredStages { get; private set; } = new() { "Pedido Sincronizado", "Confirmado", "En manufactura", "Hecho" };

    public event Action? OnStateChanged;

    public void SetViewMode(string mode)
    {
        ViewMode = mode;
        NotifyStateChanged();
    }

    public void SetDocumentType(string docType, List<string> stages)
    {
        DocumentType = docType;
        PreconfiguredStages = stages;
        NotifyStateChanged();
    }

    public void SetSearchQuery(string query)
    {
        SearchQuery = query;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
