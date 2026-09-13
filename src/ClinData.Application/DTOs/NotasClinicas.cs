
namespace ClinData.Application.DTOs.NotasClinicas;

public class CreateNotaClinicaDto
{
    public int CitaId { get; set; }

    public string Contenido { get; set; } = string.Empty;

    public string EscritoPor { get; set; } = string.Empty;
}