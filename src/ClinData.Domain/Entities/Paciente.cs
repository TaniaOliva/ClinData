namespace ClinData.Domain.Entities;

public class Paciente
{
    public int Id { get; set; }

    public string Identidad { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    
}