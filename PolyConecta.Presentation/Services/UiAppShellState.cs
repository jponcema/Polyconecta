namespace PolyConecta.Presentation.Services;

public class UiAppShellState
{
    public string ActiveModule { get; private set; } = "Inicio";
    public string BreadcrumbPath { get; private set; } = "Inicio / Dashboard";
    public bool IsSidebarCollapsed { get; private set; } = false;
    public string? SpotlightSearchQuery { get; private set; }

    public event Action? OnStateChanged;

    public void SetActiveModule(string module, string breadcrumb)
    {
        ActiveModule = module;
        BreadcrumbPath = breadcrumb;
        NotifyStateChanged();
    }

    public void ToggleSidebar()
    {
        IsSidebarCollapsed = !IsSidebarCollapsed;
        NotifyStateChanged();
    }

    public void SetSpotlightSearch(string? query)
    {
        SpotlightSearchQuery = query;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
