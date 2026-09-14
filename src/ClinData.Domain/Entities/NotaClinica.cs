namespace ClinData.Domain.Entities;

public class NotaClinica
{
    private NotaClinica()
    {
    }

    public NotaClinica(
        int citaId,
        int pacienteId,
        string texto,
        DateTime fechaHoraRegistro,
        string registradaPor)
    {
        CitaId = citaId;
        PacienteId = pacienteId;
        Texto = texto;
        FechaHoraRegistro = fechaHoraRegistro;
        RegistradaPor = registradaPor;
    }

    public int Id { get; private set; }

    public int CitaId { get; private set; }

    public int PacienteId { get; private set; }

    public string Texto { get; private set; } = string.Empty;

    public DateTime FechaHoraRegistro { get; private set; }

    public string RegistradaPor { get; private set; } = string.Empty;

    public Cita? Cita { get; private set; }

    public Paciente? Paciente { get; private set; }
}