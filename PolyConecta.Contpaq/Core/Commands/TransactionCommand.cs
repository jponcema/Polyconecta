using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contpaq.Bridge.Core.Commands
{
    public class CreateTransactionRequest
    {
        [JsonPropertyName("correlation_id")]
        public string? CorrelationId { get; set; }

        [Required]
        [JsonPropertyName("client_app_id")]
        public string ClientAppId { get; set; } = string.Empty;

        [JsonPropertyName("idempotency_key")]
        public string? IdempotencyKey { get; set; }

        [JsonPropertyName("callback_url")]
        public string? CallbackUrl { get; set; }

        [Required]
        [JsonPropertyName("command_type")]
        public string CommandType { get; set; } = "DOCUMENT_CREATE";

        [Required]
        [JsonPropertyName("payload")]
        public TransactionPayload Payload { get; set; } = new();
    }

    public class TransactionPayload
    {
        [JsonPropertyName("codigo_concepto")]
        public string CodigoConcepto { get; set; } = string.Empty;

        [JsonPropertyName("codigo_cliente_proveedor")]
        public string CodigoClienteProveedor { get; set; } = string.Empty;

        [JsonPropertyName("fecha")]
        public string Fecha { get; set; } = string.Empty;

        [JsonPropertyName("codigo_agente")]
        public string? CodigoAgente { get; set; }

        [JsonPropertyName("referencia")]
        public string? Referencia { get; set; }

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("movimientos")]
        public List<MovementPayload> Movimientos { get; set; } = new();
    }

    public class MovementPayload
    {
        [JsonPropertyName("codigo_producto")]
        public string CodigoProducto { get; set; } = string.Empty;

        [JsonPropertyName("unidades")]
        public double Unidades { get; set; }

        [JsonPropertyName("precio")]
        public double Precio { get; set; }

        [JsonPropertyName("codigo_almacen")]
        public string CodigoAlmacen { get; set; } = string.Empty;

        [JsonPropertyName("lote")]
        public LotPayload? Lote { get; set; }
    }

    public class LotPayload
    {
        [JsonPropertyName("numero_lote")]
        public string NumeroLote { get; set; } = string.Empty;

        [JsonPropertyName("fecha_caducidad")]
        public string? FechaCaducidad { get; set; }

        [JsonPropertyName("pedimento")]
        public string? Pedimento { get; set; }
    }
}
