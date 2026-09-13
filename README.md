# ClinData

API REST desarrollada para la gestión de expedientes
clínicos de un consultorio médico independiente.

## Arquitectura

El proyecto utiliza una arquitectura N-Capas:

- ClinData.Domain
- ClinData.Application
- ClinData.Infrastructure
- ClinData.API

## Tecnologías

- ASP.NET Core Web API
- C#
- Entity Framework Core
- Azure SQL Database
- Git / GitHub
- Postman

## Librerías usadas

- `Microsoft.EntityFrameworkCore.SqlServer`: permite conectar Entity Framework Core con SQL Server.
- `Microsoft.EntityFrameworkCore.Design`: proporciona herramientas de diseño para crear migraciones de EF Core.

## Base de datos y migraciones

La cadena de conexión se lee de la clave `ConnectionStrings:ClinData`.
En desarrollo está en `src/ClinData.API/appsettings.Development.json` y apunta
a SQL Server LocalDB con la base `ClinDataDb`.

Para trabajar con migraciones hace falta la herramienta de línea de comandos
de EF Core (se instala una sola vez por computadora):

```bash
dotnet tool install --global dotnet-ef
```

Crear una migración nueva (cambiar `NombreDeLaMigracion` por algo descriptivo):

```bash
dotnet ef migrations add NombreDeLaMigracion --project src/ClinData.Infrastructure --startup-project src/ClinData.API
```

Aplicar las migraciones pendientes a la base de datos:

```bash
dotnet ef database update --project src/ClinData.Infrastructure --startup-project src/ClinData.API
```

Las migraciones se guardan en `src/ClinData.Infrastructure/Migrations/` porque
ahí vive el `ClinDataDbContext`. El proyecto de arranque es `ClinData.API`
porque de ahí sale la configuración con la cadena de conexión.

### Trabajar en Mac o Linux

LocalDB solo existe en Windows, así que en Mac o Linux hay que cambiar la
cadena de conexión por una base de SQL Server propia. La forma más sencilla es
levantar SQL Server en Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Clave_Segura123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Y dejar así la clave `ConnectionStrings:ClinData` en
`src/ClinData.API/appsettings.Development.json`:

```json
"ConnectionStrings": {
  "ClinData": "Server=localhost,1433;Database=ClinDataDb;User Id=sa;Password=Clave_Segura123;TrustServerCertificate=True"
}
```

Después se corre `dotnet ef database update` igual que en Windows. No hace falta
tocar el código: solo cambia la cadena de conexión.

> Nota: `appsettings.Development.json` es de uso local. Si alguien usa una clave
> real, conviene guardarla con `dotnet user-secrets` en lugar de subirla al repo.

## Entidades principales

- Paciente
- Cita
- Nota Clínica

## Integrantes

- Milton
- Tania
- Heber