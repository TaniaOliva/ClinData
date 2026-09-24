using ClinData.Domain.Entities;

namespace ClinData.Application.DTOs.Citas;

public class CreacionCitaDto
{
    public int PacienteId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Motivo { get; set; } = string.Empty;
}

public class CitaRespuestaDto
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Motivo { get; set; } = string.Empty;

    public EstadoCita Estado { get; set; }

    public static CitaRespuestaDto DesdeEntidad(Cita cita)
    {
        return new CitaRespuestaDto
        {
            Id = cita.Id,
            PacienteId = cita.PacienteId,
            FechaHora = cita.FechaHora,
            Motivo = cita.Motivo,
            Estado = cita.Estado
        };
    }
}
