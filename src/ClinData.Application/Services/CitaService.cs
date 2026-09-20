using ClinData.Application.DTOs.Citas;
using ClinData.Application.Interfaces;
using ClinData.Application.Validators;
using ClinData.Domain.Entities;

namespace ClinData.Application.Services;

public class CitaService
{
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
}
