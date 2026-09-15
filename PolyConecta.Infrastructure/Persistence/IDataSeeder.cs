namespace PolyConecta.Infrastructure.Persistence;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
