namespace ClinData.Domain.Entities;

public class NotaClinica
{
    public int Id { get; private set; }

    public int CitaId { get; private set; }

    public string Texto { get; private set; } = string.Empty;

    public DateTime FechaHoraRegistro { get; private set; }

    public string RegistradaPor { get; private set; } = string.Empty;

    public Cita? Cita { get; private set; }

    private NotaClinica()
    {
    }

    public NotaClinica(int citaId, string texto, string registradaPor)
    {
        CitaId = citaId;
        Texto = texto;
        RegistradaPor = registradaPor;
        FechaHoraRegistro = DateTime.UtcNow;
    }
}