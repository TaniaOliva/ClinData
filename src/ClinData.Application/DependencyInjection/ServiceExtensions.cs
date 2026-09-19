using ClinData.Application.Services;
using ClinData.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ClinData.Application.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<PacienteValidator>();

        services.AddScoped<PacienteService>();

        return services;
    }
}
