# ClinData 
Version del ASP.NET Core 11.0.100-preview.6.26359.118
8.0.419 [/usr/local/share/dotnet/sdk]
10.0.103 [/usr/local/share/dotnet/sdk]
11.0.100-preview.6.26359.118 [/usr/local/share/dotnet/sdk]

# Crearemos la solucion 
dotnet new sln -n ClinData

# Crearemos las 4 Capas  
Domain
Application
Infrastructure
API

dotnet new classlib -n ClinData.Domain
dotnet new classlib -n ClinData.Application
dotnet new classlib -n ClinData.Infrastructure
dotnet new webapi --use-controllers -n ClinData.API

# Agregaremos los proyectos a la Solucion

dotnet sln ClinData.slnx add src/ClinData.Domain/ClinData.Domain.csproj
dotnet sln ClinData.slnx add src/ClinData.Application/ClinData.Application.csproj
dotnet sln ClinData.slnx add src/ClinData.Application/ClinData.Application.csproj
dotnet sln ClinData.slnx add src/ClinData.API/ClinData.API.csproj

#### Configuracion de las Referencias ####

# Aplicacion hace Referencia a Domain
dotnet add src/ClinData.Application/ClinData.Application.csproj reference src/ClinData.Domain/ClinData.Domain.csproj

# Infraestructura hace Referencia a Domain
dotnet add src/ClinData.Infrastructure/ClinData.Infrastructure.csproj reference src/ClinData.Domain/ClinData.Domain.csproj

# Infraestructura hace Referencia a Aplicaicon
dotnet add src/ClinData.Infrastructure/ClinData.Infrastructure.csproj reference src/ClinData.Application/ClinData.Application.csproj

# API hace referencia a Aplicacion
dotnet add src/ClinData.API/ClinData.API.csproj reference src/ClinData.Application/ClinData.Application.csproj

# API hace Referencia a Infraestructure
dotnet add src/ClinData.API/ClinData.API.csproj reference src/ClinData.Infrastructure/ClinData.Infrastructure.csproj

# Para Manejo de Secretos Locales
dotnet user-secrets init --project src/ClinData.API

# Para hacer conexcion a Azur Data base con secrets 
dotnet user-secrets set "ConnectionStrings:ClinData" "Server=tcp:clindata-sql-server.database.windows.net,1433;Initial Catalog=ClinDataDb;Persist Security Info=False;User ID=dbclindata;Password=TU_PASSWORD_REAL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" --project src/ClinData.API

# Comprobando que se Guardo 
dotnet user-secrets list --project src/ClinData.API

# Verificacion de Paquetes intalados 
dotnet list src/ClinData.Infrastructure package

## Onboarding rapido para cualquier integrante del repo

Objetivo: que cualquier persona del equipo pueda levantar la API y probar endpoints contra Azure SQL.

### 1. Prerrequisitos

- Tener .NET SDK instalado (idealmente net10 para este proyecto).
- Tener acceso al repositorio.
- Tener connection string de Azure SQL valida.
- Tener su IP habilitada en el firewall del SQL Server de Azure.

### 2. Restaurar y compilar

```bash
cd '/Users/salyluz/Desktop/Universidad/AV #2/ClinData'
dotnet restore
dotnet build
```

### 3. Configurar secretos locales (no guardar password en el repo)

```bash
dotnet user-secrets init --project src/ClinData.API
dotnet user-secrets set "ConnectionStrings:ClinData" "Server=tcp:clindata-sql-server.database.windows.net,1433;Initial Catalog=ClinDataDb;Persist Security Info=False;User ID=dbclindata;Password=TU_PASSWORD_REAL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" --project src/ClinData.API
dotnet user-secrets list --project src/ClinData.API
```

Nota: la clave correcta es `ConnectionStrings:ClinData`.

### 4. Habilitar IP en Azure SQL Server

Si aparece error `Error Number:40615` o mensaje `Client with IP address ... is not allowed`:

1. Ir a Azure Portal -> SQL Server `clindata-sql-server`.
2. Abrir `Networking`.
3. Clic en `Add client IPv4 address`.
4. Guardar y esperar 1-5 minutos.

### 5. Aplicar migraciones (crear tablas en Azure)

```bash
dotnet ef database update --project src/ClinData.Infrastructure --startup-project src/ClinData.API
```

Si no existe `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

### 6. Ejecutar API

```bash
dotnet run --project src/ClinData.API/ClinData.API.csproj
```

### 7. Prueba minima de endpoints

1. Listar pacientes:

```bash
curl -i http://localhost:5189/api/pacientes
```

2. Crear paciente:

```bash
curl -i -X POST http://localhost:5189/api/pacientes \
	-H "Content-Type: application/json" \
	-d '{
		"nombres":"Pedro",
		"apellidos":"Lopez",
		"sexo":"M",
		"identidad":"0801-2000-12345",
		"fechaNacimiento":"2000-05-10T00:00:00",
		"telefono":"99999999"
	}'
```

3. Volver a listar pacientes para confirmar persistencia en Azure SQL.

### 8. Recomendacion para trabajo en equipo

- No subir cadenas de conexion con password al repo.
- Cada integrante configura su secreto local.
- Si alguien cambia de red, debe re-autorizar su IP en Azure SQL.

### 9. Conexion a Azure SQL sin dolor por IP dinamica

Se agrego el script `scripts/connect-azure-sql-and-run.sh` para automatizar:

1. Detectar tu IP publica actual.
2. Actualizar la regla de firewall en Azure SQL.
3. Levantar la API.

Uso:

```bash
cd '/Users/salyluz/Desktop/Universidad/AV #2/ClinData'
RESOURCE_GROUP='TU_RESOURCE_GROUP' ./scripts/connect-azure-sql-and-run.sh
```

Opcional (si quieres aplicar migraciones antes de correr):

```bash
cd '/Users/salyluz/Desktop/Universidad/AV #2/ClinData'
RESOURCE_GROUP='TU_RESOURCE_GROUP' APPLY_MIGRATIONS='1' ./scripts/connect-azure-sql-and-run.sh
```

Requisitos:

- Azure CLI instalado.
- Sesion iniciada con `az login`.
- User secret configurado con la clave correcta `ConnectionStrings:ClinData`.

# para descargar el .zip y hacer deploy

az webapp deploy --resource-group rg-clindata --name clindata-api-20260923215645 --src-path api.zip --type zip --track-status true

# Comando par ajecutar el Deploy en un solo click
./scripts/deploy-api.sh

1. hacemos build del proyecto Api
2. Publish en carpeta publish
3. secrea el .zip con ese publish
4. hacemos deploy a la App Service
5. se verifica con curl la raiz / y /api/pacientes.
