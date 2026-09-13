namespace ClinData.Domain.Entities;

public class NotaClinica
{
    public int Id { get; set; }

    public int CitaId { get; set; }

    public string Contenido { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; } = DateTime.UtcNow;

    public string EscritoPor { get; set; } = string.Empty;

    public Cita? Cita { get; set; }
}