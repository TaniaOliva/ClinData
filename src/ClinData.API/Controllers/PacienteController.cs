using ClinData.Application.DTOs.Pacientes;
using ClinData.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinData.API.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacienteController : ControllerBase
{
    private readonly PacienteService _pacienteService;

    public PacienteController(PacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CreacionPacienteDto dto)
    {
        var (exitoso, errores, paciente) =
            await _pacienteService.CrearPacienteAsync(dto);

        if (!exitoso)
        {
            return BadRequest(new { errores });
        }

        return Created($"/api/pacientes/{paciente!.Id}", paciente);
    }
}