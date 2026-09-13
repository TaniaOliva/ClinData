namespace ClinData.Domain.Entities;

public class Paciente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Identidad { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}