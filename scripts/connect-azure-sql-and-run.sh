#!/usr/bin/env bash
set -euo pipefail

# Uso:
# RESOURCE_GROUP="tu-rg" SQL_SERVER="clindata-sql-server" ./scripts/connect-azure-sql-and-run.sh
# Variables opcionales:
# FIREWALL_RULE="clindata-dev-ip"
# API_PROJECT="src/ClinData.API/ClinData.API.csproj"
# APPLY_MIGRATIONS="1"   # aplica migraciones antes de correr

RESOURCE_GROUP="${RESOURCE_GROUP:-}"
SQL_SERVER="${SQL_SERVER:-clindata-sql-server}"
FIREWALL_RULE="${FIREWALL_RULE:-clindata-dev-ip}"
API_PROJECT="${API_PROJECT:-src/ClinData.API/ClinData.API.csproj}"
APPLY_MIGRATIONS="${APPLY_MIGRATIONS:-0}"

if [[ -z "$RESOURCE_GROUP" ]]; then
  echo "ERROR: Debes definir RESOURCE_GROUP." >&2
  echo "Ejemplo: RESOURCE_GROUP='rg-clindata-dev' $0" >&2
  exit 1
fi

for cmd in az curl dotnet; do
  if ! command -v "$cmd" >/dev/null 2>&1; then
    echo "ERROR: No se encontro el comando '$cmd'." >&2
    exit 1
  fi
done

# Verifica login de Azure CLI
if ! az account show >/dev/null 2>&1; then
  echo "No hay sesion de Azure CLI. Ejecuta: az login" >&2
  exit 1
fi

IP="$(curl -s https://api.ipify.org)"
if [[ -z "$IP" ]]; then
  echo "ERROR: No se pudo obtener la IP publica." >&2
  exit 1
fi

echo "IP publica detectada: $IP"
echo "Actualizando firewall de Azure SQL..."
az sql server firewall-rule create \
  --resource-group "$RESOURCE_GROUP" \
  --server "$SQL_SERVER" \
  --name "$FIREWALL_RULE" \
  --start-ip-address "$IP" \
  --end-ip-address "$IP" \
  --output table

echo "Firewall actualizado para $IP"

if [[ "$APPLY_MIGRATIONS" == "1" ]]; then
  echo "Aplicando migraciones..."
  dotnet ef database update \
    --project src/ClinData.Infrastructure \
    --startup-project src/ClinData.API
fi

echo "Levantando API..."
dotnet run --project "$API_PROJECT"
