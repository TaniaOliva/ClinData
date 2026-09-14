using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinData.Infrastructure.Persistence.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.Property(paciente => paciente.Identidad)
            .IsRequired()
            .HasMaxLength(15);

        builder.HasIndex(paciente => paciente.Identidad)
            .IsUnique();

        builder.Property(paciente => paciente.Nombres)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(paciente => paciente.Apellidos)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(paciente => paciente.Telefono)
            .HasMaxLength(8);

        builder.Property(paciente => paciente.Sexo)
            .HasMaxLength(20);
    }
}