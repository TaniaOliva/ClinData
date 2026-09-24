#!/usr/bin/env bash
set -euo pipefail

# Uso:
#   ./scripts/deploy-api.sh
# Variables opcionales:
#   RG=rg-clindata
#   APP=clindata-api-20260923215645
#   PROJECT=src/ClinData.API/ClinData.API.csproj

RG="${RG:-rg-clindata}"
APP="${APP:-clindata-api-20260923215645}"
PROJECT="${PROJECT:-src/ClinData.API/ClinData.API.csproj}"
PUBLISH_DIR="publish"
ZIP_FILE="api.zip"

for cmd in dotnet az zip curl; do
  if ! command -v "$cmd" >/dev/null 2>&1; then
    echo "ERROR: No se encontro el comando '$cmd'." >&2
    exit 1
  fi
done

if [[ ! -f "$PROJECT" ]]; then
  echo "ERROR: No se encontro el proyecto '$PROJECT'." >&2
  exit 1
fi

echo "==> 1/5 Build"
dotnet build "$PROJECT"

echo "==> 2/5 Publish"
rm -rf "$PUBLISH_DIR" "$ZIP_FILE"
dotnet publish "$PROJECT" -c Release -o "$PUBLISH_DIR"

echo "==> 3/5 Empaquetar zip"
(
  cd "$PUBLISH_DIR"
  zip -r "../$ZIP_FILE" . >/dev/null
)

echo "==> 4/5 Deploy a Azure App Service"
az webapp deploy \
  --resource-group "$RG" \
  --name "$APP" \
  --src-path "$ZIP_FILE" \
  --type zip \
  --track-status true

echo "==> 5/5 Verificacion"
echo "Root (/):"
curl -s -i "https://$APP.azurewebsites.net/" | sed -n '1,20p'
echo
echo "API (/api/pacientes):"
curl -s -i "https://$APP.azurewebsites.net/api/pacientes" | sed -n '1,20p'

echo
echo "Deploy completado para $APP"