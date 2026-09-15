using System.Text.RegularExpressions;

namespace PolyConecta.Domain.ValueObjects;

public record Folio
{
    private static readonly Regex FolioRegex = new(@"^EX-[0-9]{2}-[0-9]{6}-[0-9]{6}$", RegexOptions.Compiled);

    public string Value { get; }

    public Folio(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !FolioRegex.IsMatch(value))
        {
            throw new ArgumentException($"Invalid Rollo Maestro Folio format: '{value}'. Expected format: 'EX-01-YYMMDD-HHMMSS'.", nameof(value));
        }
        Value = value;
    }

    public static Folio Generate(int lineId, DateTime timestamp)
    {
        var folioStr = $"EX-{lineId:D2}-{timestamp:yyMMdd}-{timestamp:HHmmss}";
        return new Folio(folioStr);
    }

    public override string ToString() => Value;
}
