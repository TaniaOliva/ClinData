using ClinData.Application.DTOs.Pacientes;
using ClinData.Application.Interfaces;
using ClinData.Application.Validators;
using ClinData.Domain.Entities;


namespace ClinData.Application.Services;

public class PacienteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly PacienteValidator _pacienteValidator;

    public PacienteService(
        IPacienteRepository pacienteRepository,
        PacienteValidator pacienteValidator)
    {
        _pacienteRepository = pacienteRepository;
        _pacienteValidator = pacienteValidator;
    }

    public async Task<(bool Exitoso, List<string> Errores, Paciente? Paciente)>
        CrearPacienteAsync(CreacionPacienteDto dto)
    {
        var errores = await _pacienteValidator.CreacionPacienteAsync(dto);

        if (errores.Count > 0)
        {
            return (false, errores, null);
        }

        var paciente = new Paciente
        {
            Nombres = dto.Nombre.Trim(),
            Apellidos = dto.Apellido.Trim(),
            Sexo = dto.Sexo.Trim(),
            Identidad = dto.Identidad.Trim(),
            FechaNacimiento = dto.FechaNacimiento,
            Telefono = dto.Telefono?.Trim(),
            FechaRegistro = DateTime.UtcNow
        };

        await _pacienteRepository.AddAsync(paciente);
        await _pacienteRepository.SaveChangesAsync();

        return (true, new List<string>(), paciente);
    }
}