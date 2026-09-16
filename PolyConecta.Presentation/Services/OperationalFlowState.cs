namespace PolyConecta.Presentation.Services;

/// <summary>
/// Scoped (per-session) shared state for the 7-phase operational demo, so that
/// navigating between real routes (/pedidos, /manufactura, ...) never loses what
/// the user already captured in that browser session.
/// </summary>
public class OperationalFlowState
{
    public string CurrentOrderStage { get; private set; } = "Borrador";
    public int PedidoSubStep { get; private set; } = 1; // 1 = Captura, 2 = Sincronización/Aprobaciones (misma ruta /pedidos)
    public bool SalesApproved { get; private set; }
    public bool CreditApproved { get; private set; }

    public string ExtrusionState { get; private set; } = "Borrador";
    public string AssignedMachine { get; private set; } = "Por programar";
    public bool BomLoaded { get; private set; }

    public bool ClosureExecuted { get; private set; }
    public int TransferStep { get; private set; }
    public bool BaggingCompleted { get; private set; }

    public Dictionary<int, bool> StepCompleted { get; } = new()
    {
        { 1, false }, { 2, false }, { 3, false }, { 4, false }, { 5, false }, { 6, false }, { 7, false }
    };

    public event Action? OnChange;

    public void AdvancePedidoSubStep()
    {
        PedidoSubStep = 2;
        Notify();
    }

    public void SetOrderStage(string stage)
    {
        CurrentOrderStage = stage;
        if (stage == "Autorizado")
        {
            StepCompleted[1] = true;
            StepCompleted[2] = true;
        }
        Notify();
    }

    public void SetSalesApproved(bool value)
    {
        SalesApproved = value;
        TryAuthorize();
        Notify();
    }

    public void SetCreditApproved(bool value)
    {
        CreditApproved = value;
        TryAuthorize();
        Notify();
    }

    private void TryAuthorize()
    {
        if (SalesApproved && CreditApproved && CurrentOrderStage == "Confirmado")
        {
            CurrentOrderStage = "Autorizado";
            StepCompleted[1] = true;
            StepCompleted[2] = true;
        }
    }

    public void ParseBom()
    {
        BomLoaded = true;
        Notify();
    }

    public void AssignExtruder()
    {
        AssignedMachine = "EXT-01 (Extrusora 1)";
        ExtrusionState = "En progreso";
        StepCompleted[3] = true;
        Notify();
    }

    public void MarkStep4Complete()
    {
        StepCompleted[4] = true;
        Notify();
    }

    public void CompleteClosure()
    {
        ClosureExecuted = true;
        StepCompleted[5] = true;
        Notify();
    }

    public void SetTransferStep(int step)
    {
        TransferStep = step;
        if (step >= 2) StepCompleted[6] = true;
        Notify();
    }

    public void CompleteBagging()
    {
        BaggingCompleted = true;
        CurrentOrderStage = "Hecho";
        StepCompleted[7] = true;
        Notify();
    }

    public void ResetAll()
    {
        CurrentOrderStage = "Borrador";
        PedidoSubStep = 1;
        SalesApproved = false;
        CreditApproved = false;
        ExtrusionState = "Borrador";
        AssignedMachine = "Por programar";
        BomLoaded = false;
        ClosureExecuted = false;
        TransferStep = 0;
        BaggingCompleted = false;
        for (int i = 1; i <= 7; i++) StepCompleted[i] = false;
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}
