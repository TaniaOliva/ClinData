using ClinData.Application.DTOs.NotasClinicas;
using ClinData.Application.Interfaces;
using ClinData.Application.Validators;
using ClinData.Domain.Entities;

namespace ClinData.Application.Services;

public class NotaClinicaService
{
    private readonly INotaClinicaRepository _notaClinicaRepository;
    private readonly NotaClinicaValidator _notaClinicaValidator;

    public NotaClinicaService(
        INotaClinicaRepository notaClinicaRepository,
        NotaClinicaValidator notaClinicaValidator)
    {
        _notaClinicaRepository = notaClinicaRepository;
        _notaClinicaValidator = notaClinicaValidator;
    }

    public async Task<(bool Exitoso, List<string> Errores, NotaClinica? Nota)>
        CrearNotaAsync(CreacionNotaClinicaDto dto)
    {
        var errores = await _notaClinicaValidator
            .CreacionNotaClinicaAsync(dto);

        if (errores.Count > 0)
        {
            return (false, errores, null);
        }

        var nota = new NotaClinica(
            dto.CitaId,
            dto.Texto.Trim(),
            DateTime.UtcNow,
            dto.RegistradaPor.Trim());

        await _notaClinicaRepository.AddAsync(nota);
        await _notaClinicaRepository.SaveChangesAsync();

        return (true, new List<string>(), nota);
    }

    public async Task<NotaClinica?> ObtenerPorIdAsync(int id)
    {
        return await _notaClinicaRepository.GetByIdAsync(id);
    }
}
