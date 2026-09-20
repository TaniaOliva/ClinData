using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface ICitaRepository
{
    Task<Cita?> ObtenerPorIdAsync(int id);

    Task<IEnumerable<Cita>> ObtenerPorFechaAsync(DateOnly fecha);

    Task<bool> ExisteCitaMismoHorarioAsync(
        int pacienteId,
        DateTime fechaHora);

    Task AgregarAsync(Cita cita);

    Task GuardarCambiosAsync();
}
