#!/usr/bin/env bash
set -euo pipefail

APP_ID="io.clin.reekta"
MANIFEST="flatpak/${APP_ID}.yml"
BUILD_DIR=".flatpak-builder/build-${APP_ID}"
REPO_DIR=".flatpak-builder/repo"
BUNDLE_FILE="${APP_ID}.flatpak"

if ! command -v flatpak-builder >/dev/null 2>&1; then
  echo "flatpak-builder is not installed." >&2
  echo "Install it (for example on Debian/Ubuntu): sudo apt install flatpak-builder" >&2
  exit 1
fi

flatpak-builder \
  --user \
  --force-clean \
  --install-deps-from=flathub \
  --repo="${REPO_DIR}" \
  "${BUILD_DIR}" \
  "${MANIFEST}"

flatpak build-bundle "${REPO_DIR}" "${BUNDLE_FILE}" "${APP_ID}"

echo "Flatpak build complete."
echo "Bundle: ${BUNDLE_FILE}"
echo "Run (installed app): flatpak run ${APP_ID}"
