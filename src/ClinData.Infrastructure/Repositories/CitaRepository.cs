using ClinData.Application.Interfaces;
using ClinData.Domain.Entities;
using ClinData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinData.Infrastructure.Repositories;

public class CitaRepository : ICitaRepository
{
    private readonly ClinDataDbContext _dbContext;

    public CitaRepository(ClinDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cita?> ObtenerPorIdAsync(int id)
    {
        return await _dbContext.Citas
            .FirstOrDefaultAsync(cita => cita.Id == id);
    }

    public async Task<IEnumerable<Cita>> ObtenerEntreAsync(
        DateTime inicioUtc,
        DateTime finUtc)
    {
        return await _dbContext.Citas
            .AsNoTracking()
            .Where(cita => cita.FechaHora >= inicioUtc &&
                        cita.FechaHora < finUtc)
            .OrderBy(cita => cita.FechaHora)
            .ToListAsync();
    }

    public async Task AgregarAsync(Cita cita)
    {
        await _dbContext.Citas.AddAsync(cita);
    }

    public async Task<bool> ExisteCitaMismoHorarioAsync(int pacienteId, DateTime fechaHora)
    {
        return await _dbContext.Citas
            .AnyAsync(cita => cita.PacienteId == pacienteId && cita.FechaHora == fechaHora);
    }

    public async Task GuardarCambiosAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
