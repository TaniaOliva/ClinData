using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinData.Infrastructure.Persistence;

public class ClinDataDbContext : DbContext
{
    public ClinDataDbContext(DbContextOptions<ClinDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<Paciente> Pacientes => Set<Paciente>();

    public DbSet<Cita> Citas => Set<Cita>();

    public DbSet<NotaClinica> NotasClinicas => Set<NotaClinica>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinDataDbContext).Assembly);
    }
}