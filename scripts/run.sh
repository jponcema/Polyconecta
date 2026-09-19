#!/bin/bash
# ==============================================================================
# PolyConecta - Solution Build, Test & Multi-Layer Launch Script
# ==============================================================================
# Usage:
#   ./run.sh                  # Inicia SPA (9000) + API Gateway (9020)
#   ./run.sh --with-bridge    # Inicia SPA (9000) + API Gateway (9020) + CONTPAQi Bridge (5005)
# ==============================================================================

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

PRESENTATION_PORT="9000"
API_PORT="9020"
BRIDGE_PORT="5005"
WITH_BRIDGE=false

for arg in "$@"; do
    case $arg in
        --with-bridge|--bridge)
            WITH_BRIDGE=true
            ;;
        [0-9]*)
            API_PORT="$arg"
            ;;
    esac
done

echo "================================================================="
echo "🚀  PolyConecta Operational Suite - Build, Test & Run"
echo "================================================================="
echo "Repository Root: ${REPO_ROOT}"
echo "PolyConecta Web Presentation Port    : ${PRESENTATION_PORT}"
echo "Swagger & REST API (PolyConecta.Api) : ${API_PORT}"
if [ "$WITH_BRIDGE" = true ]; then
    echo "CONTPAQi Bridge Port                : ${BRIDGE_PORT} (Activo)"
else
    echo "CONTPAQi Bridge Mode                : Desactivado (Usa --with-bridge para arrancar)"
fi
echo "================================================================="

# Step 1: Check .NET 8 SDK
echo "🔍 Step 1: Checking .NET SDK environment..."
if ! command -v dotnet &> /dev/null; then
    echo "❌ Error: .NET SDK is not installed or not in PATH."
    exit 1
fi
echo "   Found .NET SDK version: $(dotnet --version)"

# Step 2: Build complete solution
echo "🛠️  Step 2: Building full solution (PolyConecta.slnx)..."
dotnet build PolyConecta.slnx -c Debug

# Step 3: Run test suites
echo "🧪 Step 3: Running domain unit & integration test suites..."
dotnet test PolyConecta.slnx --no-build --verbosity quiet

echo "================================================================="
echo "✅ Build & Tests Succeeded! Launching PolyConecta Solution Layers..."
echo "================================================================="
echo "💻 PolyConecta Web Presentation   : http://localhost:${PRESENTATION_PORT}"
echo "📚 Swagger API (PolyConecta.Api)  : http://localhost:${API_PORT}/swagger"
echo "⚙️  REST API Endpoints Base       : http://localhost:${API_PORT}/api/v1"

PIDS=()
cleanup() {
    echo ""
    echo "🛑 Shutting down PolyConecta processes..."
    for pid in "${PIDS[@]}"; do
        kill "$pid" 2>/dev/null || true
    done
}
trap cleanup EXIT INT TERM

if [ "$WITH_BRIDGE" = true ]; then
    echo "🌉 CONTPAQi Bridge Worker         : http://localhost:${BRIDGE_PORT}"
    echo "-----------------------------------------------------------------"
    echo "Iniciando servicio CONTPAQi Bridge en segundo plano (Puerto ${BRIDGE_PORT})..."
    dotnet run --project PolyConecta.Contpaq/PolyConecta.Contpaq.csproj --no-build --urls "http://localhost:${BRIDGE_PORT}" &
    PIDS+=($!)
fi

echo "-----------------------------------------------------------------"
echo "Iniciando PolyConecta Web Presentation en segundo plano (Puerto ${PRESENTATION_PORT})..."
dotnet run --project PolyConecta.Presentation/PolyConecta.Presentation.csproj --no-build --urls "http://localhost:${PRESENTATION_PORT}" &
PIDS+=($!)

echo "================================================================="
echo "Press Ctrl+C to stop the servers."
echo "================================================================="

# Step 4: Run PolyConecta.Api server in foreground
dotnet run --project PolyConecta.Api/PolyConecta.Api.csproj --no-build --urls "http://localhost:${API_PORT}"
