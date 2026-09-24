using ClinData.Application.DTOs.NotasClinicas;
using ClinData.Application.Interfaces;

namespace ClinData.Application.Validators;

public class NotaClinicaValidator
{
    private readonly ICitaRepository _citaRepository;
    private readonly INotaClinicaRepository _notaClinicaRepository;

    public NotaClinicaValidator(
        ICitaRepository citaRepository,
        INotaClinicaRepository notaClinicaRepository)
    {
        _citaRepository = citaRepository;
        _notaClinicaRepository = notaClinicaRepository;
    }

    public Task<List<string>> CreacionNotaClinicaAsync(
        CreacionNotaClinicaDto dto)
    {
        return ValidarCreacionAsync(dto);
    }

    private async Task<List<string>> ValidarCreacionAsync(
        CreacionNotaClinicaDto dto)
    {
        var errores = new List<string>();

        // 1. Validar CitaId
        if (dto.CitaId <= 0)
        {
            errores.Add(
                "La cita asociada es obligatoria.");

            return errores;
        }

        // 2. Verificar que exista la cita
        var cita = await _citaRepository
            .ObtenerPorIdAsync(dto.CitaId);

        if (cita == null)
        {
            errores.Add(
                "La cita asociada no existe.");

            return errores;
        }

        // 3. Una cita solo puede tener una nota clínica
        if (await _notaClinicaRepository.ExisteNotaParaCitaAsync(dto.CitaId))
        {
            errores.Add(
                "La cita ya tiene una nota clínica registrada.");
        }

        // 4. La cita debe haber ocurrido
        if (cita.FechaHora > DateTime.UtcNow)
        {
            errores.Add(
                "No se puede registrar una nota clínica porque la cita todavía no ha ocurrido.");
        }

        // 5. Toda nota debe indicar quién la escribió
        if (string.IsNullOrWhiteSpace(dto.RegistradaPor))
        {
            errores.Add(
                "Debe indicar quién escribió la nota clínica.");
        }

        return errores;
    }
}
