using System.Text.Json.Serialization;

namespace Contpaq.Bridge.Core.Models
{
    public class ConsoleLogEntry
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public string Level { get; set; } = "INFO";

        [JsonPropertyName("source_context")]
        public string SourceContext { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("exception")]
        public string? Exception { get; set; }
    }
}
