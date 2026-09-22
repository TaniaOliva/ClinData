using ClinData.Application.DTOs.Citas;
using ClinData.Application.Interfaces;

namespace ClinData.Application.Validators;

public class CitaValidator
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly ICitaRepository _citaRepository;

    public CitaValidator(
        IPacienteRepository pacienteRepository,
        ICitaRepository citaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _citaRepository = citaRepository;
    }

    public async Task<List<string>> ValidarCreacionAsync(
        CreacionCitaDto dto)
    {
        var errores = new List<string>();

        // 1. Validar paciente
        if (dto.PacienteId <= 0)
        {
            errores.Add("El paciente es obligatorio.");
        }
        else
        {
            var paciente = await _pacienteRepository
                .GetByIdAsync(dto.PacienteId);

            if (paciente == null)
            {
                errores.Add("El paciente indicado no existe.");
            }
        }

        // 2. No permitir citas en el pasado
        if (dto.FechaHora <= DateTime.UtcNow)
        {
            errores.Add(
                "La cita debe programarse para una fecha y hora futura.");
        }

        // 3. Evitar cita duplicada
        if (dto.PacienteId > 0 &&
            dto.FechaHora > DateTime.UtcNow)
        {
            var existeCita = await _citaRepository
                .ExisteCitaMismoHorarioAsync(
                    dto.PacienteId,
                    dto.FechaHora);

            if (existeCita)
            {
                errores.Add(
                    "El paciente ya tiene una cita programada en ese horario.");
            }
        }

        return errores;
    }
}