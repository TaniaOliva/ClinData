using ClinData.Application.Interfaces;
using ClinData.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace ClinData.Infrastructure.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<ICitaRepository, CitaRepository>();
        services.AddScoped<INotaClinicaRepository, NotaClinicaRepository>();

        return services;
    }
}
