
namespace ClinData.Application.DTOs.NotasClinicas;

public class CreateNotaClinicaDto
{
    public int CitaId { get; set; }

    public string Texto { get; set; } = string.Empty;

    public string RegistradaPor { get; set; } = string.Empty;
}