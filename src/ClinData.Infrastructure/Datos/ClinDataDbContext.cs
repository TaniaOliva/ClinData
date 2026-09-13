using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinData.Infrastructure.Datos;

public class ClinDataDbContext : DbContext
{
    public ClinDataDbContext(DbContextOptions<ClinDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<Paciente> Pacientes { get; set; } = null!;

    public DbSet<Cita> Citas { get; set; } = null!;

    public DbSet<NotaClinica> NotasClinicas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ClinDataDbContext).Assembly);
    }
}