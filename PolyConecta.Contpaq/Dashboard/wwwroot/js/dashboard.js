let throughputChart, latencyChart;
const maxDataPoints = 20;
let lastMetricsTimestamp = 0;

document.addEventListener("DOMContentLoaded", () => {
    initCharts();
    setupSignalR();
    loadRecentLogs();
    refreshDlq();
    fetchMetricsFallback();

    // Dual Fallback Polling Timers (Metrics 5s, DLQ 10s)
    setInterval(fetchMetricsFallback, 5000);
    setInterval(refreshDlq, 10000);
});

function initCharts() {
    if (throughputChart) throughputChart.destroy();
    if (latencyChart) latencyChart.destroy();

    // Chart 1: Operations Throughput (Reads/sec vs Writes/sec)
    const ctxT = document.getElementById('chartThroughput').getContext('2d');
    throughputChart = new Chart(ctxT, {
        type: 'line',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'SQL Reads / sec',
                    data: [],
                    borderColor: '#2563eb',
                    backgroundColor: 'rgba(37, 99, 235, 0.08)',
                    fill: true,
                    tension: 0.1,
                    borderWidth: 2,
                    pointRadius: 2
                },
                {
                    label: 'SDK Writes / sec',
                    data: [],
                    borderColor: '#059669',
                    backgroundColor: 'rgba(5, 150, 105, 0.08)',
                    fill: true,
                    tension: 0.1,
                    borderWidth: 2,
                    pointRadius: 2
                }
            ]
        },
        options: {
            animation: false,
            responsive: true,
            maintainAspectRatio: false,
            interaction: { intersect: false, mode: 'index' },
            scales: {
                x: {
                    grid: { color: '#f1f5f9' },
                    ticks: { color: '#64748b', font: { size: 11 }, maxRotation: 0 }
                },
                y: {
                    beginAtZero: true,
                    grid: { color: '#e2e8f0' },
                    ticks: { color: '#64748b', font: { size: 11 } }
                }
            },
            plugins: {
                legend: { position: 'top', labels: { usePointStyle: true, boxWidth: 8, font: { size: 12 } } }
            }
        }
    });

    // Chart 2: Latency Profile & Queue Depth
    const ctxL = document.getElementById('chartLatency').getContext('2d');
    latencyChart = new Chart(ctxL, {
        type: 'line',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'SQL Read Latency (ms)',
                    data: [],
                    borderColor: '#4f46e5',
                    backgroundColor: 'transparent',
                    tension: 0.1,
                    borderWidth: 2,
                    pointRadius: 2,
                    yAxisID: 'y'
                },
                {
                    label: 'SDK Write Latency (ms)',
                    data: [],
                    borderColor: '#0d9488',
                    backgroundColor: 'transparent',
                    tension: 0.1,
                    borderWidth: 2,
                    pointRadius: 2,
                    yAxisID: 'y'
                },
                {
                    label: 'Outbox Queue Depth',
                    data: [],
                    borderColor: '#d97706',
                    borderDash: [4, 4],
                    backgroundColor: 'transparent',
                    tension: 0.1,
                    borderWidth: 2,
                    pointRadius: 2,
                    yAxisID: 'y1'
                }
            ]
        },
        options: {
            animation: false,
            responsive: true,
            maintainAspectRatio: false,
            interaction: { intersect: false, mode: 'index' },
            scales: {
                x: {
                    grid: { color: '#f1f5f9' },
                    ticks: { color: '#64748b', font: { size: 11 }, maxRotation: 0 }
                },
                y: {
                    type: 'linear',
                    display: true,
                    position: 'left',
                    beginAtZero: true,
                    title: { display: true, text: 'Latency (ms)', color: '#64748b', font: { size: 11 } },
                    grid: { color: '#e2e8f0' },
                    ticks: { color: '#64748b', font: { size: 11 } }
                },
                y1: {
                    type: 'linear',
                    display: true,
                    position: 'right',
                    beginAtZero: true,
                    title: { display: true, text: 'Queue Depth', color: '#d97706', font: { size: 11 } },
                    grid: { drawOnChartArea: false },
                    ticks: { color: '#d97706', font: { size: 11 } }
                }
            },
            plugins: {
                legend: { position: 'top', labels: { usePointStyle: true, boxWidth: 8, font: { size: 12 } } }
            }
        }
    });
}

