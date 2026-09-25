using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface ICitaRepository
{
    Task<Cita?> ObtenerPorIdAsync(int id);

    Task<IEnumerable<Cita>> ObtenerEntreAsync(
        DateTime inicioUtc,
        DateTime finUtc);

    Task<IEnumerable<Cita>> ObtenerTodasAsync();

    Task<bool> ExisteCitaMismoHorarioAsync(
        int pacienteId,
        DateTime fechaHora);

    Task AgregarAsync(Cita cita);

    Task GuardarCambiosAsync();
}
