using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinData.Infrastructure.Datos.Configuraciones;

public class ConfiguracionPaciente : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(paciente => paciente.Id);

        builder.Property(paciente => paciente.Identidad)
            .HasMaxLength(15)
            .IsRequired();

        builder.HasIndex(paciente => paciente.Identidad)
            .IsUnique();

        builder.Property(paciente => paciente.Nombres)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(paciente => paciente.Apellidos)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(paciente => paciente.Telefono)
            .HasMaxLength(20);

        builder.Property(paciente => paciente.Sexo)
            .HasMaxLength(20)
            .IsRequired();
    }
}