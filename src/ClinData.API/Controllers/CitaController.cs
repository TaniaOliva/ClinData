using ClinData.Application.DTOs.Citas;
using ClinData.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinData.API.Controllers;

[ApiController]
[Route("api/citas")]
public class CitaController : ControllerBase
{
    private readonly CitaService _citaService;

    public CitaController(CitaService citaService)
    {
        _citaService = citaService;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CreacionCitaDto dto)
    {
        var resultado = await _citaService.CrearCitaAsync(dto);

        if (!resultado.Exitoso)
        {
            return BadRequest(resultado.Errores);
        }

        if (resultado.Cita is null)
        {
            return StatusCode(500, "No se pudo crear la cita.");
        }

        return Created($"/api/citas/{resultado.Cita.Id}", resultado.Cita);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPorFecha(
        [FromQuery] DateOnly? fecha)
    {
        if (fecha is null)
        {
            return BadRequest(new { mensaje = "La fecha es obligatoria." });
        }

        var citas = await _citaService.ObtenerPorFechaAsync(fecha.Value);

        return Ok(citas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var cita = await _citaService.ObtenerPorIdAsync(id);

        if (cita is null)
        {
            return NotFound();
        }

        return Ok(cita);
    }
}