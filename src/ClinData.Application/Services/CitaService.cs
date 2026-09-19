using ClinData.Application.DTOs.Citas;
using ClinData.Application.Interfaces;
using ClinData.Domain.Entities;

namespace ClinData.Application.Services;

// TEMPORAL: alta minima de citas para poder registrar notas clinicas.
// Se reemplaza por el servicio definitivo, con su validador, cuando se
// construya el slice completo de Citas.
public class CitaService
{
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
            FechaHora = dto.FechaHora,
            Motivo = dto.Motivo.Trim(),
            Estado = EstadoCita.Programada
        };

        await _citaRepository.AddAsync(cita);
        await _citaRepository.SaveChangesAsync();

        return cita;
    }
}
