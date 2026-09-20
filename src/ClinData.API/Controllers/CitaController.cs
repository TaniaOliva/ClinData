using ClinData.Application.DTOs.Citas;
using ClinData.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinData.API.Controllers;

// TEMPORAL: alta minima de citas, sin validador, para poder registrar
// notas clinicas. Se reemplaza cuando se construya el slice de Citas.
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
}
