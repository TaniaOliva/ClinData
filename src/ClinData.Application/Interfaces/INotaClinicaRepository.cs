using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface INotaClinicaRepository
{
    Task<IEnumerable<NotaClinica>> ObtenerPorPacienteIdAsync(int pacienteId);

    Task<NotaClinica?> GetByIdAsync(int id);

    Task<bool> ExisteNotaParaCitaAsync(int citaId);

    Task AddAsync(NotaClinica nota);

    Task SaveChangesAsync();
}
