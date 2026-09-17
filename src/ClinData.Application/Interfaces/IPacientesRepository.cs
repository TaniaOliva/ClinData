using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface IPacienteRepository
{
    Task<Paciente?> GetByIdAsync(int id);

    Task<Paciente?> GetByIdentidadAsync(string identidad);

    Task<IEnumerable<Paciente>> GetAllAsync();

    Task AddAsync(Paciente paciente);

    Task<bool> ExisteIdentidadAsync(string identidad);

    Task SaveChangesAsync();
}