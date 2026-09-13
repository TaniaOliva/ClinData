using ClinData.Domain.Enums;

namespace ClinData.Domain.Entities;

public class Cita
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Motivo { get; set; } = string.Empty;

    public EstadoCita Estado { get; set; } = EstadoCita.Programada;

    public Paciente? Paciente { get; set; }

    public NotaClinica? NotaClinica { get; set; }
}