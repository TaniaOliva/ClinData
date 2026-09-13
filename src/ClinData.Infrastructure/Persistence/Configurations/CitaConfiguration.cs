using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinData.Infrastructure.Persistence.Configurations;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.Property(cita => cita.Motivo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(cita => cita.Estado)
            .HasConversion<string>();

        builder.HasOne(cita => cita.Paciente)
            .WithMany(paciente => paciente.Citas)
            .HasForeignKey(cita => cita.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(cita => cita.NotasClinicas);
    }
}