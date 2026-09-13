namespace ClinData.Domain.Entities;

public class Cita
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Motivo { get; set; } = string.Empty;

    public string Estado { get; set; } = "Agendada";

    public Paciente? Paciente { get; set; }

    public ICollection<NotaClinica> NotasClinicas { get; set; }
        = new List<NotaClinica>();
}