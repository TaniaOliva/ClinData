using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface INotaClinicaRepository
{
    Task<IEnumerable<NotaClinica>> ObtenerPorPacienteIdAsync(int pacienteId);
}
