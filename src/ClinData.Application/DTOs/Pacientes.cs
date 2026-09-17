



namespace ClinData.Application.DTOs.Pacientes;

public class CreacionPacienteDto
{
    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string Sexo { get; set; } = string.Empty;

    public string Identidad { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }
}