function processMetricsData(data) {
    if (!data) return;

    // Avoid duplicate chart pushes within the same second
    const now = Date.now();
    const isNewTick = (now - lastMetricsTimestamp) >= 3000;
    if (isNewTick) {
        lastMetricsTimestamp = now;
    }

    // Primary KPI Cards
    document.getElementById('metricReadsRate').innerText = (data.reads_per_sec || 0).toFixed(1);
    document.getElementById('metricWritesRate').innerText = (data.writes_per_sec || 0).toFixed(1);
    document.getElementById('metricQueueDepth').innerText = data.queue_depth || 0;
    document.getElementById('metricQueryLatency').innerText = Math.round(data.avg_query_latency_ms || 0) + " ms";
    document.getElementById('metricSdkLatency').innerText = Math.round(data.avg_sdk_latency_ms || 0) + " ms";

    // Secondary KPI Summary Bar
    document.getElementById('metricTotalReads').innerText = (data.total_reads || 0).toLocaleString();
    document.getElementById('metricTotalWrites').innerText = (data.total_writes || 0).toLocaleString();
    document.getElementById('metricErrorRate').innerText = (data.error_rate_percent || 0).toFixed(1) + " %";

    // System Badges
    updateSystemStatusBadge(true);

    const sdkBadge = document.getElementById('sdkStatusBadge');
    sdkBadge.innerHTML = data.active_sdk_session ? '<i class="bi bi-hdd-rack-fill me-1"></i> SDK Session: Active' : '<i class="bi bi-hdd-rack me-1"></i> SDK Session: Idle';
    sdkBadge.className = data.active_sdk_session ? "badge bg-success-subtle text-success border border-success-subtle badge-pill-custom" : "badge bg-secondary-subtle text-secondary border border-secondary-subtle badge-pill-custom";

    const circuitBadge = document.getElementById('circuitStateBadge');
    circuitBadge.innerHTML = '<i class="bi bi-shield-check me-1"></i> Circuit: ' + (data.circuit_state || 'CLOSED');
    circuitBadge.className = data.circuit_state === "CLOSED" ? "badge bg-primary-subtle text-primary border border-primary-subtle badge-pill-custom" : "badge bg-danger-subtle text-danger border border-danger-subtle badge-pill-custom";

    // Real-Time Charts Update
    if (isNewTick) {
        const timeLabel = new Date().toLocaleTimeString();
        updateMultiDatasetChart(throughputChart, timeLabel, [data.reads_per_sec || 0, data.writes_per_sec || 0]);
        updateMultiDatasetChart(latencyChart, timeLabel, [data.avg_query_latency_ms || 0, data.avg_sdk_latency_ms || 0, data.queue_depth || 0]);
    }
}

function updateSystemStatusBadge(isOnline) {
    const badge = document.getElementById('systemStatusBadge');
    if (!badge) return;
    if (isOnline) {
        badge.innerHTML = '<i class="bi bi-check-circle-fill me-1"></i> System Operational';
        badge.className = "badge bg-success-subtle text-success border border-success-subtle badge-pill-custom";
    } else {
        badge.innerHTML = '<i class="bi bi-exclamation-octagon-fill me-1"></i> Reconnecting...';
        badge.className = "badge bg-warning-subtle text-warning border border-warning-subtle badge-pill-custom";
    }
}

async function fetchMetricsFallback() {
    try {
        const res = await fetch('/api/v1/metrics/snapshot');
        if (!res.ok) return;
        const data = await res.json();
        processMetricsData(data);
    } catch (e) {
        // Fallback polling network error handled gracefully
    }
}

function setupSignalR() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/dashboard")
        .withAutomaticReconnect([0, 2000, 5000, 10000])
        .build();

    connection.on("ReceiveMetricsSnapshot", (data) => {
        processMetricsData(data);
    });

    connection.on("ReceiveLogLine", (entry) => {
        appendConsoleLine(entry);
    });

    connection.onclose(async () => {
        console.warn("SignalR Connection lost. Retrying in 3 seconds...");
        updateSystemStatusBadge(false);
        setTimeout(() => startSignalR(connection), 3000);
    });

    startSignalR(connection);
}

function startSignalR(connection) {
    connection.start()
        .then(() => updateSystemStatusBadge(true))
        .catch(err => {
            console.error("SignalR Connection Error:", err);
            updateSystemStatusBadge(false);
            setTimeout(() => startSignalR(connection), 5000);
        });
}

