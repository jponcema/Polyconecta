#!/bin/bash
# ==============================================================================
# PolyConecta.Contpaq - Build Script for Windows 32-bit (win-x86) Package
# ==============================================================================
# Usage:
#   chmod +x scripts/build.sh
#   ./scripts/build.sh
# ==============================================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

PROJECT_PATH="PolyConecta.Contpaq/PolyConecta.Contpaq.csproj"
OUTPUT_DIR="./dist/win-x86"

echo "================================================================="
echo "🛠️  Building PolyConecta.Contpaq Bridge for Windows 32-bit (win-x86)..."
echo "Project: ${PROJECT_PATH}"
echo "Output Directory: ${OUTPUT_DIR}"
echo "================================================================="

# Step 1: Clean previous output
echo "🧹 Step 1: Cleaning previous build output..."
rm -rf "${OUTPUT_DIR}"
mkdir -p "${OUTPUT_DIR}"

# Step 2: Publish .NET 8 x86 Release package
echo "📦 Step 2: Compiling .NET 8 x86 binaries..."
dotnet publish "${PROJECT_PATH}" \
  -c Release \
  -r win-x86 \
  --self-contained false \
  -o "${OUTPUT_DIR}"

echo "================================================================="
echo "✅ Build Completed Successfully!"
echo "Package Location: ${OUTPUT_DIR}"
echo "Total Artifact Size: $(du -sh ${OUTPUT_DIR} | cut -f1)"
echo "Files Generated: $(ls -1 ${OUTPUT_DIR} | wc -l | xargs)"
echo "================================================================="
