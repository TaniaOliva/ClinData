using ClinData.Application.Interfaces;
using ClinData.Domain.Entities;
using ClinData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinData.Infrastructure.Repositories;

public class NotaClinicaRepository : INotaClinicaRepository
{
    private readonly ClinDataDbContext _dbContext;

    public NotaClinicaRepository(ClinDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<NotaClinica>> ObtenerPorPacienteIdAsync(
        int pacienteId)
    {
        return await _dbContext.NotasClinicas
            .AsNoTracking()
            .Where(nota => nota.Cita!.PacienteId == pacienteId)
            .OrderByDescending(nota => nota.FechaHoraRegistro)
            .ToListAsync();
    }

    public async Task<NotaClinica?> GetByIdAsync(int id)
    {
        return await _dbContext.NotasClinicas
            .FirstOrDefaultAsync(nota => nota.Id == id);
    }

    public async Task<bool> ExisteNotaParaCitaAsync(int citaId)
    {
        return await _dbContext.NotasClinicas
            .AnyAsync(nota => nota.CitaId == citaId);
    }

    public async Task AddAsync(NotaClinica nota)
    {
        await _dbContext.NotasClinicas.AddAsync(nota);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
