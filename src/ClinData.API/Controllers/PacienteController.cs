using ClinData.Application.DTOs.NotasClinicas;
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

        return Created(
            $"/api/pacientes/{paciente!.Id}",
            PacienteRespuestaDto.DesdeEntidad(paciente));
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var pacientes = await _pacienteService.ObtenerTodosAsync();

        return Ok(pacientes.Select(PacienteRespuestaDto.DesdeEntidad));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var paciente = await _pacienteService.ObtenerPorIdAsync(id);

        if (paciente == null)
        {
            return NotFound();
        }

        return Ok(PacienteRespuestaDto.DesdeEntidad(paciente));
    }

    [HttpGet("{id:int}/notas")]
    public async Task<IActionResult> ObtenerNotas(int id)
    {
        var notas = await _pacienteService.ObtenerNotasAsync(id);

        if (notas is null)
        {
            return NotFound(new { mensaje = "Paciente no encontrado." });
        }

        return Ok(notas.Select(NotaClinicaRespuestaDto.DesdeEntidad));
    }
}
