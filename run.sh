#!/bin/bash
# Delegate execution to scripts/run.sh
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
exec "$SCRIPT_DIR/scripts/run.sh" "$@"
