using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contpaq.Bridge.Core.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Contpaq.Bridge.Infrastructure.Persistence
{
    public interface IOutboxRepository
    {
        Task<bool> AddTransactionAsync(BridgeTransaction transaction);
        Task<BridgeTransaction?> GetByIdAsync(string transactionId);
        Task<BridgeTransaction?> GetByIdempotencyKeyAsync(string idempotencyKey);
        Task<IEnumerable<BridgeTransaction>> GetPendingTransactionsAsync(int limit = 20);
        Task UpdateStatusAsync(string transactionId, string status, int? docId = null, string? folio = null, int? errorCode = null, string? errorMessage = null);
        Task IncrementRetryAsync(string transactionId, int retryCount, DateTime nextAttemptAt, int? errorCode, string? errorMessage);
        Task AddLogAsync(TransactionLog log);
        Task<IEnumerable<BridgeTransaction>> GetDlqTransactionsAsync();
        Task<bool> DeleteDlqTransactionAsync(string transactionId);
        Task<int> GetQueueDepthAsync();
        Task<int> DeletePendingTransactionsAsync();
        Task<int> PurgeAllTransactionsAsync();
        Task<bool> DeleteTransactionAsync(string transactionId);
    }

    public class OutboxRepository : IOutboxRepository
    {
        private readonly string _connectionString;

        private const string SelectFields = @"
            transaction_id AS TransactionId,
            correlation_id AS CorrelationId,
            client_app_id AS ClientAppId,
            idempotency_key AS IdempotencyKey,
            command_type AS CommandType,
            payload_json AS PayloadJson,
            callback_url AS CallbackUrl,
            status AS Status,
            retry_count AS RetryCount,
            max_retries AS MaxRetries,
            next_attempt_at AS NextAttemptAt,
            contpaqi_doc_id AS ContpaqiDocId,
            contpaqi_folio AS ContpaqiFolio,
            last_error_code AS LastErrorCode,
            last_error_message AS LastErrorMessage,
            created_at AS CreatedAt,
            updated_at AS UpdatedAt";

        public OutboxRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public async Task<bool> AddTransactionAsync(BridgeTransaction transaction)
        {
            using var conn = GetConnection();
            const string sql = @"
                INSERT INTO bridge_transactions (
                    transaction_id, correlation_id, client_app_id, idempotency_key,
                    command_type, payload_json, callback_url, status, retry_count,
                    max_retries, next_attempt_at, created_at, updated_at
                ) VALUES (
                    @TransactionId, @CorrelationId, @ClientAppId, @IdempotencyKey,
                    @CommandType, @PayloadJson, @CallbackUrl, @Status, @RetryCount,
                    @MaxRetries, @NextAttemptAt, @CreatedAt, @UpdatedAt
                );";
            try
            {
                var rows = await conn.ExecuteAsync(sql, transaction);
                return rows > 0;
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
            {
                return false;
            }
        }

        public async Task<BridgeTransaction?> GetByIdAsync(string transactionId)
        {
            using var conn = GetConnection();
            var sql = $"SELECT {SelectFields} FROM bridge_transactions WHERE transaction_id = @TransactionId;";
            return await conn.QuerySingleOrDefaultAsync<BridgeTransaction>(sql, new { TransactionId = transactionId });
        }

        public async Task<BridgeTransaction?> GetByIdempotencyKeyAsync(string idempotencyKey)
        {
            using var conn = GetConnection();
            var sql = $"SELECT {SelectFields} FROM bridge_transactions WHERE idempotency_key = @IdempotencyKey;";
            return await conn.QuerySingleOrDefaultAsync<BridgeTransaction>(sql, new { IdempotencyKey = idempotencyKey });
        }

        public async Task<IEnumerable<BridgeTransaction>> GetPendingTransactionsAsync(int limit = 20)
        {
            using var conn = GetConnection();
            var now = DateTime.UtcNow.ToString("o");
            var sql = $@"
                SELECT {SelectFields} FROM bridge_transactions 
                WHERE status = 'PENDING' AND next_attempt_at <= @Now 
                ORDER BY created_at ASC LIMIT @Limit;";
            return await conn.QueryAsync<BridgeTransaction>(sql, new { Now = now, Limit = limit });
        }

        public async Task UpdateStatusAsync(string transactionId, string status, int? docId = null, string? folio = null, int? errorCode = null, string? errorMessage = null)
        {
            using var conn = GetConnection();
            var now = DateTime.UtcNow.ToString("o");
            const string sql = @"
                UPDATE bridge_transactions 
                SET status = @Status, contpaqi_doc_id = COALESCE(@DocId, contpaqi_doc_id),
                    contpaqi_folio = COALESCE(@Folio, contpaqi_folio),
                    last_error_code = @ErrorCode, last_error_message = @ErrorMessage,
                    updated_at = @Now
                WHERE transaction_id = @TransactionId;";
            await conn.ExecuteAsync(sql, new { TransactionId = transactionId, Status = status, DocId = docId, Folio = folio, ErrorCode = errorCode, ErrorMessage = errorMessage, Now = now });
        }

        public async Task IncrementRetryAsync(string transactionId, int retryCount, DateTime nextAttemptAt, int? errorCode, string? errorMessage)
        {
            using var conn = GetConnection();
            var now = DateTime.UtcNow.ToString("o");
            var nextAttempt = nextAttemptAt.ToString("o");
            var status = retryCount >= 5 ? "DEAD_LETTER_QUEUE" : "PENDING";
            const string sql = @"
                UPDATE bridge_transactions 
                SET retry_count = @RetryCount, status = @Status, next_attempt_at = @NextAttempt,
                    last_error_code = @ErrorCode, last_error_message = @ErrorMessage, updated_at = @Now
                WHERE transaction_id = @TransactionId;";
            await conn.ExecuteAsync(sql, new { TransactionId = transactionId, RetryCount = retryCount, Status = status, NextAttempt = nextAttempt, ErrorCode = errorCode, ErrorMessage = errorMessage, Now = now });
        }

        public async Task AddLogAsync(TransactionLog log)
        {
            using var conn = GetConnection();
            const string sql = @"
                INSERT INTO transaction_logs (
                    log_id, transaction_id, correlation_id, attempt_number,
                    sdk_function_name, sdk_error_code, error_message, duration_ms, timestamp
                ) VALUES (
                    @LogId, @TransactionId, @CorrelationId, @AttemptNumber,
                    @SdkFunctionName, @SdkErrorCode, @ErrorMessage, @DurationMs, @Timestamp
                );";
            await conn.ExecuteAsync(sql, log);
        }

        public async Task<IEnumerable<BridgeTransaction>> GetDlqTransactionsAsync()
        {
            using var conn = GetConnection();
            var sql = $"SELECT {SelectFields} FROM bridge_transactions WHERE status = 'DEAD_LETTER_QUEUE' ORDER BY updated_at DESC;";
            return await conn.QueryAsync<BridgeTransaction>(sql);
        }

        public async Task<bool> DeleteDlqTransactionAsync(string transactionId)
        {
            using var conn = GetConnection();
            const string sql = "DELETE FROM bridge_transactions WHERE transaction_id = @TransactionId AND status = 'DEAD_LETTER_QUEUE';";
            var rows = await conn.ExecuteAsync(sql, new { TransactionId = transactionId });
            return rows > 0;
        }

        public async Task<int> GetQueueDepthAsync()
        {
            using var conn = GetConnection();
            const string sql = "SELECT COUNT(*) FROM bridge_transactions WHERE status IN ('PENDING', 'PROCESSING');";
            return await conn.ExecuteScalarAsync<int>(sql);
        }

        public async Task<int> DeletePendingTransactionsAsync()
        {
            using var conn = GetConnection();
            const string sql = "DELETE FROM bridge_transactions WHERE status IN ('PENDING', 'PROCESSING');";
            return await conn.ExecuteAsync(sql);
        }

        public async Task<int> PurgeAllTransactionsAsync()
        {
            using var conn = GetConnection();
            const string sql = "DELETE FROM bridge_transactions;";
            return await conn.ExecuteAsync(sql);
        }

        public async Task<bool> DeleteTransactionAsync(string transactionId)
        {
            using var conn = GetConnection();
            const string sql = "DELETE FROM bridge_transactions WHERE transaction_id = @TransactionId;";
            var rows = await conn.ExecuteAsync(sql, new { TransactionId = transactionId });
            return rows > 0;
        }
    }
}
