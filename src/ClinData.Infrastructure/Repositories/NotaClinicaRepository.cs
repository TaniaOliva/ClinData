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
}
