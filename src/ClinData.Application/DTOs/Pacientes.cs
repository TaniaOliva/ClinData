namespace ClinData.Application.DTOs.Pacientes;

public class CreatePacienteDto
{
    public string Identidad { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string Sexo { get; set; } = string.Empty;
}