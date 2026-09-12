#!/bin/bash
# ==============================================================================
# Contpaq.Bridge - VPS Automated Deployment Script (Mac to Windows VPS)
# ==============================================================================
# Usage:
#   ./scripts/deploy.sh
# ==============================================================================

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# Native system binaries to bypass terminal wrappers (Warp, zsh aliases)
SSH_BIN="/usr/bin/ssh"
SCP_BIN="/usr/bin/scp"
ZIP_BIN="/usr/bin/zip"

# Configuration Constants
readonly SSH_TARGET="vps-innatos"
readonly TARGET_DIR="C:/inetpub/wwwroot/ContpaqBridge"
readonly APP_POOL="ContpaqBridgeAppPool"
readonly PORT="5005"
readonly DIST_DIR="./dist/win-x86"
readonly ZIP_FILE="./dist/publish.zip"

echo "================================================================="
echo "🚀 Starting Automated Deployment for Contpaq.Bridge"
echo "SSH Target: ${SSH_TARGET}"
echo "Target Folder: ${TARGET_DIR}"
echo "IIS AppPool: ${APP_POOL}"
echo "================================================================="

# 1. Clean & Build .NET 8 32-bit (x86) Release Package
echo "📦 Step 1: Compiling .NET 8 x86 Release package via build.sh..."
"$SCRIPT_DIR/build.sh"

# 2. Compress output into a single ZIP archive
echo "🗜️  Step 2: Compressing binaries into $ZIP_FILE..."
rm -f "$ZIP_FILE"
(cd "$DIST_DIR" && "$ZIP_BIN" -r -q "../../$ZIP_FILE" .)

echo "✅ Archive created: $(du -sh $ZIP_FILE | cut -f1)"

# 3. Stop IIS Application Pool on Windows VPS & terminate worker process locks
echo "⏸️  Step 3: Stopping IIS Application Pool '$APP_POOL' and killing Contpaq.Bridge.exe..."
"$SSH_BIN" "$SSH_TARGET" "powershell -Command \"Import-Module WebAdministration; Stop-WebAppPool -Name '$APP_POOL' -ErrorAction SilentlyContinue; Stop-Website -Name 'CONTPAQ.BRIDGE' -ErrorAction SilentlyContinue; Get-Process Contpaq.Bridge -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue; Get-Process w3wp -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue\"" || true

# Brief pause to allow IIS worker process (w3wp.exe) to release DLL/file locks
sleep 2

# 4. Upload ZIP Archive via SCP
echo "📤 Step 4: Uploading ZIP archive to VPS..."
"$SCP_BIN" "$ZIP_FILE" "${SSH_TARGET}:${TARGET_DIR}/publish.zip"

# 5. Extract ZIP Archive and Cleanup on Windows VPS via PowerShell
echo "📂 Step 5: Extracting archive on VPS and cleaning temporary ZIP..."
"$SSH_BIN" "$SSH_TARGET" "Expand-Archive -Path '${TARGET_DIR}/publish.zip' -DestinationPath '${TARGET_DIR}' -Force; Remove-Item '${TARGET_DIR}/publish.zip' -Force"

# 6. Start IIS Application Pool on Windows VPS via PowerShell
echo "▶️  Step 6: Starting IIS Application Pool '$APP_POOL'..."
"$SSH_BIN" "$SSH_TARGET" "Import-Module WebAdministration; Start-Sleep -Seconds 1; Start-WebAppPool -Name '$APP_POOL'"

# 7. Verify Health Check
echo "🔍 Step 7: Validating deployment health..."
sleep 3

HOST_IP=$(echo "$SSH_TARGET" | sed -E 's/.*@//')
curl -s --connect-timeout 5 "http://${HOST_IP}:${PORT}/health" || echo "ℹ️ Note: If port ${PORT} is internal/firewalled, test via VPS browser at http://localhost:${PORT}/health"

echo "================================================================="
echo "🎉 Contpaq.Bridge Deployed Successfully to ${SSH_TARGET}!"
echo "================================================================="
