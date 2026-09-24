using ClinData.Domain.Entities;

namespace ClinData.Application.DTOs.Pacientes;

public class CreacionPacienteDto
{
    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public string Sexo { get; set; } = string.Empty;

    public string Identidad { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }
}

public class PacienteRespuestaDto
{
    public int Id { get; set; }

    public string Identidad { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; }

    public static PacienteRespuestaDto DesdeEntidad(Paciente paciente)
    {
        return new PacienteRespuestaDto
        {
            Id = paciente.Id,
            Identidad = paciente.Identidad,
            Nombres = paciente.Nombres,
            Apellidos = paciente.Apellidos,
            FechaNacimiento = paciente.FechaNacimiento,
            Telefono = paciente.Telefono,
            Sexo = paciente.Sexo,
            FechaRegistro = paciente.FechaRegistro
        };
    }
}
