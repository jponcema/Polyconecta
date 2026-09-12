using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Contpaq.Bridge.Core.Models;
using Contpaq.Bridge.Core.Services;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Contpaq.Bridge.Infrastructure.Persistence
{
    public interface ISqlReadRepository
    {
        Task<IEnumerable<ProductCatalogItem>> GetProductsAsync(string? search = null, int limit = 100);
        Task<IEnumerable<ClientCatalogItem>> GetClientsAsync(string? search = null, int limit = 100);
        Task<IEnumerable<WarehouseCatalogItem>> GetWarehousesAsync();
        Task<IEnumerable<ConceptCatalogItem>> GetConceptsAsync();
        Task<ProductStockResponse?> GetProductStockAsync(string codigoProducto, string? codigoAlmacen = null);
        Task<IEnumerable<InvoiceCatalogItem>> GetInvoicesAsync(string? search = null, int limit = 100);
    }

    public class SqlReadRepository : ISqlReadRepository
    {
        private readonly string _connectionString;

        public SqlReadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public async Task<IEnumerable<ProductCatalogItem>> GetProductsAsync(string? search = null, int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sql = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT TOP (@Limit)
                    CIDPRODUCTO AS IdProducto,
                    CCODIGOPRODUCTO AS CodigoProducto,
                    CNOMBREPRODUCTO AS NombreProducto,
                    CTIPOPRODUCTO AS TipoProducto,
                    CCONTROLEXISTENCIA AS ControlExistencia,
                    CSTATUSPRODUCTO AS Status
                FROM admProductos WITH (NOLOCK)
                WHERE (@Search IS NULL OR CCODIGOPRODUCTO LIKE '%' + @Search + '%' OR CNOMBREPRODUCTO LIKE '%' + @Search + '%')
                ORDER BY CCODIGOPRODUCTO;";
            var res = await conn.QueryAsync<ProductCatalogItem>(sql, new { Search = search, Limit = limit });
            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);
            return res;
        }

        public async Task<IEnumerable<ClientCatalogItem>> GetClientsAsync(string? search = null, int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sql = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT TOP (@Limit)
                    CIDCLIENTEPROVEEDOR AS IdCliente,
                    CCODIGOCLIENTE AS CodigoCliente,
                    CRAZONSOCIAL AS RazonSocial,
                    CRFC AS RFC,
                    CTIPOCLIENTE AS TipoCliente
                FROM admClientes WITH (NOLOCK)
                WHERE (@Search IS NULL OR CCODIGOCLIENTE LIKE '%' + @Search + '%' OR CRAZONSOCIAL LIKE '%' + @Search + '%')
                ORDER BY CCODIGOCLIENTE;";
            var res = await conn.QueryAsync<ClientCatalogItem>(sql, new { Search = search, Limit = limit });
            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);
            return res;
        }

        public async Task<IEnumerable<WarehouseCatalogItem>> GetWarehousesAsync()
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sql = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT 
                    CIDALMACEN AS IdAlmacen,
                    CCODIGOALMACEN AS CodigoAlmacen,
                    CNOMBREALMACEN AS NombreAlmacen
                FROM admAlmacenes WITH (NOLOCK)
                ORDER BY CCODIGOALMACEN;";
            var res = await conn.QueryAsync<WarehouseCatalogItem>(sql);
            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);
            return res;
        }

        public async Task<IEnumerable<ConceptCatalogItem>> GetConceptsAsync()
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sql = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT 
                    CIDCONCEPTODOCUMENTO AS IdConcepto,
                    CCODIGOCONCEPTO AS CodigoConcepto,
                    CNOMBRECONCEPTO AS NombreConcepto,
                    CIDDOCUMENTODE AS DocumentoModelo
                FROM admConceptos WITH (NOLOCK)
                ORDER BY CCODIGOCONCEPTO;";
            var res = await conn.QueryAsync<ConceptCatalogItem>(sql);
            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);
            return res;
        }

        public async Task<ProductStockResponse?> GetProductStockAsync(string codigoProducto, string? codigoAlmacen = null)
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sqlLayers = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT 
                    a.CCODIGOALMACEN AS CodigoAlmacen,
                    ISNULL(cp.CNUMEROLOTE, '') AS NumeroLote,
                    CONVERT(varchar, cp.CFECHACADUCIDAD, 23) AS FechaCaducidad,
                    ISNULL(cp.CPEDIMENTO, '') AS Pedimento,
                    cp.CEXISTENCIA AS Existencia
                FROM admCapasProducto cp WITH (NOLOCK)
                JOIN admProductos p WITH (NOLOCK) ON cp.CIDPRODUCTO = p.CIDPRODUCTO
                JOIN admAlmacenes a WITH (NOLOCK) ON cp.CIDALMACEN = a.CIDALMACEN
                WHERE p.CCODIGOPRODUCTO = @CodigoProducto
                  AND (@CodigoAlmacen IS NULL OR a.CCODIGOALMACEN = @CodigoAlmacen)
                  AND cp.CEXISTENCIA > 0;";

            var layers = (await conn.QueryAsync<StockLayerItem>(sqlLayers, new { CodigoProducto = codigoProducto, CodigoAlmacen = codigoAlmacen })).AsList();

            double total = 0;
            foreach (var l in layers) total += l.Existencia;

            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);

            return new ProductStockResponse
            {
                CodigoProducto = codigoProducto,
                TotalExistencia = total,
                Layers = layers
            };
        }

        public async Task<IEnumerable<InvoiceCatalogItem>> GetInvoicesAsync(string? search = null, int limit = 100)
        {
            var sw = Stopwatch.StartNew();
            using var conn = GetConnection();
            const string sql = @"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                SELECT TOP (@Limit)
                    d.CIDDOCUMENTO AS IdDocumento,
                    ISNULL(d.CSERIEDOCUMENTO, '') AS Serie,
                    d.CFOLIO AS Folio,
                    CONVERT(varchar, d.CFECHA, 23) AS Fecha,
                    ISNULL(c.CCODIGOCLIENTE, '') AS CodigoCliente,
                    ISNULL(d.CRAZONSOCIAL, '') AS RazonSocial,
                    ISNULL(d.CRFC, '') AS RFC,
                    d.CNETO AS Neto,
                    d.CIMPUESTO1 AS Impuesto1,
                    d.CTOTAL AS Total,
                    d.CPENDIENTE AS Pendiente,
                    d.CCANCELADO AS Cancelado
                FROM admDocumentos d WITH (NOLOCK)
                LEFT JOIN admClientes c WITH (NOLOCK) ON d.CIDCLIENTEPROVEEDOR = c.CIDCLIENTEPROVEEDOR
                WHERE (@Search IS NULL 
                   OR d.CSERIEDOCUMENTO LIKE '%' + @Search + '%' 
                   OR CAST(d.CFOLIO AS varchar) LIKE '%' + @Search + '%' 
                   OR d.CRAZONSOCIAL LIKE '%' + @Search + '%' 
                   OR d.CRFC LIKE '%' + @Search + '%'
                   OR c.CCODIGOCLIENTE LIKE '%' + @Search + '%')
                ORDER BY d.CFECHA DESC, d.CFOLIO DESC;";

            var res = await conn.QueryAsync<InvoiceCatalogItem>(sql, new { Search = search, Limit = limit });
            sw.Stop();
            PerformanceMetrics.RecordReadQuery(sw.ElapsedMilliseconds);
            return res;
        }
    }
}
