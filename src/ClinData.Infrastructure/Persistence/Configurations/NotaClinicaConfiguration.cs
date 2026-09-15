using ClinData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinData.Infrastructure.Persistence.Configurations;

public class NotaClinicaConfiguration : IEntityTypeConfiguration<NotaClinica>
{
    public void Configure(EntityTypeBuilder<NotaClinica> builder)
    {
        builder.Property(nota => nota.Texto)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(nota => nota.RegistradaPor)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(nota => nota.Cita)
            .WithOne()
            .HasForeignKey<NotaClinica>(nota => nota.CitaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(nota => nota.Paciente)
            .WithMany()
            .HasForeignKey(nota => nota.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}