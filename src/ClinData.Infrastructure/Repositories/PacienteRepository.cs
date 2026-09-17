using ClinData.Application.Interfaces;
using ClinData.Domain.Entities;
using ClinData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinData.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly ClinDataDbContext _dbContext;

    public PacienteRepository(ClinDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Paciente?> GetByIdAsync(int id)
    {
        return await _dbContext.Pacientes
            .FirstOrDefaultAsync(paciente => paciente.Id == id);
    }

    public async Task<Paciente?> GetByIdentidadAsync(string identidad)
    {
        return await _dbContext.Pacientes
            .FirstOrDefaultAsync(paciente => paciente.Identidad == identidad);
    }

    public async Task<IEnumerable<Paciente>> GetAllAsync()
    {
        return await _dbContext.Pacientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Paciente paciente)
    {
        await _dbContext.Pacientes.AddAsync(paciente);
    }

    public async Task<bool> ExisteIdentidadAsync(string identidad)
    {
        return await _dbContext.Pacientes
            .AnyAsync(paciente => paciente.Identidad == identidad);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
