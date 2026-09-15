



namespace ClinData.Application.DTOs.Pacientes;

public class CreatePacienteDto
{
    public string Nombre { get; set; } = string.Empty;

    public string Identidad { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }
}