function updateMultiDatasetChart(chart, label, values) {
    if (!chart || !chart.data) return;

    if (chart.data.labels.length >= maxDataPoints) {
        chart.data.labels.shift();
        chart.data.datasets.forEach(dataset => dataset.data.shift());
    }

    chart.data.labels.push(label);
    chart.data.datasets.forEach((dataset, index) => {
        dataset.data.push(values[index] ?? 0);
    });

    // Mode 'none' disables animation completely, preventing memory leak
    chart.update('none');
}

async function refreshDlq() {
    try {
        const res = await fetch('/api/v1/dlq');
        if (!res.ok) return;
        const items = await res.json();
        const tbody = document.getElementById('dlqTableBody');
        tbody.innerHTML = '';

        if (items.length === 0) {
            tbody.innerHTML = '<tr><td colspan="6" class="text-center text-muted py-4"><i class="bi bi-check-circle text-success me-2"></i>No DLQ items found. System operational.</td></tr>';
            return;
        }

        items.forEach(item => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td><code class="text-dark bg-light px-2 py-1 border rounded">${item.transaction_id}</code></td>
                <td><span class="fw-medium">${item.client_app_id}</span></td>
                <td><span class="badge bg-light text-dark border font-monospace">${item.command_type}</span></td>
                <td><span class="badge bg-danger-subtle text-danger border border-danger-subtle">${item.retry_count} / ${item.max_retries}</span></td>
                <td class="text-truncate text-secondary" style="max-width: 250px;" title="${item.last_error_message || ''}">${item.last_error_message || 'N/A'}</td>
                <td class="text-end">
                    <button class="btn btn-sm btn-outline-success me-1 shadow-sm" onclick="retryItem('${item.transaction_id}')"><i class="bi bi-arrow-repeat me-1"></i>Retry</button>
                    <button class="btn btn-sm btn-outline-warning me-1 shadow-sm" onclick="openEditModal('${item.transaction_id}', '${encodeURIComponent(item.payload_json)}')"><i class="bi bi-pencil me-1"></i>Edit</button>
                    <button class="btn btn-sm btn-outline-danger shadow-sm" onclick="purgeItem('${item.transaction_id}')"><i class="bi bi-trash me-1"></i>Purge</button>
                </td>
            `;
            tbody.appendChild(tr);
        });
    } catch (e) {
        console.error("Failed to load DLQ:", e);
    }
}

async function retryItem(id) {
    await fetch(`/api/v1/dlq/${id}/retry`, { method: 'POST' });
    refreshDlq();
}

async function purgeItem(id) {
    if (!confirm("Are you sure you want to purge this transaction from DLQ?")) return;
    await fetch(`/api/v1/dlq/${id}`, { method: 'DELETE' });
    refreshDlq();
}

async function purgePendingTransactions() {
    if (!confirm("Are you sure you want to purge all PENDING transactions from the Outbox queue?")) return;
    try {
        const res = await fetch('/api/v1/transactions/pending', { method: 'DELETE' });
        const data = await res.json();
        alert(data.message || "Pending transactions purged");
        refreshDlq();
    } catch (e) {
        alert("Failed to purge pending transactions: " + e.message);
    }
}

async function purgeAllTransactions() {
    if (!confirm("WARNING: Are you sure you want to purge ALL transactions (pending, completed, failed) from the Outbox database?")) return;
    try {
        const res = await fetch('/api/v1/transactions/purge-all', { method: 'DELETE' });
        const data = await res.json();
        alert(data.message || "All transactions purged");
        refreshDlq();
    } catch (e) {
        alert("Failed to purge all transactions: " + e.message);
    }
}

function openEditModal(id, encodedJson) {
    document.getElementById('editTxId').value = id;
    try {
        const parsed = JSON.parse(decodeURIComponent(encodedJson));
        document.getElementById('editJsonText').value = JSON.stringify(parsed, null, 2);
    } catch {
        document.getElementById('editJsonText').value = decodeURIComponent(encodedJson);
    }
    new bootstrap.Modal(document.getElementById('editModal')).show();
}

async function submitEditAndRetry() {
    const id = document.getElementById('editTxId').value;
    const jsonStr = document.getElementById('editJsonText').value;
    try {
        const payloadObj = JSON.parse(jsonStr);
        await fetch(`/api/v1/dlq/${id}/edit-and-retry`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ payload: payloadObj })
        });
        bootstrap.Modal.getInstance(document.getElementById('editModal')).hide();
        refreshDlq();
    } catch (e) {
        alert("Invalid JSON payload format");
    }
}

async function loadRecentLogs() {
    try {
        const res = await fetch('/api/v1/logs/recent?limit=100');
        if (!res.ok) return;
        const logs = await res.json();
        const term = document.getElementById('consoleTerminal');
        if (!term) return;
        term.innerHTML = '';

        if (!logs || logs.length === 0) {
            term.innerHTML = '<div class="text-secondary py-2"><i class="bi bi-info-circle me-1"></i> Log buffer initialized. Waiting for log output...</div>';
            return;
        }

        logs.forEach(entry => {
            appendConsoleLine(entry, false);
        });

        scrollConsoleToBottom();
    } catch (e) {
        console.error("Failed to load recent logs:", e);
    }
}

function appendConsoleLine(entry, shouldScroll = true) {
    const term = document.getElementById('consoleTerminal');
    if (!term) return;

    while (term.children.length >= 300) {
        term.removeChild(term.firstChild);
    }

    const div = document.createElement('div');
    div.className = "console-line py-1 font-monospace border-bottom border-dark border-opacity-40";
    div.style.fontSize = "0.8125rem";
    div.style.fontFamily = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, monospace";

    const lvl = (entry.level || 'INFO').toUpperCase();
    let badgeHtml = '<span class="badge bg-info-subtle text-info border border-info-subtle font-monospace px-1.5 py-0.5 me-2" style="font-size:0.675rem; min-width: 44px; text-align:center; display:inline-block;">INF</span>';

    if (lvl.includes("ERR") || lvl.includes("FATAL")) {
        badgeHtml = '<span class="badge bg-danger-subtle text-danger border border-danger-subtle font-monospace px-1.5 py-0.5 me-2" style="font-size:0.675rem; min-width: 44px; text-align:center; display:inline-block;">ERR</span>';
    } else if (lvl.includes("WRN") || lvl.includes("WARN")) {
        badgeHtml = '<span class="badge bg-warning-subtle text-warning border border-warning-subtle font-monospace px-1.5 py-0.5 me-2" style="font-size:0.675rem; min-width: 44px; text-align:center; display:inline-block;">WRN</span>';
    } else if (lvl.includes("DBG") || lvl.includes("DEBUG")) {
        badgeHtml = '<span class="badge font-monospace px-1.5 py-0.5 me-2" style="font-size:0.675rem; min-width: 44px; text-align:center; display:inline-block; color:#c084fc; background:rgba(192,132,252,0.12); border:1px solid rgba(192,132,252,0.25);">DBG</span>';
    }

    const timeSpan = `<span class="me-2" style="color: #64748b; font-size: 0.775rem;">${entry.timestamp || ''}</span>`;
    const ctxSpan = (entry.source_context || entry.sourceContext) ? `<span class="me-2" style="color: #94a3b8; font-size: 0.775rem;">[${escapeHtml(entry.source_context || entry.sourceContext)}]</span>` : '';

    let msgColor = "#f8fafc";
    if (lvl.includes("ERR") || lvl.includes("FATAL")) msgColor = "#fca5a5";
    else if (lvl.includes("WRN") || lvl.includes("WARN")) msgColor = "#fde047";

    const msgSpan = `<span style="color: ${msgColor}; word-break: break-word;">${escapeHtml(entry.message || '')}</span>`;
    const exDiv = entry.exception ? `<div class="text-danger small ps-3 ms-4 border-start border-danger mt-1" style="white-space: pre-wrap; font-size: 0.775rem;">${escapeHtml(entry.exception)}</div>` : '';

    div.innerHTML = `<div class="d-flex align-items-baseline flex-wrap">${timeSpan}${badgeHtml}${ctxSpan}${msgSpan}</div>${exDiv}`;
    term.appendChild(div);

    if (shouldScroll) {
        const toggle = document.getElementById('autoScrollToggle');
        if (!toggle || toggle.checked) {
            scrollConsoleToBottom();
        }
    }
}

function scrollConsoleToBottom() {
    const term = document.getElementById('consoleTerminal');
    if (term) {
        term.scrollTop = term.scrollHeight;
    }
}

function clearConsoleTerminal() {
    const term = document.getElementById('consoleTerminal');
    if (term) {
        term.innerHTML = '<div class="text-secondary py-2"><i class="bi bi-eraser me-1"></i> Console buffer cleared.</div>';
    }
}

function copyConsoleTerminal() {
    const term = document.getElementById('consoleTerminal');
    if (!term) return;
    const text = term.innerText;
    navigator.clipboard.writeText(text).then(() => {
        alert("Console logs copied to clipboard!");
    }).catch(() => {
        alert("Failed to copy console logs");
    });
}

function escapeHtml(str) {
    if (!str) return '';
    return str.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#039;");
}
