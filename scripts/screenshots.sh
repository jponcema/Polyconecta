#!/bin/bash
# ==============================================================================
# PolyConecta - Captura de pantallas de todo el sistema
# ==============================================================================
# Recorre cada pantalla de la SPA y guarda una captura .jpeg numerada en
# docs/screenshots/. Si la app no esta corriendo, la levanta y la apaga al salir.
#
# Uso:
#   ./scripts/screenshots.sh
# ==============================================================================

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

PRESENTATION_PORT="9000"
BASE_URL="http://localhost:${PRESENTATION_PORT}"

echo "================================================================="
echo "📸  PolyConecta - Captura de pantallas"
echo "================================================================="

# --- Dependencias de Node ---
if [ ! -d "$SCRIPT_DIR/screenshots/node_modules" ]; then
    echo "📦 Instalando dependencias de Playwright..."
    (cd "$SCRIPT_DIR/screenshots" && npm install --silent && npx playwright install chromium)
fi

# --- .NET: misma seleccion de instalacion que run.sh ---
has_aspnet8_runtime() {
    "$1" --list-runtimes 2>/dev/null | grep -q '^Microsoft\.AspNetCore\.App 8\.'
}

DOTNET_BIN=""
for candidate in "$(command -v dotnet 2>/dev/null)" \
                 "/usr/local/share/dotnet/dotnet" \
                 "/usr/share/dotnet/dotnet" \
                 "$HOME/.dotnet/dotnet"; do
    if [ -n "$candidate" ] && [ -x "$candidate" ] && has_aspnet8_runtime "$candidate"; then
        DOTNET_BIN="$candidate"
        break
    fi
done

APP_PID=""
cleanup() {
    if [ -n "$APP_PID" ]; then
        echo "🛑 Deteniendo la instancia levantada para las capturas..."
        kill "$APP_PID" 2>/dev/null || true
        pkill -P "$APP_PID" 2>/dev/null || true
    fi
}
trap cleanup EXIT INT TERM

# --- Levantar la app solo si no responde ya ---
if curl -s -o /dev/null --max-time 2 "$BASE_URL"; then
    echo "✅ Usando la instancia que ya responde en ${BASE_URL}"
else
    if [ -z "$DOTNET_BIN" ]; then
        echo "❌ Error: No se encontro una instalacion de .NET con el runtime ASP.NET Core 8.x."
        echo "   Instala el runtime 8 (macOS: brew install --cask dotnet-sdk@8)."
        exit 1
    fi

    echo "🛠️  Compilando PolyConecta.Presentation..."
    "$DOTNET_BIN" build PolyConecta.Presentation/PolyConecta.Presentation.csproj -c Debug --verbosity quiet

    echo "🚀 Levantando la app en ${BASE_URL}..."
    "$DOTNET_BIN" run --project PolyConecta.Presentation/PolyConecta.Presentation.csproj \
        --no-build --urls "$BASE_URL" > /dev/null 2>&1 &
    APP_PID=$!

    for _ in $(seq 1 60); do
        if curl -s -o /dev/null --max-time 2 "$BASE_URL"; then break; fi
        sleep 1
    done

    if ! curl -s -o /dev/null --max-time 2 "$BASE_URL"; then
        echo "❌ Error: la app no respondio en ${BASE_URL}."
        exit 1
    fi
fi

echo "-----------------------------------------------------------------"
BASE_URL="$BASE_URL" node "$SCRIPT_DIR/screenshots/capture.mjs"
echo "================================================================="
