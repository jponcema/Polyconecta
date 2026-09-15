using System;
using System.Collections.Generic;

namespace Contpaq.Bridge.Core.Models
{
    public class ProductCatalogItem
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int TipoProducto { get; set; }
        public int ControlExistencia { get; set; }
        public int Status { get; set; }
    }

    public class ClientCatalogItem
    {
        public int IdCliente { get; set; }
        public string CodigoCliente { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public int TipoCliente { get; set; }
    }

    public class WarehouseCatalogItem
    {
        public int IdAlmacen { get; set; }
        public string CodigoAlmacen { get; set; } = string.Empty;
        public string NombreAlmacen { get; set; } = string.Empty;
    }

    public class ConceptCatalogItem
    {
        public int IdConcepto { get; set; }
        public string CodigoConcepto { get; set; } = string.Empty;
        public string NombreConcepto { get; set; } = string.Empty;
        public int DocumentoModelo { get; set; }
    }

    public class StockLayerItem
    {
        public string CodigoAlmacen { get; set; } = string.Empty;
        public string NumeroLote { get; set; } = string.Empty;
        public string? FechaCaducidad { get; set; }
        public string? Pedimento { get; set; }
        public double Existencia { get; set; }
    }

    public class ProductStockResponse
    {
        public string CodigoProducto { get; set; } = string.Empty;
        public double TotalExistencia { get; set; }
        public List<StockLayerItem> Layers { get; set; } = new();
    }

    public class InvoiceCatalogItem
    {
        public int IdDocumento { get; set; }
        public string Serie { get; set; } = string.Empty;
        public double Folio { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public double Neto { get; set; }
        public double Impuesto1 { get; set; }
        public double Total { get; set; }
        public double Pendiente { get; set; }
        public int Cancelado { get; set; }
    }
}
