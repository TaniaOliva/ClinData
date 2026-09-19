using ClinData.Domain.Entities;

namespace ClinData.Application.Interfaces;

public interface ICitaRepository
{
    Task<Cita?> GetByIdAsync(int id);

    Task AddAsync(Cita cita);

    Task SaveChangesAsync();
}
