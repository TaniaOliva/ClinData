

namespace ClinData.Application.DTOs.Citas;

public class CreateCitaDto
{
    public int PacienteId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Motivo { get; set; } = string.Empty;
}
