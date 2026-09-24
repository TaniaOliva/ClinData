using ClinData.Domain.Entities;

namespace ClinData.Application.DTOs.NotasClinicas;

public class CreacionNotaClinicaDto
{
    public int CitaId { get; set; }

    public string Texto { get; set; } = string.Empty;

    public string RegistradaPor { get; set; } = string.Empty;
}

public class NotaClinicaRespuestaDto
{
    public int Id { get; set; }

    public int CitaId { get; set; }

    public string Texto { get; set; } = string.Empty;

    public DateTime FechaHoraRegistro { get; set; }

    public string RegistradaPor { get; set; } = string.Empty;

    public static NotaClinicaRespuestaDto DesdeEntidad(NotaClinica nota)
    {
        return new NotaClinicaRespuestaDto
        {
            Id = nota.Id,
            CitaId = nota.CitaId,
            Texto = nota.Texto,
            FechaHoraRegistro = nota.FechaHoraRegistro,
            RegistradaPor = nota.RegistradaPor
        };
    }
}
