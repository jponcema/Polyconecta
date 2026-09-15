namespace PolyConecta.Contpaq.Resilience;

public enum CircuitState
{
    Closed,
    Open,
    HalfOpen
}

/// <summary>
/// Circuit Breaker resilience policy interface.
/// </summary>
public interface ICircuitBreaker
{
    CircuitState State { get; }
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
}
