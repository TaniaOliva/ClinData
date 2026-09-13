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
