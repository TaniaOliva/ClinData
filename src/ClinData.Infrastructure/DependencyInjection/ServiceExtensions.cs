using ClinData.Application.Interfaces;
using ClinData.Application.Validators;
using ClinData.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace ClinData.Infrastructure.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPacienteRepository, PacienteRepository>();

        services.AddScoped<PacienteValidator>();

        return services;
    }
}