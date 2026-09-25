using ClinData.Application.DTOs.Citas;
using ClinData.Application.Interfaces;
using ClinData.Application.Validators;
using ClinData.Domain.Entities;

namespace ClinData.Application.Services;

// ZONA HORARIA: toda FechaHora se persiste en UTC. Si el cliente envia una
// hora sin zona (por ejemplo "2026-09-10T09:00:00"), se interpreta como hora
// de Honduras (UTC-6) y se convierte. Honduras no aplica horario de verano,
// asi que el desfase es fijo. Esta regla no es evidente leyendo el codigo:
// dos citas identicas en el JSON pueden quedar guardadas con horas distintas
// segun traigan o no zona horaria.
public class CitaService
{
    private static readonly TimeSpan DesfaseHonduras = TimeSpan.FromHours(-6);

    private readonly ICitaRepository _citaRepository;
    private readonly CitaValidator _citaValidator;

    public CitaService(
        ICitaRepository citaRepository,
        CitaValidator citaValidator)
    {
        _citaRepository = citaRepository;
        _citaValidator = citaValidator;
    }

    public async Task<(bool Exitoso, List<string> Errores, Cita? Cita)>
        CrearCitaAsync(CreacionCitaDto dto)
    {
        // Se normaliza antes de validar para que el validador, la revision
        // de duplicados y el guardado usen la misma hora UTC.
        dto.FechaHora = NormalizarAUtc(dto.FechaHora);

        var errores = await _citaValidator.ValidarCreacionAsync(dto);

        if (errores.Count > 0)
        {
            return (false, errores, null);
        }

        var cita = new Cita
        {
            PacienteId = dto.PacienteId,
            FechaHora = dto.FechaHora,
            Motivo = dto.Motivo.Trim(),
            Estado = EstadoCita.Programada
        };

        await _citaRepository.AgregarAsync(cita);
        await _citaRepository.GuardarCambiosAsync();

        return (true, new List<string>(), cita);
    }

    public async Task<Cita?> ObtenerPorIdAsync(int id)
    {
        return await _citaRepository.ObtenerPorIdAsync(id);
    }

    public async Task<IEnumerable<Cita>> ObtenerPorFechaAsync(DateOnly fecha)
    {
        // El dia se entiende en hora de Honduras: 00:00 HN equivale a 06:00 UTC.
        var inicioUtc = fecha.ToDateTime(TimeOnly.MinValue) - DesfaseHonduras;
        var finUtc = inicioUtc.AddDays(1);

        return await _citaRepository.ObtenerEntreAsync(inicioUtc, finUtc);
    }

    public async Task<IEnumerable<Cita>> ObtenerTodasAsync()
    {
        return await _citaRepository.ObtenerTodasAsync();
    }

    private static DateTime NormalizarAUtc(DateTime fechaHora)
    {
        return fechaHora.Kind switch
        {
            // Ya viene en UTC (el cliente envio "Z"): se guarda tal cual.
            DateTimeKind.Utc => fechaHora,

            // Trajo un desfase explicito y .NET ya lo convirtio a hora local
            // del servidor: se lleva a UTC segun esa misma zona.
            DateTimeKind.Local => fechaHora.ToUniversalTime(),

            // Sin zona: se asume hora de Honduras y se compensa el UTC-6.
            _ => DateTime.SpecifyKind(fechaHora - DesfaseHonduras, DateTimeKind.Utc)
        };
    }
}
