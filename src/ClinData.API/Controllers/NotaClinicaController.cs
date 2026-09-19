using ClinData.Application.DTOs.NotasClinicas;
using ClinData.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinData.API.Controllers;

[ApiController]
[Route("api/notas")]
public class NotaClinicaController : ControllerBase
{
    private readonly NotaClinicaService _notaClinicaService;

    public NotaClinicaController(NotaClinicaService notaClinicaService)
    {
        _notaClinicaService = notaClinicaService;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CreacionNotaClinicaDto dto)
    {
        var (exitoso, errores, nota) =
            await _notaClinicaService.CrearNotaAsync(dto);

        if (!exitoso)
        {
            return BadRequest(new { errores });
        }

        return Created($"/api/notas/{nota!.Id}", nota);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var nota = await _notaClinicaService.ObtenerPorIdAsync(id);

        if (nota is null)
        {
            return NotFound();
        }

        return Ok(nota);
    }
}
