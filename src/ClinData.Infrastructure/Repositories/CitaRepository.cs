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

    public async Task<Cita?> GetByIdAsync(int id)
    {
        return await _dbContext.Citas
            .FirstOrDefaultAsync(cita => cita.Id == id);
    }

    public async Task AddAsync(Cita cita)
    {
        await _dbContext.Citas.AddAsync(cita);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
