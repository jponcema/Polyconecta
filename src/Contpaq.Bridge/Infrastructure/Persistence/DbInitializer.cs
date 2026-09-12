using System;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Contpaq.Bridge.Infrastructure.Persistence
{
    public class DbInitializer
    {
        private readonly string _connectionString;

        public DbInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            const string sql = @"
                CREATE TABLE IF NOT EXISTS bridge_transactions (
                    transaction_id TEXT PRIMARY KEY,
                    correlation_id TEXT NOT NULL,
                    client_app_id TEXT NOT NULL,
                    idempotency_key TEXT UNIQUE,
                    command_type TEXT NOT NULL,
                    payload_json TEXT NOT NULL,
                    callback_url TEXT,
                    status TEXT NOT NULL,
                    retry_count INTEGER NOT NULL DEFAULT 0,
                    max_retries INTEGER NOT NULL DEFAULT 5,
                    next_attempt_at TEXT NOT NULL,
                    contpaqi_doc_id INTEGER,
                    contpaqi_folio TEXT,
                    last_error_code INTEGER,
                    last_error_message TEXT,
                    created_at TEXT NOT NULL,
                    updated_at TEXT NOT NULL
                );

                CREATE INDEX IF NOT EXISTS idx_transactions_status_next ON bridge_transactions(status, next_attempt_at);
                CREATE INDEX IF NOT EXISTS idx_transactions_correlation ON bridge_transactions(correlation_id);
                CREATE INDEX IF NOT EXISTS idx_transactions_idempotency ON bridge_transactions(idempotency_key);

                CREATE TABLE IF NOT EXISTS transaction_logs (
                    log_id TEXT PRIMARY KEY,
                    transaction_id TEXT NOT NULL,
                    correlation_id TEXT NOT NULL,
                    attempt_number INTEGER NOT NULL,
                    sdk_function_name TEXT NOT NULL,
                    sdk_error_code INTEGER NOT NULL,
                    error_message TEXT,
                    duration_ms INTEGER NOT NULL,
                    timestamp TEXT NOT NULL,
                    FOREIGN KEY(transaction_id) REFERENCES bridge_transactions(transaction_id)
                );

                CREATE INDEX IF NOT EXISTS idx_logs_transaction ON transaction_logs(transaction_id);

                CREATE TABLE IF NOT EXISTS webhook_deliveries (
                    delivery_id TEXT PRIMARY KEY,
                    transaction_id TEXT NOT NULL,
                    callback_url TEXT NOT NULL,
                    http_status INTEGER,
                    response_body TEXT,
                    attempt_count INTEGER NOT NULL DEFAULT 1,
                    delivered_at TEXT,
                    next_retry_at TEXT,
                    FOREIGN KEY(transaction_id) REFERENCES bridge_transactions(transaction_id)
                );

                CREATE TABLE IF NOT EXISTS metric_snapshots (
                    snapshot_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    timestamp TEXT NOT NULL,
                    throughput_ops_sec REAL NOT NULL,
                    queue_depth INTEGER NOT NULL,
                    average_sdk_latency_ms REAL NOT NULL,
                    error_rate_percent REAL NOT NULL,
                    active_sdk_session INTEGER NOT NULL,
                    circuit_state TEXT NOT NULL
                );

                CREATE INDEX IF NOT EXISTS idx_metrics_timestamp ON metric_snapshots(timestamp);
            ";

            connection.Execute(sql);
        }
    }
}
