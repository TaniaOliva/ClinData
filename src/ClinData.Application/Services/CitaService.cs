using ClinData.Application.DTOs.Citas;
using ClinData.Application.Interfaces;
using ClinData.Domain.Entities;

namespace ClinData.Application.Services;

// TEMPORAL: alta minima de citas para poder registrar notas clinicas.
// Se reemplaza por el servicio definitivo, con su validador, cuando se
// construya el slice completo de Citas.
//
// ZONA HORARIA: toda FechaHora se persiste en UTC. Si el cliente envia una
// hora sin zona (por ejemplo "2026-09-10T09:00:00"), se interpreta como hora
// de Honduras (UTC-6) y se convierte. Honduras no aplica horario de verano,
// asi que el desfase es fijo. Esta regla no es evidente leyendo el codigo:
// dos citas identicas en el JSON pueden quedar guardadas con horas distintas
// segun traigan o no zona horaria.
// NOTA: mover este comentario al CitaValidator cuando ese validador exista.
public class CitaService
{
    private static readonly TimeSpan DesfaseHonduras = TimeSpan.FromHours(-6);

    private readonly ICitaRepository _citaRepository;

    public CitaService(ICitaRepository citaRepository)
    {
        _citaRepository = citaRepository;
    }

    public async Task<Cita> CrearCitaAsync(CreacionCitaDto dto)
    {
        var cita = new Cita
        {
            PacienteId = dto.PacienteId,
            FechaHora = NormalizarAUtc(dto.FechaHora),
            Motivo = dto.Motivo.Trim(),
            Estado = EstadoCita.Programada
        };

        await _citaRepository.AddAsync(cita);
        await _citaRepository.SaveChangesAsync();

        return cita;
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
