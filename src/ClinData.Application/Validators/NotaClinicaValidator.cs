using ClinData.Application.DTOs.NotasClinicas;
using ClinData.Application.Interfaces;

namespace ClinData.Application.Validators;

public class NotaClinicaValidator
{
    private readonly ICitaRepository _citaRepository;

    public NotaClinicaValidator(ICitaRepository citaRepository)
    {
        _citaRepository = citaRepository;
    }

    public async Task<List<string>> CreacionNotaClinicaAsync(
        CreacionNotaClinicaDto dto)
    {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Texto))
        {
            errores.Add("El texto de la nota es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(dto.RegistradaPor))
        {
            errores.Add("El campo RegistradaPor es obligatorio.");
        }

        var cita = await _citaRepository.GetByIdAsync(dto.CitaId);

        if (cita is null)
        {
            errores.Add("La cita indicada no existe.");
        }
        else if (cita.FechaHora >= DateTime.UtcNow)
        {
            errores.Add(
                "No se puede registrar una nota sobre una cita que aun no ha ocurrido.");
        }

        return errores;
    }
}
